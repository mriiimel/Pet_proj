using Enemy_Config;
using Object_Pool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Enemy_Factory
{
    public class FactoryModel
    {

        private Factory _factory;
        [Inject]private ConfigAllEnemys _allEnemys;
        private ObjectPool _pool;
        [Inject]private EnemyCounter _enemyCounter;
        private UIController _uiController;
        

        private int _maxEnemy;
        protected TextMeshProUGUI _totalEnemyCountText;

        
        public FactoryModel(Factory factory, ObjectPool objectPool, EnemyCounter enemyCounter, UIController uIController)
        {
            _factory = factory;
            _pool = objectPool;
            _enemyCounter = enemyCounter;
            _uiController = uIController;
        }

        public GameObject CreateEnemy(EnemyTypes enemyTypes)
        {
            return _allEnemys.GetEnemyWithType(enemyTypes).Enemys.gameObject;
        }

        protected void Init()
        {
            _totalEnemyCountText = _uiController.TotalEnemyText;
            
            _enemyCounter.CountEnemy(_pool, ref _maxEnemy);
            _totalEnemyCountText.text = $"TOTAL ENEMY IS  {_maxEnemy}";
            for (int i = 0; i < _maxEnemy; i++)
            {
                var value = Random.Range(0, _factory.SpawnEnemyPosition.Length);
                var obj = _pool.GetFromPool();
                obj.SetActive(true);
                obj.transform.position = _factory.SpawnEnemyPosition[value].position;
            }
        }
    }
}
