using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    [SerializeField] List<GameObject> Objects = new List<GameObject>();

    int poolSize;

    Queue<GameObject> poolQ = new Queue<GameObject>();

    Dictionary<string,Queue<GameObject>> poolD = new Dictionary<string,Queue<GameObject>>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        poolSize = 15;

        foreach(GameObject obj in Objects)
        {
            poolD[obj.name] = new Queue<GameObject>();

            GameObject parentpool = new GameObject($"{obj.name}_pool");
            parentpool.transform.SetParent(this.transform);

            for(int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(obj,parentpool.transform);
                go.SetActive(false);
                poolD[obj.name].Enqueue(go);
            }
        }
    }

    public GameObject GetObject(string name)
    {
        if (!poolD.ContainsKey(name))
            return null;
        if(poolD[name].Count > 0)
        {
            GameObject go = poolD[name].Dequeue();
            go.SetActive(true);
            return go;
        }
        else 
        {
         GameObject go = Instantiate(Objects.Find(obj =>  obj.name == name));
            return go;
        }
    }

    public void ReturnObject(string name,GameObject obj)
    {
        if(!poolD.ContainsKey(name))
        {
            Destroy(obj);
        }
        obj.SetActive(false);
        poolD[name].Enqueue(obj);
    }




}
