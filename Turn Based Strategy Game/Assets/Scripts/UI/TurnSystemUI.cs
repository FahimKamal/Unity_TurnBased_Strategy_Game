using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI{
    public class TurnSystemUI : MonoBehaviour{
        [SerializeField] private Button nextTurnButton;
        [SerializeField] private TextMeshProUGUI turnNumberText;

        private void OnEnable(){
            TurnSystem.Instance.OnTurnChanged  += TurnSystem_OnTurnChanged;
        }

        private void Start(){
            
            nextTurnButton.onClick.AddListener(() => {
                TurnSystem.Instance.NextTurn();
            });
            
            
            UpdateTurnNumberText();
        }

        private void TurnSystem_OnTurnChanged(object sender, EventArgs e){
            UpdateTurnNumberText();
        }


        public void UpdateTurnNumberText(){
                turnNumberText.text = "TURN :" + TurnSystem.Instance.GetTurnNumber;
        }
    }
}
