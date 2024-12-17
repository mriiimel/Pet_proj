using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
public class EnemyController: IInitializable,ITickable,IDisposable
{
    private Enemy _enemy;
    private EnemyModel _enemyModel;
    private Image _healthBar;
    private NavMeshAgent _navMeshAgent;
    private CameraView _cameraView;
    private GameObject _enemyWeapon;
    private Transform _point;
    private Animator _animator;
    private PlayerView _playerView;
    public EnemyController(EnemyModel enemyModel)
    {
        _enemy = enemyModel.Enemy;
        _enemyModel = enemyModel;
        _healthBar = enemyModel.EnemyHealthBar;
        _navMeshAgent = enemyModel.Agent;
        _cameraView = enemyModel.CameraViews;
        _enemyWeapon = enemyModel.EnemyWeapon;
        _point = enemyModel.Point;
        _animator = enemyModel.Animator;
        _playerView = enemyModel.PlayerView;
    }

    

    public void Initialize()
    {
        _animator.SetBool("Attack", true);
        UbdateEnemyHealhBar();
        
    }

    public void Tick()
    {

        EnemyMoving();
        UbdateEnemyHealhBar();
    }

    private void UbdateEnemyHealhBar()
    {
        Camera camera = _cameraView.Camera;
        Vector3 screenPos = camera.WorldToScreenPoint(_enemy.transform.position + Vector3.up * 2);
        _healthBar.transform.position = screenPos;
        if (screenPos.z < 0)
        {
            _healthBar.gameObject.SetActive(false);
        }
        else
        {
            _healthBar.gameObject.SetActive(true);
        }
    }

    private void EnemyMoving()
    {
        var distanceToHero = Vector3.Distance(this._enemy.transform.position, _playerView.transform.position);
        if(_navMeshAgent.stoppingDistance <= distanceToHero)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_playerView.transform.position);
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }
        
    }
    public void Dispose()
    {

    }
}

