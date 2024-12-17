using Enemy_Factory;
using Object_Pool;
using UnityEngine;
using Zenject;


public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Factory _enemyFactory;
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private UIController _uiController;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private CameraView _cameraController;
    [SerializeField] private Enemy _enemy;
    
    private FactoryModel _factoryModel;
    private ObjectPool _objectPool;
    private PlayerView _player;
    private EnemyModel _enemyModel;
    private EnemyController _enemyController;

    public override void InstallBindings()
    {

        CameraBindings();
        ObjectPoolBindings();
        FactoryBindings();
        UIBindings();
        AnimatorControllerBindings();
        PlayerBindings();
        EnemyBindings();

    }

    private void EnemyBindings()
    {
        Container.Bind<Enemy>().FromInstance(_enemy).AsSingle();
        Container.Bind<EnemyModel>().AsSingle().WithArguments(_enemy);
        Container.BindInterfacesAndSelfTo<EnemyController>().AsSingle();
    }


    private void CameraBindings()
    {
        Container.Bind<CameraView>().FromInstance(_cameraController).AsSingle().NonLazy();
        
    }

    private void AnimatorControllerBindings()
    {
        Container.Bind<AnimatorController>().FromInstance(_animatorController).AsSingle().NonLazy();
    }

    private void UIBindings()
    {
        Container.Bind<UIController>().FromInstance(_uiController).AsSingle().NonLazy();
    }

    private void ObjectPoolBindings()
    {
        Container.Bind<ObjectPool>().AsSingle();
    }

    private void FactoryBindings()
    {
        Container.Bind<Factory>().FromInstance(_enemyFactory).AsSingle().NonLazy();
        
    }

    private void PlayerBindings()
    {
        _player = ProjectContext.Instance.Container.Resolve<PlayerView>();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<PlayerModel>().AsSingle().WithArguments(_player);
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().WithArguments(_player);
        
    }

    
    
}
