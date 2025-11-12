using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

using UnityEngine.SceneManagement;

/*
 * Created By: Abraar Sadek
 * Created On: 10-25-2025
 * Purpose: This script will handle player movement and interactions within the game world.
 * 
 * Last Modified By: Aiden Wong
 * Last Modified On: 10-30-2025
 * Last Modification Made: Collision detection and score implementation.
 */

//PlayerConroller Class - Manages player movement and interactions

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRB; //Reference to the player's Rigidbody2D component
    private int count;

    private float playerMovementX; //Store horizontal movement input
    private float playerMovementY; //Store vertical movement input

    [Header("Player Settings")]
    public float playerSpeed = 5.0f; //Player movement speed
    public TextMeshProUGUI countText;

    public GameObject winTextObject;

    //Start Method - Called before the first frame update
    void Start()
    {

        playerRB = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

    } //End of Start Method

    //FixedUpdate Method - Called at fixed intervals for physics updates
    private void FixedUpdate()
    {

        Vector3 playerMovement = new Vector3(playerMovementX, 0.0f, playerMovementY);

        playerRB.AddForce(playerMovement * playerSpeed);

    } //End of FixedUpdate Method

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }//end onTriggerEnter

    //OnMove Method - 
    void OnMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>(); //Get and store the movement vector from input

        playerMovementX = movementVector.x; //Update horizontal movement input
        playerMovementY = movementVector.y; //Update vertical movement input

    } //End of OnMove Method

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            SceneTracker.CurrentScore = count;
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            SceneTracker.WonLastLevel = true;
            SceneManager.LoadSceneAsync(5); // Go to Game Over Scene.
        }
    }//end setCountText

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            SceneTracker.WonLastLevel = false;
            SceneManager.LoadSceneAsync(5); // Go to Game Over Scene.
        }
    }

} //End of PlayerController Class
