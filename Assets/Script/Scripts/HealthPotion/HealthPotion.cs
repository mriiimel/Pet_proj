using UnityEngine;


public class HealthPotion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _pickUpEffect;
    [SerializeField] private Animator _animator;
    [field: SerializeField] public HealthPotionType PotionType { get; private set; }
    public Animator Animator { get => _animator; set => _animator = value; }

    private void Start()
    {
        _pickUpEffect.Stop();
    }

    private void ActivateEffect()
    {
        _pickUpEffect.Play();
    }

    private void EndAnimation()
    {
        _animator.SetBool("PickUp", false);
        _pickUpEffect.Stop();
        gameObject.SetActive(false);
    }
}
