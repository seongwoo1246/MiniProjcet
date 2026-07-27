
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
    public bool CharMoveEnd=false;
    public Tride enemyData { get; protected set; }

    public List<YutPiace> EnemyGroup = new List<YutPiace>();

    protected override void Start()
    {
        base.Start();
        BattleSceneManager.instance.CuttrentEnemy = this;
        PlayerManager.Instance.enemyController = this;
         maxChar = trideDM.TrideList[CurrentEnemy].maxCharacter;

        
    }

    private void ApplyDifficulty(Difficulty difficulty)
    {
        switch(difficulty)
        {
            case Difficulty.easy:
                enemyData.maxHp *= 1.5f;
                enemyData.hp *= 1.5f;
                enemyData.damage += 50;
                enemyData.depence += 5;
                enemyData.critical += 0.05f;
                enemyData.length += 1;
                enemyData.kidnap += 0.05f;
                enemyData.infection += 0.05f;
                enemyData.rivival += 0.05f;
                break;

                case Difficulty.normal:
                enemyData.maxHp *= 3;
                enemyData.hp *= 3;
                enemyData.damage += 100;
                enemyData.depence += 10;
                enemyData.critical += 0.1f;
                enemyData.length += 1;
                enemyData.kidnap += 0.05f;
                enemyData.infection += 0.05f;
                enemyData.rivival += 0.05f;
                break;

            case Difficulty.hard:
                {
                    enemyData.maxHp *= 5;
                    enemyData.hp *= 5;
                    enemyData.damage += 200;
                    enemyData.depence += 20;
                    enemyData.critical += 0.2f;
                    enemyData.length += 2;
                    enemyData.kidnap += 0.15f;
                    enemyData.infection += 0.15f;
                    enemyData.rivival += 0.15f;
                    break;
                }
        }
    }

    protected void SetEnemy(int i)
    {
        enemyData = trideDM.TrideList[i].Clone();
        ApplyDifficulty(ScenesM.instance.SelectedDifficulty);
        Icon.sprite = enemyData.icon;
        CharacterIcon.sprite = enemyData.icon;
        maxCharacter.text = enemyData.maxCharacter.ToString();
        Hpbar.text = $"{enemyData.hp}/{enemyData.maxHp}";
        BattleSceneManager.instance.enemy = this;
       
    }

    public void Hpeffect(int i)
    {
        HP.size = enemyData.hp / enemyData.maxHp;
        Hpbar.text = $"{enemyData.hp}/{enemyData.maxHp}";
    }

   private IEnumerator moveEndGoalIn(int bestCharIndex)
    {
        yield return new WaitUntil(() => CharMoveEnd == true);
        YutPiace target = BattleSceneManager.instance.allActiveChar[bestCharIndex];
        GoalIn(target);
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
        
        var BSM = BattleSceneManager.instance;
        if(BSM == null)
        {
            BSM =FindAnyObjectByType<BattleSceneManager>();
        }

        while (BSM.CanThrowEnemy)
        {

            while (BSM.CanThrowEnemy)
            {
                yield return new WaitForSeconds(1.5f);
                BSM.ThrowYut();
                yield return new WaitForSeconds(1.5f);
            }


            while (BSM.TurnYutResult.Count > 0)
            {
                CharMoveEnd = false;
                yield return new WaitForSeconds(1.5f);
                EnemyBestMove();
                yield return new WaitForSeconds(1f);
                float timeOut = 2.0f;

                while (!CharMoveEnd && timeOut > 0)
                {
                    timeOut -= Time.deltaTime;
                    yield return null;
                }
            }

        }

        yield return new WaitForSeconds(1.5f);
        BSM.TurnEnd();
        yield break;

    }
    

    public void EnemyBestMove()
    {
        int bestCharIndex;
        int bestYutIndex;
     
       

        if (TryUseSkill())
        { }

        if (CanGoalIn(out bestCharIndex, out bestYutIndex) == true)
        {

            if (bestCharIndex == -1)
            { ifNewStart(bestCharIndex, bestYutIndex); return; }
            else
            {
                MoveEnemy(bestCharIndex, bestYutIndex);
                StartCoroutine(moveEndGoalIn(bestCharIndex));
            }
               
            return;
        }
         if (CanCatchPlayer(out bestCharIndex, out bestYutIndex) == true)
        {
            if(bestCharIndex == -1)
            { ifNewStart(bestCharIndex, bestYutIndex); return; }
            else
            {
                MoveEnemy(bestCharIndex, bestYutIndex);
                YutPiace target = BattleSceneManager.instance.allActiveChar[bestCharIndex];
                BattleSceneManager.instance.checkCatchChar(target);
                return;
            }
                
        }
         if (CanCarrieAlly(out bestCharIndex, out bestYutIndex) == true)
        {
            if (bestCharIndex == -1)
            { ifNewStart(bestCharIndex, bestYutIndex); return; }
            else
            {
                MoveEnemy(bestCharIndex, bestYutIndex);
                YutPiace target = BattleSceneManager.instance.allActiveChar[bestCharIndex];
                BattleSceneManager.instance.checkCatchChar(target);
                return;
            }
        }
         if (CanShotCut(out bestCharIndex, out bestYutIndex) == true)
        {
            if (bestCharIndex == -1)
            { ifNewStart(bestCharIndex, bestYutIndex); return; }
            else
            {
                MoveEnemy(bestCharIndex, bestYutIndex);
                return;
            }
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
        bestCharIndex = -2;
        bestYutIndex = -1;

        var BSMYutList = BattleSceneManager.instance.TurnYutResult;
        var BSMActiveChar = BattleSceneManager.instance.allActiveChar;
        //외곽 코너들 순서
        int[] shouCutTile = new int[] { 4, 9 };
        // 내곽 코너 위치
        int shoutcutpoint = 3;

        // 윷 나운 순서의 거리
        for (int y = 0; y < BSMYutList.Count; y++)
        {
            int moveAmount = GetYutMoveCount(BSMYutList[y]);
            // 말의 움직임 검사
            for (int c = 0; c < BSMActiveChar.Count; c++)
            {
                var mypiece = BSMActiveChar[c];

                if (mypiece.isCarried ==true ||  mypiece.isEnemy ==false )
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
        if (currentActiveChar < maxChar)
        {
            for (int j = 0; j < BSMYutList.Count; j++)
            {
                int moveCount = GetYutMoveCount(BSMYutList[j]);
                foreach (int cornerTile in shouCutTile)
                {
                    if (moveCount == cornerTile)
                    {
                        bestCharIndex = -1;
                        bestYutIndex = j;
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

                if (mypiece.isCarried == true ||   mypiece.isEnemy == false )
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
        monState skilltouse = checkUesSkill(enemyData.hp, enemyData.maxHp);

        if(skilltouse == monState.nomal)
            { return false; }

        if(enemyData is canSkill enemySkill)
        switch(skilltouse)
        {
            case monState.skill_hp70:
                useedSkill70 = true;
                enemySkill.UseSkill70(PlayerManager.Instance.PlayerData.block, enemyData.luck);
                break;
            case monState.skill_hp50:
                useedSkill50 = true;
                    enemySkill.UseSkill50(PlayerManager.Instance.PlayerData.block, enemyData.luck);
                break;
            case monState.skill_hp30:
                useedSkill30 = true;
                    enemySkill.UseSkill30(PlayerManager.Instance.PlayerData.block, enemyData.luck);
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
        BattleSceneManager.instance.RemoveYutUi(selectYut);

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
            if (enemy.isEnemy == true&&enemy.isMovingOnBorad == true&&enemy.isCarried == false)
            {
                activeEnemys.Add(enemy);
            }
        }
        if (activeEnemys.Count == 0)
        {

            StartNewChar(moveCount, true);
        }
        else
        {
            StartCoroutine(activeEnemys[0].MoveStepRoutine(moveCount));
        }


            BattleSceneManager.instance.RemoveYutUi(selectYut);
        CharMoveEnd = true;
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
        bestCharIndex = -2;
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
                //잡을 상대 말 검사
                var mypiece = BSMActiveChar[c];

                if (mypiece.isCarried == true || mypiece.isEnemy == false )
                { continue; }
                int nextPosion = mypiece.currentPathIndex + moveAmount;
                PathState nextpathState = mypiece.PathState1;
                foreach (YutPiace TargetPiace in BSMActiveChar)
                {
                    if (TargetPiace == mypiece || TargetPiace.isCarried )
                    { continue; }

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
        if (currentActiveChar < maxChar)
        {
            for (int j = 0; j < BSMYutList.Count; j++)
            {
                if (canCatchOrAllyForStart(BSMYutList[j]))
                {
                    bestCharIndex = -1;
                    bestYutIndex = j;
                    return true;
                }
            }
        }
        return false;
    }

  public bool canCatchOrAllyForStart(Yut yut)
    {
        int moveCount = GetYutMoveCount(yut);
        int nextposition = moveCount-1;
        foreach(YutPiace target in BattleSceneManager.instance.allActiveChar)
        {
            if(target.isCarried||target.currentPathIndex<=0) continue;
            if (target.currentPathIndex ==nextposition&&target.PathState1==PathState.main)
            {
                return true;
            }
        }
        return false;
    }

    private void ifNewStart(int character , int yut)
    {
      
        int moveCount = GetYutMoveCount(BattleSceneManager.instance.TurnYutResult[yut]);
        
        if(character == -1)
        {
            StartNewChar(moveCount,true);
            var targetYut = BattleSceneManager.instance.TurnYutResult[yut];
            BattleSceneManager.instance.TurnYutResult.RemoveAt(yut);
            BattleSceneManager.instance.RemoveYutUi(targetYut);
            
           
        }
        else
        {
            MoveEnemy(character,yut);
        }
    }

    
}
