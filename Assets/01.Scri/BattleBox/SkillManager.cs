using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [SerializeField] public TextMeshProUGUI count;
    [SerializeField] public Button SkillB;
    [SerializeField] public TextMeshProUGUI skillText;
    [SerializeField] public TextMeshProUGUI skill;

    public int skillCount = 3;

    YutPiace yutPiace;
    EnemyController enemyController;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
        skill.text = "";
        skillText.text = "";
        skillText.gameObject.SetActive(false);
        count.text=$"{skillCount}";
        enemyController = FindAnyObjectByType<EnemyController>();
        yutPiace = FindAnyObjectByType<YutPiace>();
    }

    

    //휴먼 스킬 방어력이 증가 (한턴동안) / 적은 3턴동안
    public void HumenSkill(YutPlayer caster, int defence , int turnCont)
    {
        SoundManager.instance.PlaySFX("심장소리");
        skill.text = $"{turnCont}";
        float hpbar = caster.GetHpVaule();

        int finalDefence = defence;
        if(hpbar <= 0.3f)
        {
            finalDefence *= 5;
        }
        else if(hpbar <= 0.5f)
        {
            finalDefence *= 3;
        }
        else if (hpbar <=0.7f)
        {
            finalDefence *= 2;
        }

        caster.ApplyDefence(finalDefence, turnCont);
        

    }
    //고블린 스킬 잡았을 때 일정 확률로 적의 최대 말 갯수를 줄이고 나의 최대말을 늘림 / 적은 처음은 30프로 두번째는 50프로 3번째는 70프로 확률로 훔침
    public void GoblinSkill(YutPlayer caster)
    {
        SoundManager.instance.PlayVoice("사악한웃음");
        skill.text = "가능";
       caster.isGoblinSkillUsed = true;
        
    }
    //엘프스킬 사거리 칸 안에 적을 제거함 / 적은 가장 많이 업고 있는 적을 제거
    public bool isWaitingForElfSkillTarget = false;
    public int currentElfSkillRange = 0;
    public void ElfSkill(YutPlayer player,YutPiace caster, int skillRange)
    {
        SoundManager.instance.PlayVoice("앙대");

        if (player == PlayerManager.Instance)
        {
            OnClickElfskill(PlayerManager.Instance.PlayerData.length);
         
        }
        else if(player == enemyController)
        {
            YutPiace absoluteBestTarget = null;
            int maxBsetCount = 0;

            foreach(var mypiece in enemyController.EnemyGroup)
            {
                if (CanUseElfSkill(false, enemyController.enemyData.length, caster.currentPathIndex, caster.PathState1, out YutPiace bestTarget, out int bestcount))
                {
                    if( bestcount > maxBsetCount||(bestcount==maxBsetCount&&absoluteBestTarget != null && bestTarget.currentPathIndex>absoluteBestTarget.currentPathIndex))
                    {
                        maxBsetCount = bestcount;
                        absoluteBestTarget = bestTarget;
                    }
                    
                }
            }
            if(absoluteBestTarget != null)
            {
                CatchAllOnTile(absoluteBestTarget);
            }

        }
       
    }

  
    //언데드 스킬  잡았을 때 일정 확률로 업은 말의 수 +1 / 적은 패시브로 발동하며 확률이 잃은 체력 비례해서 증가 예정
    public void UndeadSkill(YutPlayer caster)
    {
        SoundManager.instance.PlayVoice("사악한웃음");
        skill.text = "가능";
        caster.isUndeadSkillUsed = true;
    }
    //천사스킬 잡혔을 때 잡히면 일정 확률로 부활하여 반격해서 역으로 잡음 / 패시브로 반격하며 잃은 체력 비례해서 증가할 예정
    public void AngelSkill(YutPiace target)
    {
        SoundManager.instance.PlaySFX("심장소리");
        target.isAngelCounterActive = true;
        target.counterTurns = 3;
        skill.text = $"{target.counterTurns}";
    }




    public void OnClickElfskill(int range)
    {
        isWaitingForElfSkillTarget = true;
        currentElfSkillRange = range;

    }

    public bool CanUseElfSkill(bool findenemy, int skillRange, int casterPathIndex, PathState casterPathState, out YutPiace bestTarget, out int bestCount)
    {
        bestTarget = null;
        bestCount = 0;

        var allChar = BattleSceneManager.instance.allActiveChar;

        foreach (var piace in allChar)
        {
            if (piace.isCarried || piace.isMoveing) continue;

            bool isTargetVaild = findenemy ? piace.isEnemy : !piace.isEnemy;
            if (!isTargetVaild) continue;

            int distance = Mathf.Abs(piace.currentPathIndex - casterPathIndex);
            if (distance > skillRange) continue;

            int count = 1 + piace.carriedChar.Count;
            if (count > bestCount||(count == bestCount&&bestTarget !=null&&piace.currentPathIndex>bestTarget.currentPathIndex))
            {
                bestCount = count;
                bestTarget = piace;
            }
        }
        return bestTarget != null;
    }

    public void CatchAllOnTile(YutPiace target)
    {
        if (target.carriedChar != null)
        {
            foreach (YutPiace kid in new List<YutPiace>(target.carriedChar))
            {
                if (kid != null)
                {
                    kid.CatchChar(yutPiace);
                }

            }
            target.carriedChar.Clear();

            target.CatchChar(yutPiace);
        }
    }


    public IEnumerator Textfadeinout()
    {
        skillText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f) ;
        skillText.gameObject.SetActive(false);

    }

}
