using KoreanTyper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI textreading;
    [SerializeField] Image dim;
    [SerializeField] Image BackGround;
    [SerializeField] Button Re_Start;

    float duration = 2f;
    List<string> endingCridic = new List<string>();


    private void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.nomalStart);
        Re_Start.gameObject.SetActive(false);
        dim.gameObject.SetActive(false);
        textreading.gameObject.SetActive(false);

        endingCridic.Add("게임을 플레이 해주셔서 감사합니다.\r\n여기서는 이 게임의 스토리에 대해서\r\n간단하게 말하고 끝낼 생각입니다.\r\n원래 스토리상 할아버지가 소환한걸로 되어 있지만\r\n그거는 사실 함정이였고 진짜 할아버지는 \r\n함정에 빠져 세계수로 향하던 도중 죽게 됩니다.\r\n할머니가 천사였는데 자신의 남편을 모략으로 죽인\r\n세계에 절망하며 남편의 시신을 들고 사라집니다.");
        endingCridic.Add("천사가 자신의 힘으로 되살릴려고 하나\r\n힘이 부족하여 언데드로 부활을 하고 만것입니다.\r\n고블린은 할아버지의 친한 친구였는데\r\n주인공을 최종보스와 헷갈려서 \r\n복수를 위해 공격을 한 것입니다.\r\n오해가 풀리고 같이 할아버지가 죽게 된 이유인\r\n세계수로 향하게 되는데 \r\n거기서 엘프에게 방해를 받게 됩니다.\r\n");
        endingCridic.Add("엘프는 세계수를 지키기 위해 싸우는 과정에서 \r\n오해를 풀게 되는데 최근에 인간들의 침공이 \r\n많았던 것이 공격한 이유였습니다.\r\n제국에서 세게수를 탐내고 있었기에 \r\n생긴 문제였으나 자신의 결백을 증명에 성공하고\r\n제국으로 돌아가던 중\r\n언데드인 할아버지와 만나게 되는데\r\n언데드가 되며 기억을 잊어버린 할아버지가 \r\n공격해 왔습니다.");
        endingCridic.Add("언데드인 할아버지와 싸워 이기고\r\n 해치우려는 찰나에 천사가 나타나게 되는데 \r\n그게 할머니에 해당하는 천사입니다.\r\n자신의 손자 인 줄 알아보지 못한 \r\n천사가 남편을 지키기 위해 싸우게 되는데 \r\n긴 싸움 끝에 주인공 일행이 이기게 되는데\r\n천사에 애원으로 언데드를 되도릴 방법을 \r\n생각하다가 엘프가 세계수를 써보는건\r\n어떠냐는 의견을 던집니다.");
        endingCridic.Add("세계수의 힘을 빌려 언데드를 사람으로 만드는건\r\n실패했지만 정신을 되돌리는데는 성공하게 됩니다.\r\n정신을 차리는데 성공한 할아버지와 주인공에 재회\r\n그리고 천사가 알게 되는 손자의 존재등등\r\n많은 해프닝이 있지만 \r\n내용이 길기에 여기서는 생략합니다.\r\n그렇게 제국에 가니 \r\n최종 빌런인 황제가 나오는데\r\n그 모습은 놀랍게도 주인공과 같은 모습이였습니다.");
        endingCridic.Add("자신과 같은 모습을 하고 있는 상대와\r\n싸움에서 패배하게 되는데\r\n포기 하지 않고 제국군에게서 도망치며\r\n모험을 떠나 최후에 미궁 깊숙한 곳에서\r\n꽃을 찾게 되는데 그 꽃은 전설 속에서 전해져 오던\r\n꽃이였는데  그 꽃을 세계수에 가져다 주면\r\n작은 기적이 일어난다는 말을 하는 엘프와\r\n그 말에 희망을 가지게 되는  주인공 일행 ");
        endingCridic.Add("세계수에 도착한 주인공 일행은\r\n꽃을 세게수에게 가져다 주었고 \r\n세계수가 빛나기 시작했다.\r\n그 빛이 주인공 일행을 감싸며\r\n 따듯한 온기를 가져다주게되는데 \r\n그 온기에서 힌트를 얻게 된 주인공 일행은\r\n제국에 다시 싸움을 도전하고 \r\n주인공일행이 위험할 때 몸에 있던 \r\n온기가 밖으로 나와 빛나기 시작하는데");
        endingCridic.Add("빛이 점점 커지며 제국의 황제를 감싸자\r\n순식간에 황제가 사라져 버렸다.  \r\n그 모습을 바라보던\r\n주인공 일행은 당황하는 한편 \r\n주인공 철수가 가장 먼저 외친 것이다.\r\n황제를 이긴 자신이 황제라고 선언한 것이다.\r\n기뻐하는 주인공 일행 다시 평화로워 지는 세계\r\n                                                                                   \r\n인줄 알았다.....");
        endingCridic.Add("점점 자신의 권력에 취해 폭정을 행하는 철수 \r\n그를 가로막으려 했던 주인공 일행을 처형하게 되는데\r\n그렇게 고독한 왕좌에 앉아 \r\n자신의 권력에 취해 살고 있던 어느날\r\n그는 보게 된다. \r\n자신의 모습을 하고있는 또 다른 나의 모습을\r\n과거 자신이 했던 것 처럼 동료들과 함께 도전하는\r\n자신의 모습을 바라보며 철수는 알게 된다.");
        endingCridic.Add("자신이 지금 끝나지 않는\r\n 루프안에 같혀 있다는 사실을...\r\n자신이 쓰러트렸다고 \r\n생각하던 황제조차 적이 아니고 \r\n그저 세계수의 작은 기적으로 인해\r\n 과거로 날아가 버린 \r\n자신의 미래의 모습이였다는 걸\r\n철수는 조용히 기다린다.");
        endingCridic.Add("아무 것도 모르는채 도전하러 오는 자신의 모습을 \r\n-영원히 벗어날 수 없는 루프 -\r\n\r\n\r\n플레이 해주셔서 감사합니다.");
        endingCridic.Add("게임 제작 기간 : 3주\r\n\r\n게임 기획 : 정성우 \r\n\r\n게임 제작 : 정성우 \r\n\r\nUi 제작 : 정성우 \r\n\r\n에셋제작 : 제미나이 ");
        endingCridic.Add("음악 제작 : 스누\r\n\r\n영상 편집 : 정성우\r\n\r\n스토리 작가 : 정성우 \r\n\r\n총괄 감독 : 정성우 \r\n\r\n플레이 해주셔서 감사합니다.");

        StartCoroutine(EndingText(textreading, endingCridic));
    }

    public void ComeBack()
    {
        SoundManager.instance.PlaySFX("뽕");
        ScenesM.instance.LoadScenes(scenetpye.start);
    }

    public IEnumerator EndingText(TextMeshProUGUI text, List<string> strings)
    {
        dim.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        for (int i = 0; i < strings.Count; i++)
        {
            SoundManager.instance.PlaySFX("뽕");
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
        dim.gameObject.SetActive(false);

        SoundManager.instance.PlayBGM(SoundManager.instance.hidenStart);

        float elapsedTime = 0f;
        Color startColor = BackGround.color;
        Color endColor = Color.red;
        endColor = new Color(1f, 0, 0, startColor.a);

        while (elapsedTime <duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            BackGround.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }

        BackGround.color = endColor;

        Re_Start.gameObject.SetActive(true);
        ScenesM.instance.IsviewEnd = true;
    }

  

}
