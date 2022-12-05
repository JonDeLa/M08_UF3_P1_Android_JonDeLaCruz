using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //variables Movimiento
    public Rigidbody2D rb;
    public float mSpeed = 5f;
    public float rSpeed = 3f;
    //todo lo que necesitamos para movernos
    Touch touch;
    Vector3 tPosition, targetPosition;
    bool isMoving = false;
    float preDistance, currentDistance;
    //variables playerStat
    public int lifeCount = 3;
    public int scorePoints = 0;
    //variables mecanicas
    bool slow = false;
    //Referencias
    public GameObject _gm;
    public Text healthText;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _gm = GameObject.Find("GameManager");
       
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        MoveByTouch();
        faceTouch();
    }
    //funcion para el movimiento
    //void Movement()
    //{
    //    Vector2 movement;
    //    movement.x = Input.GetAxis("Horizontal");
    //    movement.y = Input.GetAxis("Vertical");
    //    //Lo aplicamos a el rigitbody
    //    rb.MovePosition(rb.position + movement * mSpeed * Time.fixedDeltaTime);
    //}
    void MoveByTouch()
    {
        //primero calculamos si nos estamos movendo
        if (isMoving)
        {
            //magnitude es por asi decirlo la distancia etre el vector de origen con la del destino
            currentDistance = (tPosition - transform.position).magnitude;
        }
        //Ahora programamos el touch
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            //aqui le preguntaremos si hemos tocado la pantalla, haga el movimiento.
            if (touch.phase == TouchPhase.Began)
            {
                //reniciamos los valores
                preDistance = 0;
                currentDistance = 0;
                //cambiamos la bool para saber que nos estamos moviendo
                isMoving = true;
                //a continuación vamos a calcular donde hemos clicado para luego saber a donde movernos
                tPosition = Camera.main.ScreenToWorldPoint(touch.position);
                tPosition.z = 0;
                //Ahora hacemos el movimiento
                targetPosition = (tPosition - transform.position).normalized;
                rb.velocity = new Vector2(targetPosition.x * mSpeed, targetPosition.y * mSpeed);
                
            }
        }
            //ahora le decimos que se pare 
            if(currentDistance > preDistance)
            {
                isMoving = false;
                rb.velocity = Vector2.zero;
            }
            //por ultimo vamos a calcular la distancia previa
            if (isMoving)
            {
                preDistance = (tPosition - transform.position).magnitude;
            }
    }
    void faceTouch()
    {
        //Creamos el vector con el que le daremos la direccion y le diremos que mire hacia donde hemos tocado
        Vector3 touchPosition;
        touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //aplicamos la direccion
        Vector2 dir = new Vector2(
            touchPosition.x - transform.position.x,
            touchPosition.y - transform.position.y);
        transform.up = dir;
    }
    public void getDMG()
    {
        lifeCount--;
        print("Vida" + lifeCount);
        healthText.text = "Health " + lifeCount;
        if (lifeCount <= 0)
        {
            die();
        }
    }
    public void getHP()
    {
        if(lifeCount <3)
        lifeCount++;
        healthText.text = "Health " + lifeCount;
    }
    public void speedUP()
    {
        StartCoroutine(SPEED());
        
    }
    IEnumerator SPEED()
    {
        mSpeed = 8f;
        yield return new WaitForSeconds(4);
        mSpeed = 5f;
        yield return new WaitForSeconds(1);
    }
    void die()
    {
        //aqui desactivamos todo y morimos+
        _gm.GetComponent<GameManager>().menuGameOver();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.transform.tag == "Speed")
        {
            speedUP();
            Destroy(_col.gameObject);
        }
        if (_col.transform.tag == "Hp")
        {
            getHP();
            Destroy(_col.gameObject);
        }
        if (_col.transform.tag == "Slow")
        {
            _gm.GetComponent<SlowTime>().slowMotion();
            Destroy(_col.gameObject);
        }
        if (_col.transform.tag == "Enemy")
        {
            getDMG();
            _col.transform.GetComponent<EnemyController>().die();
        }

    }
}
 