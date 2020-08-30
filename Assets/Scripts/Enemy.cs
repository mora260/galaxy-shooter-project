using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;
    private Player _player = null;
    private Animator _animator = null;
    private BoxCollider2D _collider = null;
    private bool _notDestroyed = true;

    [SerializeField]
    private AudioClip _explosionSound = null;

    private AudioSource _audioSource = null;

    private void Start() {
        _player = GameObject.Find("Player")?.GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        
        if (_player == null || _animator == null || _collider == null || _audioSource == null) {
            Debug.LogError("Important Components are missing, the game object will be destroyed now for safety!");
            Destroy(gameObject);
        } else {
            _audioSource.clip = _explosionSound;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed); // transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * _speed, transform.position.z);
        if (transform.position.y <= -7f && _notDestroyed) {
            transform.position = new Vector3( Random.Range(-9.3f, 9.3f), 6.6f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Laser") {
            if (_player != null) {
                _player.AddScore(10);
            }
            Destroy(other.gameObject);
            DestroyEnemy();
        } else if ( other.tag == "Player") {
            DestroyEnemy();
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage();
            }
        }
    }

    private void DestroyEnemy() {
        _notDestroyed = false;
        _collider.enabled = false;
        _speed*=0.75f;
        _animator.SetTrigger("OnEnemyDestroyed");
        _audioSource.Play();
        Destroy(gameObject, 2.4f);
    }

}
