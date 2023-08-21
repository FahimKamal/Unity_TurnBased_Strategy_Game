using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour{
    
    public static UnitManager Instance{ get; private set; }
    
    private List<Unit> _unitList;
    private List<Unit> _friendlyUnitList;
    private List<Unit> _enemyUnitList;

    public List<Unit> GetUnitList(){ return _unitList; }
    public List<Unit> GetFriendlyList(){ return _friendlyUnitList; }
    public List<Unit> GetEnemyList(){ return _enemyUnitList; }

    private void Awake(){
        if (Instance != null){
            Debug.LogError("There's more than one UnitManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        _unitList = new List<Unit>();
        _friendlyUnitList = new List<Unit>();
        _enemyUnitList = new List<Unit>();
    }

    private void Start(){
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Unit_OnAnyUnitSpawned(object sender, EventArgs e){
        var unit = sender as Unit;
        _unitList.Add(unit);

        if (unit != null && unit.IsEnemy){
            _enemyUnitList.Add(unit);
        }
        else{
            _friendlyUnitList.Add(unit);
        }
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e){
        var unit = sender as Unit;
        _unitList.Remove(unit);

        if (unit != null && unit.IsEnemy){
            _enemyUnitList.Remove(unit);
        }
        else{
            _friendlyUnitList.Remove(unit);
        }
    }
}
