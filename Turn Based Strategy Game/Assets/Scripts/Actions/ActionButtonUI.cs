using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Actions{
    public class ActionButtonUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;
        [SerializeField] private GameObject selectedImage;
        
        private BaseAction _baseAction;

        /// <summary>
        /// Set UI button for each action available for different units.
        /// </summary>
        /// <param name="baseAction"></param>
        public void SetBaseAction(BaseAction baseAction){
            _baseAction = baseAction;
            textMeshPro.text = baseAction.GetActionName().ToUpper();

            button.onClick.AddListener(() => {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
            });
        }

        public BaseAction GetBaseAction => _baseAction;

        /// <summary>
        /// Indicate that related action with this button is now selected.
        /// </summary>
        public void SetSelected(){
            selectedImage.SetActive(true);
        }

        /// <summary>
        /// Indicate that related action is not selected.
        /// </summary>
        public void ClearSelected(){
            selectedImage.SetActive(false);
        }
    }
}
