using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingSlot : MonoBehaviour
{
    [SerializeField] TrainingDataManager TrainingM;
    

    private int TrainingId = -1;
    private int UpgradeCount = 0;
    private Training Data = null;

   public Image icon;
   public TextMeshProUGUI Level;
   public TextMeshProUGUI name1;
   public TextMeshProUGUI price;



    public void SetData(int id, Sprite icon, string name ,int price , int Level)
    {
      TrainingId=id;
        if (TrainingId == -1) return;
         Data = TrainingM.TrainingList[id - 100].Clone();
    }
    public void UpgradeLevel()
    {
       
        if(Data != null)
        {
            if (TrainingUi.Instance.haveMoney >= Data.price&& PlayerManager.Instance != null&&PlayerManager.Instance.PlayerData != null)
            {
                TrainingUi.Instance.haveMoney -= Data.price;
                TrainingUi.Instance.money.text = $" 현재 소유 금액 : {TrainingUi.Instance.haveMoney}";

                var playerState = PlayerManager.Instance.PlayerData;
               
                TrainingUi.Instance.TrainingSuccess.text = Data.GetDesc();
                //개량의 여지가 있음 밑에도 다 개량 가능
                switch (Data.id)
                {
                    case 100: playerState.maxHp += 50;
                        Data.price += 500;
                        
                        break;
                    case 101: playerState.damage += 25;
                        Data.price += 500;
                        break;
                    case 102:playerState.depence += 10;
                        Data.price += 500;
                        break;
                    case 103:playerState.critical += 0.05f;
                        Data.price += 1000;
                        break;
                    case 104:playerState.moneyUp += 100;
                        Data.price += 1500;
                        break;
                    case 105:playerState.maxCharacter += 1;
                        Data.price += 5000;
                        break;
                    case 106:playerState.heal += 50;
                        Data.price += 1500;
                        break;
                    case 107:playerState.luck += 0.05f;
                        Data.price += 2500;
                        break;
                    case 108:playerState.block += 0.05f;
                        Data.price += 2500;
                        break;
                    case 109:playerState.miss += 0.05f;
                        Data.price += 2000;
                        break;
                    case 110:playerState.length += 1;
                        Data.price += 5000;
                        break;
                    case 111:playerState.infection += 0.05f;
                        Data.price += 5000;
                        break;
                    case 112:playerState.kidnap += 0.05f;
                        Data.price += 5000;
                        break;
                    case 113:playerState.rivival += 0.05f;
                        Data.price += 5000;
                        break;
                    case 114:
                       // 나중에 적을 내용
                        break;

                }
                price.text = $"{Data.price}";
                TrainingUi.Instance.BuyOO();
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
            
        SetData(id,icon,name,price,level);
    }


}
