using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GenericProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float speed = 0;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Setup(Vector2 velocity)
    {
        myRigidbody.velocity = velocity.normalized * speed;
       // transform.rotation = Quaternion.Euler(direction);
    }

}
