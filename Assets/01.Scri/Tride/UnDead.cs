using UnityEngine;

public class UnDead : EnemyController ,canSkill
{
    protected override void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.undead);
        base.Start();
        CurrentEnemy = 3;
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
            SoundManager.instance.PlaySFX("Â¸±×¶û");
            SkillManager.instance.skillText.text = "ÀûÀÇ ½ºÅ³À» ¹æÇØÇß½À´Ï´Ù. ¾ßÈ£(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            return;
        }
        else
        {
            SoundManager.instance.PlaySFX("»Ñ»Ñ");
            isUndeadSkillUsed = true;
            SkillManager.instance.skillText.text = "ÀûÀÌ Áö±ÝºÎÅÍ ÀÏÁ¤ È®·ü·Î °¨¿°À» ½ÃÀÛÇÕ´Ï´Ù.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());


        }

    }
    public void UseSkill50(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            SoundManager.instance.PlaySFX("Â¸±×¶û");
            SkillManager.instance.skillText.text = "ÀûÀÇ ½ºÅ³À» ¹æÇØÇß½À´Ï´Ù. ¾ßÈ£(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            return;
        }
        else
        {
            SoundManager.instance.PlaySFX("»Ñ»Ñ");
            isUndeadSkillUsed = true;
            finalprecent += 0.05f;
            SkillManager.instance.skillText.text = "ÀûÀÌ ÈûÀ» ¹ßÈÖÇÏ±â ½ÃÀÛÇÕ´Ï´Ù.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

        }

    }
    public void UseSkill30(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            SoundManager.instance.PlaySFX("Â¸±×¶û");
            SkillManager.instance.skillText.text = "ÀûÀÇ ½ºÅ³À» ¹æÇØÇß½À´Ï´Ù. ¾ßÈ£(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            return;
        }
        else
        {
            SoundManager.instance.PlaySFX("»Ñ»Ñ");
            isUndeadSkillUsed = true;
            finalprecent += 0.1f;
            SkillManager.instance.skillText.text = "ÀûÀÇ ÈûÀÌ ÆøÆÈÇÒ µí »Õ¾îÁ® ³ª¿À±â ½ÃÀÛÇÕ´Ï´Ù.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

        }

    }

    public override void DeadMob()
    {
        if (enemyData.hp <= 0)
        {
            SoundManager.instance.PlayVoice("¸¶¸°Á×´Â¼Ò¸®");
            PlayerManager.Instance.UnLockedList(4);
            switch (PlayerManager.Instance.PlayerData.id)
            {
                case 0: PlayerManager.Instance.UnLockedList(1002); break;
                case 1: PlayerManager.Instance.UnLockedList(1005); break;
                case 2: PlayerManager.Instance.UnLockedList(1007); break;
                case 3:  break;
                case 4: PlayerManager.Instance.UnLockedList(1009); break;

            }
        }
    }


}

