using UnityEngine;

public class YutPlayer : MonoBehaviour
{
    public int trideId;
    public int maxChar = 4;
    public int currentActiveChar = 0;

    public YutPiace yutPiace;

    //오버라이드 할지는 잠시 보기 ( 새 말 출발 코드 내용)
    public virtual void StartNewChar(int SelectMoveSpace)
    {
        if(currentActiveChar>=maxChar)
        {
            return;
        }
        string selectCharName = GetCharPoolName();
        GameObject newChar = ObjectPooling.instance.GetObject(selectCharName);

        if (newChar == null)
        {
            return;
        }
        YutPiace yutPiaceScrips = newChar.GetComponent<YutPiace>();

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
            default:  return "humen"; 
        }
    }

    public void SetTrideId(int id)
    {
        this.trideId = id;
    }

    public virtual void GoalIn()
    {
        foreach(YutPiace kid in yutPiace.carriedChar)
        {
            kid.gameObject.SetActive(false);
            kid.returnReady();
        }
       yutPiace.returnReady();
    }


   


}
