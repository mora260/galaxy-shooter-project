using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed); // transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * _speed, transform.position.z);
        if (transform.position.y >= 6.0f) {
            Destroy(gameObject);
        }
    }
}
