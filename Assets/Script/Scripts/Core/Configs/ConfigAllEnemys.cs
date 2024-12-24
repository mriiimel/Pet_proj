using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Config
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObject/EnemyConfig", order = 0)]
    public class ConfigAllEnemys : ScriptableObject
    {
        [SerializeField]private List<EnemysConfig> EnemysConfigs;

        public List<EnemysConfig> EnemysConfigsList { get => EnemysConfigs; }

        public EnemysConfig GetEnemy(EnemyTypes enemyTypes)
        {
            
            for (int i = 0; i < EnemysConfigs.Count; i++)
            {
                var obj = EnemysConfigs[i];
                if(obj.enemyTypes == enemyTypes)
                {
                    return obj;
                }
                
            }
            return null;
        }
    }
}