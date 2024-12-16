using UnityEngine;
using UnityEngine.AI;

public class DebufferEnemyBehaviour : IEnemyBehaviour
{
    public void SetEnemyBehaviour(NavMeshAgent agent)
    {
        DebufferBehaviour(agent);
    }

    private void DebufferBehaviour(NavMeshAgent agent)
    {
        Debug.Log("Debuffer!");
    }
}

