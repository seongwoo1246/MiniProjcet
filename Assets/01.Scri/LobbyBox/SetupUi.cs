using UnityEngine;
using UnityEngine.UI;
public class SetUPUi : LobbyUiManager
{
    [SerializeField] GameObject SetUP;
    [SerializeField] Slider BGMVolume;
    [SerializeField] Slider SFXVolume;
    [SerializeField] Slider VoiceVolume;
    public override void Start()
    {
        SetUP.SetActive(false);
    }
    public override void OpenPanel()
    {
        base.OpenPanel();
        SetUP.SetActive(true);

    }
    public override void ExitPanel()
    {
        SetUP?.SetActive(false);
        base.ExitPanel();

    }

    public void GetBGMVolume(float volume)
    {
        volume = BGMVolume.value;
        SoundManager.instance.SetBGMVolume(volume);
    }
    public void GetVoiceVolume(float volume)
    {
        volume = VoiceVolume.value;
        SoundManager.instance.SetVoiceVolume(volume);
    }
    public void GetSFXVolume(float volume)
    {
        volume = SFXVolume.value;
        SoundManager.instance.SetSFXVolume(volume);
    }
}