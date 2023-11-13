using UnityEngine;
using UnityEngine.UI;

public class CutsceneSkipper : MonoBehaviour
{
    public Image progressBar;
    public CutsceneManager cutsceneManager;

    private float holdTime = 3f;
    private float currentHoldTime = 0f;

    private void Update()
    {
    if (cutsceneManager.IsCutscenePlaying())
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            Debug.Log("Enter key is being held down");
            currentHoldTime += Time.deltaTime;
            progressBar.fillAmount = currentHoldTime / holdTime;

            if (currentHoldTime >= holdTime)
            {
                cutsceneManager.SkipCutscene();
                progressBar.fillAmount = 0;
                gameObject.SetActive(false);
            }
        }
        else
        {
            currentHoldTime = 0;
            progressBar.fillAmount = 0;
        }
    }
    else
    {
        progressBar.gameObject.SetActive(false);
    }
    }

    public void ActivateProgressBar() {
    progressBar.gameObject.SetActive(true);
}

}
