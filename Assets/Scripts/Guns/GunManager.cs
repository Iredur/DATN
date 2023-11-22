using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    public Guns gun;

    public string name;
    
    public Sprite sprite;

    public int fireRate,damage,bulletType,level;

    private void Awake()
    {

        TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        if (_spriteRenderer == null)
        {
            _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        

        TryGetComponent<BoxCollider2D>(out _boxCollider2D);
        if (_boxCollider2D == null)
        {
            _boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        }

        _boxCollider2D.isTrigger = true;
    }

    private void Start()
    {
        name = gun.name;
        sprite = gun.sprite;
        fireRate = gun.fireRate;
        damage = gun.damage;
        bulletType = gun.bulletType;
        level = gun.level;
        _spriteRenderer.sprite = sprite;
        
        _boxCollider2D.size = _spriteRenderer.bounds.size;
    }
}
