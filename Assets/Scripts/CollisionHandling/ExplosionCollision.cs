using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollision : MonoBehaviour
{
    public bool isFriendly;
    public int damage;
    ParticleSystem particle;
    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (particle.isStopped)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.takeDamage(isFriendly, damage);
        }
    }
}
