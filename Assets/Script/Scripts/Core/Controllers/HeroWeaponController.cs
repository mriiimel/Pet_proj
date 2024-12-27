using System.Collections.Generic;
using UnityEngine;

public class HeroWeaponController : MonoBehaviour
{
    

    private List<GameObject> _enemyTarget = new();
    
    public List<GameObject> EnemyTarget { get => _enemyTarget; set => _enemyTarget = value; }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" & !_enemyTarget.Contains(collision.gameObject))
        {
            
            _enemyTarget.Add(collision.gameObject);
            collision.gameObject.GetComponent<EnemyController>().TakeDamage();
        }
    }
    

}
