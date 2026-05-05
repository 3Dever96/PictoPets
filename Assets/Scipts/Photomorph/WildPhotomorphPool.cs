using System.Collections.Generic;
using UnityEngine;

public class WildPhotomorphPool : MonoBehaviour
{
    public Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            AddGameObject(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void AddGameObject(GameObject g)
    {
        pool.Enqueue(g);
    }

    public GameObject GetGameObject()
    {
        GameObject newObject = pool.Dequeue();
        newObject.SetActive(true);
        return newObject;
    }
}
