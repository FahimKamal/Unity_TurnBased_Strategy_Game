using System;
using Actions;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour{
    private GridPosition _currentGridPosition;
    private MoveAction _moveAction;
    private SpinAction _spinAction;


    private void Awake(){
        _moveAction = GetComponent<MoveAction>();
        _spinAction = GetComponent<SpinAction>();
    }

    private void Start(){
        
        _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);
    }

    private void Update(){
        
        
        var newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != _currentGridPosition){
            LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
            _currentGridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction(){
        return _moveAction;
    }
    
    public SpinAction GetSpinAction(){
        return _spinAction;
    }

    public GridPosition GetGridPosition(){
        return _currentGridPosition;
    }

}
