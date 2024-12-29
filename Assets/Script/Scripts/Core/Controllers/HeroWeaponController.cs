using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HeroWeaponController : MonoBehaviour
{
    public WeaponType _weaponType;
    private ScriptableObjectService _service;
    private List<GameObject> _enemyTarget = new();
    private bool _damageIsApply = false;
    public List<GameObject> EnemyTarget { get => _enemyTarget; set => _enemyTarget = value; }
    
    
    [Inject]
    public void Construct(ScriptableObjectService scriptableObjectService)
    {
        _service = scriptableObjectService;
    }

   

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy" & !_enemyTarget.Contains(collision.gameObject) & !_damageIsApply)
        {
            
            _damageIsApply = true;
            _enemyTarget.Add(collision.gameObject);
            collision.gameObject.GetComponent<EnemyController>().GetDamage(_service.HeroWeaponConfig.GetWeaponValue(_weaponType).WeaponBaseDamage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _damageIsApply = false;
        }
    }

}
