
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum monState
{
    nomal,
    skill_hp70,
    skill_hp50,
    skill_hp30,
}






public class EnemyController : YutPlayer
{
    public int CurrentEnemy;
    public TrideDataManager trideDM;
    protected YutPiace yutcount;
   
    
    
    

    [SerializeField] protected Image Icon;
    [SerializeField] protected Image CharacterIcon;
    [SerializeField] protected TextMeshProUGUI maxCharacter;
    [SerializeField] protected TextMeshProUGUI Hpbar;
    [SerializeField] protected Scrollbar HP;

    public bool IsEnemyTurn = false;
    public bool findenemy;

    public List<YutPiace> EnemyGroup = new List<YutPiace>();

    protected override void Start()
    {
        base.Start();
        BattleSceneManager.instance.CuttrentEnemy = this;
         maxChar = trideDM.TrideList[CurrentEnemy].maxCharacter;
    }

    protected void SetEnemy(int i)
    {
        Icon.sprite = trideDM.TrideList[i].icon;
        CharacterIcon.sprite = trideDM.TrideList[i].icon;
        maxCharacter.text = trideDM.TrideList[i].maxCharacter.ToString();
        Hpbar.text = $"{trideDM.TrideList[i].hp}/{trideDM.TrideList[i].maxHp}";
        BattleSceneManager.instance.enemy = this;
    }

    public void Hpeffect(int i)
    {
        HP.value = trideDM.TrideList[i].hp / trideDM.TrideList[i].maxHp;
        Hpbar.text = $"{trideDM.TrideList[i].hp}/{trideDM.TrideList[i].maxHp}";
    }

  public virtual void EnemyTurn()
    {
        if (IsEnemyTurn == true)
        {
            StartCoroutine(EnemyTurnRoutine());
        }
    }
    public IEnumerator EnemyTurnRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        BattleSceneManager.instance.ThrowYut();

        yield return new WaitForSeconds(1.5f);
        EnemyBestMove();

        yield return new WaitForSeconds(1.5f);
        BattleSceneManager.instance.TurnEnd();

    }
    

    public void EnemyBestMove()
    {
        int bestCharIndex;
        int bestYutIndex;

        if(TryUseSkill())// 나중에 연출 필요시 밑에 중괄호 만들어서 연출 넣기 ex) 스킬 대사 혹은 스킬 사용 텍스트
       
        if(CanGoalIn(out bestCharIndex, out bestYutIndex) == true)
        {
            MoveEnemy(bestCharIndex, bestYutIndex);
            GoalIn();
            return;
        }
        if (CanCatchPlayer(out bestCharIndex, out bestYutIndex) == true)
        {
            MoveEnemy(bestCharIndex, bestYutIndex);
            return;
        }
        if (CanCarrieAlly(out bestCharIndex, out bestYutIndex) == true)
        {
            MoveEnemy(bestCharIndex, bestYutIndex);
            return;
        }
        if (CanShotCut(out bestCharIndex, out bestYutIndex) == true)
        {
            MoveEnemy(bestCharIndex, bestYutIndex);
            return;
        }
        

        DefultMoveEnemy();
    }

    public bool CanCatchPlayer(out int bestCharIndex, out int bestYutIndex)
    {
       return CanTargetPicce(findenemy: true, out bestCharIndex, out bestYutIndex);
    }

    public bool CanCarrieAlly(out int bestCharIndex, out int bestYutIndex)
    {
       return CanTargetPicce(findenemy: false, out bestCharIndex, out bestYutIndex);
    }

    public bool CanShotCut(out int bestCharIndex, out int bestYutIndex)
    {
        bestCharIndex = -1;
        bestYutIndex = -1;

        var BSMYutList = BattleSceneManager.instance.TurnYutResult;
        var BSMActiveChar = BattleSceneManager.instance.allActiveChar;
        //외곽 코너들 순서
        int[] shouCutTile = new int[] { 5, 10 };
        // 내곽 코너 위치
        int shoutcutpoint = 2;

        // 윷 나운 순서의 거리
        for (int y = 0; y < BSMYutList.Count; y++)
        {
            int moveAmount = GetYutMoveCount(BSMYutList[y]);
            // 말의 움직임 검사
            for (int c = 0; c < BSMActiveChar.Count; c++)
            {
                var mypiece = BSMActiveChar[c];

                if (mypiece.isCarried || !mypiece.isMovingOnBorad || mypiece.currentPathIndex <= 0 || !mypiece.isEnemy)
                { continue; }
                int nextPosion = mypiece.currentPathIndex + moveAmount;
                if (mypiece.PathState1 == PathState.main)
                { 
                     foreach (int cornerTile in shouCutTile)
                     {
                        if(nextPosion == cornerTile)
                        {
                            bestCharIndex = c;
                            bestYutIndex = y;
                            return true;
                        }
                      }
                }
                else if( mypiece.PathState1 != PathState.main)
                {
                    if(nextPosion ==shoutcutpoint)
                    {
                        bestCharIndex = c;
                        bestYutIndex = y;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool CanGoalIn(out int bestCharIndex, out int bestYutIndex)
    {
        bestCharIndex = -1;
        bestYutIndex = -1;

        var BSMYutList = BattleSceneManager.instance.TurnYutResult;
        var BSMActiveChar = BattleSceneManager.instance.allActiveChar;

        // 윷 나운 순서의 거리
        for (int y = 0; y < BSMYutList.Count; y++)
        {
            int moveAmount = GetYutMoveCount(BSMYutList[y]);
            // 잡을 수 있는 말이 있나 검사
            for (int c = 0; c < BSMActiveChar.Count; c++)
            {
                var mypiece = BSMActiveChar[c];

                if (mypiece.isCarried || !mypiece.isMovingOnBorad || mypiece.currentPathIndex <= 0 || !mypiece.isEnemy)
                { continue; }
                int nextPosion = mypiece.currentPathIndex + moveAmount;
               
                if(mypiece.CheckGoalIn(moveAmount))
                {
                    bestCharIndex = c;
                    bestYutIndex = y;
                    return true;
                }
            }
        }
        return false;
    }

    public monState checkUesSkill(float currentHp ,float MaxHp )
    {
        float Hpgage = (currentHp / MaxHp) * 100f;

        if(Hpgage<=30f&&!useedSkill30)
        {
            return monState.skill_hp30;
        }
        if(Hpgage<=50f&&!useedSkill50)
        {
            return monState.skill_hp50;
        }
        if(Hpgage<=70f&&!useedSkill70)
        {
            return monState.skill_hp70;
        }

        return monState.nomal;
    }


    public bool TryUseSkill()
    {
        monState skilltouse = checkUesSkill(trideDM.TrideList[CurrentEnemy].hp, trideDM.TrideList[CurrentEnemy].maxHp);

        if(skilltouse == monState.nomal)
            { return false; }

        if(trideDM.TrideList[CurrentEnemy] is canSkill enemySkill)
        switch(skilltouse)
        {
            case monState.skill_hp70:
                useedSkill70 = true;
                enemySkill.UseSkill70(PlayerManager.Instance.PlayerData.block, trideDM.TrideList[CurrentEnemy].luck);
                break;
            case monState.skill_hp50:
                useedSkill50 = true;
                    enemySkill.UseSkill50(PlayerManager.Instance.PlayerData.block, trideDM.TrideList[CurrentEnemy].luck);
                break;
            case monState.skill_hp30:
                useedSkill30 = true;
                    enemySkill.UseSkill30(PlayerManager.Instance.PlayerData.block, trideDM.TrideList[CurrentEnemy].luck);
                break;
        }
        return true;
    }
    public void MoveEnemy(int bestCharIndex, int bestYutIndex)
    {
        var BSMYutList = BattleSceneManager.instance.TurnYutResult;
        var BSMActiveChar = BattleSceneManager.instance.allActiveChar;

        Yut selectYut = BSMYutList[bestYutIndex];
        int moveCount = GetYutMoveCount(selectYut);

        BSMYutList.RemoveAt(bestYutIndex);

        YutPiace movechar = BSMActiveChar[bestCharIndex];
        StartCoroutine(movechar.MoveStepRoutine(moveCount));

        BattleSceneManager.instance.RemoveYutUi(bestYutIndex);
    }
    public void DefultMoveEnemy()
    {
        var BSMYutList = BattleSceneManager.instance.TurnYutResult;
        var BSMActiveChar = BattleSceneManager.instance.allActiveChar;

        if (BSMYutList.Count == 0)
        { return; }

        Yut selectYut = BSMYutList[0];
        int moveCount = GetYutMoveCount(selectYut);

        BSMYutList.RemoveAt(0);

        List<YutPiace>activeEnemys = new List<YutPiace>();
        foreach(var enemy in BSMActiveChar)
        {
            if( enemy.isEnemy&&enemy.isMovingOnBorad&&!enemy.isCarried)
            {
                activeEnemys.Add(enemy);
            }
        }

        if(activeEnemys.Count > 0)
        {
            StartCoroutine(activeEnemys[0].MoveStepRoutine(moveCount));
        }
        else
        {
            StartNewChar(moveCount,true);
        }

        BattleSceneManager.instance.RemoveYutUi(0);
    }

  

    public int GetYutMoveCount(Yut yut)
    {
        switch(yut)
        {
                case Yut.back: return -1;
                case Yut.one: return 1;
                case Yut.two: return 2;
                case Yut.three: return 3;
                case Yut.four: return 4;
                case Yut.five: return 5;
            default: return 0;
        }

    }

    private bool CanTargetPicce(bool findenemy, out int bestCharIndex, out int bestYutIndex)
    {
        bestCharIndex = -1;
        bestYutIndex = -1;

        var BSMYutList = BattleSceneManager.instance.TurnYutResult;
        var BSMActiveChar = BattleSceneManager.instance.allActiveChar;

        // 윷 나운 순서의 거리
        for (int y = 0; y < BSMYutList.Count; y++)
        {
            int moveAmount = GetYutMoveCount(BSMYutList[y]);
            // 잡을 수 있는 말이 있나 검사
            for (int c = 0; c < BSMActiveChar.Count; c++)
            {
                var mypiece = BSMActiveChar[c];

                if (mypiece.isCarried || !mypiece.isMovingOnBorad || mypiece.currentPathIndex <= 0 || !mypiece.isEnemy)
                { continue; }
                int nextPosion = mypiece.currentPathIndex + moveAmount;
                PathState nextpathState = mypiece.PathState1;
                foreach (YutPiace TargetPiace in BSMActiveChar)
                {
                    if (TargetPiace == mypiece|| TargetPiace.isCarried || !TargetPiace.isMovingOnBorad) continue;

                    bool isTargetvalid = findenemy ? (!TargetPiace.isEnemy) : (TargetPiace.isEnemy);


                    if (isTargetvalid && TargetPiace.currentPathIndex == nextPosion&&TargetPiace.PathState1 == nextpathState)
                    {
                        bestCharIndex = c;
                        bestYutIndex = y;
                        return true;
                    }
                }
            }
        }
        return false;
    }

  
    
}
