using UnityEngine;
using UnityEngine.SceneManagement;







public enum scenetpye
{
   Lobby =1, 
    humun,
    undead,
    goblin,
    elf,
    angel,
    Ending

}

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

    public void LoadScenes(scenetpye type)
    {
        SceneManager.LoadScene((int)type);
    }


   
}
