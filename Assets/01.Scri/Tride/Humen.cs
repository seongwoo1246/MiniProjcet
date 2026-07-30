using UnityEngine;

public class Humen : EnemyController, canSkill
{
    protected override void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.humen);
        base.Start();
        CurrentEnemy = 0;
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
            SkillManager.instance.HumenSkill(this, enemyData.depence, 3);
            SkillManager.instance.skillText.text = "적이 3턴간 단단해집니다.";
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
            SoundManager.instance.PlaySFX("뿌뿌");
            SkillManager.instance.HumenSkill(this, enemyData.depence, 3);
            SkillManager.instance.skillText.text = "적이 3턴간 더 단단해집니다.";
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
            SoundManager.instance.PlaySFX("뿌뿌");
            SkillManager.instance.HumenSkill(this, enemyData.depence, 3);
            SkillManager.instance.skillText.text = "적이 3턴간 더 더욱 단단해집니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

        }

    }
    public override void DeadMob()
    {
        if (enemyData.hp <= 0)
        {
            SoundManager.instance.PlayVoice("마린죽는소리");
            PlayerManager.Instance.UnLockedList(1);
            switch (PlayerManager.Instance.PlayerData.id)
            {
                case 0: break;
                case 1: PlayerManager.Instance.UnLockedList(1000); break;
                case 2: PlayerManager.Instance.UnLockedList(1001); break;
                case 3: PlayerManager.Instance.UnLockedList(1002); break;
                case 4: PlayerManager.Instance.UnLockedList(1003); break;

            }
        }
    }

}
