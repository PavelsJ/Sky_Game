using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman_Obstacle : Obstacle
{
    protected override void PushBack()
    {
        OnHit();
        DestroyObstacle();
    }

    private void DestroyObstacle()
    {
        gameObject.SetActive(false);
    }
}
