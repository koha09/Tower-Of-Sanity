//
//   CONTRIBUTORS: Montana Games (www.montana-games.com)       
//  Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com       
//  -------------------------------------------------------------------------------------------------
//  https://learn.unity.com/tutorial/5c515373edbc2a001fd5c79d?language=en#5c7f8528edbc2a002053b487                

using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Decisions/Look")]
public class LookDecision : EnemyDecision
{

    public override bool Decide(EnemyStateController controller)
    {
        return Look(controller);
        
    }

    private bool Look(EnemyStateController controller)
    {
        Vector3 s = controller.eyes.position;
        s.x += controller.enemyStats.FieldOfView * controller.transform.localScale.x;
        RaycastHit2D hit = Physics2D.Linecast(controller.eyes.position,s);

        Debug.DrawLine(controller.eyes.position, s, Color.green);
        //TODO add range to enemyData
        if (hit.collider!=null && hit.collider.CompareTag("Player"))
        {
            controller.chaseTarget = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}