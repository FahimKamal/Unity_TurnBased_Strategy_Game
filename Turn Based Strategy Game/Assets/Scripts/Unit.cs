using System;
using Actions;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour{
    private const int ACTION_POINTS_MAX = 2;
    public static event EventHandler OnAnyActionPointsChanged;

    [SerializeField] private bool isEnemy;
    
    private GridPosition _currentGridPosition;
    private HealthSystem _healthSystem;
    private MoveAction _moveAction;
    private SpinAction _spinAction;
    private BaseAction[] _baseActionArray;
    private int _actionPoints = ACTION_POINTS_MAX;

    private void OnEnable(){
        
    }

    private void Awake(){
        _healthSystem = GetComponent<HealthSystem>();
        _moveAction = GetComponent<MoveAction>();
        _spinAction = GetComponent<SpinAction>();
        _baseActionArray = GetComponents<BaseAction>();
    }

    private void Start(){
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);
        
        _healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void Update(){
        
        
        var newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != _currentGridPosition){
            LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
            _currentGridPosition = newGridPosition;
        }
    }
    
    private void HealthSystem_OnDead(object sender, EventArgs e){
        // On death occurs remove this unit from levelGrid and destroy this gameObject. 
        LevelGrid.Instance.RemoveUnitAtGridPosition(_currentGridPosition, this);
        Destroy(gameObject);
    }

    public MoveAction GetMoveAction(){
        return _moveAction;
    }
    
    public SpinAction GetSpinAction(){
        return _spinAction;
    }

    public GridPosition GetGridPosition(){
        return _currentGridPosition;
    }

    public Vector3 GetWorldPosition(){
        return LevelGrid.Instance.GetWorldPosition(_currentGridPosition);
    }

    public BaseAction[] GetBaseActionArray(){
        return _baseActionArray;
    }
    
    public int GetActionPoints => _actionPoints;
    public bool IsEnemy => isEnemy;

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction){
        if (CanSpendActionPointsToTakeAction(baseAction)){
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        return false;
    }

    private bool CanSpendActionPointsToTakeAction(BaseAction baseAction){
        return _actionPoints >= baseAction.GetActionPointsCost();
    }


    private void SpendActionPoints(int amount){
        
        _actionPoints -= amount;
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }
    
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e){
        if ((IsEnemy && !TurnSystem.Instance.IsPlayerTurn) || 
            (!IsEnemy && TurnSystem.Instance.IsPlayerTurn)){
            _actionPoints = ACTION_POINTS_MAX;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
        
    }

    public void Damage(int damageAmount){
        _healthSystem.Damage(damageAmount);
    }
}
