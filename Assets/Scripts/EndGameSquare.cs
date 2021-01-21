using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameSquare : MonoBehaviour
{
    private GameObject gm;
    private GameManager gmScript;

    string playerTag = new MainManager().PlayerTag;
    string managerObjectName = new MainManager().ManagerObjectName;
    private void Start()
    {
        gm = GameObject.Find(managerObjectName);
        gmScript = gm.GetComponent<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerTag)        {

            gmScript.gameOver = true;
        }
    }

}
