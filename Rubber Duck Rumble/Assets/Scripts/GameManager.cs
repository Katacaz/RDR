﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentPlayer = 1;
    public bool soloTesting = true;
    public GameObject[] players;

    CameraController cam;

    public KeyCode playerSwapKey;

    public Transform respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }


    public void Player1LeftArea()
    {
        Debug.Log("Player 1 has left the play area.");
        players[0].transform.position = respawnPosition.position;
        players[0].GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
    public void Player2LeftArea()
    {
        Debug.Log("Player 2 has left the play area");
        players[1].transform.position = respawnPosition.position;
        players[1].GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        if (soloTesting)
        {
            if (Input.GetKeyDown(playerSwapKey))
            {
                NextPlayer();
            }
        }
    }

    void NextPlayer()
    {
        if (currentPlayer < players.Length)
        {
            currentPlayer++;
            
        } else
        {
            currentPlayer = 1;
        }
        cam.ChangeTarget(players[currentPlayer - 1]);
    }
}
