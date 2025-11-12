using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Created By: Abraar Sadek
 * Created On: 11-06-2025
 * Purpose: This script will handle the game over menu navigation within the game.
 * 
 * Last Modified By: Juan Amorocho
 * Last Modified On: 11-12-2025
 * Last Modification Made: Changed Scene Management and Support for Game Win/Lost Text.
 */

//GameOverMenuController Class - Used to control the game over menu UI
public class GameOverMenuController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private Button nextLevelButton;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [Header("Game Over Text Presets")]
    [SerializeField]
    private string wonText = "LEVEL COMPLETE!";
    [SerializeField]
    private string wonGame = "GAME COMPLETE!";
    [SerializeField]
    private string lostGame = "YOU GOT CAUGHT!";
    //PlayAgainButton Method - Loads the level scene when the "PLAY AGAIN" button is pressed
    void Start()
    {
        //Check if we're currently in Last Level (4 -> Level 3) or if the player lost the last level. 
        if (SceneTracker.CurrentLevel >= 4 || !SceneTracker.WonLastLevel)
        {
            if (nextLevelButton != null)
            {
                nextLevelButton.gameObject.SetActive(false);
            }
            else { Debug.LogError("There's no Next Level Button assigned to Game Over Controller"); }
        }
        if (titleText != null) //Sets appropiate text depending on game state and previous level won/lost.
        {
            if (SceneTracker.WonLastLevel)
            {
                if (SceneTracker.CurrentLevel >= 4)
                    titleText.text = wonGame;
                else
                    titleText.text = wonText;
            }
            else
            {
                titleText.text = lostGame;
            }
        }
        else
        {
            Debug.LogError("There's no Title Text assigned to Game Over Controller");
        }
        if (scoreText != null)
        {
            scoreText.text = "COINS COLLECTED: " + SceneTracker.CurrentScore.ToString();
        }
        else
        {
            Debug.LogError("There's no Score Text assigned to Game Over Controller");
        }
    }
    public void PlayNextLevel() { SceneManager.LoadSceneAsync(SceneTracker.CurrentLevel + 1); }
    public void PlayAgainButton() { SceneManager.LoadSceneAsync(SceneTracker.CurrentLevel); }


    //SettingsButton Method - Loads the settings scene when the "SETTINGS" button is pressed
    public void SettingsButton()
    {

        SceneTracker.PreviousSceneIndex = SceneManager.GetActiveScene().buildIndex; //Store the current scene index before loading the settings scene
        SceneManager.LoadSceneAsync(1);

    } //End of SettingsButton Method

    //QuitGameButton Method - Loads the main menu scene when the "QUIT GAME" button is pressed
    public void QuitGameButton() { SceneManager.LoadSceneAsync(0); }

} //End of GameOverMenuController Class
