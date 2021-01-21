using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public GameObject player;
    public bool gameOver;
    float   barScore = 10;
    float redBarScore = 20;
    float nextBarSecondLimit = 0;
    float levelCount = 0;
    bool callStopGameOnce = false;


    public GameObject[] bars;
    // Start is called before the first frame update
    void Start()
    {
       
     
       
    }

    void StopGame()
    {
        if (callStopGameOnce)
            return;

        player.GetComponent<Player>().playDeathSong();
        this.gameObject.GetComponent<AudioSource>().Stop();
        player.GetComponent<Player>().Freze();
        RestartGame();
        callStopGameOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            StopGame();
            return;
        }

        levelCreator();


    }

    void levelCreator()
    {
        nextBarSecondLimit = nextBarSecondLimit + Time.deltaTime;
        if (nextBarSecondLimit > 1.5f)
        {
            float createOnX = Random.Range(-2.247f, 2.444f);
            float createOnY = -5.24f;
            levelCount++;
            nextBarSecondLimit = 0;

            if (levelCount == 1)
            {                
                Instantiate(bars[0], new Vector3(createOnX, createOnY, 0), Quaternion.identity);
            }

            if (levelCount == 2)
            {
                int randomForBarType = Random.Range(0, 2);
                Instantiate(bars[randomForBarType], new Vector3(createOnX, createOnY, 0), Quaternion.identity);
            }
            if (levelCount == 3)
            {
                int randomForBarType = Random.Range(1, 3);
                Instantiate(bars[randomForBarType], new Vector3(createOnX, createOnY, 0), Quaternion.identity);
            }
            if (levelCount == 4)
            {
                int randomForBarType = Random.Range(3, 5);
                Instantiate(bars[randomForBarType], new Vector3(createOnX, createOnY, 0), Quaternion.identity);
                levelCount = 0;
            }


        }
    }

    void RestartGame()
    {

        Invoke("loadFirstLevel", 2f);
    }

    void loadFirstLevel()
    {
        SceneManager.LoadScene("RestartMenu");

    }
}
