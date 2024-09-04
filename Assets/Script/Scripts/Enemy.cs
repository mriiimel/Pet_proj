using UnityEngine;


public class Enemy : EnemyBase,IEnemy
{
    [field:SerializeField] public EnemyTypes Type { get; private set; }
    

}
