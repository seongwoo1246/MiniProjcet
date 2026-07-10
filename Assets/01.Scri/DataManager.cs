using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    Dictionary<int,Tride> Trides = new Dictionary<int,Tride>();
    Dictionary<int,Training> Trainings = new Dictionary<int,Training>();

    [SerializeField] private TrideDataManager TrideBox;
    [SerializeField] private TrainingDataManager TrainingBox;
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

    
    // 밑에 2개는 제네릭으로 묶는거 가능할 거 같기도?
    void Start()
    {
        LoadTrideData();
        LoadTrainingData();
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

    private void LoadTrainingData()
    {
        for(int i = 0; i < TrainingBox.TrainingList.Count; i++)
        {
           Training training = TrainingBox.TrainingList[i].Clone();
            Trainings[i] = training;
        }
    }

    public Training GetTrainingData(int id)
    {
        return Trainings.GetValueOrDefault(id);
    }
}
