using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum monState
{
    nomal,
    belowHp70,
    belowHp50,
    belowHp30,
}






public class EnemyController : YutPlayer
{
    public int CurrentEnemy;
    public TrideDataManager trideDM;

    [SerializeField] protected Image Icon;
    [SerializeField] protected Image CharacterIcon;
    [SerializeField] protected TextMeshProUGUI maxCharacter;
    [SerializeField] protected TextMeshProUGUI Hpbar;
    [SerializeField] protected Scrollbar HP;



    private void Start()
    {
        BattleSceneManager.instance.CuttrentEnemy = this;
    }

    protected void SetEnemy(int i)
    {
        Icon.sprite = trideDM.TrideList[i].icon;
        CharacterIcon.sprite = trideDM.TrideList[i].icon;
        maxCharacter.text = trideDM.TrideList[i].maxCharacter.ToString();
        Hpbar.text = $"{trideDM.TrideList[i].hp}/{trideDM.TrideList[i].maxHp}";
    }

    protected void Hpeffect(int i)
    {
        HP.value = trideDM.TrideList[i].hp / trideDM.TrideList[i].maxHp;
        Hpbar.text = $"{trideDM.TrideList[i].hp}/{trideDM.TrideList[i].maxHp}";
    }
}
