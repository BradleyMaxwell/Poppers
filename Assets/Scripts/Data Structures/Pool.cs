using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private PoolObject prefab; // this is the prefab that all objects in the pool will be
    private List<PoolObject> objects; // all the instantiated prefab objects that are in the pool
    private GameObject parentGameObject; // keeps track of the pool GameObject which every pool object will be the child of

    private Pool (PoolObject prefab, int size) // private constructor so that it is protected from any external influence on the creation of the pool
    {
        this.prefab = prefab;
        objects = new List<PoolObject>(size);
    }

    public static Pool CreatePool (PoolObject prefab, int size) // static method so that the spawner can create enemy pools in an abstract way
    {
        Pool pool = new Pool(prefab, size);
        pool.parentGameObject = new GameObject(prefab.name + " Pool"); // create the game object that will be the pool, which is used to have the pool objects as children

        for (int i = 0; i < size; i++) // fill the pool with instantiated pool objects
        {
            PoolObject poolObject = pool.CreatePoolObject(prefab.name);
        }
        return pool;
    }

    private PoolObject CreatePoolObject (string name) // creates a new pool object for a prefab pool instance, making the new object a child of the pool
    {
        PoolObject poolObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parentGameObject.transform);
        poolObject.name = name;
        poolObject.SetParentPool(this); // set the parent of the pool object to the instance of the pool
        poolObject.gameObject.SetActive(false); // disabling it will trigger the pool object's OnDisable which adds it to the pool
        return poolObject;
    }

    public void ReturnPoolObject (PoolObject poolObject) // returns an object back into this pool instance
    {
        poolObject.gameObject.SetActive(false);
        objects.Add(poolObject);
    }

    public PoolObject GetPoolObject () // gets an object from the pool
    {
        if (objects.Count > 0) // if there are objects in the pool that can be used, then use the first in the list
        {
            PoolObject poolObject = objects[0];
            objects.RemoveAt(0);
            poolObject.gameObject.SetActive(true);
            return poolObject;
        } else // if there are no available objects in the pool, create a new object for this pool and add it to the pool and recall the function
        {
            PoolObject newPoolObject = this.CreatePoolObject(prefab.name); // instantiates the object
            return GetPoolObject();
        }
    }
}
