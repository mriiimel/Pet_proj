using UnityEngine;
using UnityEngine.AI;


public class SimpleEnemyBehaviour : IEnemyBehaviour
{
    
    public void SetEnemyBehaviour(NavMeshAgent agent)
    {
        SimpleBehaviour(agent);
    }

    private void SimpleBehaviour(NavMeshAgent agent)
    {
        Debug.Log("Simple!");
    }
}

