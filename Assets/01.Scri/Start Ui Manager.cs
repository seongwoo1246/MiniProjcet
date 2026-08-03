
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
    [SerializeField] VideoPlayer IntroPlay;
    [SerializeField] RawImage raw;
    [SerializeField] Image EndAfter;
    [SerializeField] GameObject IntroCanvas;
    [SerializeField] GameObject dim;
    private bool IsIntroview = false;



    void Start()
    {
        if (IsIntroview == false)
        {
            if (dim != null) dim.SetActive(true);
            if (IntroCanvas != null) IntroCanvas.SetActive(true);
            IntroPlay.gameObject.SetActive(true);

            IntroPlay.playOnAwake = false;

            IntroPlay.prepareCompleted -= onVideoPreParred;
            IntroPlay.prepareCompleted += onVideoPreParred;

            IntroPlay.loopPointReached -= IntroEnd;
            IntroPlay.loopPointReached += IntroEnd;

            if (IntroPlay.targetTexture != null)
            {
                IntroPlay.targetTexture.Release();
            }

            IntroPlay.Prepare();


            if (IntroPlay.targetTexture != null)
            {
                IntroPlay.targetTexture.Release();
            }
        }

        Easy.gameObject.SetActive(false);
        Normal.gameObject.SetActive(false);
        Hard.gameObject.SetActive(false);
        VideoPlay.gameObject.SetActive(false);
        raw.gameObject.SetActive(false);
        VideoPlay.loopPointReached += OnvideoEnd;
       
    }

  

    private void onVideoPreParred(VideoPlayer videoPlayer)
    {
        videoPlayer.Play();
    }


    void IntroEnd(VideoPlayer videoPlayer)
    {
        IsIntroview = true;

        IntroPlay.prepareCompleted -= onVideoPreParred;
        IntroPlay.loopPointReached -= OnvideoEnd;


        if(dim !=null)dim.SetActive(false);
        if(IntroCanvas != null)IntroCanvas.SetActive(false);

        if (ScenesM.instance.IsviewEnd == true)
        {
            SoundManager.instance.PlayBGM(SoundManager.instance.hidenStart);
            EndAfter.gameObject.SetActive(true);
        }
        else
        {
            SoundManager.instance.PlayBGM(SoundManager.instance.nomalStart);
            EndAfter.gameObject.SetActive(false);
        }
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
