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

    bool onWall = false;
    bool isfront = true;
    bool isJumping = false;
    float jumpMax = 4f;

    float horizontal;
    float playerRotationY = 0f;
    Vector3 groundRotation;
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


            rb.velocity = new Vector3(horizontal * moveSpeedMutiplier * 10f * Time.deltaTime, 1f * rb.velocity.y, 0f);
        }

        if (isGroundedFront && !isJumping)
        {

            rb.velocity = new Vector3(horizontal * moveSpeedMutiplier * 10f * Time.deltaTime, rb.velocity.y, 0f);
        }

        if (isJumping && isGroundedFront)
        {



            Jump();
        }




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



    //private void FixedUpdate()
    //{
       

       

    //}
    void GroundChecking()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, raycastRange, layerMask))
        {


            isGroundedFront = true;

            Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, hit.normal);

            transform.rotation = Quaternion.Lerp(transform.rotation, grndTilt, Time.deltaTime * SmoothRotation);
            Debug.DrawRay(transform.position, transform.TransformDirection(-hit.normal) * raycastRange, Color.blue);

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
        StartCoroutine(



                 JumpTime()
        );

    }

    IEnumerator JumpTime()
    {

        rb.AddForce(Vector3.up * jumpVelocity);
        yield return new WaitUntil(() => isGroundedFront);

        isJumping = false;
    }
    void OnWallJump()
    {
        rb.AddForce(transform.forward * jumpVelocity, ForceMode.Impulse);
        rb.AddForce(Vector3.up * jumpVelocity * 3f, ForceMode.Impulse);
    }



}
