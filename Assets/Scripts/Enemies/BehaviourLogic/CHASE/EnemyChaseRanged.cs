
using UnityEngine;
[CreateAssetMenu(fileName = "Chase-single", menuName = "Enemy Logic/Chase logic/Ranged/Normal-ranged")]
public class EnemyChaseRanged : EnemyChaseSOBase
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
        if (enemy.isInRange)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
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
