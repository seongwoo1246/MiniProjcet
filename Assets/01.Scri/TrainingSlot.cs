using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class TrainingSlot : MonoBehaviour
{
    public Sprite icon;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI name;
    public TextMeshProUGUI price;

    private int TrainingId = -1;
    private int UpgradeCount = 0;

    public int GetTrainingID() => TrainingId;
   
    private void Start()
    {
        Button button = GetComponent<Button>();
        if(button != null)
        {  }
    }

    public void UpgradeLevel(int id)
    {
        if (TrainingId == -1)
            return;
        var Data = DataManager.instance.GetTrainingData(TrainingId);
        if(Data != null)
        {
            if (TrainingUi.Instance.haveMoney >= Data.price)
            {
                TrainingUi.Instance.haveMoney -= Data.price;
                TrainingUi.Instance.money.text = $" 현재 소유 금액 : {TrainingUi.Instance.haveMoney}";

            }
            else
                TrainingUi.Instance.BuyXX();
        }
    }

    public void SetTraining(int id, Sprite icon, string name, int price, int level)
    {
        TrainingId = id;
        this.icon = icon;
        this.name.text = name;
        this.price.text = price.ToString();
        Level.text = level.ToString(); ;
            

    }


}
