using Enemy_Config;
using Object_Pool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Enemy_Factory
{
    public class FactoryBase:MonoBehaviour
    {
        [field: SerializeField] public Transform[] spawnEnemyPosition { get; private set; }

        private ConfigAllEnemys _allEnemys;
        private ObjectPool _pool;
        private EnemyCounter _enemyCounter;
        private UIController _menuPauseController;
        

        private int _maxEnemy;
        protected TextMeshProUGUI _totalEnemyCountText;

        [Inject]
        private void Construct(ConfigAllEnemys configAllEnemys, ObjectPool objectPool, EnemyCounter enemyCounter, UIController uIController)
        {
            _allEnemys = configAllEnemys;
            _pool = objectPool;
            _enemyCounter = enemyCounter;
            _menuPauseController = uIController;
        }

        public GameObject CreateEnemy(EnemyTypes enemyTypes)
        {
            return _allEnemys.GetEnemyWithType(enemyTypes).Enemys.gameObject;
        }

        protected void Init()
        {
            _totalEnemyCountText = _menuPauseController.TotalEnemyText;
            
            _enemyCounter.CountEnemy(_pool, ref _maxEnemy);
            _totalEnemyCountText.text = $"TOTAL ENEMY IS  {_maxEnemy}";
            for (int i = 0; i < _maxEnemy; i++)
            {
                var value = Random.Range(0, spawnEnemyPosition.Length);
                var obj = _pool.GetFromPool();
                obj.SetActive(true);
                obj.transform.position = spawnEnemyPosition[value].position;
            }
        }
    }
}
