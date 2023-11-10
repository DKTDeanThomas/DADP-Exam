using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractHA2 : MonoBehaviour, IInteractible
{
    [SerializeField] private bool inspect;
    [SerializeField] private bool examine;
    [SerializeField] private GameObject examineCamera;
    [SerializeField] private GameObject puzzle;
    [SerializeField] private GameObject icon;
    [SerializeField] private Renderer iconMat;
    [SerializeField] private Material newIcon;
    public bool Inspect { get { return inspect; } }
    public bool Examine { get { return examine; } }
    public GameObject ExamineCam { get { return examineCamera; } }
    public GameObject Puzzle { get { return puzzle; } }
    public GameObject Icon { get { return icon; } }


    public bool Interact(Interactor interact)
    {
        iconMat = icon.GetComponent<Renderer>();

        if (GameObject.FindWithTag("Player").GetComponent<PlayerInventory>().hascomputerkeyHA == true)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("isOpen", true);

            BoxCollider collider = GetComponent<BoxCollider>();
            collider.enabled = false;

            iconMat.material = newIcon;

            GameObject.FindWithTag("Player").GetComponent<Interactor>().EnableRaycast();
            GameObject.FindWithTag("Player").GetComponent<Interactor>().minimapUI.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<Interactor>().minicrosshairUI.SetActive(true);

            return true;


        }

        GameObject.FindWithTag("Player").GetComponent<Interactor>().EnableRaycast();
        GameObject.FindWithTag("Player").GetComponent<Interactor>().minimapUI.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<Interactor>().minicrosshairUI.SetActive(true);

        return false;

    }
}
