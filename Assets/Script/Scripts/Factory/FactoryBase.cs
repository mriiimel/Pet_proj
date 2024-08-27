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

        private int _totalEnemyCount;

        public TextMeshProUGUI TotalEnemyCountText { get => _totalEnemyCountText; set => _totalEnemyCountText = value; }


        public GameObject CreateEnemy(EnemyTypes enemyTypes)
        {
            return _allEnemys.GetEnemyWithType(enemyTypes).Enemys.gameObject;
        }

        protected void Init()
        {
            _enemyCounter.CountEnemy(_pool, _totalEnemyCount);
            TotalEnemyCountText.text = $"TOTAL ENEMY IS  {_totalEnemyCount}";
            for (int i = 0; i < _totalEnemyCount; i++)
            {
                 var obj = _pool.GetFromPool();
                obj.SetActive(true);
            }
        }
    }
}
