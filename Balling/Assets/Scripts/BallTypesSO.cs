using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BallTypeSO")]
public class BallTypesSO : ScriptableObject {

    public string nameString;
    public int score;
    public float damage;
    public float explosionForce;
    public float boost;
    public int healAmount;

    public Color color;
    
}
