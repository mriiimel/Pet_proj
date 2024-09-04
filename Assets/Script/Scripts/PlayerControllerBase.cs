using Enemy_Config;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Random = UnityEngine.Random;


public class PlayerControllerBase : MonoBehaviour,IPlayer
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private Collider _shildCollider;
    
    

    #region Privet and protected fields
    protected TextMeshProUGUI _textMeshProUGUI;
    private AnimatorController _animController;
    private UIController _menuPauseController;
    protected HeailhBehaviour _heailhBehaviour;
    protected HeroConfig _heroConfig;
    protected ConfigAllEnemys _enemyConfig;
    protected HealthPotionConfig _healthPotionConfig;
    protected UIController _pauseController;
    private Vector3 _moveDirection;
    private Vector3 _moveRotation;
    private Quaternion _rotationPl;
    protected int _currentHealth;
    #endregion

    #region Public propertys
    public Vector3 MoveDirection { get => _moveDirection; protected set => _moveDirection = value; }
    public Quaternion RotationPl { get => _rotationPl; protected set => _rotationPl = value; }
    #endregion

    #region Flags
    private bool _isAttacking = false;
    private bool _isShildUp = false;
    #endregion
    
    [Inject]
    private void Construct(AnimatorController animatorController, UIController uIController, HeailhBehaviour heailhBehaviour,
        HeroConfig heroConfig, ConfigAllEnemys configAllEnemys, HealthPotionConfig healthPotionConfig, UIController uIController1)
    {
        _animController = animatorController;
        _menuPauseController = uIController1;
        _heailhBehaviour = heailhBehaviour;
        _heroConfig = heroConfig;
        _enemyConfig = configAllEnemys;
        _healthPotionConfig = healthPotionConfig;
        _pauseController = uIController1;
    }

    #region Player Movement
    
    public void OnMove(InputAction.CallbackContext value)
    {
        _moveDirection = value.ReadValue<Vector2>();
        
        _moveDirection.Normalize();
    }
    protected void PlayerMove(Vector3 move)
    {

        var value = _heroConfig.MaxVerticalSpeed;
        value = Math.Clamp(value, -_heroConfig.MaxVerticalSpeed, _heroConfig.MaxVerticalSpeed);
        string animName = move.x >= 0.125F || move.y >= 0.125F || move.x <= -0.125F || move.y <= -0.125F ? "WalkForwardBattle" : default;
        _animController.PlayAnimation(_animator,animName);
        _rigidbody.AddRelativeForce(move.x * _heroConfig.Speed, value , move.y * _heroConfig.Speed, ForceMode.VelocityChange);
        
        
    }
    public void Look(InputAction.CallbackContext value)
    {
        _moveRotation = value.ReadValue<Vector2>();
        _rotationPl = Quaternion.Euler(0f, _moveRotation.x * _heroConfig.RotationSpeed, 0f);
        _rotationPl.Normalize();

    }
    protected void LookRotation(Quaternion rotate)
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * rotate.normalized);

    }
    #endregion

    #region Player Behaviour
    public async void OnAttack(InputAction.CallbackContext context)
    {
        if (_isShildUp) return;
        if(_isAttacking) return;
        if (_menuPauseController.OnPaused) return;
        _isAttacking = true;
        _weaponCollider.enabled = true;
        var crit = Random.Range(0.0F, 101.0F);
        
        string animationName = crit <= _heroConfig.CritChance ? "Attack02" : "Attack01";
        _animController.PlayAnimation(_animator, animationName);

        var animationClip = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        
        await Task.Delay(TimeSpan.FromSeconds(animationClip.length));
        _isAttacking = false;
        _weaponCollider.enabled = false;

    }

    public async void OnBlock(InputAction.CallbackContext context)
    {
        if (_isAttacking) return;
        if (_menuPauseController.OnPaused) return;
        _isShildUp = true;
        _shildCollider.enabled = true;
        _animController.PlayAnimation(_animator, "Defend");
        var clip = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        await Task.Delay(TimeSpan.FromSeconds(clip.length));
        _shildCollider.enabled = false;
        _isShildUp = false;
    }
    #endregion

    #region For OnCollisionEnter methods
    protected void OnCollisionWithEnemy(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Pseudopod")) return;
        var obj = collision.gameObject.GetComponentInParent<Enemy>().Type;
        _currentHealth = _heailhBehaviour.RemoveHealth(_enemyConfig.GetEnemyWithType(obj).EnemyAttack, _currentHealth, _heroConfig.MaxHealh);
    }
    #endregion

    #region For OnTriggerEnter Methods
    protected void OnTriggerMethod(Collider collider)
    {
        if (!collider.gameObject.CompareTag("HealthPotion")) return;
        var obj = collider.gameObject.GetComponent<HealthPotion>().PotionType;
        _currentHealth = _heailhBehaviour.AddHealth(_healthPotionConfig.GetPotionValue(obj).HealthRecover, _currentHealth, _heroConfig.MaxHealh);
    }
    #endregion
}
