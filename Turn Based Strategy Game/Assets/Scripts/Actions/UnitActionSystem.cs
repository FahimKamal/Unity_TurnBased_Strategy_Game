using System;
using Grid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Actions{
    public class UnitActionSystem : MonoBehaviour{
        public static UnitActionSystem Instance{ get; private set; }
        
        public event EventHandler OnSelectedUnitChanged;
        public event EventHandler OnSelectedActionChanged;
        public event EventHandler<bool> OnBusyChanged;
        public event EventHandler OnActionStarted;
    
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

            // If it is not player's turn then don't do anything.
            if (!TurnSystem.Instance.IsPlayerTurn) return;

            // if mouse pointer is over any UI elements them don't select or execute any actions.
            if (EventSystem.current.IsPointerOverGameObject()) return;

                // If on mouse click a unit is selected, then don't move the unit on click just select it and return.
            if (TryHandleUnitSelection()){
                return;
            }


            HandleSelectedAction();
        }

        private void HandleSelectedAction(){
            if (Input.GetMouseButtonDown(0)){
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());


                if (!_selectedAction.IsValidActionGridPosition(mouseGridPosition)) return;

                if (!selectedUnit.TrySpendActionPointsToTakeAction(_selectedAction)) return;
                
                SetBusy();
                _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                OnActionStarted?.Invoke(this, EventArgs.Empty);
            }
        }

        private void SetBusy(){
            _isBusy = true;
            OnBusyChanged?.Invoke(this, _isBusy);
        }

        private void ClearBusy(){
            _isBusy = false;
            OnBusyChanged?.Invoke(this, _isBusy);
        }

        /// <summary>
        /// Try to select object of Type Unit in units layer in game.
        /// </summary>
        /// <returns>Return true if it did select a unit else returns false.</returns>
        private bool TryHandleUnitSelection(){
            if (Input.GetMouseButtonDown(0)){
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var raycastHit, float.MaxValue, unitLayerMask)){
                    if (raycastHit.transform.TryGetComponent<Unit>(out var unit)){
                        if (unit == selectedUnit){
                            // If unit is already selected then don't select it again.
                            return false;
                        }

                        if (unit.IsEnemy){
                            // If unit is enemy type then don't select it.
                            return false;
                        }
                        SetSelectedUnit(unit);
                        return true;
                    }
                }

                return false;
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
            OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
        }

        public Unit GetSelectedUnit(){
            return selectedUnit;
        }

        public BaseAction GetSelectedAction(){
            return _selectedAction;
        }
    }
}
