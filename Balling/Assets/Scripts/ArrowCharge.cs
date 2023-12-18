using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCharge : MonoBehaviour {

    private Transform arrow;
    private bool isCharging;

    private float minCharge = 1f;
    private float midCharge = 12f;
    private float maxCharge = 19.5f;

    private Vector3 arrowOffset;

    private void Start() {
        transform.gameObject.SetActive(false);
        arrow = transform.GetChild(0);
        Player.Instance.OnCharging += Player_OnCharging;
        Player.Instance.OnChargingStopped += Player_OnChargingStopped;
    }

    private void Update() {
        if (isCharging) {
            UpdateArrow();
        }
    }

    private void Player_OnChargingStopped(object sender, System.EventArgs e) {
        transform.gameObject.SetActive(false);
        isCharging = false;
    }

    private void Player_OnCharging(object sender, System.EventArgs e) {
        Vector3 playerPosition = Player.Instance.transform.position;

        Vector3 shootingDirection = -Player.Instance.GetCurrentShootingDirection();
        arrowOffset = shootingDirection.normalized * 1.0f;

        Vector3 arrowPosition = playerPosition + arrowOffset;
        arrow.position = arrowPosition;

        transform.gameObject.SetActive(true);
        isCharging = true;
    }



    private void UpdateArrow() {
        Vector3 shootingDirection = -Player.Instance.GetCurrentShootingDirection();

        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        arrow.rotation = Quaternion.Euler(0, 0, angle);

        float newXScale = Player.Instance.GetCurrentCharge() / 20;
        arrow.localScale.Scale(new Vector3(newXScale, arrow.localScale.y));

        float currentCharge = Player.Instance.GetCurrentCharge();
        SpriteRenderer arrowSpriteRenderer = arrow.GetComponent<SpriteRenderer>();

        if (currentCharge > minCharge && currentCharge < midCharge) {
            arrowSpriteRenderer.color = Color.green;
        }
        if (currentCharge > midCharge && currentCharge < maxCharge) {
            arrowSpriteRenderer.color = Color.yellow;
        }
        if (currentCharge > maxCharge) {
            arrowSpriteRenderer.color = Color.red;
        }

        Vector3 playerPosition = Player.Instance.transform.position;
        Vector3 arrowPosition = playerPosition + arrowOffset;
        arrow.position = arrowPosition;
    }

}
