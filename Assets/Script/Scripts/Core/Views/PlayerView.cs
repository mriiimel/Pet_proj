using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class PlayerView : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private Collider _shealdCollider;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private HeroWeaponController _weaponController;

    private ScriptableObjectService _scriptableObjecService;
    private float _currentHealth = 0;
    private bool _damageIsApplay = false;

    public Rigidbody PlayerRb { get => _playerRb; private set => _playerRb = value; }
    public Collider WeaponCollider { get => _weaponCollider; private set => _weaponCollider = value; }
    public Collider ShealdCollider { get => _shealdCollider; private set => _shealdCollider = value; }
    public Animator PlayerAnimator { get => _playerAnimator; private set => _playerAnimator = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public List<GameObject> EnemyTarget { get; set; } = new();
    public HeroWeaponController WeaponController { get => _weaponController; set => _weaponController = value; }

    [Inject]
    private void Construct(ScriptableObjectService scriptableObjectService)
    {
        _scriptableObjecService = scriptableObjectService;
    }

    public void EnableWeaponCollider(bool IsEnabled)
    {
        _weaponCollider.enabled = IsEnabled;
    }

    public void EnableShealdCollider(bool IsEnabled)
    {
        _shealdCollider.enabled = IsEnabled;
    }

    private void Start()
    {
        _currentHealth = _scriptableObjecService.PlayerConfig.GetHeroValue().MaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pseudopod" & !_damageIsApplay)
        {
            _damageIsApplay = true;
            var enemyTipe = other.gameObject.GetComponentInParent<EnemyView>().EnemyType;
            var enemyDamage = _scriptableObjecService.EnemyConfig.GetEnemy(enemyTipe).EnemyAttack;
            _currentHealth -= enemyDamage;

            if (_currentHealth <= 0)
                _currentHealth = 0;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _damageIsApplay = false;
    }


    public ref float GetCurrentHealth()
    {
        return ref _currentHealth;
    }
}
