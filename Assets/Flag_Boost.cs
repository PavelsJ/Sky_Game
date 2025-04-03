using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_Boost : Flag
{
    public GameObject[] flagBoosts;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var flag in flagBoosts)
            {
                flag.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
           
            GameManager.CallBoost();
        }
    }
}
