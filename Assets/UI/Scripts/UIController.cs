using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public GameObject pause;
    
    private GameObject player;

    private GameObject gun;
    public bool isPaused;
    
    void Start()
    {
        player = GameObject.Find("Swat");
    }


    void Update()
    {
        ammoText.text = player.GetComponent<GunController>().Clip().ToString()+"/"+ player.GetComponent<GunController>().Ammo().ToString();
        healthText.text = "Healt:"+player.GetComponent<PlayerController>().Healt().ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Resume();
                isPaused = false;
            }
            else
            {
                Pause();
                isPaused = true;
            }
        }

    }
    
    public void Pause()
    {
        pause.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
