using Cinemachine;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineFreeLook _vrCamera;

    public CinemachineFreeLook VrCamera { get => _vrCamera; set => _vrCamera = value; }
    public Camera Camera { get => _camera; set => _camera = value; }
}

