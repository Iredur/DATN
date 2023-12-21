using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeBasic : Enemy
{
    public override void Start()
    {
        base.Start();
    }
    public override void Die()
    {
        scoreManager.AddPoint(5);
        base.Die();
    }
}
