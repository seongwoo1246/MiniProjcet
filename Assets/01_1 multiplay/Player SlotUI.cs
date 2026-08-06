using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlotUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI escapeText;
    public TextMeshProUGUI remainText;
    

    public void SetPlayerInfo(string name , int escapecount,int remainCount)
    {
        this.gameObject.SetActive(true);
        if(nameText != null) nameText.text = name;
        if (escapeText != null) escapeText.text = $"≈ª√‚ : {escapecount}";
        if (remainText != null) remainText.text = $" ≥≤¿∫ ∏ª : {remainCount}";
        
    }

    public void ClearSlot()
    {
        this.gameObject.SetActive(false);
    }

}
