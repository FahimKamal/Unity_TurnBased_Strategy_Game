using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actions{
    public class UnitActionSystemUI : MonoBehaviour{
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainerTransform;
        private void Start(){
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            CreateUnitActionButtons();
            UpdateSelectedVisual();
        }

        private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e){
            UpdateSelectedVisual();
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e){
            CreateUnitActionButtons();
            UpdateSelectedVisual();
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
                if (actionButtonUI.GetBaseAction == selectedAction){
                    actionButtonUI.SetSelected();
                }
                else{
                    actionButtonUI.ClearSelected();
                }
            }
        }
    }
}
