
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Actions/Attack")]
public class EnemyAttackAction : EnemyAction
{
    public override void Act(EnemyStateController controller)
    {
        Attack(controller);
    }

    private void Attack(EnemyStateController controller)
    {
        RaycastHit hit;
        Debug.LogError("Это действие еще не описано программистом, ожидайте 99999... years");
        //Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);

        //if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.attackRange)
        //    && hit.collider.CompareTag("Player"))
        //{
        if (controller.CheckIfCountDownElapsed(controller.enemyStats.AttackRate))
        {
            Debug.Log("Enemy attackt Player");
        }
        //}
    }
}