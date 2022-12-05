using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variables sistema de spawn

    Vector2 upSpawn, downSpawn, rightSpawn, leftSpawn, spawn;
    public Vector2 currentSpawn;
    bool isGenerating;
    bool isCreating;
    public float contador = 30f;
    public float timeGenerating = 10;
    [SerializeField]
    private float rng,rngPickUp;
    //variables 
    public GameObject enemy;
    float enemySpeedIncrement = 0;
    public Quaternion rotation;
    //scores
    int score = 0;
    public Text scoreText, gameOverScoreText;
    //Referencia a Menus
    public GameObject gameOver, pauseMenu, mainMenu;
    bool gameStop = false;
    //Referencias pickUps
    public GameObject pickUp1, pickUp2, pickUp3;
    private bool pickupGenerating;

    void Start()
    {
        isCreating = true;
        rotation = Quaternion.Euler(0, 0, 0);
        mainMenu.SetActive(true);
        gameOver.SetActive(false);
        pauseMenu.SetActive(false);
        gameStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStop)
        {

            if (timeGenerating <= 0.5f)
            {
                timeGenerating = 0.5f;
                isCreating = false;
            }
            contadorSpawner();
            if (!isGenerating)
            {
                StartCoroutine(instanceEnemy());
            }
            if (!pickupGenerating)
            {
                StartCoroutine(instancePickUp());
            }
        }
       

    }
    public void menuGameOver()
    {
        mainMenu.SetActive(false);
        gameOver.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverScoreText.text = "Has consegido un total de " + score + "puntos";

        gameStop = true;

    }
    public void menuPause()
    {
        mainMenu.SetActive(false);
        gameOver.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

    }
    public void reaudar()
    {
        mainMenu.SetActive(true);
        gameOver.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void goMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void reset()
    {
        SceneManager.LoadScene(1);
    }
    public void exit()
    {
        Application.Quit();
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
                timeGenerating -= 0.2f;
                contador = 30f;
            }
        }
    }
    void randomPickUps()
    {
        
        rngPickUp = UnityEngine.Random.Range(1, 4);
       
        spawn = new Vector2(UnityEngine.Random.Range(-2.5f,2.5f), UnityEngine.Random.Range(3,-4));
        
        switch (rngPickUp)
        {
            case 1:
                Instantiate(pickUp1,spawn,quaternion.identity);               
                break;
            case 2:
                Instantiate(pickUp2, spawn, quaternion.identity);
                break;
            case 3:
                Instantiate(pickUp3, spawn, quaternion.identity);
                break;
        
            default:
                print("Menudo error");
                break;
        }
       
    }
    IEnumerator instancePickUp()
    {
        pickupGenerating = true;
        randomPickUps();
        yield return new WaitForSeconds(timeGenerating);
        pickupGenerating = false;
        yield return new WaitForSeconds(1f);
    }
    void randomSpawns()
    {
        rng = UnityEngine.Random.Range(1, 5);
        switch (rng)
        {
            case 1:
                upSpawn = new Vector2(UnityEngine.Random.Range(-2.82f, 2.799f), 6f);
                currentSpawn = upSpawn;

                rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 2:
                downSpawn = new Vector2(UnityEngine.Random.Range(-2.82f, 2.799f), -6);
                currentSpawn = downSpawn;
                rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 3:
                rightSpawn = new Vector2(3f, UnityEngine.Random.Range(4.98f, -5f));
                currentSpawn = rightSpawn;
                rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 4:
                leftSpawn = new Vector2(-3f, UnityEngine.Random.Range(4.98f, -5));
                currentSpawn = leftSpawn;
                rotation = Quaternion.Euler(0, 0, -90);
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
        isGenerating = false;
        yield return new WaitForSeconds(1f);
    }
    public void updateScore()
    {

        score++;

        scoreText.text = "" + score.ToString();
        print(score);

    }
}
