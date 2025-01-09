using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;




public class PlayerController: ITickable,IInitializable,IPlayer
{
    private PlayerView _player;
    private PlayerModel _playerModel;
    private ScriptableObjectService _scriptableObjectService;
    private Camera _camera;
    private PlayerInput _playerInput;
    private InputAction _onMove;
    private UiView _uiView;
    

    private Image _heroHealthBar;
    private Vector3 m_MoveDirection;
    private float _attackSpeedMultiplier;
    private float _currentHealth = 0;

    public static event Action HealthIsRecover;
    public static event Action PlayerIsDead;
    #region Flags
    private bool _isAttacking = false;
    private bool _isShildUp = false;
    #endregion


    public PlayerController(PlayerView playerView,PlayerModel playerModel,PlayerInput inputActions, UiView uiView)
    {
        _player = playerView;
        _playerModel = playerModel;
        _scriptableObjectService = playerModel.ScriptableObjectService;
        _camera = playerModel.CameraView;
        _playerInput = inputActions;
        _uiView = uiView;   
        _attackSpeedMultiplier = playerModel.AttackSpeed;
        
    }


    public void IncreaseAttackSpeed(float amount)
    {
        _attackSpeedMultiplier += amount;
    }

    public void DecreaseAttackSpeed(float amount)
    {
        _attackSpeedMultiplier = Mathf.Max(0.1f, _attackSpeedMultiplier - amount); 
    }

    private void HandleAttack()
    {

        if (_isShildUp | _isAttacking) return;
        _isAttacking = true;
        _player.PlayerAnimator.SetTrigger("FirstAttack");
       _isAttacking = false;
        _player.WeaponController.EnemyTarget.Clear();
       
    }

    private void HandleBlock()
    {
        if (_isAttacking) return;
        _isShildUp = true;
        _player.EnableShealdCollider(true);
        _player.PlayerAnimator.SetBool("Block", true);
    }

    private void PlayerMove(Vector3 move)
    {
        if(_isAttacking == true) return ;
        _player.PlayerAnimator.SetFloat("Value", move.magnitude);
        var axisX = _camera.transform.forward;
        var axisY = _camera.transform.right;
        Vector3 movement = move.x * axisY.normalized + move.y * axisX.normalized;
        Vector3 val = new Vector3(movement.x, 0, movement.z);
        _player.PlayerRb.MovePosition(_player.PlayerRb.transform.position + val * _playerModel.PlayerSpeed * Time.fixedDeltaTime);


        if (move.magnitude != 0)
        {

            Quaternion targetRotation = Quaternion.LookRotation(val);
            _player.PlayerRb.transform.rotation = Quaternion.Lerp(_player.PlayerRb.transform.rotation,
                targetRotation, _playerModel.PlayerRotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void UpdateHealthBar()
    {
        _heroHealthBar.fillAmount = _currentHealth / _scriptableObjectService.PlayerConfig.GetHeroValue().MaxHealth;
        
    }

    public void GetDamage(float damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            _player.PlayerAnimator.SetBool("Dead",true);
            PlayerIsDead?.Invoke();
            _playerInput.Player.Disable();
            _onMove?.Disable();
            _playerInput.Player.Fire.performed -= OnAttack;
            _playerInput.Player.Block.performed -= EnableBlock;
            _playerInput.Player.Block.canceled -= DisableBlock;
            _playerInput.Player.Fire.Disable();
            _playerInput.Player.Block.Disable();
        }
    }

    public void RecoverHealth(float healthRevover)
    {
        _currentHealth += healthRevover;
        if (_currentHealth > _scriptableObjectService.PlayerConfig.GetHeroValue().MaxHealth)
        {
            HealthIsRecover?.Invoke();
            _currentHealth = _scriptableObjectService.PlayerConfig.GetHeroValue().MaxHealth;
            
        }
    }
    

    public void Tick()
    {
        m_MoveDirection = _onMove.ReadValue<Vector2>();
        PlayerMove(m_MoveDirection);
        UpdateHealthBar();

    }
    

    public void Initialize()
    {
        _heroHealthBar = _uiView.HeroHealthBar;
        _currentHealth = _playerModel.PlayerMaxHealth;
        _heroHealthBar.fillAmount = _currentHealth / _scriptableObjectService.PlayerConfig.GetHeroValue().MaxHealth;
        _onMove = _playerInput.Player.Move;
        _playerInput.Player.Enable();
        _playerInput.UI.Disable();
        _player.PlayerAnimator.SetBool("Dead",false);
        _playerInput.Player.Fire.performed += OnAttack;
        _playerInput.Player.Block.performed += EnableBlock;
        _playerInput.Player.Block.canceled += DisableBlock;
        
        
    }

    public void Dispose()
    {
        _onMove?.Disable();
        _playerInput.Player.Fire.performed -= OnAttack;
        _playerInput.Player.Block.performed -= EnableBlock;
        _playerInput.Player.Block.canceled -= DisableBlock;
        _playerInput.Player.Fire.Disable();
        _playerInput.Player.Block.Disable();
        
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (_isAttacking == true) return;
        HandleAttack();

    }

    public void EnableBlock(InputAction.CallbackContext context)
    {
        HandleBlock();
    }

    public void DisableBlock(InputAction.CallbackContext callback)
    {
        _player.PlayerAnimator.SetBool("Block", false);
        _player.EnableShealdCollider(false);
        _isShildUp = false;
    }

    

}
