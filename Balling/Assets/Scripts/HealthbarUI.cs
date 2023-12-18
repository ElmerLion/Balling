using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour {

    [SerializeField] Healthsystem healthSystem;
    [SerializeField] private Image health;

    private void Start() {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        UpdateHealthBar();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e) {
        health.fillAmount = healthSystem.GetHealthAmountNormalized();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        UpdateHealthBar();
    }

    private void UpdateHealthBar() {
        health.fillAmount = healthSystem.GetHealthAmountNormalized();
    }
}
