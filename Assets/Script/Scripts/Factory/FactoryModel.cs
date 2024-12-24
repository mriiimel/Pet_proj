using Enemy_Config;
using UnityEngine;
using Zenject;

public class FactoryModel
{
    private readonly Factory _factory;
    private readonly Transform[] _enemySpawn;
    private readonly Transform _heroSpawn;
    private readonly PlayerView _playerView;
    private readonly DiContainer _diContainer;

    public FactoryModel()
    {
        
    }

    public Factory Factory => _factory;

    public Transform[] EnemySpawn => _enemySpawn;

    public Transform HeroSpawn => _heroSpawn;

    public DiContainer DiContainer => _diContainer;
}

