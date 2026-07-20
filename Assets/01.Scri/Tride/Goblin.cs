using UnityEngine;

public class Goblin : EnemyController
{
    private void Start()
    {
        CurrentEnemy = 1;
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

