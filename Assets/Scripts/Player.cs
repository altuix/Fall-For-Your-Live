using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceValue;
    private float min_X, max_X;
    private AudioSource source;

    Vector2 screenBounds;
    float playerSizeX;
    GameObject gm;
    GameManager gmScript;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();

        //screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Debug.Log(screenBounds.x);
        //screen bounds


        playerSizeX = this.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Debug.Log(playerSizeX);

        max_X = screenBounds.x - playerSizeX;
        min_X = -screenBounds.x + playerSizeX;

        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") < 0f)
        {
            rb.velocity = new Vector2(0, 0);

        }


        if (Input.GetAxisRaw("Horizontal") > 0f)//right
        {
            //transform.position = transform.position + new Vector3(-forceValue * Time.deltaTime,
            //     0,
            //     0);
            //rb.AddForce(Vector2.left * forceValue * Time.deltaTime, ForceMode2D.Force);
            MoveRight();

        }

        if (/*Input.GetKey(KeyCode.RightArrow) ||*/ Input.GetAxisRaw("Horizontal") < 0f) //left
        {
            //rb.AddForce(Vector2.right * forceValue * Time.deltaTime, ForceMode2D.Force);
            //transform.position = transform.position + new Vector3(forceValue * Time.deltaTime,
            //    0, 
            //    0);
            //rb.AddForce(new Vector2(transform.position.x * forceValue * Time.deltaTime, transform.position.y));
            MoveLeft();
        }



        TouchControl();





    }

    private void LateUpdate()
    {
        //game wall
        Vector2 temp = transform.position;
        if (temp.x >= max_X)
        {
            temp.x = max_X;
            //rb.velocity = new Vector2(0, rb.velocity.y);

        }


        if (temp.x <= min_X)
        {
            temp.x = min_X;
            //rb.velocity = new Vector2(0, rb.velocity.y);

        }

        transform.position = temp;
        //game wall
    }

    private void TouchControl()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            //right
            if(touchPos.x > 0)
            {
                MoveRight();
            }

            //right
            if (touchPos.x < 0)
            {
                MoveLeft();
            }
        }

    }


    public void MoveLeft()
    {
        if (!gmScript.gameOver)
            rb.velocity = new Vector2(-forceValue * Time.deltaTime, rb.velocity.y);
    }

    public void MoveRight()
    {
        if (!gmScript.gameOver)
            rb.velocity = new Vector2(forceValue * Time.deltaTime, rb.velocity.y);
    }

    public void MovePlatform(float x)
    {
        rb.velocity = rb.velocity + new Vector2(x * Time.deltaTime, 0);
    }

    public void JumpPlatform(float y)
    {
        rb.velocity = rb.velocity + new Vector2(rb.velocity.x, y);
    }

    public void playDeathSong()
    {
        Debug.Log("death sound");
        source.Play();


    }

    public void Freze()
    {
        playDeathSong();
        rb.velocity = new Vector2(0, 0);
        rb.freezeRotation = true;
        rb.gravityScale = 0;
        rb.mass = 0;
    }


}
