using UnityEngine;

public class UnDead : EnemyController
{
    private void Start()
    {
        CurrentEnemy = 3;
        SetEnemy(CurrentEnemy);
    }
         public override void GoalIn()
    {
        var mydata = trideDM.TrideList[CurrentEnemy];
        var player = PlayerManager.Instance.PlayerData;
        var BSM = BattleSceneManager.instance;

        BSM.TakeDamage(player.miss, 
            player.hp, BSM.Attack(mydata.critical,
            mydata.damage), player.depence);
        
        base.GoalIn();
    }
}

