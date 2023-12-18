using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBarriers : MonoBehaviour {

    [SerializeField] private Transform playerTransform;
    private Transform upperBarrier;
    private Transform lowerBarrier;
    private float allowedDistanceToBarriers = 1.2f;

    private float damageTimer;
    private float damageTimerMax = 0.3f;

    private void Start() {
        upperBarrier = transform.Find("UpperBarrier");
        lowerBarrier = transform.Find("LowerBarrier");
    }

    private void Update() {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);

        if (Vector3.Distance(upperBarrier.position, playerTransform.position) < allowedDistanceToBarriers 
            || Vector3.Distance(lowerBarrier.position, playerTransform.position) < allowedDistanceToBarriers) {
            damageTimer += Time.deltaTime;
            if (damageTimer > damageTimerMax) {
                damageTimer = 0f;
                playerTransform.GetComponent<Healthsystem>().Damage(5);
                CameraShakeEffect.Instance.ShakeCamera(1.3f, 0.3f);
                SoundManager.Instance.PlayDamageSound();
            }
        }

       
    }



}
