using System.IO;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class FactoryController: MonoBehaviour
{
    private readonly FactoryModel _factoryModel;
    private DiContainer _container;
    private Factory _factory;
    private PlayerFactory _playerFactory;
    private ObjectPool _pool;
    private EnemyFactory _enemyFactory;
    private EnemyModel _enemyModel;
    private EnemyController _enemyController;
    private PlayerView _playerView;
    private EnemyCounter _enemyCounter;
    private HealthPotionFactory _healthPotionFactory;
    private UiView _uiView;
    private GameData _gameData;

    private int _currentEnemy = 0;
    private int _currentEnemyOnScene = 0;
    private int _totalEnemyKilled = 0;
    private GameObject _bigHealthPotion;
    private GameObject _smallHealthPotion;


    [Inject]
    public void Construct(DiContainer container, Factory factory, PlayerFactory playerFactory,
        ObjectPool objectPool,EnemyFactory enemyFactory,HealthPotionFactory healthPotionFactory,
        UiView uiView)
    {
        _container  = container;
        _factory = factory;
        _playerFactory = playerFactory;
        _pool = objectPool;
        _enemyFactory = enemyFactory;
        _enemyCounter = _factory.EnemyCounters;
        _healthPotionFactory = healthPotionFactory;
        _uiView = uiView;
        
    }

    public void HandleEnemyIsDead()
    {
        _currentEnemyOnScene--;
        _totalEnemyKilled++;
        TotalEnemyKilledToUpdate();
    }

    private void SaveToFile(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "gameData.json");
        File.WriteAllText(path, json);
    }

    private GameData LoadFromFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "gameData.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            return new GameData();  
        }
    }
    
    private void EnemyInit()
    {
        foreach (var counter in _enemyCounter.Counter)
        {


            if (_currentEnemy < _factory.MaxEnemys)
            {
                EnemyTypes enemyTypes = counter.EnemyType;
                for (int a = 0; a < counter.EnemyCount;)
                {
                    for (int b = 0; b < _factory.EnemySpawn.Length; b++)
                    {

                        if (a < counter.EnemyCount)
                        {
                            a++;
                            _currentEnemy++;
                            var obj = _enemyFactory.CreateEnemy(enemyTypes, _factory.EnemySpawn[b]);
                            _pool.AddToPool(obj);
                        }
                    }
                }
            }

        }
    }

    

    private void CountEnemy()
    {
        var randPos = Random.Range(0, _factory.EnemySpawn.Length);
        for (int i = 0; i < _factory.MaxEnemysOnScene; i++)
        {
            if (_currentEnemyOnScene < _factory.MaxEnemysOnScene)
            {
                _currentEnemyOnScene++;
                var obj = _pool.GetFromPool();
                if (obj == null) return;
                obj.transform.position = _factory.EnemySpawn[randPos].position;
                obj.SetActive(true);
            }

        }
    }
    

    private void TotalEnemyKilledToUpdate()
    {
        _uiView.TotalEnemysKill.text = $"Total Enemy Killed: {_totalEnemyKilled} ";
    }
    
    
    public void Awake()
    {
        GameData data = LoadFromFile();
        _uiView.TotalKillInLastSession.text = $"Last Session Kills: {LoadFromFile().EnemyKilled}";
        EnemyController.EnemyIsDead += HandleEnemyIsDead;
        _playerFactory.CreatePlayer(_factory.HeroSpawn);
        EnemyInit();
        _healthPotionFactory.InitHealthPotion(_factory.BigHealthPotionSpawn, _factory.SmallHealthPotionSpawn);
        TotalEnemyKilledToUpdate();
    }

    public void LateUpdate()
    {
        CountEnemy();
    }
    private void OnDisable()
    {
        EnemyController.EnemyIsDead -= HandleEnemyIsDead;
        GameData data = new() { EnemyKilled = _totalEnemyKilled };
        SaveToFile(data);
        
    }

}
