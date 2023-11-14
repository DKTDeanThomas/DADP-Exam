using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDoorLowInteract : MonoBehaviour, IInteractible
{
    [SerializeField] private bool inspect;
    [SerializeField] private bool examine;
    [SerializeField] private GameObject examineCamera;
    [SerializeField] private GameObject puzzle;
    [SerializeField] private GameObject icon;
    public bool Inspect { get { return inspect; } }
    public bool Examine { get { return examine; } }

    public GameObject ExamineCam { get { return examineCamera; } }
    public GameObject Puzzle { get { return puzzle; } }

    public GameObject Icon { get { return icon; } }

    public CommsManager cM;

    public string text;

    public Animator animator;

    private void Start()
    {
        cM = GameObject.FindWithTag("DialogueManager").GetComponent<CommsManager>();
    }

    public bool Interact(Interactor interact)
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerInventory>().hasshipKey1 == true && GameObject.FindWithTag("Player").GetComponent<PlayerInventory>().hasshipKey2 == true)
        {
            animator.SetBool("isOpen", true);

            MeshCollider collider = GetComponent<MeshCollider>();
            collider.enabled = false;

            GameObject.FindWithTag("Player").GetComponent<Interactor>().EnableRaycast();
            GameObject.FindWithTag("Player").GetComponent<Interactor>().minicrosshairUI.SetActive(true);

            return true;


        }


        else
        {
            GetComponent<NPCInteractable>().StartDialogue();

        }


        GameObject.FindWithTag("Player").GetComponent<Interactor>().EnableRaycast();
        GameObject.FindWithTag("Player").GetComponent<Interactor>().minicrosshairUI.SetActive(true);

        return false;

    }
}
