
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

    EnemyController enemyController;

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

    protected override void Start() 
    {
        enemyController = GetComponent<EnemyController>();
     
    }

        public void UpdataUI(GameObject gameObject)
         {

        if (gameObject == null)
        {
            Debug.Log("아직 플레이어 정보가 없어!! 조금만 기다려!!");
            return;
        }
        playerUiDate = gameObject;
                if (playerUiDate != null)
                 {
                     Icon = playerUiDate.transform.Find("my").GetComponent<Image>();
                     CharacterIcon = playerUiDate.transform.Find("icon").GetComponent<Image>();
                     maxCharacter = playerUiDate.transform.Find("count").GetComponent<TextMeshProUGUI>();
                     Hpbar = playerUiDate.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
                     HP = playerUiDate.transform.Find("myhp").GetComponent<Scrollbar>();
                        SetPlayer();
                 }
         }

    public void ButtonSet()
    {
        Button button = CharacterIcon.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(BattleSceneManager.instance.OnChilckStartNewChar);


    }

    public void SetTridePlayer(Tride Data)
    {
       this.PlayerData = Data.Clone();
        SetPlayer();
        maxChar = PlayerData.maxCharacter;
    }

    public void SetPlayer()
    {
        
        if (PlayerData == null||Icon ==null || playerUiDate == null) return;
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


    public override void GoalIn(YutPiace targetPiace)
    {
       
        var BSM = BattleSceneManager.instance;

        int totalcount = 1;
        if(targetPiace != null&& targetPiace.carriedChar!=null)
        {
            totalcount += targetPiace.carriedChar.Count;
        }
           

      if(enemyController.enemyData != null)
        {
            var EnemyData = enemyController.enemyData;
            BSM.TakeDamage(EnemyData, EnemyData.miss, BSM.countDamageUp(BSM.Attack(PlayerData.critical, PlayerData.damage), totalcount), EnemyData.depence);
            if (enemyController != null)
            {
                enemyController.Hpeffect(enemyController.CurrentEnemy);
            }
            BSM.Heal(PlayerData, PlayerData.heal);
            playerHpeffect();

        }
        base.GoalIn(targetPiace);
    }

    public void playerHpeffect()
    {
        
        HP.size = PlayerData.hp / PlayerData.maxHp;
        Hpbar.text = $"{PlayerData.hp}/{PlayerData.maxHp}";
    }
}
