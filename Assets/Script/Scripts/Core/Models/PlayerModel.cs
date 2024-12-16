using Enemy_Config;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;


public class PlayerModel
{
    
    private HeroConfig _heroConfig;
    private ConfigAllEnemys _enemyConfig;
    private HealthPotionConfig _healthPotionConfig;
    private Animator _animator;
    private AnimatorController _animController;
    private UIController _uiController;
    private CameraView _camera;
    private Rigidbody _rigidbody;
    private Collider _weaponCollider;
    private Collider _shealdCollider;
    private Vector3 _moveDirection;
    private Quaternion _rotationPl;
    private int _currentHealth;

    public HeroConfig HeroConfigs { get => _heroConfig; private set => _heroConfig = value; }
    public ConfigAllEnemys EnemyConfigs { get => _enemyConfig; private set => _enemyConfig = value; }
    public HealthPotionConfig HealthPotionConfigs { get => _healthPotionConfig; private set => _healthPotionConfig = value; }
    public Animator Animator { get => _animator; set => _animator = value; }
    public AnimatorController AnimControllers { get => _animController; private set => _animController = value; }
    public UIController ControllerUI { get => _uiController; private set => _uiController = value; }
    public CameraView Cameras { get => _camera; private set => _camera = value; }
    public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    public Collider WeaponCollider { get => _weaponCollider; set => _weaponCollider = value; }
    public Collider ShealdCollider { get => _shealdCollider; set => _shealdCollider = value; }
    public Vector3 MoveDirections { get => _moveDirection; set => _moveDirection = value; }
    public Quaternion RotationPls { get => _rotationPl; set => _rotationPl = value; }
    public int CurrentHealths { get => _currentHealth; set => _currentHealth = value; }



    public PlayerModel(HeroConfig heroConfig, ConfigAllEnemys configAllEnemys, HealthPotionConfig healthPotionConfig
        , AnimatorController animatorController,UIController uIController,
        CameraView cameraController,PlayerView player)
    {
        _heroConfig = heroConfig;
        _enemyConfig = configAllEnemys;
        _healthPotionConfig = healthPotionConfig;
        _animator = player.PlayerAnimator;
        _animController = animatorController;
        _uiController = uIController;
        _camera = cameraController;
        _rigidbody = player.PlayerRb;
        _weaponCollider = player.WeaponCollider;
        _shealdCollider = player.ShealdCollider;
    }
    
}
