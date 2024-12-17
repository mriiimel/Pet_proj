using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;


public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private TextMeshProUGUI _totalEnemyText;
    [SerializeField] private Image _enemyHealthBar;
    
    PlayerView _playerController;
    
    
    
    private bool _onPaused = false;

    public bool OnPaused { get => _onPaused; set => _onPaused = value; }
    
    public TextMeshProUGUI TotalEnemyText { get => _totalEnemyText; set => _totalEnemyText = value; }
    public Image EnemyHealthBar { get => _enemyHealthBar; set => _enemyHealthBar = value; }

    //[Inject]
    //private void Construct(Player playerControllerBase)
    //{
    //    _playerController = playerControllerBase;
    //}
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
