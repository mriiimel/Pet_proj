using Enemy_Config;
using UnityEngine;
using Zenject;



public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private Collider _shealdCollider;
    [SerializeField] private Animator _playerAnimator;

    private PlayerController _playerController;
    private PlayerModel _playerModel;
    private HeroConfig _heroConfig;
    private ConfigAllEnemys _configAllEnemys;
    private HealthPotionConfig _healthPotionConfig;

    public Rigidbody PlayerRb { get => _playerRb; private set => _playerRb = value; }
    public Collider WeaponCollider { get => _weaponCollider; private set => _weaponCollider = value; }
    public Collider ShealdCollider { get => _shealdCollider; private set => _shealdCollider = value; }
    public Animator PlayerAnimator { get => _playerAnimator; private set => _playerAnimator = value; }
    public HeroConfig HeroConfig { get => _heroConfig; set => _heroConfig = value; }
    public ConfigAllEnemys ConfigAllEnemys { get => _configAllEnemys; set => _configAllEnemys = value; }
    public HealthPotionConfig HealthPotionConfig { get => _healthPotionConfig; set => _healthPotionConfig = value; }

    private Vector3 _moveDir;
    private Quaternion _lookRot;

    [Inject]
    public void Construct(HeroConfig heroConfig,ConfigAllEnemys configAllEnemys,HealthPotionConfig healthPotionConfig)
    {
        _heroConfig = heroConfig;
        _configAllEnemys = configAllEnemys;
        _healthPotionConfig = healthPotionConfig;
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
