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

    private float _lastFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game started");
        transform.position = new Vector3(0f,0f,0f);
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

        transform.Translate(direction * Time.deltaTime * _speed);

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
        laserPosition.y += 0.5f;
        if(Input.GetButton("Fire1") && _lastFire == 0 ) {
            _lastFire = _normalLaserCoolDown;
            GameObject newLaser = Instantiate(_laserPrefab, laserPosition, Quaternion.identity);
        }
    }

    private void WeaponCoolDown() {
        _lastFire-=Time.deltaTime;
        if (_lastFire < 0) {
            _lastFire = 0;
        }
    }
}
