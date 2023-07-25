using Actions;
using UnityEngine;

namespace UI{
    public class ActionBusyUI : MonoBehaviour
    {

        private void Start(){
            gameObject.SetActive(false);
        }

        private void OnEnable(){
            UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;
        }

        private void UnitActionSystem_OnBusyChanged(object sender, bool e){
            gameObject.SetActive(e);
        }
    }
}
