
using UnityEngine;
[CreateAssetMenu(fileName = "Idle-Rest", menuName = "Enemy Logic/Chase logic/Approach")]
public class EnemyChaseMeelee : EnemyChaseSOBase
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
