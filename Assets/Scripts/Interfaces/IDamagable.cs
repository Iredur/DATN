
public interface IDamagable
{
    void takeDamage(bool isFriendly, int damage);
    void Die();
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
}
