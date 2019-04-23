using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    
    [SerializeField] private DataAccessService _dataAccessService;
    private int _money;
    private int _selectedSkinId;
    private float _highScore;

    // Use this for initialization
    private void Awake()
    {
        highScore = 0f;
        SetupSingleton();
        SetDataAccessService();
    }

    private void Start()
    {
        SetupUI();
    }

    private void IncreaseMoney()
    {
        _money += 5;
        _ui.SetMoneyText(_money);
        _dataAccessService.SaveMoney(_money);
    }
    
    public void DecreaseMoney(int amount)
    {
        _money -= amount;
        _ui.SetMoneyText(_money);
        _dataAccessService.SaveMoney(_money);
    }

    private void SetDataAccessService()
    {
        _dataAccessService = Instantiate(_dataAccessService, transform);
        _money = _dataAccessService.LoadMoney();
        _highScore = _dataAccessService.LoadHighScore();
    }
    
    // Setup player
    private void SetupPlayer()
    {
        _player = Instantiate(_conf.playerPrefab);
        _player.LoadSkin(_dataAccessService.LoadSelectedSkin());
        _player.Init(_conf.intialSpeed, _conf.speedIncPerSecond, _conf.initialJumpForce, _conf.maxJumpForce,
            _conf.gravityMultiplyer);
    }

    public DataAccessService GetDataService()
    {
        return _dataAccessService;
    }

    public int GetMoney()
    {
        return _money;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    private void SetupSingleton()
    {
        if (Instance != null)
        {
            throw new Exception("More than one singleton exists in the scene!");
        }

        Instance = this;
    }
   


    // Setup world
    private void SetupWorld()
    {
        _platSpawner = Instantiate(_conf.platformSpawnerPrefab);
        _platSpawner.playerTransform = _player.transform;
    }

    private void SetupUI()
    {
        _ui = Instantiate(_conf.uiPrefab);
        _ui.SetMenuMode(Menu.MenuType.Start);
        _ui.UpdateData(_money, _highScore);
    }

    public void StartGame()
    {
        SetupPlayer();
        SetupWorld();

        // Start Spawner
        _platSpawner.StartSpawning(_conf.platformConfig);
        _platSpawner.OnPlatformEnterEvent += OnPlatformEnter;


        // Other
        timePassed = 0f;
        _ui.SetHUDMode();
    }

    public void EndGame()
    {
        // Stop Spawner
        _platSpawner.OnPlatformEnterEvent -= OnPlatformEnter;

        _platSpawner.StopSpawning();
        _platSpawner.DestroyAll();

        // Save high score
        highScore = Mathf.Max(highScore, timePassed);

        _ui.UpdateData(_money, _highScore);
        _ui.SetMenuMode(Menu.MenuType.End);
        _dataAccessService.SaveData(highScore, _money, _selectedSkinId);
    }

    private void OnPlatformEnter(float platformJumpMult)
    {
        if (OnPlatformEnterEvent != null)
        {
            OnPlatformEnterEvent(platformJumpMult);
        }
        
        IncreaseMoney();
    }

    // Update is called once per frame
    private void Update()
    {
        timePassed += Time.deltaTime;
        if (_platSpawner != null)
        {
            _platSpawner.spawnDistMargin = timePassed * 1.5f;
        }

        //Debug.Log(_player.transform.position);
        if (_player != null && _player.transform.position.y < (_conf.playerDeathHeight + _conf.platformMinVertDist))
        {
            _player.DeInit();
            Destroy(_player.gameObject);
            Destroy(_platSpawner.gameObject);
            EndGame();
        }
    }
    
}