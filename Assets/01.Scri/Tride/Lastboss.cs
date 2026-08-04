using UnityEngine;

public class Lastboss : EnemyController, canSkill
{
    protected override void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.lastboss);
        base.Start();
        CurrentEnemy = 5;
        SetEnemy(CurrentEnemy);
        BattleSceneManager.instance.ItbattleSet();
        isGoblinSkillUsed = true;
        isUndeadSkillUsed = true;
        
        maxChar = 6;
       
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
            SoundManager.instance.PlaySFX("폭팔");
            SkillManager.instance.HumenSkill(this, enemyData.depence,0, 5);
            SkillManager.instance.skillText.text = "적이 5턴간 단단해집니다.";
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
            SoundManager.instance.PlaySFX("폭팔");
            SkillManager.instance.HumenSkill(this, enemyData.depence, 0,7);
            SkillManager.instance.skillText.text = "적이 7턴간 더 단단해집니다.";
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
            SoundManager.instance.PlaySFX("폭팔");
            SkillManager.instance.HumenSkill(this, enemyData.depence, 0, 10);
            SkillManager.instance.skillText.text = "적이 10턴간 더 더욱 단단해집니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

        }

    }

  
    public override void DeadMob()
    {
        if(enemyData.hp<=0)
        {
            SoundManager.instance.PlayVoice("마린죽는소리");
            PlayerManager.Instance.CanGoEnd = true;
        }
        
    }
}
