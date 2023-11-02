using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{
    Vector2 _moveInputs;
    Rigidbody2D _rb;
    [SerializeField] private float runSpeed,climbingSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Vector2 deathKick = new Vector2(20, 20);
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    Animator _myAnimator;
    CapsuleCollider2D _bodyCollider;
    BoxCollider2D _feetCollider;
    float _gravityScaleAtStart; 
    bool _isAlive=true;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _feetCollider = GetComponent<BoxCollider2D>();
        _gravityScaleAtStart = _rb.gravityScale;
    }
    void Update()
    {
        if(!_isAlive){return;}
       Run();
       FlipSprite();
       Climbing();
       Die();
    }
    void Run()
    {
        Vector2 runInputs = new Vector2(_moveInputs.x*runSpeed, _rb.velocity.y);
        _rb.velocity = runInputs;
        bool hasHorizontalInput = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;
        _myAnimator.SetBool("isRunning",hasHorizontalInput);
    }
    void FlipSprite()
    {
        bool hasHorizontalInput = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;
        if (hasHorizontalInput)//gittiği yöne doğru sprite ın kalmasını sağlarız.
        {
            transform.localScale = new Vector2(Mathf.Sign(_rb.velocity.x), 1f);//mathf.sign 0 ı da pozitif algılar.
        }
    }

    void OnFire(InputValue value)
    {
        if(!_isAlive){return;}
        Instantiate(bullet,gun.position,transform.rotation);
    }
    void OnMove(InputValue value)
    {
        if(!_isAlive){return;}
        _moveInputs = value.Get<Vector2>();
        Debug.Log(_moveInputs);
    }

    void OnJump(InputValue value)
    {
        if(!_isAlive){return;}
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            _rb.velocity += new Vector2(0, jumpSpeed);
        }
    }

    void Climbing()
    {
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _rb.gravityScale = _gravityScaleAtStart;
            _myAnimator.SetBool("isClimbing",false);
            return;
        }
        Vector2 climbingInputs = new Vector2(_rb.velocity.x, _moveInputs.y * climbingSpeed);
        _rb.velocity = climbingInputs;
        _rb.gravityScale = 0f;
        bool hasVerticalInput = Math.Abs(_rb.velocity.y) > Mathf.Epsilon;
        _myAnimator.SetBool("isClimbing",hasVerticalInput);
    }

    void Die()
    {
        if (_bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            CameraShaker.Instance.ShakeOnce(12f, 6f, .5f, 1.5f);
            _isAlive = false;
            _myAnimator.SetTrigger("Death");
            _rb.velocity = deathKick;
            StartCoroutine("DestroyCollider");
            FindObjectOfType<GameSessionController>().ProcessOfPlayerDeath();
        }
    }

    IEnumerator DestroyCollider()
    {
        yield return new WaitForSeconds(1f);
        _bodyCollider.gameObject.SetActive(false);
    }
}
