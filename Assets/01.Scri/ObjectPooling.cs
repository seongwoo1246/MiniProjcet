using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    [SerializeField] List<GameObject> Objects = new List<GameObject>();

    int poolSize;

    Queue<GameObject> poolQ = new Queue<GameObject>();

    Dictionary<string,Queue<GameObject>> poolD = new Dictionary<string,Queue<GameObject>>();
    private HashSet<GameObject> activeobj = new HashSet<GameObject>();

    public List<GameObject> garbage = new List<GameObject>();


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
        poolSize = 400;

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

        GameObject go;
        if (poolD[name].Count > 0)
        {
            go = poolD[name].Dequeue();
           
        }
        else 
        {
           go = Instantiate(Objects.Find(obj =>  obj.name == name));
            
        }

        go.SetActive(true);
        activeobj.Add(go);
        return go;
    }

    public void ReturnObject(string name,GameObject obj)
    {
        if( obj == null ) return;

        if(!activeobj.Contains(obj))
        {
          
            return;
        }
            
        activeobj.Remove(obj);

        if(!poolD.ContainsKey(name))
        {
            obj.SetActive(false);
            garbage.Add(obj);
            return;
        }
        obj.SetActive(false);
        poolD[name].Enqueue(obj);
    }

    public void cleargarbage()
    {
        foreach (var go in garbage)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }
        garbage.Clear();
    }


}
