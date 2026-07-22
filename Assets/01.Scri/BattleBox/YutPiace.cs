using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public enum PathState
{
    main,
    autumn,
    spring,
    summer,
   

}



public class YutPiace : MonoBehaviour
{
    public int currentspace = 0;
    public bool isEnemy = false;
    public bool isMovingOnBorad = false;

  public int currentPathIndex = 0;
  private bool isMoveing = false;
    public PathState PathState1 = PathState.main;
    YutPlayer player;
   public bool isCarried = false;
    private Image icon;
    public List<YutPiace>carriedChar = new List<YutPiace>();

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    public void OnBoardIn(bool isEnemyPiece)
    {
        isMovingOnBorad = true;
        isEnemy = isEnemyPiece;
        currentspace = 1;
        currentPathIndex = 0;
        PathState1 = PathState.main;
        isCarried = false;
    }


    //업은 말 상태 표시
    public void UpdateVisuals()
    {
        int count = carriedChar.Count;
        if (count == 0) icon.color = Color.white;
        else if (count == 1) icon.color = Color.red;
        else if (count == 2) icon.color = Color.orange;
        else if (count == 3) icon.color = Color.yellow;
        else if (count == 4) icon.color = Color.green;
        else if (count == 5) icon.color = Color.blue;
        else if (count == 6) icon.color = Color.navyBlue;
        else if (count == 7) icon.color = Color.purple;
        
        
    }
    //말이 잡혔을 때 하는 코드
    public void CatchChar()
    {
        returnReady();
    }
    //잡히거나 골인 후 말이 돌아가는 내용
    public void returnReady()
    {
        currentPathIndex = 0;
        isMovingOnBorad = false;
        isCarried = false;
        carriedChar.Clear();
        UpdateVisuals();
        this.gameObject.SetActive(false);
    }

    //움직이는 함수
    public void StartMove(int steps)
    {
        if (isMoveing) return;
       StartCoroutine(MoveStepRoutine(steps));

    }
    // 말이 움직이는 루틴
    public IEnumerator MoveStepRoutine(int steps)
    {
        if (steps == 0)
        {
            BattleSceneManager.instance.TurnEnd();
            yield return new WaitForSeconds(0.5f);
            yield break;
        }
           

        isMoveing = true;
        

        if(steps == -1)
        {
            if (PathState1 == PathState.main && currentPathIndex == 1)
            {
                currentPathIndex = 20;

            }
            else
            {
                currentPathIndex--;
                if (currentPathIndex < 0)
                {
                    if (PathState1 == PathState.summer)
                    {
                        PathState1 = PathState.main;
                        currentPathIndex = 5;
                    }
                    else if (PathState1 == PathState.spring)
                    {
                        PathState1 = PathState.main;
                        currentPathIndex = 10;
                    }
                    else if (PathState1 == PathState.autumn)
                    {
                        PathState1 = PathState.summer;
                        currentPathIndex = 3;
                    }



                }
            }
            steps = 1;
        }

        for (int i = 0; i < steps; i++)
        {
           currentPathIndex++;

            Vector3Int nextSpace = nextSpace = Vector3Int.zero;
            int maxCount = 0;
            var borad = YutBoardController.instance;

            switch(PathState1)
            {
                case PathState.main:
                    maxCount = borad.mainPathSpace.Count;
                    if(currentPathIndex<maxCount)
                    {
                        nextSpace = borad.mainPathSpace[currentPathIndex];
                    }
                    break;

                    case PathState.autumn:
                    maxCount = borad.shortCutAutumn.Count;
                    if (currentPathIndex < maxCount)
                    {
                        nextSpace = borad.shortCutAutumn[currentPathIndex];
                    }
                    else
                    {
                        PathState1 = PathState.main;
                        currentPathIndex = 20;
                        nextSpace =borad.mainPathSpace[currentPathIndex];

                        maxCount = borad.mainPathSpace.Count;
                    }
                        break;

                    case PathState.spring:
                    maxCount = borad.shortCutSpring.Count;
                    if(currentPathIndex<maxCount)
                    {
                        nextSpace = borad.shortCutSpring[currentPathIndex];
                    }
                    else
                    {
                        PathState1 = PathState.main;
                        currentPathIndex = 20;
                        nextSpace = borad.mainPathSpace[currentPathIndex];

                        maxCount = borad.mainPathSpace.Count;
                    }
                    break;

                    case PathState.summer:
                    maxCount = borad.shortCutSummer.Count;
                    if(currentPathIndex<maxCount)
                    {
                        nextSpace = borad.shortCutSummer[currentPathIndex];
                    }
                    else
                    {
                        PathState1 = PathState.main;
                        currentPathIndex = 15;
                        nextSpace = borad.mainPathSpace[currentPathIndex];

                        maxCount = borad.mainPathSpace.Count;
                    }
                    break;

                  
            }



          if(currentPathIndex>=maxCount)
            {
                currentPathIndex = maxCount-1;

                player.GoalIn();
                yield break;
            }

         

            Vector3 targetWorldPosition = borad.GetWorldPosition(nextSpace);

            while (Vector3.Distance(transform.position, targetWorldPosition) > 0.02)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, Time.deltaTime * 6f);
                yield return null;
            }

            transform.position = targetWorldPosition;
            yield return new WaitForSeconds(0.1f);
        }

        if(PathState1 == PathState.main)
        {
            if(currentPathIndex ==5)
            {
                PathState1 = PathState.summer;
                currentPathIndex = 0;
            }
            else if (currentPathIndex == 10)
            {
                PathState1 = PathState.spring;
                currentPathIndex = 0;
            }
           
        }
        else if (PathState1 == PathState.summer)
        {
            if( currentPathIndex ==2)
            {
                PathState1 = PathState.autumn;
                currentPathIndex = 0;
            }
        }

        EndMove(this, currentPathIndex);
        isMoveing =false;

     }

    //업은 말들이 같이 이동하기 위한 코드
    public void EndMove(YutPiace leaderPiece , int finalSpace)
    {
        leaderPiece.currentPathIndex = finalSpace;
        foreach( YutPiace kid in leaderPiece.carriedChar)
        {
            kid.currentPathIndex = finalSpace;
            kid.transform.position = leaderPiece.transform.position;
        }
        BattleSceneManager.instance.checkCatchChar(leaderPiece);
    }

    // 말 선택하기 말들 한태 버튼을 넣어줘서 이걸 넣어주는 느낌으로?
    public void OnMouseDown()
    {
        var manger = BattleSceneManager.instance;

        if (manger.isYutSelected==false)
        {
            return;
        }

        this.StartMove(manger.selectMoveSpace);
        manger.UseSelectedYut();
    }

    // 골인하는지 체크
    public bool CheckGoalIn(int movecount)
    {
        int nextPosition =currentPathIndex + movecount;
        int maxCount = 0;
        switch (PathState1)
        {
            case PathState.main: maxCount = YutBoardController.instance.mainPathSpace.Count; break;

            case PathState.spring: maxCount = YutBoardController.instance.shortCutSpring.Count + 1; break;

            case PathState.autumn: maxCount = YutBoardController.instance.shortCutAutumn.Count + 1; break;

            default: maxCount = YutBoardController.instance.mainPathSpace.Count; break;
        }


        if (nextPosition >= maxCount)
        {
            return true;
        }
        return false;
    }
}
