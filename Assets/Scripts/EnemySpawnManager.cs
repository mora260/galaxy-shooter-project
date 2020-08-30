using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab = null;

    [SerializeField]
    private GameObject _enemyContainer = null;

    
    [SerializeField]
    private float _enemyWaitTime = 5.0f;

    private bool _spawnEnemies = true;

    IEnumerator SpawnEnemies() {
        while(_spawnEnemies) {
            yield return new WaitForSeconds(_enemyWaitTime);
            GameObject newEnemy = Instantiate(_enemyPrefab, RandomEnemyPosition(), Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);
        }
    }

    Vector3 RandomEnemyPosition() {
        Vector3 position = new Vector3(Random.Range(-9.3f, 9.3f), 6.6f, 0f);
        return position;
    }

    public void StopSpawning() {
        _spawnEnemies = false;
    }

    public void StartSpawning() {
        StartCoroutine(SpawnEnemies());
    }

}
