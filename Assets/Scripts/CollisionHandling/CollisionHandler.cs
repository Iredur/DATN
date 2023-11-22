using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    //this class is used to handle collision
    
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    private bool canCollide = true;
    
    private void Start()
    {
        TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        TryGetComponent<BoxCollider2D>(out _boxCollider2D);
        if (_boxCollider2D == null)
        {
            _boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        }

        if (_spriteRenderer)
            _boxCollider2D.size = _spriteRenderer.bounds.size;
    }

    private void Update()
    {
        CollisionDetection();
    }

    public virtual void EnterCollision(Collider2D collider2d)
    {
        //handle collision then exit
        ColliderDistance2D colliderDistance2D = collider2d.Distance(_boxCollider2D);
        if (!colliderDistance2D.isOverlapped)
        {
            ExitCollision(collider2d);
        }
    }
    public void CollisionDetection()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _boxCollider2D.size, 0);
        
        foreach (Collider2D hit in hits)
        {
            if (hit == _boxCollider2D)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(_boxCollider2D);
            //check if collided, if collided, handle it
            if (colliderDistance.isOverlapped)
            {
                EnterCollision(hit);
            }
        }
    }
    public virtual void ExitCollision(Collider2D collider2D)
    {
        
    }
}
