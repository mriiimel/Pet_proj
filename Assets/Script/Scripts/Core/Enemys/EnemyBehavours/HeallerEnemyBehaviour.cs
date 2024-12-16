using UnityEngine;
using UnityEngine.AI;

public class HeallerEnemyBehaviour : IEnemyBehaviour
{
    public void SetEnemyBehaviour(NavMeshAgent agent)
    {
        HeallerBehaviour(agent);
    }

    private void HeallerBehaviour(NavMeshAgent agent)
    {
        Debug.Log("Healler!");
    }
}

