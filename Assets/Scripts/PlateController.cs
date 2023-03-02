using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{

    public static PlateController instance;

    public List<GameObject> attachedBlocks;

    private void Awake()
    {
        // set singleton
        instance = this;
    }

    public float moveSpeed = 5;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            

            foreach (GameObject g in attachedBlocks)
            {
                g.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        } 
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            foreach (GameObject g in attachedBlocks)
            {
                g.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }

        }

        
    }

    public void AttachBlock(GameObject block)
    {
        attachedBlocks.Add(block);
    }

    public void RemoveBlock(GameObject block)
    {
        attachedBlocks.Remove(block);
    }

}
