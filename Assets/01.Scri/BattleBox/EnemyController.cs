
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum monState
{
    nomal,
    belowHp70,
    belowHp50,
    belowHp30,
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

    private void Start()
    {
        BattleSceneManager.instance.CuttrentEnemy = this;
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
        if (CanUseSkill() == true)
        {
            BattleSceneManager.instance.UseSkill(PlayerManager.Instance.PlayerData.block, trideDM.TrideList[CurrentEnemy].luck);
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

    public bool CanUseSkill()
    {
        return false; // 나중에 상태 변화에 따라 넣을 예정
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
            StartNewChar(moveCount);
        }

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
                foreach (YutPiace TargetPiace in BSMActiveChar)
                {
                    if (TargetPiace == mypiece|| TargetPiace.isCarried || !TargetPiace.isMovingOnBorad) continue;

                    bool isTargetvalid = findenemy ? (!TargetPiace.isEnemy) : (TargetPiace.isEnemy);


                    if (isTargetvalid && TargetPiace.currentPathIndex == nextPosion)
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
