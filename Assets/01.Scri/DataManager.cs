using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    Dictionary<int,Tride> Trides = new Dictionary<int,Tride>();

    [SerializeField] private TrideDataManager TrideBox;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadTrideData();
    }

    private void LoadTrideData()
    {
        for(int i = 0; i < TrideBox.TrideList.Count; i++)
        {
            Tride tride = TrideBox.TrideList[i].Clone();
            Trides[i] = tride;
        }
    }

    public Tride GetTrideData(int id)
    {
        return Trides.GetValueOrDefault(id);
    }
}
