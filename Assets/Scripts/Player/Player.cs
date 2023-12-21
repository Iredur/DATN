using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
     [SerializeField] public float health = 100;
     [SerializeField] private float iFrameDuration;
     [SerializeField] private int numberOfFlashes;
     [SerializeField] AudioClip hurt;


     SpriteRenderer spriteRenderer;
     PlayerController playerController;
     bool canHurt = true;
     bool DTMode = false;
     public bool lost = false;
     float speedMultiplier = 1.5f;
     float fireRateMultiplier = 2;

     //for testing only dont worry about it
     public bool damaged = false;

     public float MaxHealth { get; set; } = 100f;
     public float CurrentHealth { get; set; }

     private void Awake()
     {
          Time.timeScale = 1;
          health = MaxHealth;
          playerController = GetComponent<PlayerController>();
          spriteRenderer = playerController.currRenderer;
     }
     private void Start()
     {
          lost = false;
          health = MaxHealth;
     }
     private void OnEnable()
     {
          HealingItem.OnHealthCollected += Heal;
     }

     public void Heal()
     {
          if (health + 20 <= MaxHealth)
               health += 20;
          if (health + 20 > MaxHealth)
               health = MaxHealth;
     }

     private void OnDisable()
     {

     }
     private void Update()
     {
          damage();
     }

     public void takeDamage(bool isFriendly, int damage)
     {
          if (!isFriendly)
          {
               if (canHurt)
               {
                    SoundManager.Instance.PlaySound(hurt);
                    health -= damage;
                    if (health <= 0)
                         Die();
                    StartCoroutine(Invulnerability());
               }
          }
     }

     IEnumerator Invulnerability()
     {
          canHurt = false;
          for (int i = 0; i < numberOfFlashes; i++)
          {
               spriteRenderer.color = new Color(255, 255, 255, 0.5f);
               yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
               spriteRenderer.color = new Color(1, 0, 0, 0.5f);
               yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
          }
          canHurt = true;
          spriteRenderer.color = new Color(255, 255, 255);
     }

     void damage()
     {
          if (damaged)
          {
               takeDamage(false, 5);
               damaged = false;
          }
     }

     public void Die()
     {
          lost = true;
          playerController.isPausing = true;
          Time.timeScale = 0;

          //Destroy(gameObject);
     }
}
