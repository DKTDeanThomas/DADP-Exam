using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintingInteract : MonoBehaviour, IInteractible
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

    public GameManager gm;


    public bool Interact(Interactor interact)
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerInventory>().hasmapPiece)
        {
            //gm.GetComponent<GameManager>().CreateGamePieces(0.01f);
            puzzle.SetActive(true);
           
            return true;
        }

        return false;
    }

}
