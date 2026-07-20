
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : YutPlayer
{
    public static PlayerManager Instance;

    public GameObject playerUiDate;

     Image Icon;
     Image CharacterIcon;
     TextMeshProUGUI maxCharacter;
     TextMeshProUGUI Hpbar;
     Scrollbar HP;

    public Tride PlayerData { get; private set; }

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
        if (playerUiDate != null)
        {
            Icon = playerUiDate.transform.Find("my").GetComponent<Image>();
            CharacterIcon = playerUiDate.transform.Find("icon").GetComponent<Image>();
            maxCharacter = playerUiDate.transform.Find("count").GetComponent<TextMeshProUGUI>();
            Hpbar = playerUiDate.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            HP = playerUiDate.transform.Find("myhp").GetComponent<Scrollbar>();

            Button button = CharacterIcon.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(BattleSceneManager.instance.OnChilckStartNewChar);

        }
    }


    public void SetTridePlayer(Tride Data)
    {
        PlayerData = Data.Clone();
        SetPlayer();
    }

    public void SetPlayer()
    {
        if (PlayerData == null) return;

        playerUiDate.SetActive(true);
    
            
        Icon.sprite = PlayerData.icon;
        CharacterIcon.sprite = PlayerData.icon;
        maxCharacter.text = PlayerData.maxCharacter.ToString();
        Hpbar.text = $" {PlayerData.hp}/{PlayerData.maxHp}";
    }

   public void SetHp()
    {
        PlayerData.hp = PlayerData.maxHp;
    }


    public override void GoalIn()
    {
        var Enemy =BattleSceneManager.instance.CuttrentEnemy;
        var BSM = BattleSceneManager.instance;
        if (Enemy != null)
        {
            int enemyindex = Enemy.CurrentEnemy;
            var EnemyData = Enemy.trideDM.TrideList[enemyindex];
            BSM.TakeDamage(EnemyData.miss, EnemyData.hp, BSM.Attack(PlayerData.critical, PlayerData.damage), EnemyData.depence);
        }

        base.GoalIn();
    }
}
