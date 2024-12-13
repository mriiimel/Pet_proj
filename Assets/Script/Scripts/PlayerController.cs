using Camera_Controller;
using Enemy_Config;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;


public class PlayerController: ITickable,IInitializable,IDisposable
{
    public Action _playerMove;
    
    private Player _player;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private AnimatorController _animController;
    private Collider _weaponCollider;
    private Collider _shildCollider;
    private UIController _menuPauseController;
    private IHeailhBehaviour _heailhBehaviour;
    private HeroConfig _heroConfig;
    private ConfigAllEnemys _enemyConfig;
    private HealthPotionConfig _healthPotionConfig;
    private int _currentHealth;
    private CameraController _camera;
    private Vector3 m_MoveDirection;
    private PlayerInput _playerInput;
    private InputAction _onMove;
    
    #region Flags
    private bool _isAttacking = false;
    private bool _isShildUp = false;
    #endregion


    public PlayerController(Player player,PlayerModel playerModel,PlayerInput inputActions)
    {
        _player = player;
        _rigidbody = playerModel.Rigidbody;
        _animator = playerModel.Animator;
        _animController = playerModel.AnimControllers;
        _weaponCollider = player.WeaponCollider;
        _shildCollider = player.ShealdCollider;
        _heroConfig = playerModel.HeroConfigs;
        _enemyConfig = playerModel.EnemyConfigs;
        _healthPotionConfig = playerModel.HealthPotionConfigs;
        _currentHealth = playerModel.CurrentHealths;
        _camera = playerModel.Cameras;
        m_MoveDirection = playerModel.MoveDirections;
        _playerInput = inputActions;
    }



    #region Player Movement

    
    private void PlayerMove(Vector3 move)
    {
        _animController.PlayAnimation(_animator,"Value",move.magnitude);
        var axisX = _camera.Camera.transform.forward;
        var axisY = _camera.Camera.transform.right;
        Vector3 movement = move.x * axisY.normalized + move.y * axisX.normalized;
        Vector3 val = new Vector3(movement.x,0,movement.z);
        _player.PlayerRb.MovePosition(_player.PlayerRb.transform.position + val * _heroConfig.Speed * Time.fixedDeltaTime);
        if(move.magnitude != 0)
        {
            _player.PlayerRb.transform.rotation = Quaternion.LookRotation(val);
        }
    }
    
    #endregion

    #region Player Behaviour
    private async void OnAttack(InputAction.CallbackContext context)
    {
        if (_isShildUp) return;
        if (_isAttacking) return;
        _isAttacking = true;
        _weaponCollider.enabled = true;
        var crit = Random.Range(0.0F, 100.0F);
        
        string animationName = crit <= _heroConfig.CritChance ? "Attack02" : "Attack01";
        _animController.PlayAnimation(_animator, animationName);

        var animationClip = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        
        await Task.Delay(TimeSpan.FromSeconds(animationClip.length));
        _isAttacking = false;
        _weaponCollider.enabled = false;

    }

    private async void OnBlock(InputAction.CallbackContext context)
    {
        if (_isAttacking) return;
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
    public void OnCollisionWithEnemy(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Pseudopod")) return;
        var obj = collision.gameObject.GetComponentInParent<Enemy>().Type;
        _currentHealth = _heailhBehaviour.RemoveHealth(_enemyConfig.GetEnemyWithType(obj).EnemyAttack, _currentHealth, _heroConfig.MaxHealh);
    }
    #endregion

    #region For OnTriggerEnter Methods
    public void OnTriggerMethod(Collider collider)
    {
        if (!collider.gameObject.CompareTag("HealthPotion")) return;
        var obj = collider.gameObject.GetComponent<HealthPotion>().PotionType;
        _currentHealth = _heailhBehaviour.AddHealth(_healthPotionConfig.GetPotionValue(obj).HealthRecover, _currentHealth, _heroConfig.MaxHealh);
    }
    #endregion

    private void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void Tick()
    {
        m_MoveDirection = _onMove.ReadValue<Vector2>();
        PlayerMove(m_MoveDirection);
        
    }

    public void Initialize()
    {
        
        _onMove = _playerInput.Player.Move;
        _playerInput.Player.InvokeMenuPause.performed += Restart;
        _playerInput.Player.Fire.performed += OnAttack;
        _playerInput.Player.Block.performed += OnBlock;
        _playerInput.Player.Enable();
        
    }

    public void Dispose()
    {
        _onMove?.Disable();
        _playerInput.Player.Fire.Disable();
        _playerInput.Player.Block.Disable();
        _playerInput.Player.InvokeMenuPause.Disable();
    }
    
}
