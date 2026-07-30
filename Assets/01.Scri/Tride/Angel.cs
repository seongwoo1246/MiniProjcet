using UnityEngine;

public class Angel: EnemyController, canSkill
{
    protected override void Start( )    
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.angel);
        base.Start();
        CurrentEnemy = 4;
        SetEnemy(CurrentEnemy);
        BattleSceneManager.instance.ItbattleSet();
    }
    public override void GoalIn(YutPiace targetPiace)
    {
        var mydata = enemyData;
        var player = PlayerManager.Instance.PlayerData;
        var BSM = BattleSceneManager.instance;

        int totalcount = 1;
        if (targetPiace != null && targetPiace.carriedChar != null)
        {
            totalcount += targetPiace.carriedChar.Count;
        }


        BSM.TakeDamage(player, player.miss
          , BSM.countDamageUp(BSM.Attack(mydata.critical,
            mydata.damage), totalcount), player.depence);
        PlayerManager.Instance.playerHpeffect();
        BSM.Heal(mydata, mydata.heal);
        Hpeffect(CurrentEnemy);
        PlayerManager.Instance.playerHpeffect();
        base.GoalIn(targetPiace);
    }

    public void UseSkill70(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            SoundManager.instance.PlaySFX("쨍그랑");
            SkillManager.instance.skillText.text = "적의 스킬을 방해했습니다. 야호(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            return;
            
        }
        else
        {
            SoundManager.instance.PlaySFX("뿌뿌");
            SkillManager.instance.skillText.text = "적이 지금부터 일정 확률로 반격을 시작합니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

            yutcount.isAngelCounterActive = true;
            yutcount.counterTurns = 999;
        }

    }
    public void UseSkill50(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            SoundManager.instance.PlaySFX("쨍그랑");
            SkillManager.instance.skillText.text = "적의 스킬을 방해했습니다. 야호(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            return;
        }
        else
        {
            SoundManager.instance.PlaySFX("뿌뿌");
            SkillManager.instance.skillText.text = "적이 빛나기 시작합니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            yutcount.isAngelCounterActive = true;
            yutcount.counterTurns = 999;
            finalprecent = 0.05f;
            
        }

    }
    public void UseSkill30(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            SoundManager.instance.PlaySFX("쨍그랑");
            SkillManager.instance.skillText.text = "적의 스킬을 방해했습니다. 야호(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            return;
        }
        else
        {
            SoundManager.instance.PlaySFX("뿌뿌");
            SkillManager.instance.skillText.text = "적의 모습이 심상치 않습니다. 주의하세요. ";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            yutcount.isAngelCounterActive = true;
            yutcount.counterTurns = 999;
            finalprecent = 0.1f;
            
        }

    }

    public override void DeadMob()
    {
        SoundManager.instance.PlayVoice("매딕죽는소리");
        if (enemyData.hp <= 0)
        {
            PlayerManager.Instance.CanAttackLastBoss = true;

            switch (PlayerManager.Instance.PlayerData.id)
            {
                case 0: PlayerManager.Instance.UnLockedList(1003); break;
                case 1: PlayerManager.Instance.UnLockedList(1006); break;
                case 2: PlayerManager.Instance.UnLockedList(1008); break;
                case 3: PlayerManager.Instance.UnLockedList(1009); break;
                case 4:  break;

            }
        }


    }


}

