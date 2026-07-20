using System.Collections;

using UnityEngine;

public enum PathState
{
    main,
    autumn,
    spring,
    summer,
   

}

public class YutPiace : MonoBehaviour
{

  public int currentPathIndex = 0;
  private bool isMoveing = false;
    public PathState PathState1 = PathState.main;
    YutPlayer player;
   
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
        
        if(steps == -1)
        {
            if (PathState1 == PathState.main && currentPathIndex == 0)
            {
                currentPathIndex = 19;

            }
            else
            {
                currentPathIndex--;
                if (currentPathIndex < 0)
                {
                    if (PathState1 == PathState.summer)
                    {
                        PathState1 = PathState.main;
                        currentPathIndex = 4;
                    }
                    else if (PathState1 == PathState.spring)
                    {
                        PathState1 = PathState.main;
                        currentPathIndex = 9;
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
                        currentPathIndex = 19;
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
                        currentPathIndex = 19;
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
                        currentPathIndex = 14;
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
            if(currentPathIndex ==4)
            {
                PathState1 = PathState.summer;
                currentPathIndex = 0;
            }
            else if (currentPathIndex == 9)
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







        isMoveing =false;

     }

    // 말 선택하기
    private void OnMouseDown()
    {
        var manger = BattleSceneManager.instance;

        if (manger.isYutSelected==false)
        {
            return;
        }

        this.StartMove(manger.selectMoveSpace);
        manger.UseSelectedYut();
    }


  








}
