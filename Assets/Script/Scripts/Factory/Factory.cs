using Enemy_Config;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class Factory : MonoBehaviour
{
    [SerializeField] private  Transform[] _enemySpawn;
    [SerializeField] private  Transform _heroSpawn;

    private FactoryController _factoryController;
    private DiContainer _container;
    public Transform[] EnemySpawn => _enemySpawn;

    public Transform HeroSpawn => _heroSpawn;
    
    
}

