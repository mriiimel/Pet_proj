using UnityEngine;
using Zenject;

public class EnemyFactory 
{
    private DiContainer _container;
    private Canvas _canvas;
    private GameObject _enemyHealthBar;
    public EnemyFactory(DiContainer diContainer, Canvas canvas, GameObject enemyHealthBar)
    {
        _container = diContainer;
        _canvas = canvas;
        _enemyHealthBar = enemyHealthBar;
    }

    public void CreateEnemy(EnemyTypes enemyTypes,Transform enemySpawn)
    {
        var enemy = _container.Resolve<ScriptableObjectService>().EnemyConfig.GetEnemy(enemyTypes).Enemys.gameObject;
        var enemyInstace = _container.InstantiatePrefab(enemy,enemySpawn.position,Quaternion.identity,null);
        var enemyHealthBar = _container.InstantiatePrefab(_enemyHealthBar, _canvas.transform);
        var enemyController = enemyInstace.GetComponent<EnemyController>();
        var enemyView = enemyInstace.GetComponent<EnemyView>();
        enemyController.Construct(enemyView,_container.Resolve<PlayerView>(),_container.Resolve<EnemyModel>(),
            _container.Resolve<UiView>(), _container.Resolve<Camera>(), enemyHealthBar);
        
    }
}