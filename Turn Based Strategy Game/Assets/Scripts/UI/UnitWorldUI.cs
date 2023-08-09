using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Unit unit;


    private void Start(){
        Unit.OnAnyActionPointsChanged  += (sender, args) => UpdateActionPointsText(); 
        healthSystem.OnDamaged += (sender, args) => UpdateHealthBar();
        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void UpdateActionPointsText(){
        actionPointsText.text = unit.GetActionPoints.ToString();
    }

    private void UpdateHealthBar(){
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
}
