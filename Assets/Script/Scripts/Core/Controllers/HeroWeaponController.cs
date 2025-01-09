using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HeroWeaponController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _damageEffect;
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
        if(collision.gameObject.CompareTag("Enemy") & !_enemyTarget.Contains(collision.gameObject) & !_damageIsApply)
        {
            Vector3 HitTransform = collision.ClosestPoint(transform.position);
            _damageEffect.transform.position = HitTransform;
            _damageEffect.Play();
            _damageIsApply = true;
            _enemyTarget.Add(collision.gameObject);
            collision.gameObject.GetComponent<EnemyController>().GetDamage(_service.HeroWeaponConfig.GetWeaponValue(_weaponType).WeaponBaseDamage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _damageIsApply = false;
        }
    }

}
