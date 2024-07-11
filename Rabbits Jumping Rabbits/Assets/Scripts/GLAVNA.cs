using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glavna : MonoBehaviour
{

    public GameObject DeathScreen;
    public int numberOfPlayers;
    public Transform UFOstart;
    public Transform UFOend;
    public Transform UFOstop;
    void Start()
    {
        MainMenu.numberOfPlayers = numberOfPlayers;
        MainMenu.lastDied = -1;
        MainMenu.UFOstart = UFOstart;
        MainMenu.UFOend = UFOend;
        MainMenu.UFOstop = UFOstop;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (MainMenu.numberOfPlayers == 1)
        {
            DeathScreen.SetActive(true);
        }
    }

}
