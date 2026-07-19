using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : YutPlayer
{
  


    [SerializeField] Image Icon;
    [SerializeField] Image CharacterIcon;
    [SerializeField] TextMeshProUGUI maxCharacter;
    [SerializeField] TextMeshProUGUI Hpbar;
    [SerializeField] Scrollbar HP;

    private void Start()
    {
        BattleSceneManager.instance.CuttrentEnemy = this;
    }
}
