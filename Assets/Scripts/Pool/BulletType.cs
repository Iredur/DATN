using UnityEngine;
[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class BulletType : ScriptableObject
{
    public new string name;

    public Sprite sprite;
    public int explosionDamage;
    public int damage;

    public int speed, lifeTime;
    public int explosionSize;

}
