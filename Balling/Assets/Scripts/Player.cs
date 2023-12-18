using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event EventHandler OnCharging;
    public event EventHandler OnChargingStopped;

    private float chargeLevel = 0f;
    private bool isCharging = false;
    private float chargeRate = 35f;
    private float shootForce = 0.8f;
    private float minChargeTime = 0.25f; 
    private float chargeTimer = 0f;
    private float maxCharge = 20f;

    private Vector2 initialMousePos;
    private Vector2 shootingDirection;
    public Healthsystem healthSystem { get; private set; }
    private Rigidbody2D rb;

    private bool menuIsOpen;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        menuIsOpen = false;

        rb = transform.GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<Healthsystem>();

        healthSystem.OnDied += HealthSystem_OnDied;

        Time.timeScale = 1f;
    }

    private void HealthSystem_OnDied(object sender, EventArgs e) {
        Time.timeScale = 0f;
        GameOverUI.Instance.Show();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartCharging();
        }

        if (Input.GetMouseButton(0)) {
            ContinueCharging();
            if (chargeTimer > minChargeTime) {
                OnCharging?.Invoke(this, EventArgs.Empty);
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !menuIsOpen) {
            menuIsOpen = true;
            EscapeMenuUI.Instance.Show();
        } else if (Input.GetKeyDown(KeyCode.Escape) && menuIsOpen) {
            menuIsOpen = false;
            EscapeMenuUI.Instance.Hide();
            OptionsMenuUI.Instance.Hide();
        }
    }

    private void StartCharging() {
        initialMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isCharging = true;
        chargeLevel = 0f;
        chargeTimer = 0f;

        
    }

    private void ContinueCharging() {
        if (chargeTimer < minChargeTime) {
            chargeTimer += Time.deltaTime;
        }

        Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootingDirection = (currentMousePos - (Vector2)transform.position).normalized;

        if (isCharging && chargeLevel <= maxCharge) {
            
            chargeLevel += Time.deltaTime * chargeRate;
        }
    }

    private void Shoot() {
        if (isCharging && chargeTimer >= minChargeTime) {
            rb.velocity = -shootingDirection * chargeLevel * shootForce;
            isCharging = false;
            OnChargingStopped?.Invoke(this, EventArgs.Empty);
        }
    }

    public void AddExplosionForce(float force) {
        if (force > 0) {
            Vector2 forceDirection = -transform.position.normalized; // Calculate the direction away from the explosion point
            rb.AddForce(forceDirection * force, ForceMode2D.Impulse);
        }
        
    }

    public void AddExtraBoost(float boost) {
        if (boost > 0) {
            rb.velocity = -shootingDirection * boost;
        }
        
    }

    public float GetCurrentCharge() {
        return chargeLevel;
    }

    public Vector2 GetCurrentShootingDirection() {
        return shootingDirection;
    }

}
