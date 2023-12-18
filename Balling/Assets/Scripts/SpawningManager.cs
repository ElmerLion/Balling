using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour {

    [SerializeField] private List<Transform> ballTransforms;
    [SerializeField] private List<Transform> rareBallTransforms;
    [SerializeField] private List<Transform> spawnLocations;

    private float previousXPosition;
    private float currentXPosition;
    private float xUntilNextSpawn = 10;
    private int chanceToSpawnRareBall = 50;
    

    private void Start() {
        previousXPosition = xUntilNextSpawn;
    }

    private void Update() {
        currentXPosition = Player.Instance.transform.position.x;
        if (currentXPosition >= previousXPosition) {
            
            foreach (Transform spawnLocation in spawnLocations) {
                int randomBallIndex = Random.Range(0, ballTransforms.Count);
                Transform ballToSpawn = ballTransforms[randomBallIndex];

                float randomY = Random.Range(0f, 5f);
                float randomX = Random.Range(0f, 5f);
                Vector3 spawnPosition = spawnLocation.position + new Vector3(randomX, randomY, 0);

                if (Random.Range(0, chanceToSpawnRareBall) == chanceToSpawnRareBall - 1) {
                    SpawnRareBall(spawnPosition);
                } else {
                    Transform spawnedBall = Instantiate(ballToSpawn, spawnPosition, Quaternion.identity);
                }

                
            }

            previousXPosition = currentXPosition + xUntilNextSpawn;
            
        }
        
    }

    private void SpawnRareBall(Vector3 position) {
        int randomBallIndex = Random.Range(0, rareBallTransforms.Count);
        Instantiate(rareBallTransforms[randomBallIndex], position, Quaternion.identity);
    }

}
