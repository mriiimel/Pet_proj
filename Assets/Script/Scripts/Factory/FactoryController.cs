using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class FactoryController: ITickable,IInitializable,IDisposable
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


    private int _currentEnemy = 0;
    private int _currentEnemyOnScene = 0;
    private int _totalEnemyKilled = 0;

   
    
    public FactoryController(DiContainer container, Factory factory, PlayerFactory playerFactory,
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

    private void InitHealthPotion(Transform spawn)
    {
        var Potion = GameObject.Instantiate(_healthPotionFactory.CreateHealthPotion());
        Potion.transform.position = spawn.position;
        Potion.SetActive(false);
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

    
    public void Initialize()
    {
        EnemyController.EnemyIsDead += HandleEnemyIsDead;
        _playerFactory.CreatePlayer(_factory.HeroSpawn);
        EnemyInit();
        InitHealthPotion(_factory.HealthPotionSpawn);
        TotalEnemyKilledToUpdate();
    }

    public void Tick()
    {
        CountEnemy();
    }

    public void Dispose()
    {
        EnemyController.EnemyIsDead -= HandleEnemyIsDead;
    }
}
