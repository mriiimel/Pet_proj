using Enemy_Config;
using Object_Pool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Enemy_Factory
{
    public class FactoryModel
    {
        public Factory Factory { get; }
        public ObjectPool Pool { get; }
        public ConfigAllEnemys AllEnemysConfig { get; }
        public EnemyCounter EnemyCounters { get; }
        public UIController UiController { get; }
        public TextMeshProUGUI TotalEnemyCountText { get; }
        public int MaxEnemy { get; private set; }



        public FactoryModel(Factory factory, ObjectPool objectPool, ConfigAllEnemys configAllEnemys, 
            EnemyCounter enemyCounter, UIController uIController)
            
        {
            Factory = factory;
            Pool = objectPool;
            AllEnemysConfig = configAllEnemys;
            EnemyCounters = enemyCounter;
            UiController = uIController;
            TotalEnemyCountText = uIController.TotalEnemyText;
            MaxEnemy = 0;
        }

        
    }
}
