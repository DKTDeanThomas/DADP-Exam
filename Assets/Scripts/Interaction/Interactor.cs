using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask interactLayer;

    private IInteractible teractible;

    private Transform teractibleTransform;
    private Collider teractibleCollider;
    [SerializeField] private int teractiblesFound;
    [SerializeField] private GameObject teractibleCamera;

    public ObjectInspect OI;

    [SerializeField] public GameObject interactUI;
    [SerializeField] public GameObject talkUI;
    public GameObject minimapUI;
    public GameObject minicrosshairUI;


    [SerializeField] private bool canRaycast = true;


    private void Update()
    {
        if (canRaycast)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity))
            {
                if ((interactLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    teractibleTransform = hit.transform;
                    
                    teractibleCollider = hit.collider;
                    teractible = hit.collider.GetComponent<IInteractible>();

                    teractibleCamera = teractible.ExamineCam;

                    if (teractible != null)
                    {
                        OutlineOn();
                        EnableInteractUI();
                        TryInteract();
                    }


                }
                else
                {
                    OutlineOff();
                    DisableInteractUI();
                }


            }
            else
            {
                OutlineOff();
                DisableInteractUI();
            }
        }
    }

    private void TryInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            DisableInteractUI();
            OutlineOff();

            minimapUI.SetActive(false);
            minicrosshairUI.SetActive(false);

            // If you want to pick up the item
            if (teractible.Inspect && !teractible.Examine)
            {
                Inspect(teractibleTransform);
                teractible.Icon.SetActive(false);
            }

            // If you want to zoom in
            else if (!teractible.Inspect && teractible.Examine)
            {
                Examine(teractibleCamera);
            }

           
            canRaycast = false;
            teractible.Interact(this);
           


        }

        if (GetComponent<PlayerInventory>().hasMap)
        {
            minimapUI.SetActive(true);
        }

    }

    public void OutlineOn()
    {
        
        teractibleCollider.GetComponent<Outline>().enabled = true;
    }

    public void OutlineOff()
    {
        if (teractibleCollider != null)
        {
            
            teractibleCollider.GetComponent<Outline>().enabled = false;
        }
    }

    public void EnableInteractUI()
    {
        interactUI.SetActive(true);      
    }

    public void DisableInteractUI()
    {
        interactUI.SetActive(false);
    }

    private void Inspect(Transform T)
    {
        OI.Pickup(T);


    }

    private void Examine(GameObject C)
    {
        OI.ZoomIn(C);
    }

    public void EnableRaycast()
    {
        canRaycast = true;
    }

    private void OnDrawGizmos()
    {
        // Align gizmo with the player's camera crosshair.
        Vector3 cameraCenter = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cameraCenter, cameraCenter + playerCamera.transform.forward * 3f);

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity, interactLayer))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(playerCamera.transform.position, hit.point);
        }
    }
}
