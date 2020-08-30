using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    [SerializeField]
    private float _powerUpID = 0;

    [SerializeField]
    private AudioClip _powerUpSound = null;

    private void Update() {
         transform.Translate(Vector3.down * Time.deltaTime * _speed); // transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * _speed, transform.position.z);
        if (transform.position.y <= -6.5f) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player p = other.transform.GetComponent<Player>();
            if (p != null) {
                switch (_powerUpID)
                {
                    case 0:
                        p.TripleLaserPowerOn = true;
                        p.StartDeactivateTripleShot();
                        break;
                    case 1:
                        p.ActivateSpeedBoost();
                        break;
                    case 2:
                        p.ActivateShield();
                        break;
                }
                AudioSource.PlayClipAtPoint(_powerUpSound, new Vector3(0,0,-10), 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
