using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public PlayerMovement playerScript;
    public GameObject player;



    public void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
    }

    public  void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.CompareTag("SceneTrrigger"))
        {
            SceneManager.LoadScene("Act IP Address Puzzle");
            Debug.Log(" your're now in the scene where  yiu have to solve this puzzle");
        }
    }
}
