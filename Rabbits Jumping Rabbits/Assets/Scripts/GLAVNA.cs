using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject DeathScreen;
    public int numberOfPlayers;
    void Start()
    {
        MainMenu.numberOfPlayers = numberOfPlayers;
        MainMenu.lastDied = -1;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        print(MainMenu.numberOfPlayers);
        if (MainMenu.numberOfPlayers == 1)
        {
            DeathScreen.SetActive(true);
        }
    }

}
