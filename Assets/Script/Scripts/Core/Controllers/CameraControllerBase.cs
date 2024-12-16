using UnityEngine;
using Cinemachine;
using Zenject;
using UnityEngine.InputSystem;


    public class CameraControllerBase : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineFreeLook _vCam;

        private PlayerView _player;

        public Camera Camera { get => _camera; private set => _camera = value; }
        public CinemachineFreeLook VCam { get => _vCam; private set => _vCam = value; }

        public Vector3 _cameraMovement;

        [Inject]
        private void Construct(PlayerView playerController)
        {
            _player = playerController;
        }

        public void CameraLook(InputAction.CallbackContext context)
        {
            _cameraMovement = context.ReadValue<Vector2>().normalized;
            
        }
        protected void Init()
        {
            
            _vCam.LookAt = _player.transform;
            _vCam.Follow = _player.transform;
        }

        protected void ToUpdate()
        {
            _player.transform.Rotate(new Vector3(0,_cameraMovement.x,0),Space.World);
        }

        
    }

