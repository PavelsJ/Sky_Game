using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float force = 20;
    public float acceleration = 200;

    public float turnSpeed = 60, minAcceleration, maxAcceleration, minSpeed = 0, maxSpeed = 200;
    
    public LayerMask groundLayer;
    public Transform groundCheck;
    
    private bool isGrounded;
    
    private Rigidbody rb;
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.Linecast(transform.position, groundCheck.position, groundLayer);
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurnLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TurnRight();
        }
    }
    
    void FixedUpdate()
    {
        if (isGrounded)
        {
            float angle = Mathf.Abs(180 - transform.eulerAngles.y);
            acceleration = Remap(0, 90, maxAcceleration, minAcceleration, angle);
            
            force += acceleration * Time.fixedDeltaTime;
            force = Mathf.Clamp(acceleration, minSpeed, maxSpeed);
            
            animator.SetFloat("playerSpeed", force);
            
            Vector3 forceVector = transform.forward * (force * Time.fixedDeltaTime);
            forceVector.y = rb.velocity.y;
        
            rb.velocity = forceVector;
        }
    }

    private void TurnRight()
    {
        if (transform.eulerAngles.y > 91)
        {
            transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0), Space.Self);
        }
    }

    private void TurnLeft()
    {
        if (transform.eulerAngles.y < 269)
        {
            transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0), Space.Self);
        }
    }
    
    private float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
