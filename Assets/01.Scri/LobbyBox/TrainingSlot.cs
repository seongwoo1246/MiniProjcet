using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingSlot : MonoBehaviour
{
    [SerializeField] TrainingDataManager TrainingM;
    

    private int TrainingId = -1;
    private int UpgradeCount = 0;


   public Image icon;
   public TextMeshProUGUI Level;
   public TextMeshProUGUI name1;
   public TextMeshProUGUI price;


    public void UpgradeLevel()
    {
        if (TrainingId == -1)
            return;
        var Data = TrainingM.TrainingList[TrainingId-100].Clone();
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
         this.icon.sprite= icon;
        name1.text = name;
        this.price.text = price.ToString();
        Level.text = level.ToString(); ;
            

    }


}
