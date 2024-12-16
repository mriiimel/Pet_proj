using Enemy_Config;
using UnityEngine;
using Zenject;



public class PlayerView : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private Collider _shealdCollider;
    [SerializeField] private Animator _playerAnimator;

    
    public Rigidbody PlayerRb { get => _playerRb; private set => _playerRb = value; }
    public Collider WeaponCollider { get => _weaponCollider; private set => _weaponCollider = value; }
    public Collider ShealdCollider { get => _shealdCollider; private set => _shealdCollider = value; }
    public Animator PlayerAnimator { get => _playerAnimator; private set => _playerAnimator = value; }
    
}
