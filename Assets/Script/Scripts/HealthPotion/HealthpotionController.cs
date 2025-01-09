using UnityEngine;
using Zenject;




public class HealthPotionController:MonoBehaviour
{
    
    private PlayerController _playerController;
    private ScriptableObjectService _scriptableObjectService;
    

    [Inject]
    public void Construct(PlayerController playerController, ScriptableObjectService scriptableObjectService)
    {
        
        _playerController = playerController;
        _scriptableObjectService = scriptableObjectService;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController.RecoverHealth(_scriptableObjectService.PotionConfig.GetPotion(gameObject.GetComponent<HealthPotion>().PotionType).HealthRecover);
            var healthPotion = gameObject.GetComponent<HealthPotion>();
            healthPotion.Animator.SetBool("PickUp",true);
        }
    }

    

}
