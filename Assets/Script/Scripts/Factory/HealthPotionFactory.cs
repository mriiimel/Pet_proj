using UnityEngine;
using Zenject;

public class HealthPotionFactory 
{
    private ScriptableObjectService _scriptableObjectService;
    private DiContainer _container;
    private HealthPotionActivator _healthPotionActivator;
    
    public HealthPotionFactory(DiContainer diContainer,HealthPotionActivator healthPotionActivator)
    {
        _scriptableObjectService = diContainer.Resolve<ScriptableObjectService>();
        _container = diContainer;
        _healthPotionActivator = healthPotionActivator;
        
    }


    public void InitHealthPotion(Transform BigPotionSpawn,Transform SmallPotionSpawn)
    {
        var bigPotion = _container.InstantiatePrefab(_scriptableObjectService.PotionConfig.GetPotion(HealthPotionType.BigPotion).potion,BigPotionSpawn.position,
            Quaternion.identity,null);
        bigPotion.SetActive(false);
        var smallPotion = _container.InstantiatePrefab(_scriptableObjectService.PotionConfig.GetPotion(HealthPotionType.SmallPotion).potion, SmallPotionSpawn.position,
            Quaternion.identity,null);
        smallPotion.SetActive(false);
        var random = Random.Range(0f, 2f);
        if (random < 1)
        {
            smallPotion.SetActive(true);
        }
        else
        {
            bigPotion.SetActive(true);
        }

        _healthPotionActivator.Construct(_scriptableObjectService,bigPotion,smallPotion);
    }


}
