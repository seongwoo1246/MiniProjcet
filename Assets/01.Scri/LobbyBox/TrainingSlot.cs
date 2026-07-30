using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingSlot : MonoBehaviour
{
    [SerializeField] TrainingDataManager TrainingM;
    

    private int TrainingId = -1;
   
    private Training Data = null;

   public Image icon1;
   public TextMeshProUGUI Level;
   public TextMeshProUGUI name1;
   public TextMeshProUGUI price1;



    public void SetTraining(Training trainingData)
    {
        if (trainingData == null) return;

        this.Data = trainingData;
        this.TrainingId = trainingData.id;



        if (icon1 != null) icon1.sprite = trainingData.icon;
        if (name1 != null) name1.text = trainingData.name;
        if (price1 != null) price1.text = trainingData.price.ToString();
        if (Level != null) Level.text = trainingData.upgrad.ToString();


    }

    public void UpgradeLevel()
    {
        var player = PlayerManager.Instance;
        
        if (Data != null)
        {
            if (player.haveMoney >= Data.price&& player != null&& player.PlayerData != null)
            {

                player.haveMoney -= Data.price;
                TrainingUi.Instance.money.text = $" 현재 소유 금액 : {player.haveMoney}";

                var playerState = PlayerManager.Instance.PlayerData;
               
                TrainingUi.Instance.TrainingSuccess.text = Data.GetDesc();
                


                switch (Data.id)
                {
                    case 100: playerState.maxHp += 50;playerState.hp += 50;
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
                        playerState.damage += 99999;
                        TrainingUi.Instance.Secret.gameObject.SetActive(true);
                        TrainingUi.Instance.SecretRaw.gameObject.SetActive(true);
                       TrainingUi.Instance.Secret.Play();
                        
                        break;

                }
                price1.text = $"{Data.price}";
                TrainingUi.Instance.BuyOO();
                Data.upgrad++;
                Level.text = Data.upgrad.ToString();
            }
            else
                TrainingUi.Instance.BuyXX();
        }
    }

    

    


}
