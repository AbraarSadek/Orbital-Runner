using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Created By: Abraar Sadek
 * Created On: 11-26-2025
 * Purpose: This script will handle the pause panel navigation within the game.
 * 
 * Last Modified By: Abraar Sadek
 * Last Modified On: 11-26-2025
 * Last Modification Made: Initial creation of the PausePanelController script.
 */

//PausePanelController Class - Used to control the pause panel UI
public class PausePanelController : MonoBehaviour {

    [Header("Pause Panel UI")]
    public GameObject pausePanel;

    private bool isPaused = false;

    //Start Method - Called before the first frame update
    void Start() {

        Time.timeScale = 1f; //Set the time scale to 1 at the start of the game
        pausePanel.SetActive(false); //Ensure the pause panel is hidden at the start of the run

    } //End of Start Method

    //Update Method - Called once per frame
    void Update() {

        //If-Statement - Check for Escape key press to toggle pause state
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {

            //Nested If-Else Statement - Toggle between pausing and resuming the game
            if (isPaused) {
                ResumeGame(); //Call Method
            } else {
                PauseGame(); //Call Method
            } //End of Nested If-Else Statement

        } //End of If-Statement

    } //End of Update Method

    //PauseGame Method - Pauses the game when the Escape key button is pressed
    public void PauseGame() {

        Time.timeScale = 0f; // Pause the game by setting time scale to 0
        pausePanel.SetActive(true);
        isPaused = true;
        Debug.Log("Game Paused");

    } //End of PauseGame Method

    //ResumeGame Method - Unpauses the game when the "button_Play" button or Escape key or "P" key is pressed
    public void ResumeGame() {

        Time.timeScale = 1f; //Resume the game by setting time scale back to 1
        pausePanel.SetActive(false);
        isPaused = false;
        Debug.Log("Game Resumed");

    } //End of ResumeGame Method

    //RestartLevel Method - Restarts the current level when the "button_Restart" button is pressed
    public void RestartLevel() {

        Time.timeScale = 1f; //Ensure the time scale is set back to 1
        Debug.Log("Restarting Level..."); //Log restarting message
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); //Reload the current active scene

    } //End of RestartLevel Method

    //QuitGame Method - Loads the main menu scene when the "button_Home" button is pressed
    public void QuitGame() {

        Debug.Log("Quitting Level..."); //Log quitting message
        SceneManager.LoadSceneAsync(0); //Load Main Menu Scene

    } //End of QuitGame Method

} //End of PausePanelController Class
