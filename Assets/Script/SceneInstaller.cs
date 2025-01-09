using Cinemachine;
using Enemy_Config;
using UnityEngine;
using Zenject;


public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Factory _enemyFactory;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private UiView _uiView;
    [SerializeField] private GameObject _enemyHealthBar;

    [SerializeField] private ScriptableObjectService _scriptableObjectService;
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineFreeLook _vrCamera;


    private PlayerView _player;
    private PlayerModel _playerModel;
    private PlayerController _playerController;
    private PlayerInput _playerInput;
    private CameraView _cameraView;


    private FactoryModel _factoryModel;
    private ObjectPool _objectPool;
    //private PlayerView _player;
    private EnemyModel _enemyModel;
    private EnemyController _enemyController;
    private ConfigAllEnemys _enemyConfig;

    public override void InstallBindings()
    {
        ConfigsBindings();
        //UIBindings();
        ObjectPoolBindings();
        FactoryBindings();
        CameraBindings();
        PlayerBindings();
        EnemyBindings();
        HealthPotionBindings();
        UIBindings();



    }

    private void EnemyBindings()
    {
        Container.Bind<EnemyModel>().AsTransient();
        Container.Bind<EnemyController>().AsTransient();
        Container.Bind<EnemyView>().FromComponentInHierarchy().AsTransient();
    }


    private void CameraBindings()
    {
        //CameraView camera = ProjectContext.Instance.Container.Resolve<CameraView>();
        Camera camera = Container.InstantiatePrefabForComponent<Camera>(_camera);
        Container.Bind<Camera>().FromInstance(camera).AsSingle();
        _cameraView = Container.InstantiatePrefabForComponent<CameraView>(_vrCamera);
        Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
        Container.Bind<CameraModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().WithArguments(camera);
        
        
    }

    private void HealthPotionBindings()
    {
        Container.Bind<HealthPotionActivator>().FromComponentInHierarchy().AsTransient(); 
        Container.Bind<HealthPotionController>().AsTransient();
        Container.Bind<HealthPotion>().FromComponentInHierarchy().AsTransient();
    }
   

    private void UIBindings()
    {
        Container.Bind<Canvas>().FromInstance(_canvas).AsSingle();
        Container.Bind<GameObject>().FromInstance(_enemyHealthBar).AsTransient();
        Container.Bind<UiView>().FromInstance(_uiView).AsSingle();
        Container.Bind<UIModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIController>().FromComponentInHierarchy().AsSingle();
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
        Container.Bind<HealthPotionFactory>().AsSingle();
        Container.Bind<FactoryModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<FactoryController>().FromComponentInHierarchy().AsSingle();
        
        
    }

    private void PlayerBindings()
    {
        //_player = ProjectContext.Instance.Container.Resolve<PlayerView>();
        _player = Container.InstantiatePrefabForComponent<PlayerView>(Container.Resolve<ScriptableObjectService>().PlayerConfig.GetHeroValue().Player);
        Container.Bind<PlayerView>().FromInstance(_player).AsSingle();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<PlayerModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().WithArguments(_player);
        
    }
    private void OnDisable()
    {
        Container.UnbindAll();
    }
    private void ConfigsBindings()
    {
        Container.Bind<ScriptableObjectService>().FromInstance(_scriptableObjectService).AsSingle();
    }
    //private void PlayerBindings()
    //{
    //    _player = Container.InstantiatePrefabForComponent<PlayerView>(Container.Resolve<ScriptableObjectService>().PlayerConfig.GetHeroValue().Player);
    //    Container.Bind<PlayerView>().FromInstance(_player).AsSingle();

    //}

    //private void CameraBindings()
    //{
    //    Camera camera = Container.InstantiatePrefabForComponent<Camera>(_camera);
    //    Container.Bind<Camera>().FromInstance(camera).AsSingle();
    //    _cameraView = Container.InstantiatePrefabForComponent<CameraView>(_vrCamera);
    //    Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
    //}
}
