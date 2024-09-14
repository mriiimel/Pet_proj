using UnityEngine;
using UnityEngine.AI;

internal class BossEnemyBehaviour : IEnemyBehaviour
{
    public void SetEnemyBehaviour(NavMeshAgent agent)
    {
        BossBehaviour(agent);
    }

    private void BossBehaviour(NavMeshAgent agent)
    {
        Debug.Log("Boss!");
    }
}

