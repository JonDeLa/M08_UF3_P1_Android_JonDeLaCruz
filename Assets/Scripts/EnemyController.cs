using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //variables movimiento
    public float speed = 1f;
    public Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    
    
    void Update()
    {
        Move();
    }
    void Move()
    {
        rb.AddForce(transform.up * speed * Time.deltaTime);
    }

    public void incrementSpeed(float _speed)
    {
        speed+= _speed;
        if (speed>5f)
        {
            speed = 5f;
        }
    }
    public void die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.transform.tag == "Limit")
        {
            //Subir Score
            //Morir
            Destroy(gameObject);
        }
    }
}
