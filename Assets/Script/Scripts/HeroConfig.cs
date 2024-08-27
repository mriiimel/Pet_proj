using UnityEngine;

[CreateAssetMenu(fileName = "HeroConfig", menuName = "ScriptableObject/HeroConfig", order = 0)]
public class HeroConfig : ScriptableObject
{
    [field: SerializeField] public int MaxHealh { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float CritChance { get; private set; }

    public void SetHeroSpeed(float value)
    {
        Speed = value;
    }

    public void SetHeroCritChance(float value)
    {
        CritChance = value;
    }
}
