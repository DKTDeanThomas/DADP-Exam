using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("General")]
    public bool hasMap;

    [Header("HomeAffairs")]
    public bool hasmapPiece;
    public bool hasstoragekeyHA; // for storage room
    public bool hascomputerkeyHA; // for computer room

    [Header("Ship")]
    public bool hasshipKey1;
    public bool hasshipKey2;
    public bool hasLamp;


    [Header("Other")]
    public List<GameObject> interactParticlesList = new List<GameObject>();
    public GameObject Lamp;

    private void Update()
    {
        if (hasLamp)
        {
            Lamp.SetActive(true);
            EnableInteractParticles();

            hasLamp = false;
        }
    }

   

    private void Start()
    {
        PopulateInteractParticlesList();
        DisableInteractParticlesList();
    }


    void PopulateInteractParticlesList()
    {
        GameObject[] interactParticlesArray = GameObject.FindGameObjectsWithTag("InteractParticle");
        interactParticlesList.AddRange(interactParticlesArray);
    }

    void EnableInteractParticles()
    {
        foreach (GameObject interactParticle in interactParticlesList)
        {
            interactParticle.SetActive(true);
        }
    }

    void DisableInteractParticlesList()
    {
        foreach (GameObject interactParticle in interactParticlesList)
        {
            interactParticle.SetActive(false);
        }
    }

}
