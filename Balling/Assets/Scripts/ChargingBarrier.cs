using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingBarrier : MonoBehaviour {

    private Vector3 speed = new Vector3(8f, 0, 0);

    private float speedupTimer;
    private float speedupTimerMax = 30f;

    private void Update() {
        transform.position += speed * Time.deltaTime;
        speedupTimer += Time.deltaTime;

        if (speedupTimer > speedupTimerMax) {
            speedupTimer = 0f;
            speed += new Vector3(1f, 0, 0);
            Debug.Log("ChargingBarrier Speed: " + speed);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<BallTypeHolder>() != null) {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.GetComponent<Player>() != null) {
            Player.Instance.healthSystem.Damage(1000000);
        }
    }

}
