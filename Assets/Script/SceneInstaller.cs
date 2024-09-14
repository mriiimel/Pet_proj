using Camera_Controller;
using Enemy_Factory;
using Object_Pool;
using System;
using UnityEngine;
using Zenject;


public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Factory _enemyFactory;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private UIController _menuPauseController;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CameraController _cameraController;
    
    public override void InstallBindings()
    {
        
        ObjectPoolBindings();
        FactoryBindings();
        PlayerBindings();
        UIBindings();
        AnimatorControllerBindings();
        CameraBindings();
        EnemyBehaviourBindings();
    }

    private void EnemyBehaviourBindings()
    {
        Container.Bind<IEnemyBehaviour>().To<SimpleEnemyBehaviour>().AsCached();
        
        Container.Bind<IEnemyBehaviour>().To<WarriorEnemyBehaviour>().AsCached();
        
        Container.Bind<IEnemyBehaviour>().To<HeallerEnemyBehaviour>().AsCached();
        
        Container.Bind<IEnemyBehaviour>().To<BerserkEnemyBehaviour>().AsCached();
        
        Container.Bind<IEnemyBehaviour>().To<DebufferEnemyBehaviour>().AsCached();
        
        Container.Bind<IEnemyBehaviour>().To<BossEnemyBehaviour>().AsCached();
        
    }

    private void CameraBindings()
    {
        Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle();
    }

    private void AnimatorControllerBindings()
    {
        Container.Bind<AnimatorController>().FromInstance(_animatorController).AsSingle();
    }

    private void UIBindings()
    {
        Container.Bind<UIController>().FromInstance(_menuPauseController).AsSingle();
    }

    private void ObjectPoolBindings()
    {
        Container.Bind<ObjectPool>().FromInstance(_objectPool).AsCached();
    }

    private void FactoryBindings()
    {
        Container.Bind<Factory>().FromInstance(_enemyFactory).AsSingle();
    }

    private void PlayerBindings()
    {
        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle();
    }
}
