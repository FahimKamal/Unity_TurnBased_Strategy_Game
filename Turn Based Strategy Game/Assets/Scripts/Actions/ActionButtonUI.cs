using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Actions{
    public class ActionButtonUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;

        public void SetBaseAction(BaseAction baseAction){
            textMeshPro.text = baseAction.GetActionName().ToUpper();

            button.onClick.AddListener(() => {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
            });
        }
    }
}
