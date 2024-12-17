using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Zenject;

public class EnemyController: IInitializable,ITickable,IDisposable
{
    private Enemy _enemy;
    private EnemyModel _enemyModel;
    private Image _healthBar;
    private NavMeshAgent _navMeshAgent;
    private CameraView _cameraView;

    public EnemyController(EnemyModel enemyModel)
    {
        _enemy = enemyModel.Enemy;
        _enemyModel = enemyModel;
        _healthBar = enemyModel.EnemyHealthBar;
        _navMeshAgent = enemyModel.Agent;
        _cameraView = enemyModel.CameraViews;
    }

    private void UbdateEnemyHealhBar()
    {
        Camera camera = _cameraView.Camera;
        Vector3 screenPos = camera.WorldToScreenPoint(_enemy.transform.position + Vector3.up * 2);
        _healthBar.transform.position = screenPos;
        if(screenPos.z < 0)
        {
            _healthBar.gameObject.SetActive(false);
        }
        else
        {
            _healthBar.gameObject.SetActive(true);
        }
    }

    public void Dispose()
    {
        
    }

    public void Initialize()
    {
        UbdateEnemyHealhBar();
        _healthBar.fillAmount = 1000;
    }

    public void Tick()
    {
        UbdateEnemyHealhBar();
    }
}

