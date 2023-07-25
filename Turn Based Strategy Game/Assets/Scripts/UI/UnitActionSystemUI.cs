using System;
using Actions;
using TMPro;
using UnityEngine;

namespace UI{
    public class UnitActionSystemUI : MonoBehaviour{
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainerTransform;
        [SerializeField] private TextMeshProUGUI actionPointsText;

        private void OnEnable(){
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        }
        
        private void Start(){
            CreateUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }
        
        private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e){
            UpdateActionPoints();
        }
        
        private void TurnSystem_OnTurnChanged(object sender, EventArgs e){
            UpdateActionPoints();
        }

        private void UnitActionSystem_OnActionStarted(object sender, EventArgs e){
            UpdateActionPoints();
        }

        private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e){
            UpdateSelectedVisual();
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e){
            CreateUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }

        private void CreateUnitActionButtons(){
            ClearActionButtons();
            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            foreach (var action in selectedUnit.GetBaseActionArray()){
                var actionButton = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
                var actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(action);
            }
        }

        private void ClearActionButtons(){
            foreach (Transform button in actionButtonContainerTransform){
                Destroy(button.gameObject);
            }
        }

        private void UpdateSelectedVisual(){
            var selectedAction = UnitActionSystem.Instance.GetSelectedAction();
            foreach (Transform button in actionButtonContainerTransform){
                var actionButtonUI = button.GetComponent<ActionButtonUI>();
                actionButtonUI.UpdateSelectedVisual();
            }
        }

        private void UpdateActionPoints(){
            var selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            actionPointsText.text = $"Action Points: {selectedUnit.GetActionPoints}";
        }
    }
}
