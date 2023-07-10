using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour{
    public static UnitActionSystem Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Debug.LogError("There's nore than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public event EventHandler OnSelectedUnitChanged;
    
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Update(){
        
        if (Input.GetMouseButtonDown(0)){
            if (TryHandleUnitSelection()) return;
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    /// <summary>
    /// Try to select object of Type Unit in units layer in game.
    /// </summary>
    /// <returns>Return true if it did select a unit else returns false.</returns>
    private bool TryHandleUnitSelection(){
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var raycastHit, float.MaxValue, unitLayerMask)){
            if (raycastHit.transform.TryGetComponent<Unit>(out var unit)){
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit){
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit(){
        return selectedUnit;
    }
}
