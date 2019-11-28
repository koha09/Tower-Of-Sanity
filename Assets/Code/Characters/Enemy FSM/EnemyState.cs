//
//   CONTRIBUTORS: https://learn.unity.com/tutorial/5c515373edbc2a001fd5c79d?language=en#5c7f8528edbc2a002053b487                
//   Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com       
//   -------------------------------------------------------------------------------------------------
//

using UnityEngine;


/// <summary>
/// Replace with comments...
/// </summary>
[CreateAssetMenu(menuName = "Enemy/State")]
public class EnemyState : ScriptableObject 
{

    public EnemyAction[] actions;
    public EnemyTransition[] transitions;
    public Color sceneGizmoColor = Color.grey;

    public void UpdateState(EnemyStateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(EnemyStateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(EnemyStateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }

}