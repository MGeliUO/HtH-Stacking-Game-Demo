using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // The number of 'attached' blocks this block is currently making contact with
    private int attachedCount = 0;

    // The Rigidbody attached to this GameObject
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // If this block falls off the screen, tell the GameManager instance that the game is over
        if (transform.position.y <= GameManager.GAME_OVER_Y) GameManager.instance.OnGameOver();
    }

    // FixedUpdate is called at the same rate that the Unity physics system ticks
    private void FixedUpdate()
    {
        // Make sure our rigidbody's downwards velocity does not exceed the velocity limit we set in GameManager
        if (rb.velocity.y < GameManager.MAX_VELOCITY && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, GameManager.MAX_VELOCITY, rb.velocity.z);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        // When we hit an 'attached' block, make this block an 'attached' block as well
        if (collision.gameObject.CompareTag("Attached"))
        {
            attachedCount++;

            if (attachedCount == 1)
            {
                tag = "Attached";

                // Notify the platform controller that this block has been caught
                PlatformController.instance.AttachBlock(gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // When we fall off of an 'attached' block, detach this block from the platform
        if(collision.gameObject.CompareTag("Attached"))
        {
            attachedCount--;

            if (attachedCount == 0)
            {
                tag = "Untagged";

                // Remove this block from the platform's list of attached blocks
                PlatformController.instance.RemoveBlock(gameObject);
            }
        }
    }
}
