using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HealthPotionConfig", menuName = "ScriptableObject/HealthPotionConfig", order = 0)]
public class HealthPotionConfig : ScriptableObject
{
    [SerializeField] private List<HealthPotionEditor> healthPotions;

    public List<HealthPotionEditor> GetPotionValue()
    {
        return healthPotions;
    }
}
