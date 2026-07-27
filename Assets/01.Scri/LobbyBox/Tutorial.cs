using KoreanTyper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Tutorial : LobbyUiManager
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI textreading;
    [SerializeField] GameObject page;
    [SerializeField] GameObject npc;
    [SerializeField] Button tutorial;

    private bool isTutorialEnd = false;

    List<string> tutorialTexts = new List<string>();

    public override void Start()
    {
        text1.gameObject.SetActive(false);
        textreading.gameObject.SetActive(false);
        page.SetActive(false);
        npc.SetActive(false);

        tutorialTexts.Add("오랜만이구나 손자야 \r\n너가 이 편지를 보고 있다면 잘 도착한거 같구나.\r\n처음 보는 장소여서 많이 낯설텐데  진정하고 잘 들어라.\r\n지금 세계가 많이 위험하구나 너까지 끌어들여 미안하구나.\r\n내가 너를 이 곳에 소환한 이유는 \r\n너가 해줬으면 하는 일이 있어서 그렇다. \r\n\r\n ");
        tutorialTexts.Add("이 일은 오직 너 밖에 할 수 없는 일이니 잘 듣고 해주거라.\r\n그렇게 어려운 일은 아니란다.\r\n너가 예전부터 윷놀이를 좋아했었지..\r\n밖에 나가서 윷놀이만 하면 된다. \r\n그게 무슨 소리인가 싶겠지만 아무튼 하는 것이다.\r\n길게 설명 할 시간이 없으니 다음으로 넘어가겠다.");
        tutorialTexts.Add("우선 종족 선택을 할 수 있는데 \r\n지금은 너밖에 선택할 수 없지만 \r\n나중에는 너가 이긴 상대도 사용 할 수 있으니 알아두거라.\r\n그리고 훈련실에서 윷놀이를 연습하면서 강화 할 수 있으니\r\n알아두거라.\r\n 상태창에서 너의 현재 상태도 알 수 있으니 알아두거라");
        tutorialTexts.Add("앨범은 너가 이긴 상대와의 인연을 그려가는 것이다.  \r\n전부 모을 수 있다면 모아두는 게 좋을거다.\r\n여기까지는 로비의 설명이고 다음으로 넘어가겠다.\r\n 영문도 모르는데 설명만 해서 미안하지만 좀 만 참아다오.\r\n이것도 다 너를 위한 것이다.");
        tutorialTexts.Add("놀러가기를 누르면 너의 이름을 가진 창이 보일 텐데\r\n여기는 연습을 위해서 내가 만든 칸이니 \r\n거기부터 시작하도록 하거라.\r\n들어가면 누가 먼저 시작하는지 나올텐데\r\n선공이면 윷 던지기를 누르면 윷의 결과가 좌측 상단에\r\n 나올 거란다. 결과를 누르고 말을 움기면 된단다.");
        tutorialTexts.Add("처음에 말을 꺼낼 때는 너의 좌측 하단의 작은 너의 아이콘이 보일텐데 그걸 누르면 새로운 말을 꺼낼 수가 있다.\r\n밖에 나온 말을 움직이고 싶으면 윷의 결과를 누르고 \r\n말을 누르면 움직인단다.\r\n너가 알고 있는 윷놀이랑 룰은 같은데 다른게 있다면\r\n윷을 던지기 싫으면 턴을 넘겨도 된단다.");
        tutorialTexts.Add("무조건 윷을 던진다고 유리해지는 건 아니니까\r\n 할아버지가 만들어 났단다.\r\n그리고 여기는 말이 들어가면 상대에게 데미지를 주면서 \r\n나를 회복할 수 있단다.  \r\n상대의 체력을 0으로 만들면 이긴거란다.\r\n상대는 체력이 줄어들면 무슨 짓을 할 지 모르니 조심하거라.");
        tutorialTexts.Add("너도 스킬을 쓸 수 있는데 그거는 윷 결과창 밑에 있으니 누르면 조건에 따라서 사용할 수 있단다.\r\n3번 밖에 사용 할 수 없으니 주의하거라.\r\n그리고 한번 싸움이 시작하면 누군가가 쓰러지기 전까지 \r\n돌아갈 수 없는 진실의 방같은 것이니 잊지 말거라.\r\n\r\n");
        tutorialTexts.Add("설명은 끝났다. 사랑하는 손자 철수야 \r\n시가 ㄴ이 많이 안ㄴ ㅏㅁ으 ㄴ 듯 하구ㄴ ㅏ\r\n아무 ㄹ ㅣ 힘 들어도  우 ㅅ음은 잊 ㅈ ㅣ\r\n 말 ㄷ ㅗ록 하 ㄱ ㅓ라.  \r\n\r\n                                            -할ㅇ ㅏ버 ㅈ ㅣ 가-\r\n");
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        page.SetActive(true);
        npc.SetActive(true);
        StartCoroutine(tutorialText(text1, tutorialTexts));
    }

    public override void ExitPanel()
    {
        if (isTutorialEnd)
        {
            page.SetActive(false);
            npc.SetActive(false);
            base.ExitPanel();
        }
        else
        {
            textreading.gameObject.SetActive(true);
            StartCoroutine(FalseText1(textreading));
        }
    }

    public IEnumerator FalseText1(TextMeshProUGUI Text)
    {
        yield return new WaitForSeconds(1f);
        Text.gameObject.SetActive(false);

    }

    public IEnumerator tutorialText(TextMeshProUGUI text, List<string> strings)
    {
        isTutorialEnd = false;
        text.gameObject.SetActive(true);
        for(int i = 0; i<strings.Count;i++)
        {
            text.text = "";
            int strTypingCount = strings[i].GetTypingLength();
            for (int j = 0; j <= strTypingCount; j++)
            {
                text.text = strings[i].Typing(j);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return null;
        }
           text.gameObject.SetActive(false);
        isTutorialEnd=true;
    }

 

}
