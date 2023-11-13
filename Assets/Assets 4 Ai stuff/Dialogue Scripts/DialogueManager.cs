using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public TMP_Text npcText;
    public GameObject[] playerButtons;
    public TMP_Text[] playerResponses;

    private NPCInteractable currentNPC;
    private int currentDialogueIndex = 0;

    private bool isNPCTextCompleted = false;

    public float typingSpeed = 0.05f;

    public GameObject playerCamera;


    private bool showingPlayerResponses = false;

    public TMP_Text skipText;
    public TMP_Text nextText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(NPCInteractable npc)
    {
        currentNPC = npc;
        currentDialogueIndex = 0;
        StartCoroutine(OpenDialoguePanel());
        npc.StartInteraction();
        if (npc.playerMovement != null)
        {
            npc.playerMovement.LockMovement(true);
        }
    }

    public void DisplayDialogue()
    {
        isNPCTextCompleted = false;
        dialoguePanel.SetActive(true);
        npcText.gameObject.SetActive(true);
        npcText.text = currentNPC.dialogue[currentDialogueIndex].npcLine;
        skipText.gameObject.SetActive(true);
        nextText.gameObject.SetActive(false);

        StartCoroutine(TypeSentence(currentNPC.dialogue[currentDialogueIndex].npcLine));

        if (currentNPC.dialogue[currentDialogueIndex].playerResponses.Length == 0)
        {
            StartCoroutine(EndDialogueAfterDelay());
        }
        else
        {
            foreach (var button in playerButtons)
            {
                button.SetActive(false);
            }
        }
    }

    public void PlayerResponse(int index)
    {
        isNPCTextCompleted = false;
        showingPlayerResponses = false;
        currentDialogueIndex = currentNPC.dialogue[currentDialogueIndex].nextDialogueIndices[index];
        if (currentDialogueIndex != -1)
        {
            DisplayDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        isNPCTextCompleted = false;
        StartCoroutine(CloseDialoguePanel());
        currentNPC.EndInteraction();
        if (currentNPC.playerMovement != null)
        {
            currentNPC.playerMovement.LockMovement(false);
        }

        //GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<Interactor>().minicrosshairUI.SetActive(true);

        currentNPC = null;
        currentDialogueIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && dialoguePanel.activeInHierarchy)
        {
            if (!isNPCTextCompleted && !showingPlayerResponses)
            {
                StopAllCoroutines();
                npcText.text = currentNPC.dialogue[currentDialogueIndex].npcLine;
                isNPCTextCompleted = true;
                skipText.gameObject.SetActive(false);
                nextText.gameObject.SetActive(true);
            }
            else if (isNPCTextCompleted && !showingPlayerResponses)
            {
                ShowPlayerResponses();
            }
            else if (showingPlayerResponses)
            {

            }
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        npcText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            npcText.text += letter;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                npcText.text = sentence;
                skipText.gameObject.SetActive(false);
                nextText.gameObject.SetActive(true);
                yield break;
            }
            yield return new WaitForSeconds(typingSpeed);
        }
        isNPCTextCompleted = true;
        skipText.gameObject.SetActive(false);
        nextText.gameObject.SetActive(true);
    }

    private void ShowPlayerResponses()
    {
        if (currentNPC.dialogue[currentDialogueIndex].playerResponses.Length > 0)
        {
            showingPlayerResponses = true;
            npcText.gameObject.SetActive(false);
            nextText.gameObject.SetActive(false);
            nextText.gameObject.SetActive(false);
            skipText.gameObject.SetActive(false);

            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

            for (int i = 0; i < playerButtons.Length; i++)
            {
                if (i < currentNPC.dialogue[currentDialogueIndex].playerResponses.Length)
                {
                    playerButtons[i].SetActive(true);
                    playerResponses[i].text = currentNPC.dialogue[currentDialogueIndex].playerResponses[i];
                }
                else
                {
                    playerButtons[i].SetActive(false);
                }
            }
        }
    }

    IEnumerator EndDialogueAfterDelay()
    {
        yield return new WaitForSeconds(2.0f);
        EndDialogue();
    }

    IEnumerator OpenDialoguePanel()
    {
        dialoguePanel.SetActive(true);
        dialoguePanel.transform.localScale = Vector3.zero;
        Vector3 targetScale = new Vector3(1, 1, 1);
        float duration = 0.5f;

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            dialoguePanel.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
            yield return null;
        }

        dialoguePanel.transform.localScale = targetScale;
        DisplayDialogue();
    }


    IEnumerator CloseDialoguePanel()
    {
        Vector3 initialScale = dialoguePanel.transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float duration = 0.5f;

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            dialoguePanel.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        dialoguePanel.transform.localScale = targetScale;
        dialoguePanel.SetActive(false);
    }
}
