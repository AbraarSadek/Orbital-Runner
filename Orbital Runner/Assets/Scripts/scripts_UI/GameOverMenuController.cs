using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Created By: Abraar Sadek
 * Created On: 11-06-2025
 * Purpose: This script will handle the game over menu navigation within the game.
 * 
 * Modifie By: Juan Amorocho
 * Modified On: 11-12-2025
 * Last Modification Made: Changed Scene Management and Support for Game Win/Lost Text.
 * 
 * Modifie By: Abraar Sadek
 * Modified On: 11-20-2025
 * Last Modification Made: Made it so that the "NEXT LEVEL" button is greyed out instead of being diabled, cleaned up code and added comments for clarity.
 * 
 */

//GameOverMenuController Class - Used to control the game over menu UI
public class GameOverMenuController : MonoBehaviour {

    [Header("Dependencies")]
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Game Over Text Presets")]
    [SerializeField] private string wonText = "LEVEL COMPLETE!";
    [SerializeField] private string wonGame = "GAME COMPLETE!";
    [SerializeField] private string lostGame = "YOU GOT CAUGHT!";

    //Start Method - Called before the first frame update
    private void Start() {

        //If-Else Statement - To Check if the Next Level button should be disabled
        // Disable (grey out) the Next Level button if:
        // - the player lost the level, OR
        // - the player is on the final level (level 4 in your current setup)
        if (!SceneTracker.WonLastLevel || SceneTracker.CurrentLevel >= 4) {

            //Nested If-Else Statement - To Check if the Next Level button is assigned
            if (nextLevelButton != null) {
                nextLevelButton.interactable = false;  // <--- GREY OUT / DISABLE CLICK
            } else {
                Debug.LogError("Next Level Button is not assigned in the inspector.");
            }

        } // End of If-Else Statement

        //If-Else Statement - Set the title text (win, lose, game complete)
        if (titleText != null) {

            //Nested If-Else Statement - To Check if the player won or lost the last level
            if (SceneTracker.WonLastLevel) {

                //Nested If-Else Statement - To Check if the player completed the game
                if (SceneTracker.CurrentLevel >= 4) { titleText.text = wonGame; } else { titleText.text = wonText; }
            
            } else {
            
                titleText.text = lostGame;
            
            } // End of Nested If-Else Statement
        
        } else {
            Debug.LogError("Title Text is not assigned in the inspector.");
        } //End of If-Else Statement

        //If-Else Statement - Set the score text
        if (scoreText != null) {
            scoreText.text = $"COINS COLLECTED: {SceneTracker.CurrentScore}";
        } else {
            Debug.LogError("Score Text is not assigned in the inspector.");
        } //End of If-Else Statement

    } //End of Start Method

    //PlayNextLevel Method - Loads the the next level scene when the "NEXT LEVEL" button is pressed
    public void PlayNextLevel() { SceneManager.LoadSceneAsync(SceneTracker.CurrentLevel + 1); }

    //PlayAgainButton Method - Loads the the previous level scene when the "NEXT LEVEL" button is pressed
    public void PlayAgainButton() { SceneManager.LoadSceneAsync(SceneTracker.CurrentLevel); }

    //SettingsButton Method - Loads the settings menu scene when the "SETTINGS" button is pressed
    public void SettingsButton() {

        // Store the current scene index before loading the settings scene
        SceneTracker.PreviousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(1);

    } //End of SettingsButton Method

    //QuitGameButton Method - Loads the main menu scene when the "QUIT GAME" button is pressed
    public void QuitGameButton() { SceneManager.LoadSceneAsync(0); }

} //End of GameOverMenuController Class