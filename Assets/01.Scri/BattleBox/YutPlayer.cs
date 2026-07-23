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

    protected YutPiace yutPiace;

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

        if(currentActiveChar>=maxChar)
        {
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
        yutPiaceScrips.OnBoardIn(isEnemy);
        BattleSceneManager.instance.allActiveChar.Add(yutPiaceScrips);
        yutPiaceScrips.currentPathIndex = 0;
        Vector3 StartWorldPosition = YutBoardController.instance.GetWorldPosition(YutBoardController.instance.mainPathSpace[0]);
        newChar.transform.position = StartWorldPosition;

        yutPiaceScrips.StartMove(SelectMoveSpace);
        currentActiveChar++;
    }

    protected string GetCharPoolName()
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
        if (targetPiace == null) return;
        if (targetPiace.carriedChar != null)
        {
            foreach (YutPiace kid in targetPiace.carriedChar)
            {
                if(kid != null)
                {
                    kid.gameObject.SetActive(false);
                    kid.returnReady();
                }
               
            }
            targetPiace.carriedChar.Clear();
        }
        targetPiace.gameObject.SetActive(false);
       targetPiace.returnReady();
    }


   


}
