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
    RaycastHit hit;
    public Transform groundCheck;
    public LayerMask layerMask;
    public LayerMask groundMask;
    private bool isIdle;

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
    float playerRotationY=0f;
    Vector3 groundRotation;

    private void Awake()
    {
        
    }
    private void Update()
    {
        

        Walk();
        if (isGroundedFront)
        {
           

            
           
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

      
       
    }
    void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(horizontal*moveSpeedMutiplier*Time.deltaTime, 0f, 0f));


    }
  

    
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(-transform.up), out hit, raycastRange, layerMask))
        {

           
            isGroundedFront = true;
            Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, hit.normal);
            
            transform.rotation=Quaternion.Lerp(transform.rotation, grndTilt,Time.deltaTime*5f);
            Debug.DrawRay(transform.position, transform.TransformDirection(-hit.normal) * raycastRange, Color.blue);
            Debug.Log("Did Hit");
        }
        else
        {
            isGroundedFront = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(-transform.up) * raycastRange, Color.red);
            Debug.Log("Did not Hit");
        }

    }
    void Jump()
    {
        //rb.AddForce(transform.forward*jumpVelocity,ForceMode.Impulse);
        rb.AddForce(Vector3.up*jumpVelocity,ForceMode.Impulse);
    }
    void OnWallJump()
    {
        rb.AddForce(transform.forward * jumpVelocity, ForceMode.Impulse);
        rb.AddForce(Vector3.up * jumpVelocity*3f, ForceMode.Impulse);
    }
    
    

}
