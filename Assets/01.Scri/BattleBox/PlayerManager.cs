
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : YutPlayer
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

    private void Start()
    {
        Icon.sprite = PlayerData.icon;
        CharacterIcon.sprite = PlayerData.icon;
        maxCharacter.text = PlayerData.maxCharacter.ToString();
        Hpbar.text = $" {PlayerData.hp}/{PlayerData.maxHp}";
    }


    public void SetTridePlayer(Tride Data)
    {
        PlayerData = Data.Clone();
    }

   

   
}
