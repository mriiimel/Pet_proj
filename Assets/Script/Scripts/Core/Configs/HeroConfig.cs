using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroConfig", menuName = "ScriptableObject/HeroConfig", order = 0)]
public class HeroConfig : ScriptableObject
{
    [SerializeField]private List<HeroStatValue> heroConfigs; 
    

    public HeroStatValue GetHeroValue()
    {
        foreach (var hero in heroConfigs) 
        {
            return hero;
        }
        return null;
    }
}
