using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TrideSlot : MonoBehaviour
{
    private Sprite icon;
    private Sprite iconIn;
    private TextMeshProUGUI name;
    private TextMeshProUGUI character;
    private TextMeshProUGUI TrideDescription;

   protected TrideUi trideUi;
    protected BattleUi battleUi;
 
    private int TrideId = -1;
   
    private void Start()
    {
       
        trideUi = GetComponent<TrideUi>();
      battleUi = GetComponent<BattleUi>();
        Button button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnSlotTride);
    }
    public void OnSlotTride()
    {
        if (TrideId == -1)
            return;
        trideUi.SelectTride(TrideId);
        trideUi.TrideSlot.SetActive(true);
    }

   
   

    public void SetTride(int id ,Sprite icon, string name, string character, string Description)
    {
       TrideId = id;
        this.icon = icon;
        iconIn = icon;
        this.name.text = name;
        this.character.text = character;
        TrideDescription.text = Description;
    }

}
