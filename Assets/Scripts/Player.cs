using System;
using UnityEngine;
// POLYMORPHISM

public class Player : MonoBehaviour, IDamageable // INHERITANCE, POLYMORPHISM
{
    [SerializeField]
    private Bullet bulletPrefab;
    
    [SerializeField]
    private float thrustSpeed = 1.0f;
    
    [SerializeField]
    private float turnSpeed = 1.0f;
    
    private bool _thrusting;
    
    private float _turnDirection;
    
    private Rigidbody2D _rigidbody;
    
    public event System.Action OnPlayerDied;
    
    public void Die() // POLYMORPHISM
    {
        OnPlayerDied?.Invoke();
    }

    // POLYMORPHISM
    public void TakeDamage(float amount)
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.angularVelocity = 0.0f;
        this.gameObject.SetActive(false);
        Die();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
    }
    
    private void HandleInput()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        } else
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * thrustSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    public void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }
    
    // ABSTRACTION
    public void Respawn(Vector3 position) 
    {
        transform.position = position;
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.angularVelocity = 0.0f;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            TakeDamage(1.0f); // POLYMORPHISM
        }
    }

}
 