using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


public class CameraController : IInitializable,ITickable
{
    private PlayerView _player;
    private Camera _camera;
    private CameraView _cameraView;
    private CameraModel _cameraModel;  

    private Vector3 _cameraMovement;

    
    public  CameraController(CameraView cameraView, PlayerView playerController,Camera camera,CameraModel cameraModel)
    {
        _player = playerController;
        _camera = camera;
        _cameraView = cameraView;
        _cameraModel = cameraModel;
    }

    public void CameraLook(InputAction.CallbackContext context)
    {
        _cameraMovement = context.ReadValue<Vector2>().normalized;
            
    }

    

    private void Init()
    {
            
        _cameraView.VrCamera.LookAt = _player.transform;
        _cameraView.VrCamera.Follow = _player.transform;
        
    }

    private void ToUpdate()
    {
        _player.transform.Rotate(new Vector3(0,_cameraMovement.x,0),Space.World);
    }
    
    public void Initialize()
    {
        Init();
    }

    public void Tick()
    {
        ToUpdate();
    }
}

