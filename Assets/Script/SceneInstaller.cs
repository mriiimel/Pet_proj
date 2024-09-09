using Camera_Controller;
using Enemy_Factory;
using Object_Pool;
using UnityEngine;
using Zenject;


public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Factory _enemyFactory;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private UIController _menuPauseController;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private PlayerController _playerController;
    //[SerializeField] private GameObject _playerPrefab;
    [SerializeField] private CameraController _cameraController;
    //[SerializeField] private Transform _heroSpawn;
    public override void InstallBindings()
    {
        ObjectPoolBindings();
        FactoryBindings();
        PlayerBindings();
        UIBindings();
        AnimatorControllerBindings();
        CameraBindings();
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
        //PlayerController playerController = Container.
        //    InstantiatePrefabForComponent<PlayerController>(_playerPrefab, _heroSpawn.position,Quaternion.identity,null);
        //Container.Bind<PlayerController>().FromInstance(playerController).AsCached().NonLazy();
        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle();
    }
}
