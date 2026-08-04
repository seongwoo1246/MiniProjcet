using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [SerializeField] VideoPlayer IntroPlay;
    [SerializeField] GameObject IntroCanvas;
    [SerializeField] GameObject dim;
    [SerializeField] Image EndAfter;

    
    private void Start()
    {
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

        if (ScenesM.instance.IsIntroview == false)
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
        else
        {
            if (dim != null) dim.SetActive(false);
            if (IntroCanvas != null) IntroCanvas.SetActive(false);
            IntroPlay.gameObject.SetActive(false);
        }

    }

    void IntroEnd(VideoPlayer videoPlayer)
    {
        ScenesM.instance.IsIntroview = true;

        IntroPlay.prepareCompleted -= onVideoPreParred;
        //IntroPlay.loopPointReached -= OnvideoEnd;


        if (dim != null) dim.SetActive(false);
        if (IntroCanvas != null) IntroCanvas.SetActive(false);

      
    }

    private void onVideoPreParred(VideoPlayer videoPlayer)
    {
        videoPlayer.Play();
    }

 
}
