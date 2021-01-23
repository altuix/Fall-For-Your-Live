using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public GameObject player;
    public bool gameOver;

    float nextBarSecondLimit = 0;
    float levelCount = 0;
    bool callStopGameOnce = false;
    private Player playerScript;

    string direction;
    string tempDirection;
    bool hold = false;

    public GameObject[] bars;

    Vector2 screenBounds;
    float barSize;



    // Start is called before the first frame update
    void Start()
    {
        //screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Debug.Log(screenBounds.x);
        //screen bounds

        
        barSize = bars[0].GetComponent<SpriteRenderer>().bounds.size.x /2;
        //Debug.Log(barSize);

        playerScript = player.GetComponent<Player>();

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

    private void LateUpdate()
    {
        
    }

    void StopGame()
    {
        if (callStopGameOnce)
            return;

        playerScript.playDeathSong();
        this.gameObject.GetComponent<AudioSource>().Stop();
        playerScript.Freze();
        RestartGame();
        callStopGameOnce = true;
    }
    void levelCreator()
    {
        nextBarSecondLimit = nextBarSecondLimit + Time.deltaTime;
        if (nextBarSecondLimit > 1.5f)
        {
            float createOnX = Random.Range(-screenBounds.x + barSize, screenBounds.x - barSize);
            float createOnY = -screenBounds.y - barSize;
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
