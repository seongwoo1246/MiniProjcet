using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : YutPlayer
{
  public static EnemyController Instance;


     [SerializeField] Image Icon;
    [SerializeField] Image CharacterIcon;
    [SerializeField] TextMeshProUGUI maxCharacter;
    [SerializeField] TextMeshProUGUI Hpbar;
    [SerializeField] Scrollbar HP;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
