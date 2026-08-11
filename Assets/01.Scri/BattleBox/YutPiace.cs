using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Photon.Pun;


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
    public YutPlayer player;


    public PathState PathState1 = PathState.main;
    
    public int currentPathIndex = 0;
   


    public bool isMoveing = false;
    public bool isEnemy = false;
    public bool isMovingOnBorad = false;
    public bool isCarried = false;
    public bool isReturned = false;


    //멀티 관련 코드
    public int priceID;
    public int ownerActorNo;

    public List<YutPiace>carriedChar = new List<YutPiace>();

    private void Awake()
    {
        
        icon = GetComponent<SpriteRenderer>();
        if(enemyController  != null )
        {
            enemyController = FindAnyObjectByType<EnemyController>();
        }
       
    }

    public void Init(YutPlayer ownerPlayer)
    {
        this.player = ownerPlayer;
    }


    public string GetmyPoolName()
    {
        if (player != null)
        {
            return player.GetCharPoolName();
        }
        else if (enemyController != null)
        {
            return enemyController.GetCharPoolName1();
        }

        return gameObject.name.Replace("(Clone)","").Trim();
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
       this.isEnemy = isEnemyPiece;
       
        currentPathIndex = -1;
        PathState1 = PathState.main;
        isCarried = false;
        isReturned = false;
    }


    //업은 말 상태 표시
    public void UpdateVisuals()
    {
        if(TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
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
        
        
    }
    //말이 잡혔을 때 하는 코드
    public void CatchChar(YutPiace attacker)
    {
        if (attacker == null) return;

      

        SoundManager.instance.PlaySFX("한입");
        bool iscountered = this.Counter(attacker);

       if(iscountered)
        {
            SoundManager.instance.PlayVoice("이거너무");
            attacker.returnReady();
        }
        else
        {
            this.returnReady();
        }


        if (this.isEnemy == true)
        {
            enemyController.currentActiveChar--;
            enemyController.isMaxChar = false;
        }
        else
        {
            PlayerManager.Instance.currentActiveChar--;
            PlayerManager.Instance.isMaxChar = false;
        }
            
    }
    //잡히거나 골인 후 말이 돌아가는 내용
    public void returnReady()
    {
        if (isReturned) return;
        isReturned = true;
        string poolname = GetmyPoolName();

        if(this ==null||gameObject==null) return;
        if(gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
        }
       
        SoundManager.instance.PlayVoice("뚝배기");
        if ( BattleSceneManager.instance!=null&&BattleSceneManager.instance.allActiveChar.Contains(this))
        {
            BattleSceneManager.instance.allActiveChar.Remove(this);
        }
        if ( enemyController !=null&&enemyController.EnemyGroup.Contains(this))
        {
            enemyController.EnemyGroup.Remove(this);
        }
        
        enemyController.EnemyGroup.Remove(this);
        currentPathIndex = -1;
        isMovingOnBorad = false;
        isCarried = false;
        carriedChar.Clear();
        UpdateVisuals();
        transform.position = new Vector3(-39, -1,0);
        ObjectPooling.instance.ReturnObject(poolname, this.gameObject);
        
      
        

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
        if (steps == -1)
        {
            SoundManager.instance.PlaySFX("폭팔");
            if (PathState1 == PathState.main && currentPathIndex > 0)
            {
                currentPathIndex--;


            }
            else if (PathState1 == PathState.main && currentPathIndex == 0)
            {
                SoundManager.instance.PlayVoice("사악한웃음");
                currentPathIndex = 19;

            }
            else
            {

                if (currentPathIndex == 0)
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
                else
                {
                    if (PathState1 == PathState.summer)
                    {
                        PathState1 = PathState.summer;
                        currentPathIndex--;
                    }
                    else if (PathState1 == PathState.spring)
                    {
                        PathState1 = PathState.spring;
                        currentPathIndex--;
                    }
                    else if (PathState1 == PathState.autumn)
                    {
                        PathState1 = PathState.autumn;
                        currentPathIndex--;
                    }
                }
            }

            
             steps = 0; 
               

        }

        // 이동

        int targetIndex = currentPathIndex + steps;
        Vector3Int nextSpace = Vector3Int.zero;
        var borad = YutBoardController.instance;
        int maxCount = 0;

        if(targetIndex ==-1)
        {
            isMoveing = false;
            yield break;
        }

        switch (PathState1)
        {
            case PathState.main:
                maxCount = borad.mainPathSpace.Count;
                if (targetIndex < maxCount - 1)
                {
                    currentPathIndex = targetIndex;
                    nextSpace = borad.mainPathSpace[currentPathIndex];
                }

                else if (targetIndex == maxCount - 1)
                {
                    currentPathIndex = 19;
                    nextSpace = borad.mainPathSpace[currentPathIndex];
                }
                else
                {
                    if (CheckGoalIn(steps) == true)
                    {
                        SoundManager.instance.PlayVoice("좋아하는소리");
                        player.GoalIn(this);
                        yield break;
                    }

                }
                break;


            case PathState.autumn:
                maxCount = borad.shortCutAutumn.Count;
                if (targetIndex < maxCount)
                {
                    currentPathIndex = targetIndex;
                    nextSpace = borad.shortCutAutumn[currentPathIndex];
                }
                else if (targetIndex == maxCount)
                {



                    PathState1 = PathState.main;
                    currentPathIndex = 19;

                    nextSpace = borad.mainPathSpace[currentPathIndex];
                }
                else
                {
                    if (CheckGoalIn(steps) == true)
                    {
                        SoundManager.instance.PlayVoice("좋아하는소리");
                        player.GoalIn(this);
                        yield break;
                    }
                }
                break;

            case PathState.spring:
                maxCount = borad.shortCutSpring.Count;
                if (targetIndex < maxCount)
                {
                    currentPathIndex = targetIndex;
                    nextSpace = borad.shortCutSpring[currentPathIndex];
                }
                else if (targetIndex == maxCount)
                {



                    PathState1 = PathState.main;
                    currentPathIndex = 19;

                    nextSpace = borad.mainPathSpace[currentPathIndex];
                }
                else
                {

                    if (CheckGoalIn(steps) == true)
                    {
                        SoundManager.instance.PlayVoice("좋아하는소리");
                        player.GoalIn(this);
                        yield break;
                    }
                }
                break;

            case PathState.summer:
                maxCount = borad.shortCutSummer.Count;
                if (targetIndex < maxCount)
                {
                    currentPathIndex = targetIndex;
                    nextSpace = borad.shortCutSummer[currentPathIndex];
                }
                else if (targetIndex >= maxCount)
                {

                    int overCount = targetIndex - maxCount;

                    PathState1 = PathState.main;
                    currentPathIndex = 14 + overCount;


                    nextSpace = borad.mainPathSpace[currentPathIndex];

                }
                break;

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
            if (kid == null) continue;

            kid.currentPathIndex = finalSpace;
            kid.transform.position = leaderPiece.transform.position;
        }
        BattleSceneManager.instance.checkCatchChar(leaderPiece);
    }

    // 말 선택하기 
    public void OnMouseDown()
    {

        SoundManager.instance.PlaySFX("뽕");

        if (MultiYutGameManager.instance != null)
        {
            var multi = MultiYutGameManager.instance;
            if (PhotonNetwork.LocalPlayer.ActorNumber != multi.currentTurnPlayerActorNumber)
            { return; }
            if (ownerActorNo != PhotonNetwork.LocalPlayer.ActorNumber)
            { return; }

            
        }


        if (SkillManager.instance != null)
        {
            var skill = SkillManager.instance;

            if (skill.isWaitingForElfSkillTarget && !isEnemy)
            {
                skill.isWaitingForElfSkillTarget = false;
                if (skill.CanUseElfSkill(true, skill.currentElfSkillRange, this.currentPathIndex, this.PathState1, out YutPiace bestTarget, out int bestcount))
                {
                    skill.CatchAllOnTile(bestTarget);

                }
                return;
            }

            if (skill.isWaitingForAngelTarget && !isEnemy)
            {
                skill.AngelSkill(this);
                skill.isWaitingForAngelTarget = false;
            }
        }
        if (BattleSceneManager.instance != null)
        {
            var manger = BattleSceneManager.instance;

            if (manger.isYutSelected == false)
            { return; }
            if (manger.IsMyTurn == false)
            { return; }
            if (this.isEnemy == true)
            { return; }
            if (this.isMoveing == true)
            { return; }

            this.StartMove(manger.selectMoveSpace);
            manger.UseSelectedYut();
        }
    }

    // 골인하는지 체크
    public bool CheckGoalIn(int movecount)
    {
        int nextPosition =currentPathIndex + movecount;
        int maxCount = 0;
        switch (PathState1)
        {
            case PathState.main: maxCount = YutBoardController.instance.mainPathSpace.Count-1; break;

            case PathState.spring: maxCount = YutBoardController.instance.shortCutSpring.Count; break;

            case PathState.autumn: maxCount = YutBoardController.instance.shortCutAutumn.Count; break;

            default: maxCount = YutBoardController.instance.mainPathSpace.Count-1; break;
        }


        if (nextPosition > maxCount)
        {
            return true;
        }
        return false;
    }

    //----------------------------------------------------------------- 여기부터는 스킬 관련 함수들

    public bool isAngelCounterActive = false;
    public int counterTurns = 0;
    public float angelCountChance = 0;

    public void AngelTurnDisCount()
    {
        if(counterTurns == 0)
        {  return; }
        if (counterTurns > 0)
        {
            counterTurns--;
            
            
                SkillManager.instance.skill.text = $"{counterTurns}";
            

            if (counterTurns <= 0)
            {
                isAngelCounterActive = false;
                SkillManager.instance.skill.text = "0";

            }
        }
    }

    public bool Counter(YutPiace attacker)
    {
        if(isAngelCounterActive)
        {
            angelCountChance = player.AngelSkillPercent();
            if(Random.value < angelCountChance)
            {

                SkillManager.instance.skillText.text = "반격 성공했습니다. 야호(>.<)/*";
                SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
                BattleSceneManager.instance.countSuccess= true;
                isAngelCounterActive =false;
                counterTurns = 0;
                SkillManager.instance.skill.text = "불가능";
                return true;
            }
        }
        return false;
    }




}
