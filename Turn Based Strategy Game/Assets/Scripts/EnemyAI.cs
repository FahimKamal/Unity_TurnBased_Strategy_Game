using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour{
    private float _timer;

    private void Start(){
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e){
        _timer = 2f;
    }

    private void Update(){
        if (TurnSystem.Instance.IsPlayerTurn){
            return;
        }
        
        _timer -= Time.deltaTime;
        if (_timer <= 0){
            TurnSystem.Instance.NextTurn();
        }
    }
}
