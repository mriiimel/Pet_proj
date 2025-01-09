using UnityEngine;
using Zenject;

public class EnemyWeaponController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _damageEffect;
    
    private PlayerController _playerController;
    private ScriptableObjectService _scriptableObjectService;
    private EnemyView _enemyView;

    [Inject]
    public void Construct(PlayerController playerController,ScriptableObjectService scriptableObjectService,EnemyView enemyView)
    {
        _playerController = playerController;
        _scriptableObjectService = scriptableObjectService;
        _enemyView = enemyView;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _playerController.GetDamage(_scriptableObjectService.EnemyConfig.GetEnemy(_enemyView.EnemyType).EnemyAttack);
            _damageEffect.Play();
            _damageEffect.transform.position = other.gameObject.transform.position;
        }
    }
    
    
}
