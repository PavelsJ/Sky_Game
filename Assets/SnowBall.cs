using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {  
        StopCoroutine(Delay());
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
