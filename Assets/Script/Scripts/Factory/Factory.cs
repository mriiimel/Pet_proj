using UnityEngine;
using Zenject;



public class Factory : MonoBehaviour
{
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private Transform[] _enemySpawn;
    [SerializeField] private Transform _heroSpawn;
    [SerializeField] private Transform _healthPotionSpawn;
    [SerializeField] private int _maxEnemys;
    [SerializeField] private int _maxEnemysOnScene;
    

    private FactoryController _factoryController;
    private DiContainer _container;
    public Transform[] EnemySpawn => _enemySpawn;

    public Transform HeroSpawn => _heroSpawn;

    

    public int MaxEnemysOnScene { get => _maxEnemysOnScene; set => _maxEnemysOnScene = value; }

    

    public EnemyCounter EnemyCounters => _enemyCounter;

    public int MaxEnemys { get => _maxEnemys; set => _maxEnemys = value; }
    public Transform HealthPotionSpawn { get => _healthPotionSpawn; set => _healthPotionSpawn = value; }
}

