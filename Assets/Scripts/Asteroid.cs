using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 10;

    [SerializeField]
    private GameObject _explosionPrefab = null;

    private EnemySpawnManager _enemySpawnManager = null;
    private PowerUpSpawnManager _powerUpSpawnManager = null;
    private CircleCollider2D _collider = null;

    // Start is called before the first frame update
    void Start()
    {
        _powerUpSpawnManager = GameObject.Find("PowerUpSpawnManager")?.GetComponent<PowerUpSpawnManager>();
        _enemySpawnManager = GameObject.Find("EnemySpawnManager")?.GetComponent<EnemySpawnManager>();
        _collider = GetComponent<CircleCollider2D>();
        if (_powerUpSpawnManager == null || _enemySpawnManager == null || _collider == null) {
            Debug.LogError("Important components are missing. The object will destroy itself for safety.");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Laser") {
            Destroy(other.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            DestroyAndStartSpawning();
        }  
    }

    private void DestroyAndStartSpawning() {
        _collider.enabled = false;
        _enemySpawnManager.StartSpawning();
        _powerUpSpawnManager.StartSpawning();
        Destroy(gameObject, 0.2f);
    }
}
