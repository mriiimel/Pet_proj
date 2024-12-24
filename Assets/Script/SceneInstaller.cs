using Enemy_Config;
using Object_Pool;
using UnityEngine;
using Zenject;


public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Factory _enemyFactory;
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private UiView _uiView;
    [SerializeField] private GameObject _enemyHealthBar;
    
    

    
    private FactoryModel _factoryModel;
    private ObjectPool _objectPool;
    private PlayerView _player;
    private EnemyModel _enemyModel;
    private EnemyController _enemyController;
    private ConfigAllEnemys _enemyConfig;

    public override void InstallBindings()
    {
        UIBindings();
        ObjectPoolBindings();
        FactoryBindings();
        CameraBindings();
        PlayerBindings();
        EnemyBindings();
        Container.Bind<Canvas>().FromInstance(_canvas).AsSingle();
        Container.Bind<GameObject>().FromInstance(_enemyHealthBar).AsTransient() ;
        
    }

    private void EnemyBindings()
    {
        Container.Bind<EnemyModel>().AsTransient();
        Container.Bind<EnemyController>().AsTransient();
        Container.Bind<EnemyView>().FromComponentInHierarchy().AsTransient();
    }


    private void CameraBindings()
    {
        CameraView camera = ProjectContext.Instance.Container.Resolve<CameraView>();
        Container.Bind<CameraModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().WithArguments(camera);
        
        
    }

   

    private void UIBindings()
    {
        Container.Bind<UiView>().FromInstance(_uiView).AsCached();
        Container.Bind<UIModel>().AsCached();
        Container.BindInterfacesAndSelfTo<UIController>().AsCached();
    }

    private void ObjectPoolBindings()
    {
        Container.Bind<ObjectPool>().AsSingle();
    }

    private void FactoryBindings()
    {
        Container.Bind<Factory>().FromInstance(_enemyFactory).AsSingle();
        Container.Bind<PlayerFactory>().AsSingle();
        Container.Bind<EnemyFactory>().AsSingle();
        Container.Bind<FactoryModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<FactoryController>().AsSingle();
        
        
    }

    private void PlayerBindings()
    {
        _player = ProjectContext.Instance.Container.Resolve<PlayerView>();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<PlayerModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().WithArguments(_player);
        
    }

    
    
}
