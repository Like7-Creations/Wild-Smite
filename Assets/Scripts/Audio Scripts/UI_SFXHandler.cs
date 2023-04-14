using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SFXHandler : MonoBehaviour
{
    AudioSource audioSource;

    #region Base Menu SFX
    [SerializeField] AudioClip pauseGame_SFX;
    [SerializeField] AudioClip resumeGame_SFX;

    [SerializeField] AudioClip pointerHover_SFX;
    [SerializeField] AudioClip sceneTransition_SFX;
    #endregion

    #region PlayerLobby SFX
    [SerializeField] AudioClip playerJoined_SFX;
    [SerializeField] AudioClip playerReady_SFX;
    #endregion

    #region Levelling Stats Screen SFX
    [SerializeField] AudioClip displayBattleLog_SFX;

    [SerializeField] AudioClip increaseStat_SFX;
    [SerializeField] AudioClip decreaseStat_SFX;

    [SerializeField] AudioClip lvlSliderIncrease_SFX;
    [SerializeField] AudioClip lvlSliderComplete_SFX;
    #endregion

    //----------Functions----------
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    #region Base Menu SFX Functions
    public void Play_PauseGameSFX()
    {
        audioSource.clip = pauseGame_SFX;
        audioSource.Play();
    }

    public void Play_ResumeGameSFX()
    {
        audioSource.clip = resumeGame_SFX;
        audioSource.Play();
    }

    public void Play_PointerHoverSFX()
    {
        audioSource.clip = pointerHover_SFX;
        audioSource.Play();
    }

    public void Play_SceneTransitionSFX()
    {
        audioSource.clip = sceneTransition_SFX;
        audioSource.Play();
    }
    #endregion

    #region Player Lobby SFX Functions
    public void Play_PlayerJoinedSFX()
    {
        audioSource.clip = playerJoined_SFX;
        audioSource.Play();
    }

    public void Play_PlayerReadySFX()
    {
        audioSource.clip = playerReady_SFX;
        audioSource.Play();
    }
    #endregion

    #region Levelling Stats Screen SFX Functions
    public void Play_DisplayBattleLogSFX()
    {
        audioSource.clip = displayBattleLog_SFX;
        audioSource.Play();
    }

    public void Play_IncreaseStatSFX()
    {
        audioSource.clip = increaseStat_SFX;
        audioSource.Play();
    }

    public void Play_DecreaseStatSFX()
    {
        audioSource.clip = decreaseStat_SFX;
        audioSource.Play();
    }

    public void Play_LvlSliderSFX()
    {
        audioSource.clip = lvlSliderIncrease_SFX;
        audioSource.Play();
    }

    public void Play_LvlSliderCompleteSFX()
    {
        audioSource.clip = lvlSliderComplete_SFX;
        audioSource.Play();
    }
    #endregion
    //----------Functions----------
}