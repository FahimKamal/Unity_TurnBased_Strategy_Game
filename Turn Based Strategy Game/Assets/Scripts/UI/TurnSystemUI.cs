using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI{
    public class TurnSystemUI : MonoBehaviour{
        [SerializeField] private Button nextTurnButton;
        [SerializeField] private TextMeshProUGUI turnNumberText;
        [SerializeField] private GameObject enemyTurnVisualGameObject;

        private void OnEnable(){
            TurnSystem.Instance.OnTurnChanged  += TurnSystem_OnTurnChanged;
        }

        private void Start(){
            
            nextTurnButton.onClick.AddListener(() => {
                TurnSystem.Instance.NextTurn();
            });
            
            
            UpdateTurnNumberText();
            UpdateEnemyTurnVisual();
            UpdateEndTurnButtonVisibility();
        }

        private void TurnSystem_OnTurnChanged(object sender, EventArgs e){
            UpdateTurnNumberText();
            UpdateEnemyTurnVisual();
            UpdateEndTurnButtonVisibility();
        }


        private void UpdateTurnNumberText(){
                turnNumberText.text = "TURN :" + TurnSystem.Instance.GetTurnNumber;
        }

        private void UpdateEnemyTurnVisual(){
            enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn);
        }

        private void UpdateEndTurnButtonVisibility(){
            nextTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn);
        }
    }
}
