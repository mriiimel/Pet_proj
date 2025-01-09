using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _heroHealthBar;
    [SerializeField] private TextMeshProUGUI _totalEnemysKill;
    [SerializeField] private GameObject _playerIsDeadWindow;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitGameButton;
    
    
    public Image HeroHealthBar { get => _heroHealthBar; set => _heroHealthBar = value; }
    public Canvas Canvas { get => _canvas; set => _canvas = value; }
    public TextMeshProUGUI TotalEnemysKill { get => _totalEnemysKill; set => _totalEnemysKill = value; }
    public GameObject PlayerIsDeadWindow { get => _playerIsDeadWindow; set => _playerIsDeadWindow = value; }
    public Button RestartButton { get => _restartButton; set => _restartButton = value; }
    public Button ExitGameButton { get => _exitGameButton; set => _exitGameButton = value; }
}
