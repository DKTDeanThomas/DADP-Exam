using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

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

    public GameObject mainCam;
    public GameObject examCam;

    public GameObject tutScreen;
    public Transform newPos;
    public Transform prevPos;

    public GameObject playerCam;
    public GameObject FirstPersonCam;

    public bool isSolving = false;
    public GameManager gm;

    public GameObject particlePrefab;
    [SerializeField] private GameObject instanPoint;

    private bool puzzleDone = false;

    public CommsManager cM;
    public string popupinfo;

   
    public bool Interact(Interactor interact)
    {
   
        if (GameObject.FindWithTag("Player").GetComponent<PlayerInventory>().hasmapPiece)
        {
            Debug.Log("piece");
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            CameraMove(1);

            // make the minimap appear if completed, and disable puzzle view

            GameObject.FindWithTag("Player").GetComponent<Interactor>().EnableRaycast();

            return true;
        }

        else
        {
            GetComponent<NPCInteractable>().StartDialogue();
            GameObject.FindWithTag("Player").GetComponent<Interactor>().EnableRaycast();

        }

        tutScreen.SetActive(false);
        return false;
    }


    public void CameraMove(int move)
    {
        //moves to
        if (move == 1)
        {
            isSolving = true;   
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            FirstPersonCam.SetActive(false);
            playerCam.transform.position = newPos.position;
            playerCam.transform.rotation = newPos.rotation;
            tutScreen.SetActive(true);
        }

        //moves back
        if (move == 2)
        {
            isSolving = false;
            playerCam.transform.position = newPos.position;
            playerCam.transform.rotation = newPos.rotation;
            FirstPersonCam.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
            tutScreen.SetActive(false);
            gameObject.GetComponent<MeshRenderer>().enabled = true;

            if (puzzleDone)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
            else
            gameObject.GetComponent<BoxCollider>().enabled = true;

            GameObject.FindWithTag("Player").GetComponent<Interactor>().minicrosshairUI.SetActive(true);
        }
    }

    public void Update()
    {
        if (isSolving)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("clicked");
                CameraMove(2);
            }

            if (!gm.shuffling && gm.CheckCompletion())
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerInventory>().hasMap = true;
                GameObject.FindWithTag("Player").GetComponent<Interactor>().minimapUI.SetActive(true);


                gm.GetComponent<GameManager>().enabled = false;
                Instantiate(particlePrefab, instanPoint.transform.position, instanPoint.transform.rotation);

                cM.changepopup(popupinfo);
                StartCoroutine(waitabit());
               

            }
        }

    }

    IEnumerator waitabit()
    {

        yield return new WaitForSeconds(3f);
        puzzleDone = true;
       
        CameraMove(2);

    }

}
