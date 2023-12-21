using UnityEngine;

public class EnemyChaseSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

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
        if (enemy.isAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }
    public virtual void DoPhysicLogic() { }
    public virtual void DoAnimationTriggerLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void DoResetValues()
    {
    }
}
