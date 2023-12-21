using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack-ranged-multi", menuName = "Enemy Logic/Attack logic/ranged/ranged-multi")]
public class EnemyAttackRangedMulti : EnemyAttackSOBase
{
    bool canFire = true;

    private float time;
    //float angle;
    Quaternion _rotationDown;
    Quaternion _rotationUp;
    Quaternion _rotationDown2;
    Quaternion _rotationUp2;
    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();

    }
    public override void DoFrameUpdate()
    {
        base.DoFrameUpdate();
        if (!canFire)
        {
            time += Time.deltaTime;
            float nextTimeToFire = 1 / 1.5f;
            if (time >= nextTimeToFire)
            {
                canFire = true;
                time = 0;
            }
        }
        Shoot();
        if (!enemy.isInRange)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }
    public override void DoPhysicLogic() { base.DoPhysicLogic(); }
    public override void DoAnimationTriggerLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerLogic(triggerType);
    }
    public override void DoResetValues()
    {
        base.DoResetValues();
    }
    void Shoot()
    {
        if (canFire)
        {
            //angle = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
            _rotationDown = Quaternion.AngleAxis(angle - 5, Vector3.forward);
            _rotationUp = Quaternion.AngleAxis(angle + 5, Vector3.forward);
            _rotationDown2 = Quaternion.AngleAxis(angle - 10, Vector3.forward);
            _rotationUp2 = Quaternion.AngleAxis(angle + 10, Vector3.forward);
            SoundManager.Instance.PlaySound(enemy.shot);
            CallBullet(transform.position, rotationDeg, enemy.smallBullet);
            CallBullet(transform.position, _rotationDown, enemy.smallBullet);
            CallBullet(transform.position, _rotationUp, enemy.smallBullet);
            CallBullet(transform.position, _rotationUp2, enemy.smallBullet);
            CallBullet(transform.position, _rotationDown2, enemy.smallBullet);
            canFire = false;
        }
    }
}


