using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_Boost : Flag
{
    private List<GameObject> flagBoosts = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            flagBoosts.Add(transform.GetChild(i).gameObject);
        }
    }
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
