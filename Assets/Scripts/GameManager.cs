using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //variables sistema de spawn
    
    Vector2 upSpawn,downSpawn,rightSpawn,leftSpawn;
    public Vector2 currentSpawn;
    bool isGenerating;
    bool isCreating;
    public float contador = 30f;
    public float timeGenerating = 10;
    [SerializeField]
    private float rng;
    //variables 
    public GameObject enemy;
    float enemySpeedIncrement = 0;
    public Quaternion rotation;
    //scores
    int score = 0;
    public Text scoreText;
    void Start()
    {
        isCreating= true;
        rotation = Quaternion.Euler(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeGenerating<=0.5f)
        {
            timeGenerating = 0.5f;
            isCreating= false;
        }
        contadorSpawner();
        if (!isGenerating)
        {
            StartCoroutine(instanceEnemy());
        }
    }
   
    void contadorSpawner()
    {
        if (isCreating)
        {
            if (contador > 0)
            {
                contador -= Time.deltaTime;
            }
            else
            {
                timeGenerating-=0.2f;
                contador = 30f;
            }
        }
    }
    void randomSpawns()
    {
        rng = UnityEngine.Random.Range(1, 5);
        switch (rng)
        {
            case 1:
                upSpawn = new Vector2(UnityEngine.Random.Range(-2.82f, 2.799f), 6f);
                currentSpawn = upSpawn;
                
                rotation = Quaternion.Euler(0,0,180);
                break;
            case 2:
                downSpawn = new Vector2(UnityEngine.Random.Range(-2.82f, 2.799f), -6);
                currentSpawn = downSpawn;
                rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 3:
                rightSpawn = new Vector2(3f, UnityEngine.Random.Range(4.98f, -5f));
                currentSpawn = rightSpawn;
                rotation = Quaternion.Euler(0, 0,90);
                break;
            case 4:
                leftSpawn = new Vector2(-3f, UnityEngine.Random.Range(4.98f, -5));
                currentSpawn = leftSpawn;
                rotation = Quaternion.Euler(0, 0,-90);
                break;
            default:
                print("Menudo error");
                break;
        }
        print(currentSpawn);
    }
    
    IEnumerator instanceEnemy()
    {
        isGenerating = true;
        randomSpawns();
        Instantiate(enemy, currentSpawn, rotation);
        enemySpeedIncrement += 0.1f;
        if (enemySpeedIncrement > 10f)
        {
            enemySpeedIncrement = 10f;
        }
        enemy.GetComponent<EnemyController>().incrementSpeed(enemySpeedIncrement);
        yield return new WaitForSeconds(timeGenerating);
        isGenerating= false;
        yield return new WaitForSeconds(1f);
    }
    public void updateScore()
    {
       
        score++;
        
        scoreText.text = "" + score.ToString();
        print (score);
       
    }
}
