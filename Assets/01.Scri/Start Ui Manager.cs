
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class StartUi : MonoBehaviour
{
    [SerializeField] Button StartB;
    [SerializeField] Button Easy;
    [SerializeField] Button Normal;
    [SerializeField] Button Hard;
    [SerializeField] Button ExitGameButton;
    [SerializeField] VideoPlayer VideoPlay;
    [SerializeField] RawImage raw;
  
   
   



    void Start()
    {
        

        Easy.gameObject.SetActive(false);
        Normal.gameObject.SetActive(false);
        Hard.gameObject.SetActive(false);
        VideoPlay.gameObject.SetActive(false);
        raw.gameObject.SetActive(false);
        VideoPlay.loopPointReached += OnvideoEnd;
       
    }







    void OnvideoEnd(VideoPlayer videoPlayer)
    {
        VideoPlay.gameObject.SetActive(false);
        raw.gameObject.SetActive(false);
        ExitGameButton.gameObject.SetActive(true);
        StartB.gameObject.SetActive(true);
        Easy.gameObject.SetActive(false);
        Normal.gameObject.SetActive(false);
        Hard.gameObject.SetActive(false);
        ScenesM.instance.LoadScenes(scenetpye.Lobby);

    }

    public void GameStart()
    {
        SoundManager.instance.PlayVoice("æ»≥Á«œººø‰");
        StartB.gameObject.SetActive(false);
        ExitGameButton.gameObject.SetActive(false);
        Easy.gameObject.SetActive(true);
        Normal.gameObject.SetActive(true);
        Hard.gameObject.SetActive(true);
    }

    public void EasyStart()
    {
        SoundManager.instance.PlaySFX("ªÕ");
        ScenesM.instance.SetDifficulty(Difficulty.easy);
        VideoPlay.gameObject.SetActive(true);
        raw.gameObject.SetActive(true);
        VideoPlay.Play();
        
    }
    public void NormalStart()
    {
        SoundManager.instance.PlaySFX("ªÕ");
        ScenesM.instance.SetDifficulty(Difficulty.normal);
        VideoPlay.gameObject.SetActive(true);
        raw.gameObject.SetActive(true);
        VideoPlay.Play();

    }

    public void HardStart()
    {
        SoundManager.instance.PlaySFX("ªÕ");
        ScenesM.instance.SetDifficulty(Difficulty.hard);
        VideoPlay.gameObject.SetActive(true);
        raw.gameObject.SetActive(true);
        VideoPlay.Play();


    }


    public void ExitGame()
    {

        
        Application.Quit();
    }
}
