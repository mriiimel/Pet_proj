using System;
using UnityEngine;

namespace Enemy_Config
{
    [Serializable]
    public class EnemysConfig
    {
        public EnemyTypes enemyTypes;
        public TypeOfAttack typeOfAttack;
        public  Enemy Enemys;


        [field: SerializeField] public int EnemyHealth { get; private set; }

        [field: SerializeField] public int EnemyAttack { get; private set; }

        [field: SerializeField] public float EnemyDefence { get; private set; }

        [field: SerializeField] public float EnemyAttackRange { get; private set; }

        [field: SerializeField] public float EnemySpeed { get; private set; }

        [field: SerializeField] public float EnemyAttackSpeed { get; private set; }


    }
}
