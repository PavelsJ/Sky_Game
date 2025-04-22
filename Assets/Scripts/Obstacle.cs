using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         PushBack();
      }
   }

   protected virtual void PushBack()
   {
      OnHit();
   }
   

   internal void OnHit()
   {
      PlayerManager.CallOnHit();
   }
}
