using Enemy_Config;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AIControllerBase : MonoBehaviour ,IEnemy,IMoveable,IAttakable
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private ConfigAllEnemys _enemyCinfig;
    [SerializeField] private Collider _collider;
    
    [Inject] private AnimatorController _animatorController;
    
    

    public void Attack()
    {
        _animatorController.PlayAnimation(_animator,"Attac");
    }

    public void OnMove()
    {
        _animatorController.PlayAnimation(_animator, "Move");
    }
}
