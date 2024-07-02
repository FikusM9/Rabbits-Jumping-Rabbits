using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject DeathScreen;
    void Start()
    {
        DeathScreen.SetActive(false);
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
