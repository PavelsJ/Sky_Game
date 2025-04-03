using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlle : MonoBehaviour
{
    [SerializeField] private KeyCode leftInput, rightInput;
    [SerializeField] private float acceleration = 100, turnSpeed = 100,
        minSpeed =0, maxSpeed = 500, minAcceleration= -100, maxAcceleration = 200;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundTransform;

    private float speed = 0;
    private bool boost = false;
    
    private Rigidbody rb;
    private Animator animator;
    private Coroutine boostCoroutine;
    
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
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    // Start is called before the first frame update
   
    private void FixedUpdate()
    {
        if(TakeDamage.isHurt) return;
        
        float angle = Mathf.Abs(transform.eulerAngles.y - 180);
        acceleration = Remap(0, 90, maxAcceleration, minAcceleration, angle);
        speed += acceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        if (!boost)
        {
            Move(speed);
        }
        else
        {
            Move(speed += maxSpeed);
        }
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
        if (boostCoroutine != null)
        {
            StopCoroutine(boostCoroutine);   
        }
        
        boost = true;
        boostCoroutine = StartCoroutine(BoostCoroutine());
    }

    private IEnumerator BoostCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        boost = false;
    }
}
