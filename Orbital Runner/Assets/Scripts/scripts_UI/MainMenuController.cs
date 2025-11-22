using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Created By: Abraar Sadek
 * Created On: 11-06-2025
 * Purpose: This script will handle the main menu navigation within the game.
 * 
 * Last Modified By: Abraar Sadek
 * Last Modified On: 11-06-2025
 * Last Modification Made: Initial creation of the MainMenuController script.
 */

//MainMenuController Class - Manages main menu navigation
public class MainMenuController : MonoBehaviour {

    //PlayGameButton Method - Loads the level scene when the "PLAY GAME" button is pressed
    public void PlayGameButton() { SceneManager.LoadSceneAsync(2); }

    //OptionsButton Method - Loads the settings scene when the "Options" button is pressed
    public void OptionsButton() {

        SceneTracker.PreviousSceneIndex = SceneManager.GetActiveScene().buildIndex; //Store the current scene index before loading the options scene
        SceneManager.LoadSceneAsync(1);

    } //End of OptionsButton Method

    //QuitGameButton Method - Quits the game when the "QUIT GAME" button is pressed
    public void QuitGameButton()
    {
        Debug.Log("Closing Application");
        Application.Quit();
    }

} //End of MainMenuController Class