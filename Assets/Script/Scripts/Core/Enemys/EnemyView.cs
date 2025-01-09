using UnityEngine;
using UnityEngine.AI;


public class EnemyView : MonoBehaviour
{
    [SerializeField] private EnemyTypes _enemyType;
    [SerializeField] private GameObject _enemyWeapon;
    [SerializeField] private Collider _weponCollider;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _dieEffect;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    private Material[] materials;
    
    public GameObject EnemyWeapon { get => _enemyWeapon; set => _enemyWeapon = value; }
    public Collider WeponCollider { get => _weponCollider; set => _weponCollider = value; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Animator Animator { get => _animator; set => _animator = value; }
    public EnemyTypes EnemyType { get => _enemyType; set => _enemyType = value; }
    public SkinnedMeshRenderer EnemyFaceMesh { get => _skinnedMeshRenderer; set => _skinnedMeshRenderer = value; }
    public ParticleSystem DieEffect { get => _dieEffect; set => _dieEffect = value; }

    private void Start()
    {
        materials = _skinnedMeshRenderer.materials;
    }

    public void SetEnemyFase(Material material)
    {
        materials[1] = material;
        _skinnedMeshRenderer.materials = materials;
        
    }
}
