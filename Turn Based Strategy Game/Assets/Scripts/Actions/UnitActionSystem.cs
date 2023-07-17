using System;
using Grid;
using UnityEngine;

namespace Actions{
    public class UnitActionSystem : MonoBehaviour{
        public static UnitActionSystem Instance{ get; private set; }

        private void Awake(){
            if (Instance != null){
                Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
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
                // If on mouse click a unit is selected, then don't move the unit on click just select it and return.
                if (TryHandleUnitSelection()) return;
            
                // if no new unit is selected then move the already selected unit to that position.
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
                if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition)){
                    selectedUnit.GetMoveAction().Move(mouseGridPosition);
                }
                // selectedUnit.GetMoveAction().Move(MouseWorld.GetPosition());
            }

            if (Input.GetMouseButtonDown(1)){
                selectedUnit.GetSpinAction().Spin();
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
}
