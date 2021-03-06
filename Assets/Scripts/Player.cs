﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _normalLaserCoolDown = 0;

    [SerializeField]
    private GameObject _laserPrefab = null;

    [SerializeField]
    private GameObject _tripleLaserPrefab = null;

    private float _lastFire = 0;

    [SerializeField]
    private int _lives = 3;

    private EnemySpawnManager _enemySpawner = null; 

    public bool TripleLaserPowerOn {get; set;}

    [SerializeField]
    private float _tripleShotActiveTime = 5.0f;

    [SerializeField]
    private float _speedBoostActiveTime = 10.0f;

    [SerializeField]
    private float _speedBoost = 2.0f;
    private float _speedModifier = 1.0f;

    private bool _shieldActive = false;

    private GameObject _shield = null;

    [SerializeField]
    private int _score = 0;

    private UIManager _uiManager = null;

    [SerializeField]
    private GameObject _leftEngineDamage = null;

    [SerializeField]
    private GameObject _rightEngineDamage = null;
    private AudioSource _audioSource = null;

    [SerializeField]
    private AudioClip _laserSound = null;

    [SerializeField]
    private GameObject _explosionPrefab = null;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game started");
        transform.position = new Vector3(0f,-2.5f,0f);
        _enemySpawner = GameObject.Find("EnemySpawnManager")?.GetComponent<EnemySpawnManager>();
        _shield = transform.Find("Shield")?.gameObject;
        _uiManager = GameObject.Find("Canvas")?.GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_enemySpawner == null || _shield == null || _uiManager == null || _audioSource == null) {
            Debug.LogError("Important element are null, Player will be destroyed for safety!!");
            Destroy(gameObject);
        } else {
            _audioSource.clip = _laserSound;
        }

    }

    // Update is called once per frame
    void Update()
    { 
       MovePlayer();
       FireLaser();
       WeaponCoolDown();
    }

    void MovePlayer() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x ,y, 0f);

        transform.Translate(direction * Time.deltaTime * _speed *_speedModifier);

        // Mathf.Clamp() could be used!
        if (transform.position.y >= 0) {
            transform.position =  new Vector3(transform.position.x, 0f, 0f);
        } else if ( transform.position.y <= -4.8) {
            transform.position =  new Vector3(transform.position.x, -4.8f, 0f);
        }
        // -- -- --

        if (transform.position.x >= 11.3) {
            transform.position =  new Vector3(-11.3f, transform.position.y, 0f);
        } else if (transform.position.x <= -11.3) {
            transform.position =  new Vector3(11.3f, transform.position.y, 0f);
        }
    }

    private void FireLaser() {
        Vector3 laserPosition = transform.position;
        laserPosition.y += 1.15f;
        if(Input.GetButton("Fire1") && _lastFire == 0 ) {
            _lastFire = _normalLaserCoolDown;
            if (TripleLaserPowerOn) {
                GameObject newLaser = Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
            } else {
                GameObject newLaser = Instantiate(_laserPrefab, laserPosition, Quaternion.identity);
            }
            _audioSource.Play();
        }
    }

    private void WeaponCoolDown() {
        _lastFire-=Time.deltaTime;
        if (_lastFire < 0) {
            _lastFire = 0;
        }
    }

    public void TakeDamage() { //int damage) {
        if (!_shieldActive && _lives > 0) {
            _lives--;
            DamageEngine();
            _uiManager.UpdateLives(_lives);
            if (_lives < 1) {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                _enemySpawner.StopSpawning();
            }
        } else {
            // disable shield
            _shieldActive = false;
            _shield.SetActive(false);
        }
    }

    IEnumerator DeactivateTripleShot() {
        yield return new WaitForSeconds(_tripleShotActiveTime);
        TripleLaserPowerOn = false;
    }

    public void StartDeactivateTripleShot() {
        StartCoroutine(DeactivateTripleShot());
    }

    IEnumerator SpeedBoost() {
        _speedModifier = _speedBoost;
        yield return new WaitForSeconds(_speedBoostActiveTime);
        _speedModifier = 1.0f;
    }

    public void ActivateSpeedBoost() {
        StartCoroutine(SpeedBoost());
    }
    
    public void ActivateShield() {
        _shieldActive = true;
        _shield.SetActive(true);
    }

    public void AddScore(int amount) {
        _score += amount;
        _uiManager.UpdateScore(_score);
    }

    private void DamageEngine() {
        if (_leftEngineDamage.activeSelf) {
            _rightEngineDamage.SetActive(true);
            return;
        }

        if (_rightEngineDamage.activeSelf) {
            _leftEngineDamage.SetActive(true);
            return;
        }

        int side = Random.Range(0,2);

        switch (side) {
            case 0:
                _leftEngineDamage.SetActive(true);
                break;
            case 1:
                _rightEngineDamage.SetActive(true);
                break;
        }

    }
}
