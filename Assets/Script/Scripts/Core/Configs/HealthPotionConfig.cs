using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HealthPotionConfig", menuName = "ScriptableObject/HealthPotionConfig", order = 0)]
public class HealthPotionConfig : ScriptableObject
{
    [SerializeField] private List<HealthPotionEditor> healthPotions;

    public HealthPotionEditor GetPotionValue(HealthPotionType type)
    {
        for (int i = 0; i < healthPotions.Count; i++)
        {
            if (type == healthPotions[i].Type)
            {
                return healthPotions[i];
            }
        }
        return null;
    }
}
