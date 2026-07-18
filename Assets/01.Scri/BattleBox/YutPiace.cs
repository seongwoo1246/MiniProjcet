using System.Collections;

using UnityEngine;

public enum PathState
{
    main,
    autumn,
    spring,
    summer,
    magic,

}

public class YutPiace : MonoBehaviour
{
  public int currentPathIndex = 0;
  private bool isMoveing = false;
    public PathState PathState = PathState.main;
   
    //움직이는 함수
    public void StartMove(int steps)
    {
        if (isMoveing) return;
       StartCoroutine(MoveStepRoutine(steps));

    }
    // 말이 움직이는 루틴
    private IEnumerator MoveStepRoutine(int steps)
    {
        isMoveing = true;
        

        for (int i = 0; i < steps; i++)
        {
           currentPathIndex++;

            if (currentPathIndex >= YutBoardController.instance.mainPathSpace.Count)
            {
                //골인 말 비활성화 및 상대 공격 연출
                yield break;
            }

            Vector3Int nextSpace = YutBoardController.instance.mainPathSpace[currentPathIndex];

            Vector3 targetWorldPosition = YutBoardController.instance.GetWorldPosition(nextSpace);

            while (Vector3.Distance(transform.position, targetWorldPosition) > 0.02)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, Time.deltaTime * 6f);
                yield return null;
            }

            transform.position = targetWorldPosition;
            yield return new WaitForSeconds(0.1f);
        }
        isMoveing =false;

     }

    // 말 선택하기
    private void OnMouseDown()
    {
        var manger = BattleSceneManager.instance;

        if(manger.isYutSelected==false)
        {
            return;
        }

        this.StartMove(manger.selectMoveSpace);
        manger.UseSelectedYut();
    }











}
