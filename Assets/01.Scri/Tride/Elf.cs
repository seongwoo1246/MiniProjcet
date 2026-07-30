using UnityEngine;

public class Elf : EnemyController, canSkill
{
    private YutPiace myPiace;
    protected override void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.elf);
        base.Start();
        myPiace = GetComponent<YutPiace>();
        CurrentEnemy = 2;
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

        BSM.TakeDamage(player,player.miss
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
            SkillManager.instance.ElfSkill(this, myPiace, enemyData.length);
            SkillManager.instance.skillText.text = "적이 사냥을 시작합니다.";
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
            SkillManager.instance.ElfSkill(this, myPiace, enemyData.length);
            SkillManager.instance.skillText.text = "적이 사냥을 재개합니다.";
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
            SkillManager.instance.ElfSkill(this, myPiace, enemyData.length);
            SkillManager.instance.skillText.text = "적이 마무리 사냥을 시작합니다.";
            SkillManager.instance.StartCoroutine(SkillManager.instance.Textfadeinout());

        }

    }

    public override void DeadMob()
    {
        if (enemyData.hp <= 0)
        {
            SoundManager.instance.PlayVoice("매딕죽는소리");
            PlayerManager.Instance.UnLockedList(3);
            switch (PlayerManager.Instance.PlayerData.id)
            {
                case 0: PlayerManager.Instance.UnLockedList(1001); break;
                case 1: PlayerManager.Instance.UnLockedList(1004); break;
                case 2:  break;
                case 3: PlayerManager.Instance.UnLockedList(1007); break;
                case 4: PlayerManager.Instance.UnLockedList(1008); break;

            }
        }
    }
}

