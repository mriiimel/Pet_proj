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
        [SerializeField] private TextMeshProUGUI _totalEnemyCountText;
        
        [field: SerializeField] public List<Transform> _spawnEnemyPosition { get; private set; }

        [Inject] private ConfigAllEnemys _allEnemys;
        [Inject] private ObjectPool _pool;
        [Inject] private EnemyCounter _enemyCounter;

        private int _maxEnemy;

        public TextMeshProUGUI TotalEnemyCountText { get => _totalEnemyCountText; set => _totalEnemyCountText = value; }
        public int TotalEnemyCount { get => _maxEnemy; }

        public GameObject CreateEnemy(EnemyTypes enemyTypes)
        {
            return _allEnemys.GetEnemyWithType(enemyTypes).Enemys.gameObject;
        }

        protected void Init()
        {
            var value = Random.Range(0,_spawnEnemyPosition.Count);
            _enemyCounter.CountEnemy(_pool, ref _maxEnemy);
            TotalEnemyCountText.text = $"TOTAL ENEMY IS  {_maxEnemy}";
            for (int i = 0; i < _maxEnemy; i++)
            {
                var obj = _pool.GetFromPool();
                obj.SetActive(true);
                obj.transform.position = _spawnEnemyPosition[value].position;
            }
        }
    }
}
