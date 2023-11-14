using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipSceneTransistion : MonoBehaviour
{
    public int sceneToLoad; // The name of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "Player" (you can customize this)
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
