using UnityEngine;
using UnityEngine.AI;

public class WarriorEnemyBehaviour : IEnemyBehaviour
{
    public void SetEnemyBehaviour(NavMeshAgent agent)
    {
        WarriorBehaviour(agent);
    }

    private void WarriorBehaviour(NavMeshAgent agent)
    {
        Debug.Log("Warrior!");
    }
}

