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
    [SerializeField] private Collider _collider;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private Collider _shildCollider;
    [SerializeField] protected TextMeshProUGUI _textMeshProUGUI;

    #region Inject fields
    [Inject]private AnimatorController _animController;
    [Inject]private MenuPauseController _menuPauseController;
    [Inject]protected HeailhBehaviour _heailhBehaviour;
    [Inject]protected HeroConfig _heroConfig;
    [Inject]protected ConfigAllEnemys _enemyConfig;
    [Inject]protected HealthPotionConfig _healthPotionConfig;
    #endregion

    #region Privet and protected fields
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

    #region Player Movement
    public void OnMove(InputAction.CallbackContext value)
    {
        _moveDirection = value.ReadValue<Vector2>();
        
        _moveDirection.Normalize();
    }
    public void PlayerMove(Vector3 move)
    {
        string animName = move.x >= 0.125F || move.y >= 0.125F || move.x <= -0.125F || move.y <= -0.125F ? "WalkForwardBattle" : default;
        _animController.PlayAnimation(_animator,animName);
        _rigidbody.AddRelativeForce(new Vector3(move.x * _heroConfig.Speed, 0, move.y * _heroConfig.Speed), ForceMode.VelocityChange);
        
        
    }
    public void Look(InputAction.CallbackContext value)
    {
        _moveRotation = value.ReadValue<Vector2>();
        _rotationPl = Quaternion.Euler(0f, _moveRotation.x * _heroConfig.RotationSpeed, 0f);
        _rotationPl.Normalize();

    }
    public void LookRotation(Quaternion rotate)
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
        if (collision.gameObject.CompareTag("Pseudopod"))
        {
            var obj = collision.gameObject.GetComponentInParent<Enemy>().Type;
            _currentHealth = _heailhBehaviour.RemoveHealth(_enemyConfig.GetEnemyWithType(obj).EnemyAttack, _currentHealth, _heroConfig.MaxHealh);
        }
    }
    #endregion

    #region For OnTriggerEnter Methods
    protected void OnTriggerMethod(Collider collider)
    {
        if (collider.gameObject.CompareTag("HealthPotion"))
        {
            var obj = collider.gameObject.GetComponent<HealthPotion>().PotionType;
            _currentHealth = _heailhBehaviour.AddHealth(_healthPotionConfig.GetPotionValue(obj).HealthRecover, _currentHealth, _heroConfig.MaxHealh); 
        }
    }
    #endregion
}
