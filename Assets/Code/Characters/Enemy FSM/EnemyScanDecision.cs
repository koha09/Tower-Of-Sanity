//
//   CONTRIBUTORS: Montana Games (www.montana-games.com)                  
//   Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com    
//   -------------------------------------------------------------------------------------------------
//

using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Decisions/Scan")]
public class ScanDecision : EnemyDecision
{
    public override bool Decide(EnemyStateController controller)
    {
        bool noEnemyInSight = Scan(controller);
        return noEnemyInSight;
    }

    private bool Scan(EnemyStateController controller)
    {
        controller.charController.isStopped = true;
        //controller.transform.Rotate(0, controller.enemyStats.searchingTurnSpeed * Time.deltaTime, 0);
        return controller.CheckIfCountDownElapsed(controller.enemyStats.SearchDuration);
    }
}