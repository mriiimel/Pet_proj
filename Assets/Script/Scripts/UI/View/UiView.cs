using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _heroHealthBar;
    

    public Image HeroHealthBar { get => _heroHealthBar; set => _heroHealthBar = value; }
    
    public Canvas Canvas { get => _canvas; set => _canvas = value; }
}
