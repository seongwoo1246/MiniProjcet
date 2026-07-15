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
            if (TrainingUi.Instance.haveMoney >= Data.price&& PlayerManager.instance != null&&PlayerManager.instance.PlayerData != null)
            {
                TrainingUi.Instance.haveMoney -= Data.price;
                TrainingUi.Instance.money.text = $" 현재 소유 금액 : {TrainingUi.Instance.haveMoney}";

                var playerState = PlayerManager.instance.PlayerData;

                switch(Data.id)
                {
                    case 0: playerState.maxHp += 50;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 최대 체력이 50증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 1: playerState.damage += 25;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 공격력이 25증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 2:playerState.depence += 10;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 방어력이 10증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 3:playerState.critical += 0.05f;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 치명율이 5%증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 4:playerState.moneyUp += 100;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 돈 획득량이 100증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 5:playerState.maxCharacter += 1;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 최대 캐릭터 소유량이 1개 증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 6:playerState.heal += 50;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 회복량이 50증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 7:playerState.luck += 0.05f;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 행운이 5%증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 8:playerState.block += 0.05f;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 방해 확율이 5%증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 9:playerState.miss += 0.05f;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 회피율이 5%증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 10:playerState.length += 1;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 사거리가 1증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 11:playerState.infection += 0.05f;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 감염확율이 5%증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 12:playerState.kidnap += 0.05f;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 납치 확율이 5%증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 13:playerState.rivival += 0.05f;
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 나의 부활 확율이 5%증가";
                        TrainingUi.Instance.BuyOO();
                        break;
                    case 14:
                        TrainingUi.Instance.TrainingSuccess.text = $"{Data.name} 훈련 완료 축하합니다. 전설의 문을 열었습니다. 당신의 전설은 먼 훝날에도 이어져 갈 것 입니다.";
                        TrainingUi.Instance.BuyOO();
                        break;

                }

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
