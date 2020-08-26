using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

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
}
