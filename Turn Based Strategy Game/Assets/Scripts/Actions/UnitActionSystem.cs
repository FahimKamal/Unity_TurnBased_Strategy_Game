using System;
using Grid;
using Unity.VisualScripting;
using UnityEngine;

namespace Actions{
    public class UnitActionSystem : MonoBehaviour{
        public static UnitActionSystem Instance{ get; private set; }
        
        public event EventHandler OnSelectedUnitChanged;
    
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;

        private BaseAction _selectedAction;
        private bool _isBusy;

        private void Awake(){
            if (Instance != null){
                Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start(){
            SetSelectedUnit(selectedUnit);
        }

        private void Update(){

            if (_isBusy) return;

            if (Input.GetMouseButtonDown(0)){
                // If on mouse click a unit is selected, then don't move the unit on click just select it and return.
                if (TryHandleUnitSelection()) return;


                HandleSelectedAction();
            }
        }

        private void HandleSelectedAction(){
            if (Input.GetMouseButtonDown(0)){
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

                if (_selectedAction.IsValidActionGridPosition(mouseGridPosition)){
                    SetBusy();
                    _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                }
            }
        }

        private void SetBusy(){
            _isBusy = true;
        }

        private void ClearBusy(){
            _isBusy = false;
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
            SetSelectedAction(unit.GetMoveAction());
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetSelectedAction(BaseAction baseAction){
            _selectedAction = baseAction;
        }

        public Unit GetSelectedUnit(){
            return selectedUnit;
        }
    }
}
