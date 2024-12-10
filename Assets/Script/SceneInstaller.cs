using Camera_Controller;
using Enemy_Factory;
using Object_Pool;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Factory _enemyFactory;
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private UIController _uiController;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private Transform _playerSpawn;
    

    private Player _player;
    private PlayerController _playerController;
    private PlayerModel _playerModel;
    private FactoryModel _factoryModel;
    private ObjectPool _objectPool;
    private PlayerInput _playerInput;

    public override void InstallBindings()
    {
        
        CameraBindings();
        ObjectPoolBindings();
        FactoryBindings();
        ObjectPoolBindings();
        UIBindings();
        AnimatorControllerBindings();
        PlayerBindings();
        PlayerInputBindings();

    }

    private void PlayerInputBindings()
    {
        _playerInput = new PlayerInput();
        Container.Bind<PlayerInput>().FromInstance( _playerInput );
    }

    private void CameraBindings()
    {
        Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle().NonLazy();
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
        
    }

    private void FactoryBindings()
    {
        _factoryModel = new(_enemyFactory,_objectPool,_enemyCounter,_uiController);
        Container.Bind<FactoryModel>().FromInstance(_factoryModel).AsSingle().NonLazy();
        Container.Bind<Factory>().FromInstance(_enemyFactory).AsSingle().NonLazy();
    }

    private void PlayerBindings()
    {
        _player = Container.InstantiatePrefabForComponent<Player>(_playerGameObject, 
            _playerSpawn.position, 
            Quaternion.identity, null);
        Container.Bind<Player>().FromInstance(_player).AsSingle();
        Container.BindInterfacesTo<PlayerController>().AsSingle();
        Container.Bind<PlayerModel>().AsSingle();

    }
    
}
