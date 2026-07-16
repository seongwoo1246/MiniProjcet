
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : BattleSceneManager
{
    public static PlayerManager Instance;

    [SerializeField] Image Icon;
    [SerializeField] Image CharacterIcon;
    [SerializeField] TextMeshProUGUI maxCharacter;
    [SerializeField] TextMeshProUGUI Hpbar;
    [SerializeField] Scrollbar HP;

    public Tride PlayerData {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
