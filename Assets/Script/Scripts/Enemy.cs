using UnityEngine;


public class Enemy : MonoBehaviour,IEnemy
{
    [field:SerializeField] public EnemyTypes Type { get; private set; }
    

}
