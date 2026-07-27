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
        Debug.Log("플레이어부모");
        if (targetPiace == null) return;
        if (targetPiace.carriedChar != null)
        {
            foreach (YutPiace kid in targetPiace.carriedChar)
            {
                if(kid != null)
                {
                    currentActiveChar--;
                    string selectCharName =GetCharPoolName();
                    ObjectPooling.instance.ReturnObject(selectCharName, kid.gameObject);
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

    public void ApplyDefence(int defence, int turn)
    {
        currentDenfence = defence;
        buttTurn = turn;


    }

    public void TurnDisCount()
    {
        if(buttTurn>0)
        {
            buttTurn--;
            if(buttTurn <= 0)
            {
                buttTurn = 0;
                currentDenfence = 0;
            }
        }
    }
}
