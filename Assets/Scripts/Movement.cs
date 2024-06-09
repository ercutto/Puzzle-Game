using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    RaycastHit hitFront;
    RaycastHit hitBack;
    public Transform groundCheck;
    public LayerMask layerMask;
    public LayerMask groundMask;

    public GameObject playerGraphics=null;
    
    [Range(0f, 300f)]
    public float moveSpeedMutiplier;

    [Range(0f, 30f)]
    public float  fallmutiplier;

    [Range(0f, 25f)]
    public float lowJumpMultiplier;

    [Range(0f,200f)]
    public float jumpVelocity;

    [Range(0, 1f)]
    public float raycastRange;
   

    bool isGroundedFront;
   
    bool onWall=false;
   
    bool isJumping=false;
    float horizontal;
    float groundRotation = 0;
    

    private void Awake()
    {
        
    }
    private void Update()
    {
       
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(-transform.up), out hit, Mathf.Infinity, layerMask))
        {
          
            transform.rotation = hit.transform.rotation;
           
            isGroundedFront = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(-transform.up) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {   isGroundedFront=false;
            Debug.DrawRay(transform.position, transform.TransformDirection(-transform.up) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

        
        if (horizontal<0)
        { 
           
          playerGraphics.transform.eulerAngles=new Vector3(0,-180,0);
            
        }else if (horizontal>0)
        {
            
           playerGraphics.transform.eulerAngles=new Vector3(0,0,0);

        }

        Walk();
        if (isGroundedFront)
        {

            Vector3.ProjectOnPlane(transform.position, hit.normal);

            //rb.transform.position += new Vector3(horizontal*moveSpeedMutiplier,0,0);

            if (!onWall)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    Jump();
            }else if (onWall)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                     OnWallJump();
            }
        }

        //if (transform.rotation.eulerAngles.z > 30f || transform.rotation.eulerAngles.z < -2f){ onWall = true; }
        //else { onWall = false; }
    }
    void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        transform.Translate(new Vector3(horizontal*moveSpeedMutiplier*Time.deltaTime, 0f, 0f));
        if (horizontal < 0)
        {

           transform.eulerAngles = new Vector3(0, -180, 0);

        }
        
        if (horizontal > 0)
        {

            transform.eulerAngles = new Vector3(0, 0, 0);

        }


    }
    private void FixedUpdate()
    {
      
       
    }
    void Jump()
    {
        rb.AddForce(Vector3.right*jumpVelocity,ForceMode.Impulse);
        rb.AddForce(Vector3.up*jumpVelocity,ForceMode.Impulse);
    }
    void OnWallJump()
    {
        rb.AddForce(Vector3.right * jumpVelocity, ForceMode.Impulse);
        rb.AddForce(Vector3.up * jumpVelocity*3f, ForceMode.Impulse);
    }
    
    

}
