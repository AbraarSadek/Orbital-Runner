using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Created By: Abraar Sadek
 * Created On: 10-25-2025
 * Purpose: This script will handle the main camera's behavior and controls within the game.
 * 
 * Last Modified By: Abraar Sadek
 * Last Modified On: 10-25-2025
 * Last Modification Made: Initial creation and setup of camera control functionality.
 */

//CameraController Class - Manages camera behavior and controls
public class CameraController : MonoBehaviour
{

    //Private Variables
    private Vector3 cameraOffset; //Store the offset between the camera and player character

    [Header("Game Object Reference's")]
    public GameObject playerCharacter; //Reference to the player character GameObject

    //Start Method - Called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - playerCharacter.transform.position; //Calculate and store the initial camera offset
        SceneTracker.CurrentLevel = SceneManager.GetActiveScene().buildIndex; // On start of level, get current level. Should be in its own script.

    } //End of Start Method

    //LateUpdate Method - Called after all Update methods have been called
    void LateUpdate()
    {
        if (gameObject != null || playerCharacter != null) //Prevents null pointer exception when transitioning between scenes.
        {
            transform.position = playerCharacter.transform.position + cameraOffset; //Update camera position to follow the player character
        }
    } //End of Update Method

} //End of CameraController Class
