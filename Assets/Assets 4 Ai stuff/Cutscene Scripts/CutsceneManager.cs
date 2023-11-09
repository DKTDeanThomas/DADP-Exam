using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutsceneManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;
    public float[] cameraDurations;
    public float[] cameraBlendTimes; // Array to hold blend times for each transition
    private int currentCameraIndex = 0;
    private bool cutscenePlayed = false;

    public PlayerMovement playerMovement; // Reference to the player movement script
    public CinemachineVirtualCamera playerCinemachineCamera; // Reference to the player's Cinemachine camera
    private Transform originalFollowTarget; // To store the original follow target of the Cinemachine camera
    public CinemachineBrain cinemachineBrain; // Reference to the CinemachineBrain
    public float defaultBlendTime = 0.5f; // Default blend time for transitions

    private void Start()
    {
        foreach (var vcam in virtualCameras)
        {
            vcam.gameObject.SetActive(false);
        }
    }

    public void PlayCutscene()
    {
        if (cutscenePlayed) return;
        cutscenePlayed = true;

        // Set the priority of all cutscene cameras to 10
        foreach (var vcam in virtualCameras)
        {
            vcam.Priority = 10;
        }

        // Set blend time for the first transition
        if (virtualCameras.Length > 0 && cameraBlendTimes.Length > 0)
        {
            cinemachineBrain.m_DefaultBlend.m_Time = cameraBlendTimes[0];
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            playerMovement.canRotate = false; // Disable rotation
        }

        // Detach the Cinemachine camera from the focus point
        if (playerCinemachineCamera != null)
        {
            originalFollowTarget = playerCinemachineCamera.Follow;
            playerCinemachineCamera.Follow = null;
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

        if (currentCameraIndex < virtualCameras.Length)
        {
            virtualCameras[currentCameraIndex].gameObject.SetActive(true);
            Invoke("SwitchToNextCamera", cameraDurations[currentCameraIndex]);
            currentCameraIndex++;
        }
        else
        {
            // Reset the priority of all cutscene cameras
            foreach (var vcam in virtualCameras)
            {
                vcam.Priority = 9; // Or whatever the default priority is
            }

            // Instantly switch back to the player camera
            if (playerCinemachineCamera != null)
            {
                playerCinemachineCamera.Priority = 11;
                cinemachineBrain.m_DefaultBlend.m_Time = defaultBlendTime; // Set blend time for the final transition
            }

            // Re-enable player controls and rotation after the last camera
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
                playerMovement.canRotate = true; // Enable rotation
            }

            // Reattach the Cinemachine camera to the focus point
            if (playerCinemachineCamera != null && originalFollowTarget != null)
            {
                playerCinemachineCamera.Follow = originalFollowTarget;
            }
        }
    }
}
