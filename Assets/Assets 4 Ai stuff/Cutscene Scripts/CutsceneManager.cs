using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
        }
    }
}
