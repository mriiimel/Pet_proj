using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyModel
{
    private readonly Enemy _enemy;
    private readonly NavMeshAgent _agent;
    private Image _healthBar;
    private CameraView _cameraView;
    private GameObject _enemyWeapon;
    private Animator _animator;
    private Transform _point;
    private PlayerView _playerView;

    public EnemyModel(Enemy enemy, CameraView cameraView, PlayerView playerView)
    {
        _enemy = enemy;
        _agent = enemy.Agent;
        _healthBar = enemy.EnemyHealthBar;
        _cameraView = cameraView;
        _enemyWeapon = enemy.EnemyWeapon;
        _animator = enemy.Animator;
        _point = enemy.Point;
        _playerView = playerView;
    }

    public Enemy Enemy => _enemy;
    public NavMeshAgent Agent => _agent;
    public Image EnemyHealthBar { get => _healthBar; set => _healthBar = value; }
    public CameraView CameraViews => _cameraView;
    public GameObject EnemyWeapon => _enemyWeapon;
    public Animator Animator => _animator;

    public Transform Point => _point;

    public PlayerView PlayerView => _playerView;
}

