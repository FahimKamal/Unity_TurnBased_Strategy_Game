using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class LevelGrid : MonoBehaviour{
    public static LevelGrid Instance { get; private set; }
    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSystem _gridSystem;

    private void Awake(){
        if (Instance != null){
            Debug.LogError("There's more than one LevelGrid! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        _gridSystem = new GridSystem(10, 10, 2f);
        _gridSystem.CreateDebugObjects(gridDebugObjectPrefab, transform);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit){
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition){
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit){
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition){
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }
    
    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);
}
