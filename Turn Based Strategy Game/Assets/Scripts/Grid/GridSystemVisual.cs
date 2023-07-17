using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using Unity.Mathematics;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour{
    
    public static GridSystemVisual Instance{ get; private set; }
    
    [SerializeField] private Transform gridSystemVisualSinglePrefab;

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
    }

    private void Update(){
        UpdateGridVisual();
    }

    private void UpdateGridVisual(){
        HideAllGridPosition();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        
        ShowGridPositionList(
            selectedUnit.GetMoveAction().GetValidActionGridPositionList()
        );
    }

    public void HideAllGridPosition(){
        for (var x = 0; x < LevelGrid.Instance.GetWidth; x++){
            for (var z = 0; z < LevelGrid.Instance.GetHeight; z++){
                _gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList){
        foreach (var gridPosition in gridPositionList){
            _gridSystemVisualSingleArray[gridPosition.X, gridPosition.Z].Show();
        }
    }
}
