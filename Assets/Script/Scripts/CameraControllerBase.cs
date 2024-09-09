using UnityEngine;
using Cinemachine;
using Zenject;
using UnityEngine.InputSystem;

namespace Camera_Controller
{
    public class CameraControllerBase : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineFreeLook _vCam;

        private PlayerController _playerController;

        public Camera Camera { get => _camera; private set => _camera = value; }
        public CinemachineFreeLook VCam { get => _vCam; private set => _vCam = value; }

        public Vector3 _cameraMovement { get; set; }

        [Inject] 
        private void Construct(PlayerController playerControllerBase)
        {
            _playerController = playerControllerBase;
        }

        public void CameraLook(InputAction.CallbackContext context)
        {
            _cameraMovement = context.ReadValue<Vector2>().normalized;
            
        }
        protected void Init()
        {
            
            _vCam.LookAt = _playerController.transform;
            _vCam.Follow = _playerController.transform;
        }

        protected void ToUpdate()
        {
            _playerController.transform.Rotate(new Vector3(0,_cameraMovement.x,0),Space.World);
        }

        
    }
}
