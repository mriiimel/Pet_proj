using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Zenject;

public  class CameraBase : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vCam;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _angle = 720;
    protected Vector3 _camMovement;
    [Inject]private PlayerController _playerController;
    public void OnCameraRotate(InputAction.CallbackContext value)
    {
        _camMovement = value.ReadValue<Vector2>();
        Vector3 rot = new Vector3(0, _camMovement.x, 0);
        Vector3 point = new Vector3(_playerController.transform.position.x, _playerController.transform.position.y, _playerController.transform.position.z);
        _camera.transform.RotateAround(_playerController.transform.position, rot, _angle);
        //_vCam.transform.RotateAround(_playerController.transform.position,new Vector3(0,_camMovement.x,0),_angle);
     }

    protected void CameraRotate(Vector3 camRotation)
    {
        Vector3 rot = new Vector3(0, camRotation.x, 0);
        _camera.transform.RotateAround(_playerController.transform.position, rot, _angle);
    }
}
