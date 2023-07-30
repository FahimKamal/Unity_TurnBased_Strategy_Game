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
    
    public int GetWidth => _gridSystem.GetWidth;
    public int GetHeight => _gridSystem.GetHeight;
    
    /// <summary>
    /// Get the gridPosition of the given worldPosition.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);

    /// <summary>
    /// Get the world position of the given girdPosition. 
    /// </summary>
    /// <param name="gridPosition">World position in Vector3</param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);

    /// <summary>
    /// Method checks if the given gridPosition is valid or not. Value of x and z have to be zero or higher not lower.
    /// Also value of x and z have to be less then width and height.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public bool IsValidGridPosition(GridPosition gridPosition) => _gridSystem.IsValidGridPosition(gridPosition);

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition){
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }
    
    public Unit GetUnitOnGridPosition(GridPosition gridPosition){
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }
}
