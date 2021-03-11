using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject mainGame;
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            startMenu.SetActive(false);
            mainGame.SetActive(true);
        }

    }
}
