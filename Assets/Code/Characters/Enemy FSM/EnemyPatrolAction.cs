//
//   CONTRIBUTORS: https://learn.unity.com/tutorial/5c515373edbc2a001fd5c79d?language=en#5c7f8528edbc2a002053b487                
//   Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com       
//   -------------------------------------------------------------------------------------------------
//

using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Actions/Patrol")]
public class EnemyPatrolAction : EnemyAction
{
    float _time;
    public override void Act(EnemyStateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(EnemyStateController controller)
    {
        controller.charController.Destination = controller.wayPointList[controller.nextWayPoint];
        controller.charController.isStopped = false;

        if (controller.charController.remainingDistance <= controller.charController.stoppingDistance && !controller.charController.pathPending)
        {
            _time -= Time.deltaTime;
            controller.charController.isStopped = true;

            if (_time <= 0)
            {
                controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
                _time = controller.patrolPath.GetTimeByIndex(controller.nextWayPoint);
            }
        }
    }
}