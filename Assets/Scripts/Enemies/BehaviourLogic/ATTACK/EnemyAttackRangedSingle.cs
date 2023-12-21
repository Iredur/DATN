using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack-ranged-single", menuName = "Enemy Logic/Attack logic/ranged/ranged-single")]
public class EnemyAttackRangedSingle : EnemyAttackSOBase
{
    bool canFire = true;

    private float time;
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
            SoundManager.Instance.PlaySound(enemy.shot);
            CallBullet(transform.position, rotationDeg, enemy.smallBullet);
            canFire = false;
        }
    }
}


