using System.Collections.Generic;
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
    private EnemyController enemyController;
    private SpriteRenderer icon;
    YutPlayer player;

    public PathState PathState1 = PathState.main;
    public int currentspace = 0;
    public int currentPathIndex = 0;
   


    private bool isMoveing = false;
    public bool isEnemy = false;
    public bool isMovingOnBorad = false;
    public bool isCarried = false;
    


    public List<YutPiace>carriedChar = new List<YutPiace>();

    private void Awake()
    {
        player = FindAnyObjectByType<YutPlayer>();
        icon = GetComponent<SpriteRenderer>();
        enemyController = FindAnyObjectByType<EnemyController>();
    }

    //말들이 판 위로 올라올 때  세팅하는 함수
    public void OnBoardIn(bool isEnemyPiece)
    {
        if(carriedChar!=null)
        {
            carriedChar.Clear();
        }
        else
        {
            carriedChar = new List<YutPiace>();
        }

        isMovingOnBorad = true;
        isEnemy = isEnemyPiece;
        currentspace = -1;
        currentPathIndex = -1;
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
        if (this.isEnemy==true)
        {
            enemyController.currentActiveChar--;
            enemyController.isMaxChar = false;
        }
        else
        {
            PlayerManager.Instance.currentActiveChar--;
            PlayerManager.Instance.isMaxChar = false;
        }
            returnReady();
    }
    //잡히거나 골인 후 말이 돌아가는 내용
    public void returnReady()
    {
        if( BattleSceneManager.instance!=null||BattleSceneManager.instance.allActiveChar.Contains(this))
        {
            BattleSceneManager.instance.allActiveChar.Remove(this);
        }
        
        
        currentPathIndex = -1;
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

       
           

        isMoveing = true;
        
        //뒷도가 나왔을 경우
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
                        currentPathIndex = 3;
                    }
                    else if (PathState1 == PathState.spring)
                    {
                        PathState1 = PathState.main;
                        currentPathIndex = 8;
                    }
                    else if (PathState1 == PathState.autumn)
                    {
                        PathState1 = PathState.summer;
                        currentPathIndex = 2;
                    }



                }
            }
            steps = 1;

        }

        //매칸 나아갈때 길 확인
        for (int i = 0; i < steps; i++)
        {
           currentPathIndex++;

            Vector3Int nextSpace  = Vector3Int.zero;
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
                    else
                    {
                        player.GoalIn(this);
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
                        maxCount = borad.shortCutAutumn.Count;
                        int overCount = currentPathIndex - maxCount;

                        PathState1 = PathState.main;
                        currentPathIndex = 19 + overCount;
                        if(currentPathIndex>=maxCount)
                        {
                            player.GoalIn(this);
                        }
                        else
                        {
                            maxCount = borad.mainPathSpace.Count;
                            nextSpace = borad.mainPathSpace[currentPathIndex];
                        }
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
                        maxCount = borad.shortCutSpring.Count;
                        int overCount = currentPathIndex - maxCount;

                        PathState1 = PathState.main;
                        currentPathIndex = 19 + overCount;
                        if (currentPathIndex >= maxCount)
                        {
                            player.GoalIn(this);
                        }
                        else
                        {
                            maxCount = borad.mainPathSpace.Count;
                            nextSpace = borad.mainPathSpace[currentPathIndex];
                        }
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
                         maxCount = borad.shortCutSummer.Count;
                        int overCount = currentPathIndex - maxCount;

                        PathState1 = PathState.main;
                        currentPathIndex = 14 + overCount;

                        maxCount = borad.mainPathSpace.Count;
                        nextSpace = borad.mainPathSpace[currentPathIndex];
                        
                    }
                    break;

                  
            }


            // 골인했을 경우
          if(currentPathIndex>=maxCount)
            {
                currentPathIndex = maxCount-1;

                player.GoalIn(this);
                yield break;
            }

         
          // 타일맵 좌표를 월드 좌표로 바꾸는 작업
            Vector3 targetWorldPosition = borad.GetWorldPosition(nextSpace);

            while (Vector3.Distance(transform.position, targetWorldPosition) > 0.02)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, Time.deltaTime * 6f);
                yield return null;
            }

            transform.position = targetWorldPosition;
            yield return new WaitForSeconds(0.1f);
        }
        //지름길로 들어가는 작업
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
            if( currentPathIndex ==3)
            {
                PathState1 = PathState.autumn;
                currentPathIndex = 0;
            }
        }

        EndMove(this, currentPathIndex);
        isMoveing =false;
        if(BattleSceneManager.instance.IsMyTurn == false)
        {
            enemyController.CharMoveEnd = true;
        }
      

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

    // 말 선택하기 (이거 문제 심각)
    public void OnMouseDown()
    {

        Debug.Log("말 잡기 실행은 되는중");
        var manger = BattleSceneManager.instance;

        if (manger.isYutSelected == false)
        { return; }
        if (manger.IsMyTurn == false)
        { return; }
        if (this.isEnemy == true)
        { return; }
        if(this.isMoveing ==true)
            { return; }

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
