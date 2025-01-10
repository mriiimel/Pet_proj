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

    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineFreeLook _vrCamera;


    private PlayerView _player;
    private PlayerModel _playerModel;
    private PlayerController _playerController;
    private PlayerInput _playerInput;
    private CameraView _cameraView;


    private FactoryModel _factoryModel;
    private ObjectPool _objectPool;
    private EnemyModel _enemyModel;
    private EnemyController _enemyController;
    private ConfigAllEnemys _enemyConfig;

    public override void InstallBindings()
    {
        //ConfigsBindings();
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
        Camera camera = Container.InstantiatePrefabForComponent<Camera>(_camera);
        Container.Bind<Camera>().FromInstance(camera).AsSingle();
        _cameraView = Container.InstantiatePrefabForComponent<CameraView>(_vrCamera);
        Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
        Container.Bind<CameraModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().WithArguments(camera);


    }

    private void HealthPotionBindings()
    {
        Container.Bind<HealthPotionActivator>().FromComponentInHierarchy().AsCached(); 
        Container.Bind<HealthPotionController>().AsCached();
        Container.Bind<HealthPotion>().FromComponentInHierarchy().AsCached();
    }
   

    private void UIBindings()
    {
        Container.Bind<Canvas>().FromInstance(_canvas).AsCached();
        Container.Bind<GameObject>().FromInstance(_enemyHealthBar).AsCached();
        Container.Bind<UiView>().FromInstance(_uiView).AsCached();
        Container.Bind<UIModel>().AsCached();
        Container.BindInterfacesAndSelfTo<UIController>().FromComponentInHierarchy().AsCached();
    }

    private void ObjectPoolBindings()
    {
        Container.Bind<ObjectPool>().AsCached();
    }

    private void FactoryBindings()
    {
        Container.Bind<Factory>().FromInstance(_enemyFactory).AsCached();
        Container.Bind<PlayerFactory>().AsCached();
        Container.Bind<EnemyFactory>().AsCached();
        Container.Bind<HealthPotionFactory>().AsCached();
        Container.Bind<FactoryModel>().AsCached();
        Container.BindInterfacesAndSelfTo<FactoryController>().FromComponentInHierarchy().AsCached();
        
        
    }

    private void PlayerBindings()
    {
        var scrService = ProjectContext.Instance.Container.Resolve<ScriptableObjectService>();
        _player = Container.InstantiatePrefabForComponent<PlayerView>(scrService.PlayerConfig.GetHeroValue().Player);
        Container.Bind<PlayerView>().FromInstance(_player).AsSingle();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<PlayerModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().WithArguments(_player);

    }

    private void GameDataBindings()
    {
        Container.Bind<GameData>().AsSingle();
    }
    private void OnDisable()
    {
        Container.UnbindAll();
    }


    
}
