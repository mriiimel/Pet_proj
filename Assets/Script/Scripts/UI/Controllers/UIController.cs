using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;


public class UIController : IInitializable,ITickable
{
    private UiView _view;
    private Camera _camera;
    public UIController(UiView uiView,Camera camera)
    {
        _view = uiView;
        _camera = camera;
    }
    
    
    
    
    
    
    
    

    
    public  void OnPause(InputAction.CallbackContext context)
    {
        
    }

    private void Paused()
    {
        
        
        
    }
    public void Resume()
    {
        
        
        
    }

    public void Initialize()
    {
        _view.Canvas.worldCamera = _camera;
        _view.Canvas.planeDistance = 1;
    }

    public void Tick()
    {
        
    }
}
