using UnityEngine;

public class UnDead : EnemyController ,canSkill
{
    protected override void Start()
    {
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
            SkillManager.instance.skillText.text = "적의 스킬을 방해했습니다. 야호(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            return;
        }
        else
        {
            isUndeadSkillUsed = true;
            SkillManager.instance.skillText.text = "적이 지금부터 일정 확률로 감염을 시작합니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());


        }

    }
    public void UseSkill50(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            SkillManager.instance.skillText.text = "적의 스킬을 방해했습니다. 야호(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            return;
        }
        else
        {

            isUndeadSkillUsed = true;
            finalprecent += 0.05f;
            SkillManager.instance.skillText.text = "적이 힘을 발휘하기 시작합니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

        }

    }
    public void UseSkill30(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            SkillManager.instance.skillText.text = "적의 스킬을 방해했습니다. 야호(>.<)/*";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());
            return;
        }
        else
        {
            isUndeadSkillUsed = true;
            finalprecent += 0.1f;
            SkillManager.instance.skillText.text = "적의 힘이 폭팔할 듯 뿜어져 나오기 시작합니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

        }

    }
}

