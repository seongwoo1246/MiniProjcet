using UnityEngine;

public class Humen : EnemyController, canSkill
{
    protected override void Start()
    {
        base.Start();
        CurrentEnemy = 0;
        SetEnemy(CurrentEnemy);
        BattleSceneManager.instance.ItbattleSet();
    }
    public override void GoalIn(YutPiace targetPiace)
    {
        var mydata = trideDM.TrideList[CurrentEnemy];
        var player = PlayerManager.Instance.PlayerData;
        var BSM = BattleSceneManager.instance;

        int totalcount = 1;
        if (targetPiace != null && targetPiace.carriedChar != null)
        {
            totalcount += targetPiace.carriedChar.Count;
        }

        BSM.TakeDamage(player.miss,
           ref player.hp, BSM.countDamageUp(BSM.Attack(mydata.critical,
            mydata.damage), totalcount), player.depence);
        PlayerManager.Instance.playerHpeffect();
        BSM.Heal(ref mydata.hp, mydata.maxHp, mydata.heal);
        Hpeffect(CurrentEnemy);
        base.GoalIn(targetPiace);
    }
    public void UseSkill70(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            return;
        }
        else
        {
            //스킬 성공
        }

    }
    public void UseSkill50(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            return;
        }
        else
        {
            //스킬 성공
        }

    }
    public void UseSkill30(float block, float luck)
    {
        if (Random.value + block > luck)
        {
            return;
        }
        else
        {
            //스킬 성공
        }

    }
}
