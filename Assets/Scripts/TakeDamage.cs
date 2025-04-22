using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [Header("StunLock")]
    public float stunForce;
    public float stunDuration;

    public static bool isHurt = false;
     
    private Rigidbody rb;
    private Coroutine stunCoroutine;

    private void Start()
    {
        PlayerManager.onHitEvent += TryTakeDamage; 
        rb = GetComponent<Rigidbody>();
    }
    
    private void TryTakeDamage()
    {
        if (!isHurt)
        {
            TakeHit();
        }
    }
    
    private void TakeHit()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.forward * stunForce, ForceMode.Impulse);
            rb.AddForce(Vector3.up * (stunForce / 2), ForceMode.Impulse);

            isHurt = true;

            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }

            stunCoroutine = StartCoroutine(StunInvulnerability());
        }
    }

    private IEnumerator StunInvulnerability()
    {
        yield return new WaitForSeconds(stunDuration);
        isHurt = false;
    }
}
