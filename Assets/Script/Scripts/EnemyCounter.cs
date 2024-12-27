using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "EnemyCounters", menuName = "ScriptableObject/EnemyCouter", order = 0)]
public class EnemyCounter : ScriptableObject
{
    [SerializeField] private List<Counter> _counter;

    public List<Counter> Counter { get => _counter; set => _counter = value; }
}
