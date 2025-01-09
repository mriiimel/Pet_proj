using Cinemachine;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _vrCamera;

    public CinemachineFreeLook VrCamera { get => _vrCamera; set => _vrCamera = value; }
    
}

