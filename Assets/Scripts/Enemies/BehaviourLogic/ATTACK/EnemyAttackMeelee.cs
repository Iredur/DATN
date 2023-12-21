using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack-meelee", menuName = "Enemy Logic/Attack logic/meelee")]
public class EnemyAttackMeelee : EnemyAttackSOBase
{
    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.agent.isStopped = false;
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();

        enemy.agent.isStopped = true;
    }
    public override void DoFrameUpdate()
    {
        base.DoFrameUpdate();
        Vector2 moveDirection = (player.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(moveDirection * enemy.EnemyUniversalSpeed, player.position);
        if (enemy.meeleeConfirm)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
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
}
