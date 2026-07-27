using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [SerializeField] TextMeshProUGUI count;
    [SerializeField] Button SkillB;


   


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    //플레이어용 적용으로 만들기 버츄얼로 만드는 것도 좋을 듯

    //휴먼 스킬 방어력이 증가 (한턴동안) / 적은 3턴동안
    public void HumenSkill(YutPlayer caster, int defence , int turnCont)
    {
        float hpbar = caster.GetHpVaule();

        int finalDefence = defence;
        if(hpbar <= 0.3f)
        {
            finalDefence *= 5;
        }
        else if(hpbar <= 0.5f)
        {
            finalDefence *= 3;
        }
        else if (hpbar <=0.7f)
        {
            finalDefence *= 2;
        }

        caster.ApplyDefence(finalDefence, turnCont);
        

    }
    //고블린 스킬 잡았을 때 일정 확률로 적의 최대 말 갯수를 줄이고 나의 최대말을 늘림 / 적은 처음은 30프로 두번째는 50프로 3번째는 70프로 확률로 훔침

    //엘프스킬 사거리 칸 안에 적을 제거함 / 적은 가장 많이 업고 있는 적을 제거

    //언데드 스킬  잡았을 때 일정 확률로 업은 말의 수 +1 / 적은 패시브로 발동하며 확률이 잃은 체력 비례해서 증가 예정

    //천사스킬 잡혔을 때 잡히면 일정 확률로 부활하여 반격해서 역으로 잡음 / 패시브로 반격하며 잃은 체력 비례해서 증가할 예정
}
