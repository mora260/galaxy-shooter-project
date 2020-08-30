using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;

    // Update is called once per frame
    void Update()
    {
        Transform leftLaser = transform.Find("LeftLaser");
        Transform rightLaser = transform.Find("RightLaser");

        if (leftLaser == null || rightLaser == null) {
            Destroy(gameObject);
        } else {
            leftLaser.Translate( new Vector3(-1,0,0) * Time.deltaTime * _speed);
            rightLaser.Translate( new Vector3(1,0,0) * Time.deltaTime * _speed);
        }

    }
}
