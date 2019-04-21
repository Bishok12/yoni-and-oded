using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    public event Action<float> OnPlatformEnterEvent;

    public bool isOnBlackout;
    private float blackoutClock = 0f;

    public Transform playerTransform;
    public float spawnDistMargin;

    private GameConfig gameConfig;
    private BasePlatformConfig platformConfig;

    private float spawnZ = 0.0f;
    private int platformsOnScreen = 4;
    private Vector3 nextPlatformPosition = Vector3.zero;
    private bool isSpawning;
    private List<BasePlatform> platforms = new List<BasePlatform>();

    public void StartSpawning(BasePlatformConfig platformConfig) {
        isOnBlackout = false;
        blackoutClock = 0f;
        this.platformConfig = platformConfig;
        gameConfig = GameManager.Instance._conf;
        isSpawning = true;
        for (int i = 0; i < platformsOnScreen; i++) {
            SpawnPlatform();
        }
    }

    public void StopSpawning() {
        isSpawning = false;
    }

    public void DestroyAll() {
        isOnBlackout = false;
        blackoutClock = 0f;

        foreach (var platform in platforms) {
            platform.OnBlackPlatformEnterEvent -= BlackPlatformTouched;
            Destroy(platform.gameObject);
        }

        platforms.Clear();
    }

    // Update is called once per frame
    private void Update () {
        if (!isSpawning) {
            return;
        }

        if (isOnBlackout) 
        {
            if (blackoutClock < gameConfig.blackPlatformEffectTime)
            {
                blackoutClock += Time.deltaTime;
            }
            else
            {
                BlackoutOver();
            }
            
        }

        if (playerTransform.position.z > (spawnZ - gameConfig.platformMaxLength * platformsOnScreen)) {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform () {
        var platform = Instantiate(platformConfig.GetNextPrefab(), transform);
        platform.OnBlackPlatformEnterEvent += BlackPlatformTouched;
        platform.transform.localScale = GetRandomScale(gameConfig);
        platform.transform.position = nextPlatformPosition;
        spawnZ = platform.transform.position.z + platform.transform.localScale.z;
        platform.Setup(gameConfig);
        platform.OnPlatformEnterEvent += OnPlatformEnter;
        platforms.Add(platform);

        if (isOnBlackout) {
            platform.isOnBlackout = true;
        }

        if (platforms.Count > (2 * platformsOnScreen)) {
            var platformToDestroy = platforms[0];
            platformToDestroy.OnBlackPlatformEnterEvent -= BlackPlatformTouched;
            Destroy(platformToDestroy.gameObject);
            platforms.RemoveAt(0);
        }

        nextPlatformPosition = GetRandomPosition(gameConfig);
    }

    private void OnPlatformEnter(float platformJumpMult) {
        if (OnPlatformEnterEvent != null) {
            OnPlatformEnterEvent(platformJumpMult);
        }
    }

    private Vector3 GetRandomScale(GameConfig config) {
        float randomPlatLength = UnityEngine.Random.Range(config.platformMinLength, config.platformMaxLength);
        return new Vector3(2.0f, 1.0f, randomPlatLength);
    }

    private Vector3 GetRandomPosition(GameConfig config) {
        float horzDist = UnityEngine.Random.Range(config.platformMinHorzDist, config.platformMaxHorzDist) + spawnDistMargin;
        horzDist = Math.Min(horzDist + spawnDistMargin, config.platformMaxHorzDist);
        float vertDist = UnityEngine.Random.Range(config.platformMinVertDist, config.platformMaxVertDist);
        return new Vector3(0, vertDist, spawnZ + horzDist);
    }

    public void BlackPlatformTouched()
    {
        print("Blackout starting!");
        isOnBlackout = true;
        foreach (var platform in platforms) {
            platform.isOnBlackout = true;
        }
    }

    public void BlackoutOver()
    {
        print("Blackout over!");
        isOnBlackout = false;
        blackoutClock = 0f;
        foreach (var platform in platforms) {
            platform.isOnBlackout = false;
        }
    }

}
