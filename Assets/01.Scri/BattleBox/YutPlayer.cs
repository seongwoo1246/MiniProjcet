using UnityEngine;

public class YutPlayer : MonoBehaviour
{
    public TrideType TrideType;
    public int maxChar = 4;
    public int currentActiveChar = 0;

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
        switch (TrideType)
        {
            case TrideType.angel: return "angel"; 
            case TrideType.elf: return "elf"; 
            case TrideType.goblin: return "goblin"; 
            case TrideType.humen: return "humen"; 
            case TrideType.undead: return "undead"; 
            default:  return "humen"; 
        }
    }
}
