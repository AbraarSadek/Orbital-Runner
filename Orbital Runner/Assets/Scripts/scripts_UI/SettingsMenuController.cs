using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Created By: Abraar Sadek
 * Created On: 11-06-2025
 * Purpose: This script will handle the settings menu navigation within the game.
 * 
 * Last Modified By: Abraar Sadek
 * Last Modified On: 11-06-2025
 * Last Modification Made: Initial creation of the SettingsMenuController script.
 */

//SettingsMenuController Class - Used to control the settings menu UI
public class SettingsMenuController : MonoBehaviour {

    //BackButton Method - Loads the main menu scene when the "BACK" button is pressed
    public void BackButton() {

        int targetScene = SceneTracker.PreviousSceneIndex; //Int variable that will hold the index of the previously loaded scene from the SceneTracker class

        //If-Else Statement - Checks if the target scene is either the Main Menu (0) or Game Over Menu (3)
        if (targetScene == 0 || targetScene == 3) {
            SceneManager.LoadSceneAsync(targetScene); //Load the target scene if it is either the Main Menu or Game Over Menu
        } else {
            SceneManager.LoadSceneAsync(0); //Load the Main Menu scene if the target scene is not the Main Menu or Game Over Menu
        } //End of If-Else Statement

    } //End of BackButton Method

} //End of SettingsMenuController Class
