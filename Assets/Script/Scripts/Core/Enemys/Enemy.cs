using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private Image _enemyHealthBar;
    [field: SerializeField] public EnemyTypes Type { get; private set; }
    public Image EnemyHealthBar { get => _enemyHealthBar; set => _enemyHealthBar = value; }
    public NavMeshAgent Agent { get => m_Agent; set => m_Agent = value; }

    private void Start()
    {
        
    }
    

}
