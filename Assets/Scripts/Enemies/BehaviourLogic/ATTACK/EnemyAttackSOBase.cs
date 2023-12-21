using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;
    public Quaternion rotationDeg;
    public Vector2 distanceVector;
    public float angle;

    protected Transform player;
    public virtual void Init(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        player = GameObject.FindObjectOfType<Player>().transform;
    }
    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { DoResetValues(); }
    public virtual void DoFrameUpdate()
    {
        GetRotationToPlayer();
    }
    public virtual void DoPhysicLogic() { }
    public virtual void DoAnimationTriggerLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void DoResetValues()
    {
    }
    public void CallBullet(Vector3 spawnPos, Quaternion rotation, BulletType _bullet)
    {
        GameObject bullet = ObjectPool.instance.getPooledBullet(spawnPos, rotation, _bullet, false);
        if (bullet != null)
        {
            bullet.transform.position = spawnPos;
            bullet.SetActive(true);
        }
    }
    public void GetRotationToPlayer()
    {
        distanceVector = (Vector2)player.transform.position - (Vector2)this.transform.position;
        angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

        rotationDeg = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
