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
    RaycastHit hit;
   

    public LayerMask layerMask;
    public LayerMask groundMask;
    private bool isIdle;

    public GameObject playerGraphics = null;

    [Range(0f, 100f)]
    public float moveSpeedMutiplier;

    [Range(0f, 30f)]
    public float fallmutiplier;

    [Range(0f, 25f)]
    public float lowJumpMultiplier;

    [Range(0f, 200f)]
    public float jumpVelocity;

    [Range(0, 5f)]
    public float raycastRange;
    [Range(0, 50f)]
    public float SmoothRotation;

    public Vector3 gravity = Physics.gravity;
    bool isGroundedFront;

  
    bool isfront = true;
    bool isJumping = false;
  

    float horizontal;
  
   
    float size = 0.7f;
    private void Awake()
    {

    }
    private void Update()
    {
        GroundChecking();

        Walk();



    }
    void Walk()
    {
        horizontal = Input.GetAxis("Horizontal");
        

        if (Input.GetKey(KeyCode.Space) && isGroundedFront)
        {
            isJumping = true;
        }


        if (!isGroundedFront && !isJumping)
        {
            Vector3 dir=hit.normal;
            Debug.Log(dir);
            rb.velocity= new Vector3(horizontal * moveSpeedMutiplier * 10f * Time.deltaTime, rb.velocity.y, 0f);
            //rb.velocity=new Vector3(dir.x*horizontal * moveSpeedMutiplier * 10f * Time.deltaTime, dir.y, 0f);
           
        }

        if (isGroundedFront && !isJumping)
        {

            rb.velocity = new Vector3(horizontal * moveSpeedMutiplier * 10f * Time.deltaTime, rb.velocity.y, 0f);
            
        }

        if (isJumping && isGroundedFront)
        {



            Jump();
        }



        RotationOfPlayer();




    }


    void RotationOfPlayer()
    {
        if (horizontal < 0)
        {
            isfront = false;

        }
        else if (horizontal > 0)
        {
            isfront = true;
        }



        if (isfront)
        {
            playerGraphics.transform.localScale = new Vector3(size, size, size);

        }
        else
        {
            playerGraphics.transform.localScale = new Vector3(size, size, -size);

        }

    }
    void GroundChecking()
    {
        if (Physics.Raycast(rb.transform.position, rb.transform.TransformDirection(-Vector3.up), out hit, raycastRange, layerMask))
        {


            isGroundedFront = true;

            Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, hit.normal);

            rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, grndTilt, Time.deltaTime * SmoothRotation).normalized;
            
            
            Debug.DrawRay(rb.transform.position, rb.transform.TransformDirection(-hit.normal) * raycastRange, Color.blue);

            //Debug.Log("Did Hit");
        }
        else
        {
            isGroundedFront = false;

            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 0, 0), Time.deltaTime * 5f).normalized;
            
            Debug.DrawRay(transform.position, transform.TransformDirection(-transform.up) * raycastRange, Color.red);
            //Debug.Log("Did not Hit");
        }

    }
    void Jump()
    {
        StartCoroutine(JumpTime());

    }

    IEnumerator JumpTime()
    {

        rb.AddForce(Vector3.up * jumpVelocity);
        yield return new WaitUntil(() => isGroundedFront);

        isJumping = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            Debug.Log("öldu");
        }
    }

}
