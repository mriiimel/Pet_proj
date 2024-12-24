using Zenject;
using UnityEngine;
using Enemy_Config;
using Cinemachine;


public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ScriptableObjectService _scriptableObjectService;
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineFreeLook _vrCamera;
    

    private PlayerView _player;
    private PlayerModel _playerModel;
    private PlayerController _playerController;
    private PlayerInput _playerInput;
    private CameraView _cameraView;
    
    



    public override void InstallBindings()
    {
        ConfigsBindings();
        CameraBindings();
        PlayerBindings();

    }

    private void ConfigsBindings()
    {
        Container.Bind<ScriptableObjectService>().FromInstance(_scriptableObjectService).AsSingle();
    }
    private void PlayerBindings()
    {
        _player = Container.InstantiatePrefabForComponent<PlayerView>(Container.Resolve<ScriptableObjectService>().PlayerConfig.GetHeroValue().Player);
        Container.Bind<PlayerView>().FromInstance(_player).AsSingle();
        
    }

    private void CameraBindings()
    {
        Camera camera = Container.InstantiatePrefabForComponent<Camera>(_camera);
        Container.Bind<Camera>().FromInstance(camera).AsSingle();
        _cameraView = Container.InstantiatePrefabForComponent<CameraView>(_vrCamera);
        Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
    }


}
