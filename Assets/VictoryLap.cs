using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryLap : MonoBehaviour
{
    public GameObject VictoryGameobject;
    public PlayerMovement playerScript;
    public GameObject winPanel;
    public Button QuitButton;

    public void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
    }



    public void OnTriggerEnter(Collider playerrr)
    {
        if(playerrr.gameObject.CompareTag("Win"))
        {
            winPanel.gameObject.SetActive(true);
            QuitButton.gameObject.SetActive(true);
        }
    }
}
