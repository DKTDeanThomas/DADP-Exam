using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutsceneManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;
    public float[] cameraDurations;
    private int currentCameraIndex = 0;
    private bool cutscenePlayed = false;

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
        SwitchToNextCamera();
    }

    void SwitchToNextCamera()
    {
        if (currentCameraIndex > 0)
        {
            virtualCameras[currentCameraIndex - 1].gameObject.SetActive(false);
        }

        if (currentCameraIndex < virtualCameras.Length)
        {
            virtualCameras[currentCameraIndex].gameObject.SetActive(true);
            Invoke("SwitchToNextCamera", cameraDurations[currentCameraIndex]);
            currentCameraIndex++;
        }
    }
}
