using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private EnemyTypes _enemyType;
    [SerializeField] private GameObject _enemyWeapon;
    [SerializeField] private Collider _weponCollider;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    
    
    
    public GameObject EnemyWeapon { get => _enemyWeapon; set => _enemyWeapon = value; }
    public Collider WeponCollider { get => _weponCollider; set => _weponCollider = value; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Animator Animator { get => _animator; set => _animator = value; }
    public EnemyTypes EnemyType { get => _enemyType; set => _enemyType = value; }
    
}
