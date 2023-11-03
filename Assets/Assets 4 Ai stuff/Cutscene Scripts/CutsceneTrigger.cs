using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public CutsceneManager cutsceneManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cutsceneManager.PlayCutscene();
            gameObject.SetActive(false);
        }
    }
}
