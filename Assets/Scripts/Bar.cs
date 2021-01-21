using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bar : MonoBehaviour
{
    float barVelocity = 2f;

    public bool regularBar;
    public bool jumpBar;
    public bool moveLeftBar;
    public bool moveRightBar;
    public bool breakableBar;
    GameObject gm;
    GameManager gmScript;
    MainManager mainManager = new MainManager();

    private void Start()
    {
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {


        if (!gmScript.gameOver)
        {



            transform.position = transform.position + new Vector3(0f, barVelocity * Time.deltaTime, 0);

            if (transform.position.y > 5.2f)
            {
                DestroyBar();
            }

        }
    }

    private void DestroyBar()
    {
        Destroy(this.gameObject);

    }

    //top side ufo
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    //for break and jump bar
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == mainManager.PlayerTag)
        {
            if (breakableBar)
            {
                Invoke("DestroyBar", 2f);
            }

            if (jumpBar)
            {
                collision.gameObject.GetComponent<Player>().JumpPlatform(8f);
                this.gameObject.GetComponent<AudioSource>().Play(0);
            }

        }
    }

    //for move bar
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (moveLeftBar)
        {
            collision.gameObject.GetComponent<Player>().MovePlatform(-4f);
        }

        if (moveRightBar)
        {
            collision.gameObject.GetComponent<Player>().MovePlatform(4f);
        }
    }


}
