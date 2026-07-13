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



  // 반복문으로 만들기?
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadHumenScene()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadUndeadScene()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadGoblinScene()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadElfScene()
    {
        SceneManager.LoadScene(5);
    }
    public void LoadAngelScene()
    {
        SceneManager.LoadScene(6);
    }
    public void LoadEndingScene()
    {
        SceneManager.LoadScene(7);
    }

   
}
