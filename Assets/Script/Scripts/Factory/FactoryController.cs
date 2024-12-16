using Enemy_Config;
using Enemy_Factory;
using Object_Pool;
using TMPro;
using UnityEngine;

public class FactoryController 
{
    private readonly Factory _factory;
    private readonly FactoryModel _factoryModel;
    private readonly ConfigAllEnemys _configAllEnemys;
    private readonly ObjectPool _objectPool;
    private readonly EnemyCounter _enemyCounter;
    private readonly UIController _uIController;
    private TextMeshProUGUI _totalEnemyCount;
    private int _maxEnemy;
    
    
    public FactoryController(FactoryModel factoryModel)
    {
        _factory = factoryModel.Factory;
        _factoryModel = factoryModel;
        _configAllEnemys = factoryModel.AllEnemysConfig;
        _objectPool = factoryModel.Pool;
        _enemyCounter = factoryModel.EnemyCounters;
        _uIController = factoryModel.UiController;
        _totalEnemyCount = factoryModel.TotalEnemyCountText;
        _maxEnemy = factoryModel.MaxEnemy;
    }
    
    public GameObject CreateEnemy(EnemyTypes enemyTypes)
    {
        return _configAllEnemys.GetEnemyWithType(enemyTypes).Enemys.gameObject;
    }
    
    public void Init()
    {
        CreateEnemys();
        UpdateUI();
        
    }

    public void UpdateUI()
    {
        _totalEnemyCount.text = $"TOTAL ENEMY IS  {_maxEnemy}";
    }

    private void CreateEnemys()
    {
        _enemyCounter.CountEnemy(_objectPool, ref _maxEnemy);
        for (int i = 0; i < _maxEnemy; i++)
        {
            var EnemyGameObject = _objectPool.GetFromPool();
            if(EnemyGameObject != null)
            {
                EnemyGameObject.SetActive(true);
                EnemyGameObject.transform.position = GetRandomSpawnPosition();
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        var SpawnIndex = Random.Range(0, _factory.SpawnEnemyPosition.Length);
        return _factory.SpawnEnemyPosition[SpawnIndex].position;
    }

    
}
