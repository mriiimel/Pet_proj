using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroWeaponConfigurator",menuName = "ScriptableObject/HeroWeaponConfiurator",order = 0)]
public class HeroWeaponConfig : ScriptableObject
{
    [SerializeField] private List<HeroWeapons> _heroWeapons = new();

    public HeroWeapons GetWeaponValue(WeaponType weaponType)
    {
        foreach (var weapon in _heroWeapons)
        {
            if(weaponType == weapon.weaponType)
            {
                return weapon;
            }
        }
        return null;
    }
}
