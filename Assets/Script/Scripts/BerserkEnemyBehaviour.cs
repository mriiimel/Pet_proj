using UnityEngine;
using UnityEngine.AI;

public class BerserkEnemyBehaviour : IEnemyBehaviour
{
    public void SetEnemyBehaviour(NavMeshAgent agent)
    {
        BerserkBehaviour(agent);
    }

    private void BerserkBehaviour(NavMeshAgent agent)
    {
        Debug.Log("Berserk!");
    }
}

