using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class DialogueNode
{
    public string npcLine;
    public string[] playerResponses;
    public int[] nextDialogueIndices;
}

public class NPCInteractable : MonoBehaviour
{
    public DialogueNode[] dialogue;
    private bool playerInRange = false;

    public CivilianPathfinding pathfindingScript;
    public PlayerMovement playerMovement;

    public CinemachineVirtualCamera npcCamera;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.StartDialogue(this);
            StartInteraction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerMovement = other.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void StartInteraction()
    {
        if (pathfindingScript != null)
        {
            pathfindingScript.LockMovement(true);
        }

        if (pathfindingScript != null && !pathfindingScript.isStationary)
        {
        pathfindingScript.enabled = false;
        pathfindingScript.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        }

        if (playerMovement != null && npcCamera != null)
        {
        playerMovement.SwitchCameraPriority(npcCamera, true);
        }
    }

    public void EndInteraction()
    {
        if (pathfindingScript != null)
        {
            pathfindingScript.LockMovement(false);
            ResumeNPCPath();
        }

        if (pathfindingScript != null && !pathfindingScript.isStationary)
        {
        pathfindingScript.enabled = true;
        pathfindingScript.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        }

        if (playerMovement != null && npcCamera != null)
        {
        playerMovement.SwitchCameraPriority(npcCamera, false);
        }
    }

    private void ResumeNPCPath()
    {
        if (pathfindingScript.isStationary) return;

        if (pathfindingScript.patrolPoints.Count > 0)
        {
            pathfindingScript.ResetPatrol();
        }
    }
}
