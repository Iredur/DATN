using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] AudioClip destroyed;
    public Vector3 firepoint;
    Player player;

    public bool isFriendly;

    public BulletType bulletType;
    [SerializeField] public float decayTime;
    [SerializeField] BulletType dummyBullet;
    public float timer;
    //[SerializeField] public float travelSpeed;
    public bool timerOn = false;

    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] SpriteRenderer spriteRenderer;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        if (bulletType == null)
            bulletType = dummyBullet;
    }

    private void OnEnable()
    {
        timerOn = true;
        decayTime += Time.deltaTime;
    }

    private void OnDisable()
    {
        this.transform.localPosition = Vector3.zero;
        timer = 0;
        timerOn = false;
    }

    private void Update()
    {
        Timer();
        BulletTravel();
    }

    public void BulletTravel()
    {
        if (timer < decayTime)
        {
            this.transform.position += bulletType.speed * Time.deltaTime * transform.right;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Timer()
    {
        if (timerOn)
            timer += Time.deltaTime;
        else timer = 0;
    }
    public void UpdateBulletData(BulletType assignedBullet)
    {
        bulletType = assignedBullet;
        decayTime = bulletType.lifeTime;
        spriteRenderer.sprite = bulletType.sprite;
        boxCollider2D.size = spriteRenderer.bounds.size;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("hit wall");
            DestroyCollide();
        }
        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            if (player == damagable && isFriendly)
            {
                return;
            }
            if (player != damagable && !isFriendly)
            {
                return;
            }
            damagable.takeDamage(isFriendly, bulletType.damage);
            DestroyCollide();
        }
    }
    void DestroyCollide()
    {
        SoundManager.Instance.PlaySound(destroyed, true);
        GameObject fx = ObjectPool.instance.getPooledFX(this.transform.rotation, bulletType.explosionSize, isFriendly, bulletType.explosionDamage);
        if (fx != null)
        {
            fx.transform.position = this.transform.position;
            fx.SetActive(true);
            fx.GetComponent<ParticleSystem>().Play();
        }
        gameObject.SetActive(false);
    }
}
