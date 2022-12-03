using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //variables movimiento
    public float speed = 1f;
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
        if (_col.transform.tag == "Die")
        {
            //Subir Score
            _gm.updateScore(1);
            //Morir
            Destroy(gameObject);
        }
    }
}
