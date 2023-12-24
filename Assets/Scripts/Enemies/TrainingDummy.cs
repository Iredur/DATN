
using UnityEngine;

public class TrainingDummy : MonoBehaviour, IDamagable
{
    AudioClip hurt;
    public float MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float CurrentHealth { get; set; } = 100f;

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void takeDamage(bool isFriendly, int damage)
    {
        if (isFriendly)
        {
            SoundManager.Instance.PlaySound(hurt);
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
