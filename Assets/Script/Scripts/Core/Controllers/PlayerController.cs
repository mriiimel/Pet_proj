using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Random = UnityEngine.Random;


public class PlayerController: ITickable,IInitializable,IDisposable,IPlayer
{
    private PlayerView _player;
    private PlayerModel _playerModel;
    private ScriptableObjectService _scriptableObjectService;
    private Camera _camera;
    private PlayerInput _playerInput;
    private InputAction _onMove;
    private Vector3 m_MoveDirection;
    private IHeailhBehaviour _heailhBehaviour;
    private float _attackSpeedMultiplier;
    private int _currentHealth;
    #region Flags
    private bool _isAttacking = false;
    private bool _isShildUp = false;
    #endregion


    public PlayerController(PlayerView playerView,PlayerModel playerModel,PlayerInput inputActions)
    {
        _player = playerView;
        _playerModel = playerModel;
        _scriptableObjectService = playerModel.ScriptableObjectService;
        _camera = playerModel.CameraView;
        _playerInput = inputActions;
        _attackSpeedMultiplier = playerModel.AttackSpeed;
        
    }


    public void IncreaseAttackSpeed(float amount)
    {
        _attackSpeedMultiplier += amount;
    }

    public void DecreaseAttackSpeed(float amount)
    {
        _attackSpeedMultiplier = Mathf.Max(0.1f, _attackSpeedMultiplier - amount); // чтобы скорость не была отрицательной
    }

    private async void HandleAttack()
    {
        if(_isAttacking || _isShildUp) return;
        _isAttacking = true;
        _player.EnableWeaponCollider(true);
        var crit = Random.Range(0.0F, 100.0F);

        string animationName = crit <= _playerModel.PlayerCritChance ? "Attack02" : "Attack01";

        _player.PlayerAnimator.Play(animationName);

        var animationClip = _player.PlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;
        await Task.Delay(TimeSpan.FromSeconds(animationClip.length * _attackSpeedMultiplier));

        _isAttacking = false;
        _player.EnableWeaponCollider(false);
    }

    private async void HandleBlock()
    {
        if (_isAttacking) return;
        _isShildUp = true;
        _player.EnableShealdCollider(true);

        _player.PlayerAnimator.Play("Defend");
        var clip = _player.PlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;
        await Task.Delay(TimeSpan.FromSeconds(clip.length / 2));

        await Task.Delay(TimeSpan.FromSeconds(clip.length));
        _player.EnableShealdCollider(false);
        _isShildUp = false;
    }
    
    public void OnCollisionWithEnemy(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Pseudopod")) return;
        var obj = collision.gameObject.GetComponentInParent<EnemyView>().EnemyType;
        _currentHealth = 
            _heailhBehaviour.RemoveHealth
            (_scriptableObjectService.EnemyConfig.GetEnemy(obj).EnemyAttack,
            _currentHealth, _playerModel.PlayerMaxHealth);
    }
    

    
    public void OnTriggerMethod(Collider collider)
    {
        if (!collider.gameObject.CompareTag("HealthPotion")) return;
        var obj = collider.gameObject.GetComponent<HealthPotion>().PotionType;
        _currentHealth = _heailhBehaviour.
            AddHealth(_scriptableObjectService.PotionConfig.GetPotionValue(obj).HealthRecover, _currentHealth, _playerModel.PlayerMaxHealth);
    }
    
    public void Tick()
    {
        m_MoveDirection = _onMove.ReadValue<Vector2>();
        PlayerMove(m_MoveDirection);

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

    public void Initialize()
    {
        
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
