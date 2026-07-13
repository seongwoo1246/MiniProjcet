using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    Dictionary<int,Tride> Trides = new Dictionary<int,Tride>();
    Dictionary<int,Training> Trainings = new Dictionary<int,Training>();
    Dictionary<int,Album> Albums = new Dictionary<int,Album>();

    [SerializeField] private TrideDataManager TrideBox;
    [SerializeField] private TrainingDataManager TrainingBox;
    [SerializeField] private AlbumDataManager AlbumBox;
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
        LoadAlbumData();
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
    private void LoadAlbumData()
    {
        for(int i = 0; i < AlbumBox.AlbumList.Count; i++)
        {
           Album album = AlbumBox.AlbumList[i].Clone();
            Albums[i] = album;
        }
    }

    public Album GetAlbumData(int id)
    {
        return Albums.GetValueOrDefault(id);
    }


}
