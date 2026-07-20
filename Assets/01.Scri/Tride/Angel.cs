using UnityEngine;

public class Angel: EnemyController
{
    private void Start()
    {
        CurrentEnemy = 4;
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

