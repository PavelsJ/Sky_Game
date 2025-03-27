using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void OnHit();
    public static event OnHit onHitEvent;
    
    [Header("Cinemachine")]
    public CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin perlin;
    private Coroutine shakeCoroutine;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shakeClip;

    private void Start()
    {
        if (vcam != null)
        {
            perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        onHitEvent += ShakeCamera;
    }

    public static void CallOnHit()
    {
        if (onHitEvent != null)
        {
            onHitEvent();
        }
    }

    private void ShakeCamera()
    {
        if (vcam == null) return;
        
        audioSource.PlayOneShot(shakeClip);

        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        shakeCoroutine = StartCoroutine(ShakeCameraCoroutine(0.5f));
    }

    private IEnumerator ShakeCameraCoroutine(float duration)
    {
        perlin.m_AmplitudeGain = 2f;
        yield return new WaitForSeconds(duration);
        perlin.m_AmplitudeGain = 0f;
    }
}
