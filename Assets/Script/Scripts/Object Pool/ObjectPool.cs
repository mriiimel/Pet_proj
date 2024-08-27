using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enemy_Factory;
using ModestTree;



namespace Object_Pool
{
    public class ObjectPool:MonoBehaviour
    {
        [Inject]private FactoryBase _enemyFactoryBase;
        private Stack<GameObject> _pool;
        
        public ObjectPool()
        {
            _pool = new Stack<GameObject>();
        }
        
        public GameObject GetFromPool()
        {
            
            for(int i = 0;i<= _pool.Count; i++)
            {
                if(_pool.IsEmpty() == false)
                {
                    return _pool.Pop();
                }
            }
            return null;
        }

        public void AddToPool(EnemyTypes enemyTyps)
        {
            var value = Random.Range(0, _enemyFactoryBase._spawnEnemyPosition.Count);
            for(int i =0; i < value; i++)
            {
                var gameObj = _enemyFactoryBase.CreateEnemy(enemyTyps);
                gameObj.gameObject.SetActive(false);
                var obj = Instantiate(gameObj, _enemyFactoryBase._spawnEnemyPosition[value]);
                _pool.Push(obj);
            }
            
        }

        
    }
}
