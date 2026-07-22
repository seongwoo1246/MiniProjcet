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
    humen,
    goblin,
    elf,
    undead,
    angel,
}

public interface canSkill
{
   void UseSkill70(float block, float luck);
   void UseSkill50(float block, float luck);
    void UseSkill30(float block, float luck);
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
    [SerializeField] TextMeshProUGUI attacktext;
    
    [SerializeField] Button mo;
    [SerializeField] Button yut;
    [SerializeField] Button elseyut;
    [SerializeField ] Button MyChar;
    [SerializeField ] Button EnemyChar;
    [SerializeField ] Button GoLobby;
    [SerializeField ] Button TurnEND;

    private YutPiace yutPiace;
    public GameObject playData1;
    public EnemyController CuttrentEnemy;
    public EnemyController enemyController;
    public YutPlayer Player;
    public YutPlayer enemy;
    public bool CanThrow;
    public bool IsMyFirst;
    private int Turn;
    private int yutC=0;
    private int moC=0;
    private Yut currentRestYut = Yut.zero;
    private bool canUseYut = false; 
    private bool IsMyTurn;

    public Yut selectYut;
    public int selectMoveSpace = 0;
    public bool isYutSelected =false;
    

    public List<Yut> TurnYutResult = new List<Yut>();

    public List<YutPiace> allActiveChar = new List<YutPiace>();

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

        PlayerManager.Instance.ButtonSet();

        Button button = GoLobby.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(GoToLobby);

    }
    //윷을 안 던지고 턴을 넘기는 것도 전략이다.
    public void TurnEndButton()
    {
        if(IsMyTurn==true)
        {
            TurnEnd();
        }
    }

    public void TurnEnd() 
    {       

        if(IsMyTurn)
        {
            
            IsMyTurn = false;
            enemyController.IsEnemyTurn = true;
            canUseYut = false;
            CanThrow = false;
            enemyController.EnemyTurn();
        }
        else
        {
            IsMyTurn = true;
            enemyController.IsEnemyTurn = false;
           
            CanThrow = true;
            Turn++;
            TurnCount.text = $"경과 턴 : {Turn}";

           
        }
    }

    public int countDamageUp(int basedamage , int count)
    {
        if(count >=8)
        { count = 7; }
        switch(count)
        {
            case 1: return basedamage;
            case 2: return basedamage * 2; 
            case 3: return basedamage * 4; 
            case 4: return basedamage * 6; 
            case 5: return basedamage * 9; 
            case 6: return basedamage * 12; 
            case 7: return basedamage + 9999999;
            default: return basedamage;
        }
        
    }
    //말을 잡을 수 있는 가 검사
    public void checkCatchChar(YutPiace MovePiace)
    {
        //판에 없거나 업혀 있는 친구는 넘어가는 코드
        if(MovePiace.currentPathIndex<=0||MovePiace.isCarried) return;

        bool isCaughtAnything = false;

        foreach( YutPiace targetPiace in allActiveChar )
        {
            if (targetPiace.isCarried || targetPiace == MovePiace || !targetPiace.isMovingOnBorad) continue;

            if(targetPiace.currentPathIndex == MovePiace.currentPathIndex)
            {
                if(targetPiace.isEnemy == MovePiace.isEnemy)
                {
                    targetPiace.carriedChar.Add(MovePiace);
                    if(  MovePiace.carriedChar.Count>=0)
                    {
                        targetPiace.carriedChar.AddRange(MovePiace.carriedChar);
                        MovePiace.carriedChar.Clear();
                    }

                    MovePiace.isCarried = true;
                    MovePiace.gameObject.SetActive(false);
                    targetPiace.UpdateVisuals();
                    return;
                }
                else if(targetPiace.isEnemy != MovePiace.isEnemy)
                {
                    targetPiace.CatchChar();
                    isCaughtAnything = true;
                    foreach( YutPiace kid in targetPiace.carriedChar)
                    {
                        kid.CatchChar();
                    }
                    targetPiace.carriedChar.Clear();

                    
                    break;
                }
            }

        }
        if(isCaughtAnything)
        {
            CanThrow = true;
        }
    }


    public void ItbattleSet()
    {
        if(Player== null&&PlayerManager.Instance != null)
         Player = PlayerManager.Instance;
        
           
            if (Player != null)
            {
            int myTrideId = PlayerManager.Instance.PlayerData.id;
            Player.SetTrideId(myTrideId);
            }

        if (CuttrentEnemy != null)
        {
            int enemyTrideId = CuttrentEnemy.CurrentEnemy;
            enemy.SetTrideId(enemyTrideId);
        }

    }


    public  void TakeDamage(float miss, float hp, int damage, int depence)
    {
       if(Random.value < miss )
        {
            attacktext.text = "공격을 회피했다.";
            return;
            
        }
       else
        {
            attacktext.text = "공격을 명중했다.";
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

   

    public  void Heal(float hp ,float maxHP, int heal)
    {
        if(hp<=maxHP)
        {
            hp = maxHP;
        }

        hp += heal;
    }

    public void GoToLobby()
    {
        PlayerManager.Instance.SetHp();
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
            IsMyTurn = true;
            First.text="당신이 선공입니다.";
        }
        else
        {
            IsMyFirst= false;
            CanThrow= false;
            IsMyTurn= false;
            enemyController.IsEnemyTurn = true;
            enemyController.EnemyTurn();
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
        if(enemyController.IsEnemyTurn||!CanThrow)
            { return; }
        ThrowYut();
    }

    public void ThrowYut()
    {
        bool IsEnemy = enemyController.IsEnemyTurn;

        if (!IsEnemy)
        {
            if (!CanThrow) return;
            CanThrow = false;
        }
           

        Yut currentYut = GetYut();
        TurnYutResult.Add(currentYut);
        Debug.Log("윷이 리스트로 들어갑니다.");
        if (currentYut == Yut.zero)
        {
            TurnYutResult.Clear();
            moC = 0;
            moCount.text = $"{moC}";
            yutC = 0;
            yutCount.text = $"{yutC}";
            currentRestYut = Yut.zero;
            yutname.text = "";
            TurnEnd();
            return;
        }
        if(currentYut == Yut.four|| currentYut == Yut.five)
        {
            if (currentYut == Yut.four) 
            { yutC++; yutCount.text = $"{yutC}"; }
            else 
            { moC++; moCount.text = $"{moC}"; }

            resultYut.text = $" {ChangeYutText(currentYut)}이 나왔군요. 한 번 더 던지세요";

            if(!IsEnemy)
            {
                CanThrow = true;
            }
            else 
            {
                StartCoroutine(EnemyAginThrow());
                resultYut.gameObject.SetActive(true);
                StartCoroutine(FalseText(resultYut));
                return;
            }
        }
        else 
        {
            resultYut.text = ChangeYutText(currentYut);
            yutname.text = ChangeYutText(currentYut);
            CanThrow = false;
            currentRestYut = currentYut;

            if(IsEnemy)
            {
                enemyController.EnemyBestMove();
               
            }
        }
        resultYut.gameObject.SetActive(true);
        StartCoroutine(FalseText(resultYut));
        if (!IsEnemy)
        { canUseYut = true; }
    }


    public void RemoveYutUi(int index)
    {
        if (index >= 0 && index < TurnYutResult.Count)
        {
            




            if (TurnYutResult[index] == Yut.five)
            {
                    moC--;
                    moCount.text = $"{moC}";
            }
                else if (TurnYutResult[index] == Yut.four)
                {
                    yutC--;
                    yutCount.text = $"{yutC}";
                }
                else
                {
                    currentRestYut = Yut.zero;
                    yutname.text = "";
                }
                
            
        }
    }

    public IEnumerator EnemyAginThrow()
    {
        yield return new WaitForSeconds(1f);
        ThrowYut();
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
       
        if(canUseYut == true)
        {
            Yut targetYut;

            if (value == -999)
            {
                if (currentRestYut == Yut.zero) return;
                targetYut = currentRestYut;
            }
            else
            {
                targetYut = (Yut)value;
            }
            if (TurnYutResult.Contains(targetYut))
            {
                selectYut = targetYut;
                selectMoveSpace = (int)targetYut;
                isYutSelected = true;
            }
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
        if(TurnYutResult ==null|| TurnYutResult.Count ==0) return;
        if (TurnYutResult[0]== Yut.zero)
        { TurnYutResult.RemoveAt(0); return; }
        if (IsMyFirst == true)
           
            Player.StartNewChar(selectMoveSpace,false);

            UseSelectedYut();

    }

    
    

}
