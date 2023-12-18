using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using System;

public class PlayerHitDetection : MonoBehaviour {
    public static PlayerHitDetection Instance { get; private set; }

    public event EventHandler OnGoodBallHit;
    public event EventHandler OnBadBallHit;

    [SerializeField] private LayerMask scoreBallLayer;
    private Player player;
    private BallTypesSO hitBallTypeSO;
    private GameObject hitGameObject;
    private Transform ballExplosionPrefab;
    private Vector3 hitObjectPosition;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        ballExplosionPrefab = Resources.Load<Transform>("BallExplosion");
        player = transform.GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<BallTypeHolder>() != null) {
            hitBallTypeSO = collision.gameObject.GetComponent<BallTypeHolder>().ballTypeSO;
            hitGameObject = collision.gameObject;
            hitObjectPosition = collision.transform.position;

            ScoreManager.Instance.CheckComboOnHit(hitBallTypeSO);
            HandleBallHit();
            
        }
    }


    private void HandleBallHit() {
        CameraShakeEffect.Instance.ShakeCamera(1.5f, 0.3f);
        ScoreManager.Instance.AddScore(hitBallTypeSO.score);

        if (hitBallTypeSO.score > 0) {
            OnGoodBallHit?.Invoke(this, EventArgs.Empty);
        }
        
        if (hitBallTypeSO.explosionForce > 0) {
            player.AddExplosionForce(hitBallTypeSO.explosionForce);
        }
        if (hitBallTypeSO.damage > 0) {
            player.healthSystem.Damage(hitBallTypeSO.damage);
            OnBadBallHit?.Invoke(this, EventArgs.Empty);
        }
        if (hitBallTypeSO.healAmount > 0) {
            player.healthSystem.Heal(hitBallTypeSO.healAmount);
            OnGoodBallHit?.Invoke(this, EventArgs.Empty);
        }

        Transform ballExplosion = Instantiate(ballExplosionPrefab, hitObjectPosition, Quaternion.identity);
        var mainModule = ballExplosion.GetComponent<ParticleSystem>().main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(hitBallTypeSO.color);

        Destroy(hitGameObject);

        if (hitBallTypeSO.boost > 0) {
            player.AddExtraBoost(hitBallTypeSO.boost);
            OnGoodBallHit?.Invoke(this, EventArgs.Empty);
        }
        
    }

}
