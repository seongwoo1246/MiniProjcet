using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public Tride PlayerData {  get; private set; }

    
    
   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    public void SetTridePlayer(Tride Data)
    {
        PlayerData = Data.Clone();
    }




}
