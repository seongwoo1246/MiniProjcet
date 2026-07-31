using UnityEngine;

public class Goblin : EnemyController, canSkill
{
    protected override void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.goblin);
        base.Start();
        CurrentEnemy = 1;
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
            SkillManager.instance.enemyskill.text = "약탈시작";
            SoundManager.instance.PlaySFX("뿌뿌");
            isGoblinSkillUsed =true;
            SkillManager.instance.skillText.text = "적이 지금부터 일정 확률로 약탈을 시전합니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

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
            SkillManager.instance.enemyskill.text = "약탈시작";
            SoundManager.instance.PlaySFX("뿌뿌");
            isGoblinSkillUsed = true;
            finalprecent += 0.05f;
            SkillManager.instance.skillText.text = "적이 화난 듯합니다. 확률이 올라갑니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

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
            SkillManager.instance.enemyskill.text = "약탈시작";
            SoundManager.instance.PlaySFX("뿌뿌");
            isGoblinSkillUsed = true;
            finalprecent += 0.1f;
            SkillManager.instance.skillText.text = "현재 적의 눈에 보이는게 없는 광분 상태입니다. 조심하세요.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());


        }

    }

    public override void DeadMob()
    {
       
        if (enemyData.hp <= 0)
        {
            SoundManager.instance.PlayVoice("마린죽는소리");
            PlayerManager.Instance.UnLockedList(2);

            switch (PlayerManager.Instance.PlayerData.id)
            {
                case 0: PlayerManager.Instance.UnLockedList(1000); break;
                case 1:  break;
                case 2: PlayerManager.Instance.UnLockedList(1004); break;
                case 3: PlayerManager.Instance.UnLockedList(1005); break;
                case 4: PlayerManager.Instance.UnLockedList(1006); break;
                
            }
        }
        
        
    }


}

