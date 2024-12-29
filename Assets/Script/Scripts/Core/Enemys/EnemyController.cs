using Enemy_Config;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;




public class EnemyController: MonoBehaviour
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

    public static event Action EnemyIsDead;

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

    public void IsDead()
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
    

    private void Start()
    {
        UbdateEnemyHealhBar();
        _enemyHealth = _enemyHealthBar.GetComponent<Image>();
        _currentHealth = _configAllEnemys.GetEnemy(_enemyView.EnemyType).EnemyHealth;
        

    }
    private void Update()
    {
        EnemyMoving();
        UbdateEnemyHealhBar();
        _enemyHealth.fillAmount = _currentHealth/ _configAllEnemys.GetEnemy(_enemyView.EnemyType).EnemyHealth;
    }

    private void UbdateEnemyHealhBar()
    {
        var screenPos = _camera.WorldToScreenPoint(_enemyView.transform.position + Vector3.up * 2);


        _enemyHealthBar.transform.position = screenPos;
        if (screenPos.z < 0)
        {
            _enemyHealthBar.gameObject.SetActive(false);
        }
        else
        {
            _enemyHealthBar.gameObject.SetActive(true);
        }


    }

    private void EnemyMoving()
    {
        if (_enemyModel.IsDead) return;
        var distanceToHero = Vector3.Distance(_enemyView.transform.position, _playerView.transform.position);
        if (_enemyView.Agent.stoppingDistance <= distanceToHero)
        {
            _enemyView.Agent.isStopped = false;
            _enemyView.Agent.SetDestination(_playerView.transform.position);
            Vector3 directionToPlayer = (_playerView.transform.position - _enemyView.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            _enemyView.transform.rotation =
                Quaternion.Lerp(_enemyView.transform.rotation,
                targetRotation, _configAllEnemys.GetEnemy(_enemyView.EnemyType).EnemyRotationSpeed * Time.fixedDeltaTime);
            _enemyView.Animator.SetFloat("Move", 1F);
            _enemyView.Animator.SetBool("Attack", false);
            _enemyView.WeponCollider.enabled = false;
        }
        else
        {
            _enemyView.Animator.SetFloat("Move", 0F);
            _enemyView.Animator.SetBool("Attack",true);
            _enemyView.WeponCollider.enabled = true;
            _enemyView.Agent.isStopped = true;
        }

    }

    
    
    public void GetDamage(float damage)
    {
        _enemyView.GetDamageEffect.Play();
        _currentHealth -= damage;
        
        
        if (_currentHealth <= 0)
        {
            _enemyView.Animator.SetBool("Dead", true);
            //IsDead();

        }
    }


}

