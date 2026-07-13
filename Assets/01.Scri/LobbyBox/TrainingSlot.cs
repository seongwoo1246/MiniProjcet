using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingSlot : MonoBehaviour
{
    private Sprite icon;
    private TextMeshProUGUI Level;
    private TextMeshProUGUI name;
    private TextMeshProUGUI price;

    private int TrainingId = -1;
    private int UpgradeCount = 0;

    public int GetTrainingID() => TrainingId;
   
   

    public void UpgradeLevel()
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
                //플레이어 스탯에 강화해주는 효과 넣어주기 
                Data.price = (int)(Data.price * 1.5);
                UpgradeCount++;
                Level.text = UpgradeCount.ToString();
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
