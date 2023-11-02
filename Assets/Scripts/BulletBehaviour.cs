using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletXSpeed;
    private float _wayOfBullet;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _myRigidbody;
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _wayOfBullet = _playerMovement.transform.localScale.x * bulletXSpeed;
    }
    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        _myRigidbody.velocity = new Vector2(_wayOfBullet, 0);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
