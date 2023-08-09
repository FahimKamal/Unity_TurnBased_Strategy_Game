using Actions;
using UnityEngine;

public class CameraManager : MonoBehaviour{
    [SerializeField] private GameObject actionCameraGameObject;

    private void Start(){
        BaseAction.OnAnyActionStarted += (sender, args) => {
            switch (sender){
                case ShootAction shootAction:
                    var shooterUnit = shootAction.GetParentUnit();
                    var targetUnit = shootAction.GetTargetUnit();
                    var cameraCharacterHeight = Vector3.up * 1.7f;
                    var shooterDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;
                    var shoulderOffsetAmount = 0.5f;
                    var shoulderOffset = Quaternion.Euler(0, 90, 1) * shooterDir * shoulderOffsetAmount;

                    var actionCameraPosition = 
                        shooterUnit.GetWorldPosition() + 
                        cameraCharacterHeight + 
                        shoulderOffset +
                        (shooterDir * -1);
                    actionCameraGameObject.transform.position = actionCameraPosition;
                    actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);
                    
                    ShowActionCamera();
                    break;
            }
        };
        BaseAction.OnAnyActionCompleted += (sender, args) => {
            switch (sender){
                case ShootAction shootAction:
                    HideActionCamera();
                    break;
            }
        };
        HideActionCamera();
    }

    private void ShowActionCamera(){
        actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera(){
        actionCameraGameObject.SetActive(false);
    }
}
