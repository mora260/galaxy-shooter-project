using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed); // transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * _speed, transform.position.z);
        if (transform.position.y <= -6.6f) {
            transform.position = new Vector3( Random.Range(-9.3f, 9.3f), 6.6f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Laser") {
            //Increase score
            Destroy(other.gameObject);
            Destroy(gameObject);
        } else if ( other.tag == "Player") {
            Destroy(gameObject);
            other.GetComponent<Player>().TakeDamage();
        }
    }
}
