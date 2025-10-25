using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Created By: Abraar Sadek
 * Created On: 10-25-2025
 * Purpose: This script will handle player movement and interactions within the game world.
 * 
 * Last Modified By: Abraar Sadek
 * Last Modified On: 10-25-2025
 * Last Modification Made: Initial creation and setup of player movement functionality.
 */

//PlayerConroller Class - Manages player movement and interactions

public class PlayerController : MonoBehaviour {

    private Rigidbody playerRB; //Reference to the player's Rigidbody2D component

    private float playerMovementX; //Store horizontal movement input
    private float playerMovementY; //Store vertical movement input

    [Header("Player Settings")]
    public float playerSpeed = 5.0f; //Player movement speed

    //Start Method - Called before the first frame update
    void Start() {

        playerRB = GetComponent<Rigidbody>();

    } //End of Start Method

    //FixedUpdate Method - Called at fixed intervals for physics updates
    private void FixedUpdate() {

        Vector3 playerMovement = new Vector3(playerMovementX, 0.0f, playerMovementY);

        playerRB.AddForce(playerMovement * playerSpeed);

    } //End of FixedUpdate Method

    //OnMove Method - 
    void OnMove (InputValue movementValue) {

        Vector2 movementVector = movementValue.Get<Vector2>(); //Get and store the movement vector from input

        playerMovementX = movementVector.x; //Update horizontal movement input
        playerMovementY = movementVector.y; //Update vertical movement input

    } //End of OnMove Method

} //End of PlayerController Class
