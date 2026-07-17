using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;




public enum Yut
{
    zero,
    one,
    two,
    three,
    four,
    five,
    back = -1
}


public enum TrideType 
{
    angel,
    elf,
    goblin,
    humen,
    undead

}





public class BattleSceneManager : MonoBehaviour
{
   public static BattleSceneManager instance;

    [SerializeField] TextMeshProUGUI resultYut;
    [SerializeField] TextMeshProUGUI TurnCount;
    [SerializeField] TextMeshProUGUI moCount;
    [SerializeField] TextMeshProUGUI yutCount;
    [SerializeField] TextMeshProUGUI yutname;
    
    [SerializeField] Button mo;
    [SerializeField] Button yut;
    [SerializeField] Button elseyut;
    [SerializeField ] Button MyChar;
    [SerializeField ] Button EnemyChar;

    public YutPlayer currentTurnPlayer;
    public bool CanThrow;
    public bool IsMyFirst;
    private int Turn;
    private int yutC=0;
    private int moC=0;
    private Yut currentRestYut = Yut.zero;

    public Yut selectYut;
    public int selectMoveSpace = 0;
    public bool isYutSelected =false;
    //임시코드
    public int maxchar = 4;

    private List<Yut> TurnYutResult = new List<Yut>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Turn = 0;
        TurnCount.text =$"경과 턴 : {Turn}";
        FirstStart();
    }


    public virtual void TakeDamage()
    {
        //회피율을 이용한 회피 여부
        
        //체력 -= (데미지(어택함수) - 방어력)
    }

    public virtual void Attack()
    {
        //행운,방해율 계산 효과 발동 여부 = 따로 함수로 만들까?
        //크리트컬 여부 계산후 공격력에 더해주기 (데미지 2배)
        //
    }

    public virtual void Heal()
    {
        //체력 +=힐량 
    }

    // 게임 시작시 선 정하기
    private void FirstStart()
    {
        if(Random.Range(0,2)==1)
        {
            IsMyFirst = true;
            CanThrow = true;
        }
        else
        {
            IsMyFirst= false;
            CanThrow= false;
        }
    }

    //윷 값 구하기
    public Yut GetYut() 
    {
       int backSideYut = 0;

        for (int i = 0; i <6 ; i++)
        {
            if(Random.Range(0,2)==1)
            {
                backSideYut++;
            }
        }

        Yut result;

        switch(backSideYut)
        {
            case 6: result = Yut.back; break;
            case 5: result = Yut.zero; break;
            case 1: result = Yut.one; break;
            case 2: result = Yut.two; break;
            case 3: result = Yut.three; break;
            case 4: result = Yut.four; break;
            case 0: result = Yut.five; break;
            default: result = Yut.zero; break;
        }

        return result;
    }
    //윷 던지기
    public void OnClickThrowButton()
    {
        if(CanThrow == false)
            { return; }

        Yut currentYut = GetYut();

        TurnYutResult.Add(currentYut);

        // 윷이 나왔을 때 저장 하는 기능
        if(currentYut == Yut.four)
        {
            yutC++;
            yutCount.text = $"{yutC}";
            resultYut.text = $" {ChangeYutText(currentYut)}이 나왔군요. 한 번 더 던지세요";
            CanThrow = true;
        }
        //모가 나왔을 때 저장 하는 기능
        else if(currentYut == Yut.five)
        {
            moC++;
            moCount.text = $"{moC}";
            resultYut.text = $" {ChangeYutText(currentYut)}이 나왔군요. 한 번 더 던지세요";
            CanThrow = true;
        }
        // 다른 말이 나왔을 때 저장 하는 기능
        else
        {
            resultYut.text = ChangeYutText(currentYut);
            yutname.text = ChangeYutText(currentYut);
            CanThrow = false;
            currentRestYut= currentYut;
        }

     
    }

    // enum 스트링 변환기
    private string ChangeYutText(Yut yut)
    {
        switch(yut)
        {
            case Yut.back: return "뒷도";
            case Yut.zero: return "낙";
            case Yut.one: return "도";
            case Yut.two: return "개";
            case Yut.three: return "걸";
            case Yut.four: return "윷";
            case Yut.five: return "모";
            default: return "";
        }
    }

   
    //윷 결과 버튼 함수

    public void OnClickYutSlot(int value)
    {
        Yut targetYut;

        if(value == -999)
        {
            if (currentRestYut == Yut.zero) return;
            targetYut = currentRestYut;
        }
        else
        {
            targetYut = (Yut)value;
        }
        if(TurnYutResult.Contains(targetYut))
        {
            selectYut = targetYut;
            selectMoveSpace = (int)targetYut;
            isYutSelected = true;
        }
    }

    // 사용한 윷 결과
    public void UseSelectedYut()
    {
        if (TurnYutResult.Contains(selectYut))
        {
            TurnYutResult.Remove(selectYut);
            if(selectYut == Yut.five)
            {
                moC--;
                moCount.text = $"{moC}";
            }
            else if (selectYut == Yut.four)
            {
                yutC--;
                yutCount.text = $"{yutC}";
            }
            else
            {
                currentRestYut = Yut.zero;
                yutname.text = "";
            }
            isYutSelected = false;
            selectMoveSpace = 0;
        }
    }

    //새로운 말 출발 코드
    public void OnChilckStartNewChar()
    {
        currentTurnPlayer.StartNewChar(selectMoveSpace);

            UseSelectedYut();

    }

    

}
