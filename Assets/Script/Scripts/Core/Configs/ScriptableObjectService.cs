using Enemy_Config;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigsService",menuName = "ScriptableObject/ConfigsService",order = 0)]
public class ScriptableObjectService : ScriptableObject
{
    public ConfigAllEnemys EnemyConfig;
    public HeroConfig PlayerConfig;
    public HealthPotionConfig PotionConfig;
    
}
