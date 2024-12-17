using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyModel
{
    private readonly Enemy _enemy;
    private readonly NavMeshAgent _agent;
    private Image _healthBar;
    private CameraView _cameraView;

    public EnemyModel(Enemy enemy, CameraView cameraView)
    {
        _enemy = enemy;
        _agent = enemy.Agent;
        _healthBar = enemy.EnemyHealthBar;
        _cameraView = cameraView;
    }

    public Enemy Enemy => _enemy;

    public NavMeshAgent Agent => _agent;

    public Image EnemyHealthBar { get => _healthBar; set => _healthBar = value; }
    public CameraView CameraViews { get => _cameraView; set => _cameraView = value; }
}

