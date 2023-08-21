using System;
using Actions;
using Grid;
using UnityEngine;

public class EnemyAI : MonoBehaviour{
    
    private enum State{
        WaitingForEnemyTurn,
        TakingTurn,
        Busy
    }

    private State _state;
    
    private float _timer;

    private void Awake(){
        _state = State.WaitingForEnemyTurn;
    }

    private void Start(){
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e){
        if (!TurnSystem.Instance.IsPlayerTurn){
            _state = State.TakingTurn;
            _timer = 2f;
        }
    }

    private void Update(){
        if (TurnSystem.Instance.IsPlayerTurn){
            return;
        }

        switch (_state){
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                _timer -= Time.deltaTime;
                if (_timer <= 0){
                    if (TryTakeEnemyAIAction(SetStateTakingTurn)){
                        _state = State.Busy;
                    }
                    else{
                        // No more enemies have actions they can take, and end enemy turn.
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
        }
    }

    private void SetStateTakingTurn(){
        _timer = 0.5f;
        _state = State.TakingTurn;
    }

    private bool TryTakeEnemyAIAction(Action onEnemyAIActionComplete){
        Debug.Log("Take Enemy AI Action");
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyList()){
            if (TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete)){ 
                return true;
            }
        }
        return false;
    }

    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete){
        SpinAction spinAction = enemyUnit.GetSpinAction();

        GridPosition actionGridPosition = enemyUnit.GetGridPosition();

        if (!spinAction.IsValidActionGridPosition(actionGridPosition)){
            return false;
        }

        if (!enemyUnit.TrySpendActionPointsToTakeAction(spinAction)){
            return false;
        }
        
        Debug.Log("Spin Action");
        
        spinAction.TakeAction(actionGridPosition, onEnemyAIActionComplete);
        return true;
        
    }
}
