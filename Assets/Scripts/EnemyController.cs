using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //variables movimiento
    [SerializeField]
    float speed;
     Rigidbody2D rb;
    [SerializeField]
     private GameManager _gm;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
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
        speed = 1;
        speed+= _speed;
        if (speed>10f)
        {
            speed = 10f;
        }
    }
    public void die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.transform.tag == "Die")
        {
            //Subir Score
            _gm.updateScore();
            //Morir
            Destroy(gameObject);
        }
    }
}
