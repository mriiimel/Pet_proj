using System.Collections.Generic;
using UnityEngine;




public class ObjectPool
{
    private Queue<GameObject> _pool;

    public ObjectPool()
    {
        _pool = new ();

    }

    public void AddToPool(GameObject gameObject)
    {
        gameObject.SetActive (false);
        _pool.Enqueue(gameObject);
    }

    public GameObject GetFromPool()
    {
        if (_pool.Count > 0)
        { 

            return _pool.Dequeue();
        }   
        return null;
    }
    public void RturnToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        _pool.Enqueue(gameObject);
    }

        
}

