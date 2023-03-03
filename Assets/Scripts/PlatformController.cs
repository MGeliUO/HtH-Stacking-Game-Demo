using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    // Declare our singleton instance
    public static PlatformController instance;

    private void Awake()
    {
        // Set the singleton instance
        instance = this;
    }

    // The list of blocks that will be affected by the movement of the platform
    public List<GameObject> attachedBlocks;

    // The speed at which the platform and its attached blocks move
    public float moveSpeed = 5;

    // The Rigidbody component attached to this GameObject
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
        // Check for key input
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Move the platform left
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            
            // Move attached blocks left as well
            foreach (GameObject g in attachedBlocks)
            {
                g.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        } 
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Move the platform right
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // Move each attached block to the right too
            foreach (GameObject g in attachedBlocks)
            {
                g.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }

        }

        
    }

    // Add a new block to the attached blocks list
    public void AttachBlock(GameObject block)
    {
        attachedBlocks.Add(block);
    }

    // Remove a block from the attached blocks list
    public void RemoveBlock(GameObject block)
    {
        attachedBlocks.Remove(block);
    }

}
