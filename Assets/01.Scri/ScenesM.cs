using UnityEngine;
using UnityEngine.SceneManagement;


public enum scenetpye
{
    start,
   Lobby, 
    humun,
    undead,
    goblin,
    elf,
    angel,
    last,
    Ending

}

public enum Difficulty
{
    easy,
    normal,
    hard
}










public class ScenesM : MonoBehaviour
{
    public static ScenesM instance;
    public Difficulty SelectedDifficulty = Difficulty.normal;
 


    public bool IsviewEnd = false;
    public bool IsIntroview = false;


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
        SoundManager.instance.PlayVoice("진실의방으로");
        SceneManager.LoadScene((int)type);
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        SelectedDifficulty = difficulty;
    }
   
   
}
