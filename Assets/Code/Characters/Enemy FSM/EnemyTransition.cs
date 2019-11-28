//
//   CONTRIBUTORS: https://learn.unity.com/tutorial/5c515373edbc2a001fd5c79d?language=en#5c7f8528edbc2a002053b487                
//      
//   -------------------------------------------------------------------------------------------------
//


[System.Serializable]
public class EnemyTransition
{
    public EnemyDecision decision;
    public EnemyState trueState;
    public EnemyState falseState;
}