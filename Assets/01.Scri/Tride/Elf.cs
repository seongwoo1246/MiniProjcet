using UnityEngine;

public class Elf : EnemyController
{
    private void Start()
    {
        CurrentEnemy = 2;
        SetEnemy(CurrentEnemy);
        BattleSceneManager.instance.ItbattleSet();
    }
    public override void GoalIn()
    {
        var mydata = trideDM.TrideList[CurrentEnemy];
        var player = PlayerManager.Instance.PlayerData;
        var BSM = BattleSceneManager.instance;

        int totalcount = 1 + yutcount.carriedChar.Count;

        BSM.TakeDamage(player.miss,
            player.hp, BSM.countDamageUp(BSM.Attack(mydata.critical,
            mydata.damage), totalcount), player.depence);

        base.GoalIn();
    }
}

