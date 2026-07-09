using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesM : MonoBehaviour
{
    public static ScenesM instance;

    

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

   

  
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene(1);
    }
}
