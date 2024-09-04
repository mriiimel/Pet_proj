using UnityEngine;
using Cinemachine;
using Zenject;

namespace Camera_Controller
{
    public class CameraBase : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _vCam;

        private PlayerController _playerController;


        [Inject] 
        private void Construct(PlayerController playerControllerBase)
        {
            _playerController = playerControllerBase;
        }
        protected void Init()
        {

            _vCam.LookAt = _playerController.transform;
            _vCam.Follow = _playerController.transform;
        }
    }
}
