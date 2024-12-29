using System;
using UnityEngine;

[Serializable]
public class HeroWeapons 
{
    public WeaponType weaponType;
    //public HeroWeaponController weaponController;

    [field: SerializeField] public float WeaponBaseDamage { get; private set; }
    [field: SerializeField] public float NotPhisicalDamage { get; private set; }

}
