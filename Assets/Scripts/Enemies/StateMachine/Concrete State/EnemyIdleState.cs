using UnityEngine;


public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }
    public override void EnterState()
    {
        enemy.enemyIdleBaseInstance.DoEnterLogic();
        base.EnterState();
    }
    public override void ExitState()
    {
        enemy.enemyIdleBaseInstance.DoExitLogic();
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        enemy.enemyIdleBaseInstance.DoFrameUpdate();
        base.FrameUpdate();


    }
    public override void PhysicsUpdate()
    {
        enemy.enemyIdleBaseInstance.DoPhysicLogic();
        base.PhysicsUpdate();
    }

}
