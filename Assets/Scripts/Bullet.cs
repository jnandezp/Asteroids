using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField]
   private float speed = 200.0f;
   
   [SerializeField]
   private float maxLifeTime = 10.0f;
   
   private Rigidbody2D _rigidbody;

   private void Awake()
   {
      _rigidbody = GetComponent<Rigidbody2D>();
   }

   public void Project(Vector2 direction)
   {
      _rigidbody.AddForce(direction * this.speed);
      
      Destroy(this.gameObject, this.maxLifeTime);
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      Destroy(this.gameObject);
   }
   
   
}
