using UnityEngine;




public class EnemyController: MonoBehaviour
{
    private EnemyView _enemyView;
    private PlayerView _playerView;
    private EnemyModel _enemyModel;
    private UiView _uiView;
    private Camera _camera;
    private GameObject _enemyHealthBar;

    
    public void Construct(EnemyView enemyView,PlayerView playerView, EnemyModel enemyModel, UiView uIView, Camera camera,
        GameObject enemyHealthBar)
        
    {
        _enemyView = enemyView;
        _playerView = playerView;
        _enemyModel = enemyModel;
        _uiView = uIView;
        _camera = camera;
        _enemyHealthBar = enemyHealthBar;
    }

    public void EnemyMoving()
    {
        
        var distanceToHero = Vector3.Distance(_enemyView.transform.position, _playerView.transform.position);
        if (_enemyView.Agent.stoppingDistance <= distanceToHero)
        {
            _enemyView.Agent.isStopped = false;
            _enemyView.Agent.SetDestination(_playerView.transform.position);
        }
        else
        {
            _enemyView.Agent.isStopped = true;
        }

    }
    private void Start()
    {
        UbdateEnemyHealhBar();


    }
    private void Update()
    {
        EnemyMoving();
        UbdateEnemyHealhBar();
    }

    public void UbdateEnemyHealhBar()
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

}

