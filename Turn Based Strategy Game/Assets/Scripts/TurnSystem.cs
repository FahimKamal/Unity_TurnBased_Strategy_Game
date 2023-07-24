using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour{
    private int _turnNumber;

    public void NextTurn(){
        _turnNumber++;
    }
}
