using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


public class MenuPauseController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    
    [Inject] PlayerController _playerController;
    [Inject] PlayerInput inputs;
    
    
    private bool _onPaused = false;

    public bool OnPaused { get => _onPaused; set => _onPaused = value; }

    public  void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            
            
            if (!_onPaused)
            {
                Paused();
            }
            else
            {
                Resume();
            }
        }
    }

    private void Paused()
    {
        
        _playerController.enabled = false;
        _menu.SetActive(true);
        _onPaused = true;
    }
    public void Resume()
    {
        
        _playerController.enabled = true;
        _menu.SetActive(false);
        _onPaused = false;
    }

}
