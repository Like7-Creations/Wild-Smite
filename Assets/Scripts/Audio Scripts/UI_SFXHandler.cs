using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SFXHandler : MonoBehaviour
{
    AudioSource audioSource;

    #region Base Menu SFX
    [SerializeField] SFXClip pause_SFX;
    [SerializeField] SFXClip resume_SFX;

    [SerializeField] SFXClip hoverSelected_SFX;
    [SerializeField] SFXClip transitionScene_SFX;
    #endregion

    #region PlayerLobby SFX
    [SerializeField] SFXClip pJoined_SFX;
    [SerializeField] SFXClip pReady_SFX;
    #endregion

    #region Levelling Stats Screen SFX
    [SerializeField] SFXClip showBattleLog_SFX;

    [SerializeField] SFXClip statIncrease_SFX;
    [SerializeField] SFXClip statDecrease_SFX;

    [SerializeField] SFXClip increaseLvlSlider_SFX;
    [SerializeField] SFXClip decreaseLvlSlider_SFX;
    #endregion

    //----------Functions----------
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    #region Base Menu SFX Functions
    public void Play_PauseGameSFX()
    {
        audioSource.PlayOneShot(pause_SFX.clip, pause_SFX.voumeVal);
    }

    public void Play_ResumeGameSFX()
    {
        audioSource.PlayOneShot(resume_SFX.clip, resume_SFX.voumeVal);
    }

    public void Play_PointerHoverSFX()
    {
        audioSource.PlayOneShot(hoverSelected_SFX.clip, hoverSelected_SFX.voumeVal);
    }

    public void Play_SceneTransitionSFX()
    {
        audioSource.PlayOneShot(transitionScene_SFX.clip,transitionScene_SFX.voumeVal);
    }
    #endregion

    #region Player Lobby SFX Functions
    public void Play_PlayerJoinedSFX()
    {
        audioSource.PlayOneShot(pJoined_SFX.clip,pJoined_SFX.voumeVal);
    }

    public void Play_PlayerReadySFX()
    {
        audioSource.PlayOneShot(pReady_SFX.clip,pReady_SFX.voumeVal);
    }
    #endregion

    #region Levelling Stats Screen SFX Functions
    public void Play_DisplayBattleLogSFX()
    {
        audioSource.PlayOneShot(showBattleLog_SFX.clip,showBattleLog_SFX.voumeVal);
    }

    public void Play_IncreaseStatSFX()
    {
        audioSource.PlayOneShot(statIncrease_SFX.clip, statIncrease_SFX.voumeVal);
    }

    public void Play_DecreaseStatSFX()
    {
        audioSource.PlayOneShot(statDecrease_SFX.clip,statDecrease_SFX.voumeVal);
    }

    public void Play_LvlSliderSFX()
    {
        audioSource.PlayOneShot(increaseLvlSlider_SFX.clip,increaseLvlSlider_SFX.voumeVal);
    }

    public void Play_LvlSliderCompleteSFX()
    {
        audioSource.PlayOneShot(decreaseLvlSlider_SFX.clip, decreaseLvlSlider_SFX.voumeVal);
    }
    #endregion
    //----------Functions----------
}