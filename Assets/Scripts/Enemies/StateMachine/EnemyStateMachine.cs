using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentEnemyState;
    public void Initialize(EnemyState startingState)
    {
        currentEnemyState = startingState;
        currentEnemyState.EnterState();
    }
    public void ChangeState(EnemyState nextState)
    {
        currentEnemyState.ExitState();
        currentEnemyState = nextState;
        currentEnemyState.EnterState();
    }
}
