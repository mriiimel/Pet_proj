using System;
using UnityEngine;


[Serializable]
public class HealthPotionEditor 
{
    public HealthPotionType Type;
    public HealthPotion potion;
    [field: SerializeField] public int HealthRecover { get; private set; }
}
