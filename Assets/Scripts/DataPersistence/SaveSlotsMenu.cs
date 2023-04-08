using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotsMenu : MonoBehaviour
{
    SaveSlot[] saveSlots;

    bool isLoadingGame = false;

    void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();

    }

    #region OnSaveSlotClicked() Summary:
    /* This function*/
    #endregion
    public void OnSaveSlotClicked(SaveSlot slot)
    {
        //Update the selected ProfileID to be used for Data persistence.
        DataPersistenceManager.instance.ChangeSelectedProfileID(slot.GetProfileID());

        if (!isLoadingGame)
        {
            //Create a new game - which will initialize our data with a clean slate.
            DataPersistenceManager.instance.NewGame();
        }

        //Load the scene, which will save the scene as a result becase of OnSceneUnloaded.
        //SceneManager.LoadSceneAsync(/*insert scene name here*/);
    }

    #region ActivateMenu() Summary
    /* This function should be called when navigating from the main menu to the load saved game menu.
     * There are two instances where it will be called:
     *      1. If the bool passed in is false, then that means the player is navigating to this menu via the "NewGame" button.
     *      2. If the bool passed in is true, then that means the player is navigating to this menu via the "LoadGame" button.
     * Depending on the situation, the menu should function accordingly.
     * .*/
    #endregion
    public void ActivateMenu(bool isLoading)
    {
        this.gameObject.SetActive(true);

        isLoadingGame = isLoading;

        //Load all existing profiles
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        //Loop through each save slot and load the appropriate content.
        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);

            if (profileData == null && isLoadingGame)
            {
                //Set the save slot button to be non-interactible.
            }
            else
            {
                //Set the save slot button to be interactible.
            }
        }
    }

    //Could just do this using the buttons instead.
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
