using System;
using System.Collections;
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
    [SerializeField] private ParticleSystem _healthRecoverEffect;
    [SerializeField] private float _healthRecoverBuffDuration;
    private ScriptableObjectService _scriptableObjecService;
    private float _currentHealth = 0;
    

    public Rigidbody PlayerRb { get => _playerRb; private set => _playerRb = value; }
    public Collider WeaponCollider { get => _weaponCollider; private set => _weaponCollider = value; }
    public Collider ShealdCollider { get => _shealdCollider; private set => _shealdCollider = value; }
    public Animator PlayerAnimator { get => _playerAnimator; private set => _playerAnimator = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public List<GameObject> EnemyTarget { get; set; } = new();
    public HeroWeaponController WeaponController { get => _weaponController; set => _weaponController = value; }
    public ParticleSystem HealthRecoverEffect { get => _healthRecoverEffect; set => _healthRecoverEffect = value; }

    [Inject]
    private void Construct(ScriptableObjectService scriptableObjectService)
    {
        _scriptableObjecService = scriptableObjectService;
    }

    public void EnableWeaponCollider()
    {
        _weaponCollider.enabled = true;
    }
    public void DisableWeaponCollider()
    {
        _weaponCollider.enabled = false;
    }

    public void EnableShealdCollider(bool IsEnable)
    {
        _shealdCollider.enabled = IsEnable;
    }
    private IEnumerator ActivateRecoverHealthEffect()
    {
        HealthRecoverEffect.Play();
        yield return new WaitForSeconds(_healthRecoverBuffDuration);
        HealthRecoverEffect.Stop();
        yield break;
    }
    private void Start()
    {
        _healthRecoverEffect.Stop();
        PlayerController.HealthIsRecover += ActivateHealthRecoverBuff;
        _currentHealth = _scriptableObjecService.PlayerConfig.GetHeroValue().MaxHealth;
    }
    private void ActivateHealthRecoverBuff()
    {
        StartCoroutine(ActivateRecoverHealthEffect());
    }
    private void OnDisable()
    {
        PlayerController.HealthIsRecover -= ActivateHealthRecoverBuff;
    }
}
