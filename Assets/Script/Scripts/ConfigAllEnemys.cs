using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Config
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObject/EnemyConfig", order = 0)]
    public class ConfigAllEnemys : ScriptableObject
    {
        [SerializeField]private List<EnemysConfig> EnemysConfigs;
        
        public EnemysConfig GetEnemyWithType(EnemyTypes enemyTypes)
        {
            
            for (int i = 0; i < EnemysConfigs.Count; i++)
            {
                var obj = EnemysConfigs[i];
                if (enemyTypes == obj.enemyTypes)
                    return obj;
                
            }
            return null;
        }
    }
}