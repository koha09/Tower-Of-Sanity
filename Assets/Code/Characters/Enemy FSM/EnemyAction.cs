//
//   CONTRIBUTORS: https://learn.unity.com/tutorial/5c515373edbc2a001fd5c79d?language=en#5c7f8528edbc2a002053b487                
//      
//   -------------------------------------------------------------------------------------------------
//

using UnityEngine;


/// <summary>
/// Replace with comments...
/// </summary>
public abstract class EnemyAction : ScriptableObject 
{
    public abstract void Act(EnemyStateController controller);
}