using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
public class CameraController : MonoBehaviour {

    //Private Variables
    private Vector3 cameraOffset; //Store the offset between the camera and player character

    [Header("Game Object Reference's")]
    public GameObject playerCharacter; //Reference to the player character GameObject

    //Start Method - Called before the first frame update
    void Start() {

        cameraOffset = transform.position - playerCharacter.transform.position; //Calculate and store the initial camera offset

    } //End of Start Method

    //LateUpdate Method - Called after all Update methods have been called
    void LateUpdate() {

        transform.position = playerCharacter.transform.position + cameraOffset; //Update camera position to follow the player character

    } //End of Update Method

} //End of CameraController Class
