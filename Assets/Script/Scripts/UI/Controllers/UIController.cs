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
    private PlayerController _playerController;
    public static event Action RestartGame;
    [Inject]
    public void Construct(UiView uiView,Camera camera,PlayerInput inputActions,ZenjectSceneLoader zenjectSceneLoader,PlayerController playerController)
    {
        _view = uiView;
        _playerInput = inputActions;
        _sceneLoader = zenjectSceneLoader;
        _playerController = playerController;
    }
    
    
    
    
    
    
    
    private void ActivatePlayerIsDeadWindow()
    {
        _view.MenuWindow.SetActive(true);
        _playerInput.UI.Enable();
    }

    
    public  void OnPause(InputAction.CallbackContext context)
    {
        _playerInput.Player.Disable();
        _playerInput.UI.Enable();
        _view.MenuWindow.SetActive(true);
    }

    private void Paused()
    {
        
        
        
    }
    private void Resume()
    {
        _playerInput?.Player.Enable();
        _playerInput.UI.Disable();
        _view.MenuWindow.SetActive(false);
        
        
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
        _view.MenuWindow.SetActive(false);
        RestartGame?.Invoke();
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
        _playerInput.Player.InvokeMenuPause.performed += OnPause;
        PlayerController.PlayerIsDead += ActivatePlayerIsDeadWindow;
        _view.RestartButton.onClick.AddListener(Restart);
        _view.ExitGameButton.onClick.AddListener(Exit);
        _view.ReturnButton.onClick.AddListener(Resume);
        _playerController.SetHeroHealthBar(_view.HeroHealthBar);
        
    }
    private void OnDisable()
    {
        _view.RestartButton.onClick.RemoveListener(Restart);
        _view.ExitGameButton.onClick.RemoveListener(Exit);
        _view.ReturnButton.onClick.RemoveListener(Resume);
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
