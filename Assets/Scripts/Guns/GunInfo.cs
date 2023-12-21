using UnityEngine;

public class GunInfo : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    public Guns gun;

    public string name;

    public Sprite sprite;

    public float fireRate, damage, bulletType;

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

        _spriteRenderer.sprite = sprite;

        _boxCollider2D.size = _spriteRenderer.bounds.size;
    }
}
