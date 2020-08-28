using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
 [SerializeField]
    private GameObject[] _powerUpPrefabArray = null;

    [SerializeField]
    private GameObject _powerUpContainer = null;

    [SerializeField]
    private float _powerUpMinWait = 7.0f;

    [SerializeField]
    private float _powerUpMaxWait = 15.0f;

    [SerializeField]
    private bool _spawnPowerUps = true;


    private void Start() {
        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnPowerUps() {
        while(_spawnPowerUps) {
            yield return new WaitForSeconds(RandomWait());
            GameObject newEnemy = Instantiate(GetRandomPowerUp(), RandomPosition(), Quaternion.identity);
            newEnemy.transform.SetParent(_powerUpContainer.transform);
        }
    }

    Vector3 RandomPosition() {
        Vector3 position = new Vector3(Random.Range(-9.3f, 9.3f), 6.6f, 0f);
        return position;
    }

    float RandomWait() {
        return Random.Range(_powerUpMinWait, _powerUpMaxWait);
    }

    public void StopSpawning() {
        _spawnPowerUps = false;
    }

    private GameObject GetRandomPowerUp() {
        int index = Random.Range(0, _powerUpPrefabArray.Length);
        return _powerUpPrefabArray[index];
    }
}
