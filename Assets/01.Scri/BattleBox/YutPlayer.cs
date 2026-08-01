

using UnityEngine;


public class YutPlayer : MonoBehaviour
{
    protected bool useedSkill70 = false;
    protected bool useedSkill50 = false;
    protected bool useedSkill30 = false;

    public bool isMaxChar = false;

    public int trideId;
    public int maxChar = 0;
    
    public int currentActiveChar = 0;

    

    
    public virtual void InItState()
    {
        useedSkill70 = false;
        useedSkill50 = false;
        useedSkill30 = false;
    }

    protected virtual void Start()
    {

        InItState();
    }

   

    //오버라이드 할지는 잠시 보기 ( 새 말 출발 코드 내용)
    public virtual void StartNewChar(int SelectMoveSpace , bool isEnemy)
    {
        

        var BSM = BattleSceneManager.instance;
        if (currentActiveChar>=maxChar)
        {
            BSM.MaxCharCaption.gameObject.SetActive(true);
            StartCoroutine(BSM.FalseText(BSM.MaxCharCaption));
            isMaxChar = true;
            return;
        }
       

            string selectCharName = GetCharPoolName();
        GameObject newChar = ObjectPooling.instance.GetObject(selectCharName);

        if (newChar == null)
        {
            return;
        }
        
        YutPiace yutPiaceScrips = newChar.GetComponent<YutPiace>();
        yutPiaceScrips.Init(this);
        yutPiaceScrips.currentPathIndex = -1;
        yutPiaceScrips.OnBoardIn(isEnemy);
       
        
        Vector3 StartWorldPosition = YutBoardController.instance.GetWorldPosition(YutBoardController.instance.mainPathSpace[0]);
        newChar.transform.position = StartWorldPosition;

        
            Vector3 scale = newChar.transform.localScale;
            scale.x = isEnemy ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            newChar.transform.localScale = scale;
        


        yutPiaceScrips.StartMove(SelectMoveSpace);
        BSM.allActiveChar.Add(yutPiaceScrips);
        currentActiveChar++;
    }

    public string GetCharPoolName()
    {
        
        switch (trideId)
        {
            case 0: return "humen";
            case 1: return "goblin";
            case 2: return "elf"; 
            case 3: return "undead";
            case 4: return "angel";
            case 5: return "boss";
            default:  return   "humen"; 
        }
    }

    public void SetTrideId(int id)
    {
        this.trideId = id;
    }


    //말이 들어갔을 때 할 행동의 모체
    public virtual void GoalIn(YutPiace targetPiace)
    {
       
        if (targetPiace == null) return;
        if (targetPiace.carriedChar != null)
        {
            foreach (YutPiace kid in targetPiace.carriedChar)
            {
                if(kid != null)
                {
                   
                    currentActiveChar--;
                   
                    kid.returnReady();
                   
                }
               
            }
            targetPiace.carriedChar.Clear();
        }
        currentActiveChar--;
        targetPiace.returnReady();
        isMaxChar = false;
       
    }









    //----------------------------------------------------------------- 여기부터는 스킬 관련 함수들

    

    public virtual float GetHpVaule()
    {
        return 1.0f;
    }

    public int currentDenfence = 0;
    public int buttTurn = 0;
    public int enemyBuffTurn = 0;

    public void ApplyDefence(int defence, int turn, int enemyturn)
    {
        currentDenfence = defence;
        buttTurn = turn;
        enemyBuffTurn = enemyturn;

    }

    public virtual void TurnDisCount()
    {

        buttTurn--;
        SkillManager.instance.skill.text = $"{buttTurn}";
        if(enemyBuffTurn>0)
        {
            SkillManager.instance.enemyskill.text = $"{enemyBuffTurn}";
        }
        

        if (buttTurn == 0)
            {
                buttTurn = 0;
                currentDenfence = 0;
                SkillManager.instance.skill.text = "0";
                SkillManager.instance.skillText.text = "방어력이 돌아옵니다.";
                SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            }
        if(enemyBuffTurn == 0)
        {
            enemyBuffTurn = 0;
            currentDenfence = 0;
            SkillManager.instance.enemyskill.text = "0";
            SkillManager.instance.skillText.text = "방어력이 돌아옵니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
        }

           
        
    }

    public bool isGoblinSkillUsed = false;
    
    public virtual void UsedGoblinSkill(YutPlayer attacker,YutPlayer target)
    {
        float percent = attacker.GoblinSkillPercent();
        
        if(attacker == PlayerManager.Instance)
        {
            if (!attacker.isGoblinSkillUsed) return;
            attacker.isGoblinSkillUsed = false;
            SkillManager.instance.skill.text = "불가능";
        }
        if(target.maxChar<=1)  return;
        float randomValue = Random.value;
        if(randomValue <= percent)
        {
            SkillManager.instance.skillText.text = "약탈 성공했습니다. 야호(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            attacker.maxChar++;
            target.maxChar--;
            attacker.UpdateText();
            target.UpdateText();
        }
    }
    public virtual void UsedUndeadSkill(YutPlayer attacker, YutPiace attackerpiece)
    {
        float percent = attacker.UndeadSkillPercent();

        if (attacker == PlayerManager.Instance)
        {
            if (!attacker.isUndeadSkillUsed) return;
            attacker.isUndeadSkillUsed = false;
            SkillManager.instance.skill.text = "불가능";
        }
        
        float randomValue = Random.value;
        if (randomValue <= percent)
        {
            SkillManager.instance.skillText.text = "감염 성공했습니다. 야호(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            attackerpiece.carriedChar.Add(null);
            attackerpiece.UpdateVisuals();
        }
    }

    public virtual float GoblinSkillPercent()
    {
        return 0.5f;
    }
    public virtual float UndeadSkillPercent()
    {
        return 0.5f;
    }
    public virtual float AngelSkillPercent()
    {
        return 0.5f;
    }
    public virtual void UpdateText()
    { }

    public bool isUndeadSkillUsed = false;


}
