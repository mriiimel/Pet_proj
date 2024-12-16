using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyControllerBase: MonoBehaviour,IEnemy
{
    [SerializeField] private NavMeshAgent m_Agent;
    [field: SerializeField] public EnemyTypes Type { get; private set; }

    IEnemyBehaviour m_EnemyBehaviour;

    //[Inject]
    //private void Construct(BossEnemyBehaviour enemyBehaviour)
    //{
    //    m_EnemyBehaviour = enemyBehaviour;
    //}
    protected void Test()
    {
        m_EnemyBehaviour.SetEnemyBehaviour(m_Agent);
    }
}

