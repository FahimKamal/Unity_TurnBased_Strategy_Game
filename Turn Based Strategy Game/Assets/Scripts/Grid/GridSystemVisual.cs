using System;
using System.Collections.Generic;
using Actions;
using Grid;
using Unity.Mathematics;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour{
    
    public static GridSystemVisual Instance{ get; private set; }
    
    [Serializable]
    public struct GridVisualTypeMaterial{
        public GridVisualType gridVisualType;
        public Material material;
    }
    public enum GridVisualType{
        White, Blue, Red, Yellow, RedSoft
    }
    
    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    [SerializeField] private List<GridVisualTypeMaterial> _gridVisualTypeMaterialList;

    private GridSystemVisualSingle[,] _gridSystemVisualSingleArray;

    private void Awake(){
        if (Instance != null){
            Debug.LogError("There's more than one GridSystemVisual! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start(){
        _gridSystemVisualSingleArray = new GridSystemVisualSingle[
            LevelGrid.Instance.GetWidth, 
            LevelGrid.Instance.GetHeight
        ];
        
        for (var x = 0; x < LevelGrid.Instance.GetWidth; x++){
            for (var z = 0; z < LevelGrid.Instance.GetHeight; z++){
                var gridPosition = new GridPosition(x, z);
                var gridSystemVisualSingleTransform = Instantiate(
                        gridSystemVisualSinglePrefab, 
                        LevelGrid.Instance.GetWorldPosition(gridPosition),
                        quaternion.identity, 
                        transform
                        );

                _gridSystemVisualSingleArray[x, z] =
                    gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
        
        UnitActionSystem.Instance.OnSelectedActionChanged  += UnitActionSystem_OnSelectedActionChanged;
        LevelGrid.Instance.OnAnyUnitMovedGridPosition += LevelGrid_OnAnyUnitMovedGridPosition;
        
        UpdateGridVisual();
    }

    /// <summary>
    /// Update Grid visual whenever any unit changes it's location.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LevelGrid_OnAnyUnitMovedGridPosition(object sender, EventArgs e){
        UpdateGridVisual();
    }

    /// <summary>
    /// Update grid visual whenever Selected action is changed. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e){
        UpdateGridVisual();
    }

    private void UpdateGridVisual(){
        HideAllGridPosition();

        var selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        var selectedAction = UnitActionSystem.Instance.GetSelectedAction();

        GridVisualType gridVisualType = GridVisualType.White;
        
        switch (selectedAction){
            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;
            case SpinAction spinAction:
                gridVisualType = GridVisualType.Blue;
                break;
            case ShootAction shootAction:
                gridVisualType = GridVisualType.Red;
                
                // Show the range of shoot area
                ShowGridPositionRange(selectedUnit.GetGridPosition(), shootAction.GetMaxShootDistance(), GridVisualType.RedSoft);
                break;
        }
        
        // Visualize the grid with their respective color.
        ShowGridPositionList(
            selectedAction.GetValidActionGridPositionList(), gridVisualType
        );
    }

    /// <summary>
    /// Hide all visible grid position.
    /// </summary>
    public void HideAllGridPosition(){
        for (var x = 0; x < LevelGrid.Instance.GetWidth; x++){
            for (var z = 0; z < LevelGrid.Instance.GetHeight; z++){
                _gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }

    private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType){
        var gridPositionList = new List<GridPosition>();
        
        for (var x = -range; x <= range; x++){
            for (var z = -range; z < range; z++){
                var testGridPosition = gridPosition + new GridPosition(x, z);

                // If the grid position is not valid ignore it.
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)){
                    continue;
                }
                
                // If the position is not within circular range then ignore it.
                var testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > range){
                    continue;
                }
                
                // If all check comes out false then add it in list.
                gridPositionList.Add(testGridPosition);
            }
        }
        
        // Now visualize the list in game.
        ShowGridPositionList(gridPositionList, gridVisualType);
    }

    /// <summary>
    /// Visualize all grid position given as list along with their type of color.
    /// </summary>
    /// <param name="gridPositionList"></param>
    /// <param name="gridVisualType"></param>
    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType){
        foreach (var gridPosition in gridPositionList){
            _gridSystemVisualSingleArray[gridPosition.X, gridPosition.Z].Show(GetGridVisualTypeMaterial(gridVisualType));
        }
    }

    /// <summary>
    /// Get the assigned material for the grid visual type.
    /// </summary>
    /// <param name="gridVisualType"></param>
    /// <returns></returns>
    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType){
        var material = _gridVisualTypeMaterialList.Find(x => x.gridVisualType == gridVisualType).material;
        if (material != null){
            return material;
        }
        else{
            Debug.LogError("Could not find material for GridVisualType: " + gridVisualType);
            return null;
        }
    }
}
