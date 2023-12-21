using UnityEngine;
[CreateAssetMenu(fileName = "Idle-Rest", menuName = "Enemy Logic/Idle logic/Rest")]
public class EnemyIdleRest : EnemyIdleSOBase
{
    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }
    public override void DoExitLogic() { base.DoExitLogic(); }
    public override void DoFrameUpdate()
    {
        base.DoFrameUpdate();
        if (enemy.isAggroed)
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
}
