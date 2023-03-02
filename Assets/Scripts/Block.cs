using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int attachedCount = 0;

    public float maxVelocity = -2f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= GameManager.GAME_OVER_Y) GameManager.instance.OnGameOver();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < maxVelocity && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxVelocity, rb.velocity.z);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Attached"))
        {
            attachedCount++;

            if (attachedCount == 1)
            {
                tag = "Attached";

                PlateController.instance.AttachBlock(gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Attached"))
        {
            attachedCount--;

            if (attachedCount == 0)
            {
                tag = "Untagged";

                PlateController.instance.RemoveBlock(gameObject);
            }
        }
    }
}
