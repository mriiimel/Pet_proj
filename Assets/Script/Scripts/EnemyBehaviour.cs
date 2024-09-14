using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour 
{
    public void SetEnemyBehaviour(EnemyTypeBehaviour typeBehaviour,NavMeshAgent agent)
    {
        switch (typeBehaviour)
        {
            case EnemyTypeBehaviour.Simple:
                SetSimpleBehaviour(agent);
                break;
            case EnemyTypeBehaviour.Warrior:
                SetWarriorBehaviour(agent);
                break;
            case EnemyTypeBehaviour.Healler:
                SetHellerBehaviour(agent);
                break;
            case EnemyTypeBehaviour.Debuffer:
                SetDebufferBehaviour(agent);
                break;
            case EnemyTypeBehaviour.Berserk:
                SetBerserkBehaviour(agent);
                break;
            case EnemyTypeBehaviour.Boss:
                SetBossBehaviour(agent);
                break;
        }
    }

    private void SetBossBehaviour(NavMeshAgent agent)
    {
        throw new NotImplementedException();
    }

    private void SetBerserkBehaviour(NavMeshAgent agent)
    {
        throw new NotImplementedException();
    }

    private void SetDebufferBehaviour(NavMeshAgent agent)
    {
        throw new NotImplementedException();
    }

    private void SetHellerBehaviour(NavMeshAgent agent)
    {
        throw new NotImplementedException();
    }

    private void SetWarriorBehaviour(NavMeshAgent agent)
    {
        throw new NotImplementedException();
    }

    private void SetSimpleBehaviour(NavMeshAgent agent)
    {
        throw new NotImplementedException();
    }
}
