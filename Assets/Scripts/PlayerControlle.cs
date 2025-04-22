using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlle : MonoBehaviour
{
    [Header("Input")] 
    [SerializeField] private KeyCode leftInput;
    [SerializeField] private KeyCode rightInput;
    
    [Header("Movement")] 
    [SerializeField] private float turnSpeed = 100;
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 500;
    
    [SerializeField] private float acceleration = 100;
    [SerializeField] private float minAcceleration = -100;
    [SerializeField] private float maxAcceleration = 200;
    
    private float speed = 0;
    
    [Header("Boost")]
    [SerializeField] private float boostForce = 300f;
    [SerializeField] private float boostDuration = 1.5f;
    [SerializeField] private AnimationCurve boostCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    
    private float boostSpeed = 0f;
    private float boostTimer = 0f;
    
    [Header("Ground")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundTransform;
    
    private Rigidbody rb;
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        GameManager.OnBoost += Boost;
    }

    private void OnDisable()
    {
        GameManager.OnBoost -= Boost;
    }

    void Update()
    {
        bool isGrounded = Physics.Linecast(transform.position, groundTransform.position,
            groundLayers);
        if (isGrounded && !TakeDamage.isHurt)
        {
            if (Input.GetKey(leftInput) && transform.eulerAngles.y < 269)
            {
                transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0), Space.Self);
            }
            if (Input.GetKey(rightInput) && transform.eulerAngles.y > 91)
            {
                transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0), Space.Self);
            }
        }
    }
   
    private void FixedUpdate()
    {
        if(TakeDamage.isHurt) return;
        
        float angle = Mathf.Abs(transform.eulerAngles.y - 180);
        acceleration = Remap(0, 90, maxAcceleration, minAcceleration, angle);
        speed += acceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        if (boostTimer > 0f)
        {
            float curveValue = boostCurve.Evaluate(1f - (boostTimer / boostDuration));
            boostSpeed = boostForce * curveValue;
            boostTimer -= Time.fixedDeltaTime;
        }
        else
        {
            boostSpeed = 0f;
        }
        
        float totalSpeed = speed + boostSpeed;
        Move(totalSpeed);
    }
    
    private void Move(float newSpeed)
    {
        animator.SetFloat("playerSpeed", newSpeed);
        
        Vector3 velocity = transform.forward * (newSpeed * Time.fixedDeltaTime);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
    
    private float Remap(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((oldValue - oldMin) / oldRange) * newRange + newMin);
        return newValue;
    }

    private void Boost()
    {
        boostTimer = boostDuration;
    }
}
