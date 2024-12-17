using Enemy_Config;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Zenject;



public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private Image _enemyHealthBar;
    [SerializeField] private GameObject _enemyWeapon;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _point;

    [field: SerializeField] public EnemyTypes Type { get; private set; }
    public Image EnemyHealthBar { get => _enemyHealthBar; set => _enemyHealthBar = value; }
    public NavMeshAgent Agent => m_Agent;
    public GameObject EnemyWeapon => _enemyWeapon;
    public Animator Animator => _animator;
    public Transform Point => _point;

    private ConfigAllEnemys _enemyConfig;
    private PlayerView _playerView;
    private HeroConfig _heroConfig;
    private float _currentHealth;
    [Inject]
    public void Construct(ConfigAllEnemys configAllEnemys,PlayerView playerView,HeroConfig heroConfig)
    {
        _enemyConfig = configAllEnemys;
        _playerView = playerView;   
        _heroConfig = heroConfig;
    }
    private void Start()
    {
        _currentHealth = _enemyConfig.GetEnemyWithType(Type).EnemyHealth;
        _enemyHealthBar.fillAmount = _currentHealth / _enemyConfig.GetEnemyWithType(Type).EnemyHealth;
    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            if(_currentHealth <= 0)
            {
                _currentHealth = 0;
            }
            else
            {
                _currentHealth -= _heroConfig.Damage;
                float value = _currentHealth / _enemyConfig.GetEnemyWithType(Type).EnemyHealth;
                _enemyHealthBar.fillAmount = value;
            }
            Debug.Log(collision);
            Debug.Log(_enemyHealthBar.fillAmount);
            Debug.Log(_currentHealth);
        }
    }
}
