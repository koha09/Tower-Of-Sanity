using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Decisions/ActiveState")]
public class EnemyActiveStateDecision : EnemyDecision
{
    public override bool Decide(EnemyStateController controller)
    {
        bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;
        return chaseTargetIsActive;
    }
}