using System;
using UnityEngine;

public class BehaviourTest : CollisionHandler
{
    //kinda success?
    private PlayerController _playerController;
    public override void EnterCollision(Collider2D collider2d)
    {
        if (collider2d.TryGetComponent<PlayerController>(out _playerController))
        {
            Destroy(this.gameObject);
        }
        base.EnterCollision(collider2d);
    }
}
