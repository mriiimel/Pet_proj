using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;




public class PlayerController: ITickable,IInitializable,IDisposable,IPlayer
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

    private async void HandleAttack()
    {
        
        if (_isShildUp | _isAttacking) return;
        _isAttacking = true;
        _player.EnableWeaponCollider(true);
        _player.PlayerAnimator.SetTrigger("FirstAttack");
        var animationClip = _player.PlayerAnimator.GetCurrentAnimatorClipInfo(1)[0].clip;
        await Task.Delay(TimeSpan.FromSeconds(animationClip.length * _playerModel.AttackSpeed));
        
        _player.EnableWeaponCollider(false);
        _isAttacking = false;
        _player.WeaponController.EnemyTarget.Clear();
    }

    private void HandleBlock()
    {
        if (_isAttacking) return;
        
    }

    private void PlayerMove(Vector3 move)
    {

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
        _heroHealthBar.fillAmount = _player.GetCurrentHealth() / _scriptableObjectService.PlayerConfig.GetHeroValue().MaxHealth;
        
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
        _heroHealthBar.fillAmount = _player.GetCurrentHealth() / _scriptableObjectService.PlayerConfig.GetHeroValue().MaxHealth;
        _onMove = _playerInput.Player.Move;
        _playerInput.Player.Fire.performed += OnAttack;
        _playerInput.Player.Block.performed += OnBlock;
        _playerInput.Player.Enable();
        
    }

    public void Dispose()
    {
        _onMove?.Disable();
        _playerInput.Player.Fire.performed -= OnAttack;
        _playerInput.Player.Block.performed -= OnBlock;
        _playerInput.Player.Fire.Disable();
        _playerInput.Player.Block.Disable();
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
        HandleAttack();

    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        HandleBlock();
    }

    

}
