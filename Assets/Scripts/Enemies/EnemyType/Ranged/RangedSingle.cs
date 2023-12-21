using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSingle : Enemy
{

    public override void Start()
    {
        base.Start();
    }
    public override void Die()
    {
        scoreManager.AddPoint(10);
        base.Die();
    }
}
