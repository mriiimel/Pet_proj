using Enemy_Config;
using System;
using UnityEngine;
using UnityEngine.UI;




public class EnemyController: MonoBehaviour//,IDisposable
{
    private EnemyView _enemyView;
    private PlayerView _playerView;
    private EnemyModel _enemyModel;
    private Image _enemyHealth;
    private Camera _camera;
    private GameObject _enemyHealthBar;
    private ConfigAllEnemys _configAllEnemys;
    private HeroConfig _heroConfig;
    private ObjectPool _pool;
    
    private float _currentHealth;
    private Vector3 _playerDirection;
    

    public static event Action EnemyIsDead;

    private bool _playerIsDead = false;

    public void Construct(EnemyView enemyView,PlayerView playerView, EnemyModel enemyModel,  Camera camera,
        GameObject enemyHealthBar,ObjectPool objectPool)
        
    {
        _enemyView = enemyView;
        _playerView = playerView;
        _enemyModel = enemyModel;
        _camera = camera;
        _enemyHealthBar = enemyHealthBar;
        _configAllEnemys = enemyModel.ScriptableObjectService.EnemyConfig;
        _heroConfig = enemyModel.ScriptableObjectService.PlayerConfig;
        _pool = objectPool;
    }


    private void Start()
    {
        _enemyView.DieEffect.Stop();
        UbdateEnemyHealhBar();
        _enemyHealth = _enemyHealthBar.GetComponent<Image>();
        _currentHealth = _configAllEnemys.GetEnemy(_enemyView.EnemyType).EnemyHealth;
        _enemyView.Agent.stoppingDistance = _configAllEnemys.GetEnemy(_enemyView.EnemyType).EnemyAttackRange;
        PlayerController.PlayerIsDead += PlayerIsDead;

    }
    private void Update()
    {
        
        EnemyMoving();
        UbdateEnemyHealhBar();
        _enemyHealth.fillAmount = _currentHealth / _configAllEnemys.GetEnemy(_enemyView.EnemyType).EnemyHealth;
    }


    private void IsDead()
    {
        
        ReturnToPool();
    }


    private void ReturnToPool()
    {
        _enemyHealthBar.gameObject.SetActive(false);
        
        _currentHealth = _enemyModel.ScriptableObjectService.EnemyConfig.GetEnemy(_enemyView.EnemyType).EnemyHealth;
        
        _pool.RturnToPool(gameObject);
        EnemyIsDead?.Invoke();
    }
    

    private void UbdateEnemyHealhBar()
    {
        var screenPos = _camera.WorldToScreenPoint(_enemyView.transform.position + Vector3.up * 2);


        _enemyHealthBar.transform.position = screenPos;
        if (screenPos.z < 0)
        {
            _enemyHealthBar.SetActive(false);
        }
        else
        {
            _enemyHealthBar.SetActive(true);
        }


    }
    private void PlayerIsDead()
    {
        _playerIsDead = true;
        
    }
    
    private void EnemyMoving()
    {
        if (_currentHealth > 0) _enemyModel.IsDead = false;
        if (_enemyModel.IsDead) return;
        var distanceToHero = Vector3.Distance(_enemyView.transform.position, _playerView.transform.position);
        _enemyView.SetEnemyFase(_enemyModel.ScriptableObjectService.EnemyFaces._idleFace);
        if (_playerIsDead == true)
        {
            _enemyView.Animator.SetBool("Attack", false);
            _enemyView.Animator.SetFloat("Move", 0F);
        }
        else 
        {
            if (distanceToHero <= _configAllEnemys.GetEnemy(_enemyView.EnemyType).RadiusOfVisibility)
            {

                EnemyRotation();
                if (_enemyView.Agent.stoppingDistance <= distanceToHero)
                {

                    EnemyRotation();
                    _enemyView.SetEnemyFase(_enemyModel.ScriptableObjectService.EnemyFaces._pockerFace);
                    _enemyView.Agent.isStopped = false;
                    _enemyView.Agent.SetDestination(_playerView.transform.position);
                    _enemyView.Animator.SetFloat("Move", 1F);
                    _enemyView.Animator.SetBool("Attack", false);
                    _enemyView.WeponCollider.enabled = false;
                }
                else
                {

                    EnemyRotation();
                    _enemyView.SetEnemyFase(_enemyModel.ScriptableObjectService.EnemyFaces._attackFace);

                    _enemyView.Animator.SetFloat("Move", 0F);
                    _enemyView.Animator.SetBool("Attack", true);
                    _enemyView.WeponCollider.enabled = true;
                    _enemyView.Agent.isStopped = true;
                }
            }
        }

        

    }

    
    private void EnemyRotation()
    {
        Vector3 _playerDirection = (_playerView.transform.position - _enemyView.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(_playerDirection);
        _enemyView.transform.rotation =
            Quaternion.Lerp(_enemyView.transform.rotation,
            targetRotation, _configAllEnemys.GetEnemy(_enemyView.EnemyType).EnemyRotationSpeed * Time.fixedDeltaTime);
        
    }
    
    public void GetDamage(float damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        {
            _enemyView.DieEffect.Play();
            _enemyModel.IsDead = true;
            _enemyView.SetEnemyFase(_enemyModel.ScriptableObjectService.EnemyFaces._deadFace);
            _enemyView.Animator.SetBool("Dead", true);
            PlayerController.PlayerIsDead -= PlayerIsDead;
        }
        
    }

    
}

