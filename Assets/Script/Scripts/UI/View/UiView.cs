using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _heroHealthBar;
    [SerializeField] private TextMeshProUGUI _totalEnemysKill;

    public Image HeroHealthBar { get => _heroHealthBar; set => _heroHealthBar = value; }
    
    public Canvas Canvas { get => _canvas; set => _canvas = value; }
    public TextMeshProUGUI TotalEnemysKill { get => _totalEnemysKill; set => _totalEnemysKill = value; }
}
