using UnityEngine;

public class HealthPotionFactory 
{
    private ScriptableObjectService _scriptableObjectService;
    public HealthPotionFactory(ScriptableObjectService scriptableObjectService)
    {
        _scriptableObjectService = scriptableObjectService;
    }


    public GameObject CreateHealthPotion()
    {
        foreach(var value in _scriptableObjectService.PotionConfig.GetPotionValue())
        {
            return value.potion.gameObject;
        }
        return null;
    }
}
