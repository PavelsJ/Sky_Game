using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [Header("StunLock")]
    public float stunDuration;
    public float stunForce;

    public static bool isHurt = false;
    private Rigidbody rb;
    private Coroutine stunCoroutine;

    private void Start()
    {
        PlayerManager.onHitEvent += Push; 
            
        rb = GetComponent<Rigidbody>();
    }

    private void Push()
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
            
            stunCoroutine = StartCoroutine(Stun());
        }
    }

    private IEnumerator Stun()
    {
        yield return new WaitForSeconds(stunDuration);
        isHurt = false;
    }
}
