using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HealthPotionConfig", menuName = "ScriptableObject/HealthPotionConfig", order = 0)]
public class HealthPotionConfig : ScriptableObject
{
    [SerializeField] private List<HealthPotionEditor> healthPotions;

    
    
    public HealthPotionEditor GetPotion(HealthPotionType healthPotionType)
    {
        foreach(var value in healthPotions)
        {
            if(healthPotionType == value.Type)
            {
                return value;
            }
        }
        return null;
    }

}
