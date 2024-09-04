using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private TextMeshProUGUI _heroHealthBarText;
    [SerializeField] private TextMeshProUGUI _totalEnemyText;
    
    PlayerController _playerController;
    
    
    
    private bool _onPaused = false;

    public bool OnPaused { get => _onPaused; set => _onPaused = value; }
    public TextMeshProUGUI HeroHealthBarText { get => _heroHealthBarText; set => _heroHealthBarText = value; }
    public TextMeshProUGUI TotalEnemyText { get => _totalEnemyText; set => _totalEnemyText = value; }
    
    [Inject]
    private void Construct(PlayerController playerControllerBase)
    {
        _playerController = playerControllerBase;
    }
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
