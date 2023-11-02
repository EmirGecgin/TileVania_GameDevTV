using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneyController : MonoBehaviour
{
    private Rigidbody2D _myDoobyRb;

    [SerializeField] private float enemySpeed=1f;
    void Start()
    {
        _myDoobyRb = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        _myDoobyRb.velocity = new Vector2(enemySpeed, 0);
    }

    private void FlipDooby()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_myDoobyRb.velocity.x)), 1f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        FlipDooby();
        enemySpeed = -enemySpeed;
    }
}
