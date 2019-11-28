//
//   CONTRIBUTORS: Montana Games (www.montana-games.com)       
//  Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com       
//  -------------------------------------------------------------------------------------------------
//  https://learn.unity.com/tutorial/5c515373edbc2a001fd5c79d?language=en#5c7f8528edbc2a002053b487                


using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour 
{

    public EnemyState currentState;
    public EnemyData enemyStats;
    public Transform eyes;
    public EnemyState remainState;

    [Header("Only for Debug")]
    [HideInInspector] 
    public CharacterMovement charController;
    //[HideInInspector] 
    public WayPointsPath2D patrolPath;
    public List<Vector3> wayPointList;
    //[HideInInspector] 
    public int nextWayPoint;
    //[HideInInspector]
    public Transform chaseTarget;
    //[HideInInspector] 
    public float stateTimeElapsed;

    private bool aiActive;


    void Awake()
    {
        patrolPath = GetComponent<WayPointsPath2D>();
        charController = GetComponent<CharacterMovement>();
        SetupAI(true, patrolPath.GetAllPointsAsList());
    }

    public void SetupAI(bool aiActivation, List<Vector3> wayPoints)
    {
        wayPointList = wayPoints;
        aiActive = aiActivation;
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.FieldOfView);
        }
    }

    public void TransitionToState(EnemyState nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}