using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;


public class UIController : MonoBehaviour //,IDisposable
{
    private UiView _view;
    private PlayerInput _playerInput;
    private ZenjectSceneLoader _sceneLoader;

    [Inject]
    public void Construct(UiView uiView,Camera camera,PlayerInput inputActions,ZenjectSceneLoader zenjectSceneLoader)
    {
        _view = uiView;
        _playerInput = inputActions;
        _sceneLoader = zenjectSceneLoader;
    }
    
    
    
    
    
    
    
    private void ActivatePlayerIsDeadWindow()
    {
        _view.PlayerIsDeadWindow.SetActive(true);
        _playerInput.UI.Enable();
    }

    
    public  void OnPause(InputAction.CallbackContext context)
    {
        
    }

    private void Paused()
    {
        
        
        
    }
    private void Resume()
    {
        
        
        
    }

    private void Exit()
    {
        
        _playerInput.UI.Disable();
        PlayerController.PlayerIsDead -= ActivatePlayerIsDeadWindow;
        _view.RestartButton.onClick.RemoveListener(Restart);
        _view.ExitGameButton.onClick.RemoveListener(Exit);
        Application.Quit();
    }

    private void Restart()
    {
        _view.PlayerIsDeadWindow.SetActive(false);
        
        _playerInput.UI.Disable();
        PlayerController.PlayerIsDead -= ActivatePlayerIsDeadWindow;
        _view.RestartButton.onClick.RemoveListener(Restart);
        _view.ExitGameButton.onClick.RemoveListener(Exit);
        StartCoroutine(LoadSceneAsync("Arena"));

        
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = _sceneLoader.LoadSceneAsync(sceneName,LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void Awake()
    {
        PlayerController.PlayerIsDead += ActivatePlayerIsDeadWindow;
        _view.RestartButton.onClick.AddListener(Restart);
        _view.ExitGameButton.onClick.AddListener(Exit);
        
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
