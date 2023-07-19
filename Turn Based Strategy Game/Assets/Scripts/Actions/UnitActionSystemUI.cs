using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actions{
    public class UnitActionSystemUI : MonoBehaviour{
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainerTransform;
        private void Start(){
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            CreateUnitActionButtons();
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e){
            CreateUnitActionButtons();
        }

        private void CreateUnitActionButtons(){
            ClearActionButtons();
            
            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            foreach (var action in selectedUnit.GetBaseActionArray()){
                var actionButton = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
                var actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(action);
            }

            selectedUnit.GetBaseActionArray();
        }

        private void ClearActionButtons(){
            foreach (Transform button in actionButtonContainerTransform){
                Destroy(button.gameObject);
            }
        }
    }
}
