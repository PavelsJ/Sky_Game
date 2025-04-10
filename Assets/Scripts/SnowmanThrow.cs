using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public GameObject snowBall;
    private GameObject snowBallClone;
    private GameObject target;
    
    public float throwDistance;
    public int throwSpeed;
    private bool justThown = false;
    private Vector3 throwDirection = new Vector3(0, 0.33f);

    private void Start()
    {
        snowBallClone = Instantiate(snowBall, transform.position, transform.rotation);
        snowBallClone.SetActive(false);
        
        target = GameObject.Find("Player");
    }
    
    void Update()
    {
        if (Time.frameCount % 6 == 0)
        {
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
       
            if (distanceToTarget < throwDistance 
                && justThown == false 
                && !snowBallClone.activeSelf)
            {
                ThrowSnowBall();
            }
        }
    }

    private void ThrowSnowBall()
    {
        justThown = true;
            
        snowBallClone.transform.position = transform.position;
        snowBallClone.SetActive(justThown);
           
        Rigidbody tempRb = snowBallClone.GetComponent<Rigidbody>();
        Vector3 targetDirection =  Vector3.Normalize(target.transform.position-transform.position);
                
        targetDirection += throwDirection;
        tempRb.AddForce(targetDirection * throwSpeed);
            
        Invoke("ThrowOver", 0.1f);
    }

    void ThrowOver()
    {
        justThown = false;
    }
}
