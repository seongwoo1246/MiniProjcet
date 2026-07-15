using TMPro;
using UnityEngine;

public class StateSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI valueText;

    public void SetStateSlot(string name , string value)
    {
        NameText.text = name;
        valueText.text = value; 

    }
}
