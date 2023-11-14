using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    public GameObject teleportLocation;
    public GameObject objectToActivate;
    public float health = 100f;
//  public Image redPanel;
    public GameObject deathPanel;
    private float flashSpeed = 5f;
    private bool isHit = false;

    public bool isHiding = false;

    public float flashDuration = 0.5f;
    public Text healthText;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }


    public void Start()
    {
        healthText.text = "Health:" + health.ToString();
    }
    private void Update()
    {
        if (!isHit)
        {
        //  redPanel.color = Color.Lerp(redPanel.color, new Color(1f, 0f, 0f, 0f), flashSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthText.text = "Health: " + health.ToString();
        StartCoroutine(FlashRedPanel());
        if (health <= 0)
        {
            PlayerDied();
            health = 100;
            teleportLocation.gameObject.SetActive(true);
            objectToActivate.gameObject.SetActive(true);
            Debug.Log("player has died man ");
            

        }
    }
   
    private void PlayerDied()
    {
        Debug.Log("Player is dead!");
        deathPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    private IEnumerator FlashRedPanel()
    {
    isHit = true;
 // redPanel.color = new Color(1f, 0f, 0f, 0.5f);
    yield return new WaitForSeconds(1);
    isHit = false;
    }
}
