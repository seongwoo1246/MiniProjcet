
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : YutPlayer
{
    [SerializeField] TrideDataManager trideM;
    [SerializeField] AlbumDataManager albumM;


    public static PlayerManager Instance;

    
    public GameObject playerUiDate;


     Image Icon;
     Image CharacterIcon;
     TextMeshProUGUI maxCharacter;
     TextMeshProUGUI Hpbar;
     Scrollbar HP;

    public int haveMoney = 0;
    public EnemyController enemyController;

    public bool CanAttackLastBoss = false;
    public bool CanGoEnd = false;


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

       SelectTride(trideM.TrideList[0]);
        UnLockedList(0);


    }


    public void UnLockedList(int id)
    {
        Tride target = trideM.TrideList.Find(x => x.id == id);
        if (target != null)
        {
            target.isUnLocked = true;
        }


        var albumdata = albumM;
        Album targetalbum = albumdata.AlbumList.Find(y =>  y.id == id);
        if (targetalbum != null)
        {
            targetalbum.isUnLocked = true;
        }

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

        Button button2 = SkillManager.instance.SkillB.GetComponent<Button>();
        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(UseSkill);
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
    //----------------------------------------------------------------- 여기부터는 스킬 관련 함수들

    public  YutPiace playerPiace;
    
    public override float GetHpVaule()
    {
        if (PlayerData == null) return 1.0f;
        return PlayerData.hp / PlayerData.maxHp;
        
    }

    public void UseSkill()
    {
        var skill = SkillManager.instance;
        if (skill.skillCount <= 0|| (Random.value+ enemyController.enemyData.block) > PlayerData.luck)
        {
            SkillManager.instance.skillText.text = "적이 스킬을 방해했습니다. 앙대/(ㅠ-ㅠ)/";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            skill.skillCount--;
            skill.count.text = $"{skill.skillCount}";
            if(skill.skillCount <= 0)
            { skill.skillCount = 0; }
            return;
        }
        else
        {
            switch(PlayerData.id)
            {
                case 0:
                    skill.HumenSkill(this, PlayerData.depence, 1);
                    SkillManager.instance.skillText.text = "이번 턴 동안 잃은 체략에 비례해서 방어력이 상승합니다.";
                    SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

                    break;

                case 1:
                    skill.GoblinSkill(this);
                    SkillManager.instance.skillText.text = "이번에 적을 잡으면 일정 확률로 적의 말을 가져옵니다.";
                    SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

                    break;

                case 2:
                    skill.ElfSkill(this, playerPiace, PlayerData.length);
                    SkillManager.instance.skillText.text = " 아군 엘프중 하나를 선택하세요. 같은 루트 사거리 안 가장 많은 적을 잡습니다.";
                    SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

                    break;

                case 3:
                    skill.UndeadSkill(this);
                    SkillManager.instance.skillText.text = "이번에 적을 잡으면 일정 확률로 업은 갯수가 증가합니다.";
                    SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

                    break;

                case 4:
                    skill.AngelSkill(playerPiace);
                    SkillManager.instance.skillText.text = "3턴 동안 반격을 준비합니다. 일정 확률로 반격 성공시 해제됩니다.";
                    SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

                    break;
            }

            
        }

        skill.skillCount--;
        skill.count.text = $"{skill.skillCount}";

    }

    public override float GoblinSkillPercent()
    {
        return PlayerData.kidnap;
    }
    public override float UndeadSkillPercent()
    {

        return PlayerData.infection;
    }
    public override float AngelSkillPercent()
    {
        
        return PlayerData.rivival;
    }


    public override void UpdateText()
    {
        maxCharacter.text = $" {maxChar}";

    }

    










    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ 종족별 나누기 할 칸
    //종족별로 나눌 딕셔너리
    public Dictionary<int,Tride> TrideDataDic = new Dictionary<int,Tride>();
    // 종족별로 강화상태를 나눌 딕셔너리
    public Dictionary<int,Dictionary<int,Training>> TrideUpgradeLevels =new Dictionary<int,Dictionary<int, Training>>();


    public void SelectTride(Tride originalTrideData)
    {
        int TrideId = originalTrideData.id;

        if(!TrideDataDic.ContainsKey(TrideId))
        {
            TrideDataDic.Add(TrideId, originalTrideData.Clone());
            TrideUpgradeLevels.Add(TrideId, new Dictionary<int, Training>());
        }

        PlayerData = TrideDataDic[TrideId];
        SetPlayer();
        maxChar = PlayerData.maxCharacter;

        if(TrainingUi.Instance == null)
        { return; }
        

        TrainingUi.Instance.ReFreshSlot();


    }

    public Training GetTrainingData(int TrideId, int slotid, Training defultdata)
    {
        
        if(!TrideUpgradeLevels.ContainsKey(TrideId))
        {
            TrideUpgradeLevels.Add(TrideId,new Dictionary<int, Training>());
        }

        if(!TrideUpgradeLevels[TrideId].ContainsKey(slotid))
        {
            TrideUpgradeLevels[TrideId].Add(slotid, defultdata.Clone());
        }

        return TrideUpgradeLevels[TrideId][slotid];
    }


}
