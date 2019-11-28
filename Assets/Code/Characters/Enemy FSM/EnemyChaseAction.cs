//
//   CONTRIBUTORS: Montana Games (www.montana-games.com)       
//  Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com       
//  -------------------------------------------------------------------------------------------------
//  https://learn.unity.com/tutorial/5c515373edbc2a001fd5c79d?language=en#5c7f8528edbc2a002053b487                


using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Actions/Chase")]
public class EnemyChaseAction : EnemyAction
{
    public bool RunIfTargetFound;
    public override void Act(EnemyStateController controller)
    {
        Chase(controller);
    }

    private void Chase(EnemyStateController controller)
    {
        controller.charController.Destination = controller.chaseTarget.position;
        if(RunIfTargetFound) controller.charController.willRun = true;
        controller.charController.isStopped = false;
    }
}