using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public event Action<float> OnPlatformEnterEvent;

    // Singleton
    public static GameManager Instance { get; private set; }

    // Properties
    public GameConfig _conf;
   
    public float timePassed { get; private set; }
    public float highScore { get; private set; }

    private Player _player;
    private Camera _cam;
    private PlatformSpawner _platSpawner;

    private UI _ui;
    
    // Use this for initialization
    private void Awake () {
        highScore = 0f;
        SetupSingleton();
        SetupUI();
    }
    
    private void SetupSingleton() {
        if (Instance != null) {
            throw new Exception("More than one singleton exists in the scene!");
        }
        Instance = this;
    }

    // Setup player
    private void SetupPlayer() {
        _player = Instantiate(_conf.playerPrefab);
        _player.Init(_conf.intialSpeed, _conf.speedIncPerSecond, _conf.initialJumpForce, _conf.maxJumpForce, _conf.gravityMultiplyer);
    }

    // Setup world
    private void SetupWorld() {
        _platSpawner = Instantiate(_conf.platformSpawnerPrefab);
        _platSpawner.playerTransform = _player.transform;
    }

    private void SetupUI() {
        _ui = Instantiate(_conf.uiPrefab);
        _ui.SetMenuMode(Menu.MenuType.Start);
    }

    public void StartGame() {
        SetupPlayer();
        SetupWorld();

        // Start Spawner
        _platSpawner.StartSpawning(_conf.platformConfig);
        _platSpawner.OnPlatformEnterEvent += OnPlatformEnter;


        // Other
        timePassed = 0f;
        _ui.SetHUDMode();
    }

    public void EndGame() {
        // Stop Spawner
        _platSpawner.OnPlatformEnterEvent -= OnPlatformEnter;
        _platSpawner.StopSpawning();
        _platSpawner.DestroyAll();

        // Save high score
        highScore = Mathf.Max(highScore, timePassed);

        _ui.SetMenuMode(Menu.MenuType.End);
    }

    private void OnPlatformEnter(float platformJumpMult) {
        if (OnPlatformEnterEvent != null) {
            OnPlatformEnterEvent(platformJumpMult);
        }
    }

    // Update is called once per frame
    private void Update () {
        timePassed += Time.deltaTime;
        if (_platSpawner != null)
        {
            _platSpawner.spawnDistMargin = timePassed * 1.5f;
        }
        
        //Debug.Log(_player.transform.position);
        if (_player != null && _player.transform.position.y < (_conf.playerDeathHeight + _conf.platformMinVertDist)) {
            _player.DeInit();
            Destroy(_player.gameObject);
            Destroy(_platSpawner.gameObject);
            EndGame();
        }
	}
}
