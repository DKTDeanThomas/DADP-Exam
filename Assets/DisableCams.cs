using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCams : MonoBehaviour
{
    public string objectNameToFind = "CM vcam4"; // Replace with the name you're looking for


    private void OnTriggerEnter(Collider other)
    {
        Disablecams();
    }

    public void Disablecams()
    {
        // Find all objects in the hierarchy with the specified name
        GameObject[] objectsToDisable = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> objectsList = new List<GameObject>();

        foreach (GameObject obj in objectsToDisable)
        {
            if (obj.name == objectNameToFind)
            {
                objectsList.Add(obj);
            }
        }

        // Disable all objects in the list
        foreach (GameObject obj in objectsList)
        {
            obj.SetActive(false);
        }
    }
}
