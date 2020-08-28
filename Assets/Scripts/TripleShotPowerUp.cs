using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

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
                p.TripleLaserPowerOn = true;
                p.StartDeactivateTripleShot();
                Destroy(gameObject);
            }
        }
    }
}
