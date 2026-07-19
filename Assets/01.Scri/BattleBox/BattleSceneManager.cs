using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




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

    [SerializeField] GameObject GameOver1;

    [SerializeField] TextMeshProUGUI resultYut;
    [SerializeField] TextMeshProUGUI TurnCount;
    [SerializeField] TextMeshProUGUI moCount;
    [SerializeField] TextMeshProUGUI yutCount;
    [SerializeField] TextMeshProUGUI yutname;
    [SerializeField] TextMeshProUGUI First;
    [SerializeField] TextMeshProUGUI GainMoney;
    [SerializeField] TextMeshProUGUI Break;
    [SerializeField] TextMeshProUGUI Tip;
    
    [SerializeField] Button mo;
    [SerializeField] Button yut;
    [SerializeField] Button elseyut;
    [SerializeField ] Button MyChar;
    [SerializeField ] Button EnemyChar;
    [SerializeField ] Button GoLobby;


    public GameObject playData1;
    public EnemyController CuttrentEnemy;
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
        GameOver1.SetActive(false);
        Turn = 0;
        TurnCount.text =$"경과 턴 : {Turn}";
        FirstStart();
        PlayerManager.Instance.playerUiDate = playData1;
        
    }


    public virtual void TakeDamage(float miss, float hp, int damage, int depence)
    {
       if(Random.value < miss )
        {
            //공격 회피 문구
            return;
            
        }
       else
        {
            //공격회피 실패 문구
           int Damage = Mathf.Max(0, damage-depence);

            hp-=Damage;

            //게임 오버처리
            if(hp<=0)
            {
                hp = 0;
                GameOver();
            }

            
        }


    }

    public int Attack( float critical ,int damage)
    {
        int finalDamage = damage;
        if(Random.value<critical)
        {
            finalDamage = damage * 2;
        }
        return finalDamage;   
    }

    public void UseSkill(float block, float luck)
    {
        if(Random.value + block < luck)
        {
            //스킬 성공
        }
        
    }

    public  void Heal(float hp , int heal)
    {
        hp += heal;
    }

    public void GoToLobby()
    {
        ScenesM.instance.LoadScenes(scenetpye.Lobby);
    }

    public void GameOver()
    {
        GainMoney.text = TrainingUi.Instance.haveMoney + GainMoneys().ToString();
        if (PlayerManager.Instance.PlayerData.hp == 0)
            Break.text = "패배하셨군요 다음에 도전해 보세요.";
        else
        { Break.text = "승리를 축하드립니다.  상대 종족을 사용할 수 있게 되었습니다."; }
        GiveTip();

            GameOver1.SetActive(true);

    }

    public int GainMoneys()
    {

        int gainM = Random.Range(500, 2000);
        
        return gainM;
    }

    public void GiveTip()
    {
     int tipNum  = Random.Range(0, 3);

        if (PlayerManager.Instance.PlayerData.hp == 0)
        {
            switch (tipNum)
            {
                case 0:
                    Tip.text = " 좀 더 훈련해서 도전 해보는 걸 추천합니다.";
                    break;
                case 1:
                    Tip.text = " 아쉽게도 이번에는 운이 없었군요. 다음에는 이길 수 있을 겁니다.";
                    break;

                case 2:
                    Tip.text = " ai가 그렇게 똑똑하지 않습니다. 그 부분을 잘 노리면 이길 수 있을 겁니다.";
                    break;
            }
        }
        else
        {
            switch (tipNum)
            {
                case 0:
                    Tip.text = "오? 이걸 이기네 축하합니다. 다음에는 좀 더 악마 같은 친구를 데려 오겠습니다.";
                    break;
                case 1:
                    Tip.text = "그만해 그녀석에 HP는 이미 zero라고!!!";
                    break;

                case 2:
                    Tip.text = "그녀석은 만든 적들 중에서 가장 약한 녀석이였다. 다음 적은 이렇게 쉽지 않을 걸!!!";
                    break;
            }
        }

       
    }

    private IEnumerator FalseText (TextMeshProUGUI Text)
    {
        yield return new WaitForSeconds(1f);
       Text.gameObject.SetActive(false);

    }
    // 게임 시작시 선 정하기
    private void FirstStart()
    {
        if(Random.Range(0,2)==1)
        {
            IsMyFirst = true;
            CanThrow = true;
            First.text="당신이 선공입니다.";
        }
        else
        {
            IsMyFirst= false;
            CanThrow= false;
            First.text = "당신이 후공입니다.";
        }
        First.gameObject.SetActive(true);
        StartCoroutine(FalseText(First));
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

        resultYut.gameObject.SetActive(true);
        StartCoroutine(FalseText(resultYut));
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
