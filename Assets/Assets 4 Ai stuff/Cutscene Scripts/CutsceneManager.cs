using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;
    public float[] cameraDurations;
    public float[] cameraBlendTimes;
    private int currentCameraIndex = 0;
    private bool cutscenePlayed = false;

    public PlayerMovement playerMovement;
    public CinemachineVirtualCamera playerCinemachineCamera;
    private Transform originalFollowTarget;
    public CinemachineBrain cinemachineBrain;
    public float defaultBlendTime = 0.5f;

    public TextMeshProUGUI dialogueText;
    public string[] dialogues;

    public CutsceneSkipper cutsceneSkipper;

    public GameObject cutscenePanel;

    public GameObject CutsceneTextPanel;

    private void Start()
    {
        foreach (var vcam in virtualCameras)
        {
            vcam.gameObject.SetActive(false);
        }
    }

    public void PlayCutscene()
    {
        if (cutsceneSkipper != null) 
        { 
            cutsceneSkipper.gameObject.SetActive(true);
            cutsceneSkipper.ActivateProgressBar();
        }

        if (cutscenePlayed) return;
        cutscenePlayed = true;

        foreach (var vcam in virtualCameras)
        {
            vcam.Priority = 10;
        }

        if (virtualCameras.Length > 0 && cameraBlendTimes.Length > 0)
        {
            cinemachineBrain.m_DefaultBlend.m_Time = cameraBlendTimes[0];
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            playerMovement.canRotate = false;
        }

        if (playerCinemachineCamera != null)
        {
            originalFollowTarget = playerCinemachineCamera.Follow;
            playerCinemachineCamera.Follow = null;
        }

        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(true);
        }

        if (cutsceneSkipper != null)
        {
            cutsceneSkipper.gameObject.SetActive(true);
        }

        if (cutscenePanel != null)
        {
        cutscenePanel.SetActive(true);
        }

        if (CutsceneTextPanel != null)
        {
            StartCoroutine(ShowPanel());
        }

        SwitchToNextCamera();
    }

    void SwitchToNextCamera()
    {
    if (currentCameraIndex > 0)
    {
        virtualCameras[currentCameraIndex - 1].gameObject.SetActive(false);
        if (currentCameraIndex < cameraBlendTimes.Length)
        {
            cinemachineBrain.m_DefaultBlend.m_Time = cameraBlendTimes[currentCameraIndex];
        }
    }

    if (currentCameraIndex < dialogues.Length)
    {
        if (!string.IsNullOrEmpty(dialogues[currentCameraIndex]))
        {
            float typingDuration = Mathf.Max(0, cameraDurations[currentCameraIndex] - 2f);
            StartCoroutine(TypeDialogue(dialogues[currentCameraIndex], typingDuration));
            if (CutsceneTextPanel != null && !CutsceneTextPanel.activeSelf)
            {
                StartCoroutine(ShowPanel());
            }
        }
        else
        {
            if (CutsceneTextPanel != null && CutsceneTextPanel.activeSelf)
            {
                StartCoroutine(HidePanel());
            }
        }
    }

    if (currentCameraIndex < virtualCameras.Length)
    {
        virtualCameras[currentCameraIndex].gameObject.SetActive(true);
        if (currentCameraIndex < virtualCameras.Length - 1)
        {
            Invoke("SwitchToNextCamera", cameraDurations[currentCameraIndex]);
        }
        else
        {
            Invoke("EndCutscene", cameraDurations[currentCameraIndex]);
        }
        currentCameraIndex++;
    }
    else
    {
        EndCutscene();
    }
    }



    public void SkipCutscene()
    {
        Debug.Log("SkipCutscene called");
        CancelInvoke("SwitchToNextCamera");

    foreach (var vcam in virtualCameras)
    {
        vcam.gameObject.SetActive(false);
        vcam.Priority = 9;
    }

    if (playerCinemachineCamera != null)
    {
        playerCinemachineCamera.Priority = 11;
        cinemachineBrain.m_DefaultBlend.m_Time = defaultBlendTime;

        if (originalFollowTarget != null)
        {
            playerCinemachineCamera.Follow = originalFollowTarget;
        }
    }

    if (playerMovement != null)
    {
        playerMovement.enabled = true;
        playerMovement.canRotate = true;
    }

    if (dialogueText != null)
    {
        dialogueText.text = "";
    }

    if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }

    cutscenePlayed = false;

    if (cutscenePanel != null)
    {
    cutscenePanel.SetActive(false);
    }

    if (CutsceneTextPanel != null)
    {
    StopCoroutine(ShowPanel());
    StartCoroutine(HidePanel());
    }
    }

    void EndCutscene()
    {
        foreach (var vcam in virtualCameras)
        {
            vcam.Priority = 9;
        }

        if (playerCinemachineCamera != null)
        {
            playerCinemachineCamera.Priority = 11;
            cinemachineBrain.m_DefaultBlend.m_Time = defaultBlendTime; 
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            playerMovement.canRotate = true;
        }

        if (playerCinemachineCamera != null && originalFollowTarget != null)
        {
            playerCinemachineCamera.Follow = originalFollowTarget;
        }

        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }

        if (cutsceneSkipper != null)
        {
            cutsceneSkipper.gameObject.SetActive(false);
        }

        if (cutscenePanel != null)
        {
        cutscenePanel.SetActive(false);
        }

        if (CutsceneTextPanel != null)
        {
            StartCoroutine(HidePanel());
        }
    }

    public bool IsCutscenePlaying()
    {
    return cutscenePlayed;
    }

    IEnumerator ShowPanel()
    {
        CutsceneTextPanel.SetActive(true);
        Vector3 targetScale = new Vector3(1, 1, 1);
        float duration = 0.5f;

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            CutsceneTextPanel.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
            yield return null;
        }

        CutsceneTextPanel.transform.localScale = targetScale;
    }

    IEnumerator HidePanel()
    {
        Vector3 initialScale = CutsceneTextPanel.transform.localScale;
        float duration = 0.5f;

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            CutsceneTextPanel.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            yield return null;
        }

        CutsceneTextPanel.SetActive(false);
    }

    IEnumerator TypeDialogue(string dialogue, float duration)
    {
    dialogueText.text = "";
    if (dialogue.Length == 0) yield break;

    float typingSpeed = duration / dialogue.Length;
    foreach (char letter in dialogue.ToCharArray())
    {
        dialogueText.text += letter;
        yield return new WaitForSeconds(typingSpeed);
    }
    }
}
