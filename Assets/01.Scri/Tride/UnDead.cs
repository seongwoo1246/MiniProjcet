using UnityEngine;

public class UnDead : EnemyController
{
    private void Start()
    {
     
        CurrentEnemy = 3;
        SetEnemy(CurrentEnemy);
        BattleSceneManager.instance.ItbattleSet();
    }
         public override void GoalIn()
    {
        var mydata = trideDM.TrideList[CurrentEnemy];
        var player = PlayerManager.Instance.PlayerData;
        var BSM = BattleSceneManager.instance;

        int totalcount = 1+ yutcount.carriedChar.Count;

        BSM.TakeDamage(player.miss, 
            player.hp,BSM.countDamageUp( BSM.Attack(mydata.critical,
            mydata.damage),totalcount), player.depence);
        PlayerManager.Instance.playerHpeffect();

        base.GoalIn();
    }
}

