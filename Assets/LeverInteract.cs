using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteract : MonoBehaviour, IInteractible
{

    [SerializeField] private bool inspect;
    [SerializeField] private bool examine;
    [SerializeField] private GameObject examineCamera;
    [SerializeField] private GameObject puzzle;
    [SerializeField] private GameObject icon;

    public LeverPuzzle lP;

    public int puzzleVal;
    public Animator animator;

    public bool Inspect { get { return inspect; } }
    public bool Examine { get { return examine; } }
    public GameObject ExamineCam { get { return examineCamera; } }
    public GameObject Puzzle { get { return puzzle; } }

    public GameObject Icon { get { return icon; } }

    public GameObject mainCam;
    public GameObject examCam;

    public CommsManager cM;
    public string popupinfo;

    public bool Interact(Interactor interact)
    {

        lP.AddLever(puzzleVal);
        animator.SetBool("isActive", true);
        GameObject.FindWithTag("Player").GetComponent<Interactor>().EnableRaycast();
        GameObject.FindWithTag("Player").GetComponent<Interactor>().minicrosshairUI.SetActive(true);



        return true;
    }
}
