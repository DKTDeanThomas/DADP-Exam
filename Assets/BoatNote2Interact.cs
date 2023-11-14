using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatNote2Interact : MonoBehaviour, IInteractible
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

    private void Start()
    {
        cM = GameObject.FindWithTag("DialogueManager").GetComponent<CommsManager>();
    }

    public bool Interact(Interactor interact)
    {
        cM.InteractComment(text);
        return true;
    }
}
