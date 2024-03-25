using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float playerHealth;
    [SerializeField] TMP_Text healthText;

    public void DecreaseHealth()
    {
        playerHealth -= 15;
        healthText.text = playerHealth.ToString() + "%";
        if(playerHealth <= 0)
        {
            GetComponentInChildren<PlayerUIManager>().deathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            InvokeRepeating("IncreaseHealth", 3f, .01f);
        }
        
    }

    private void IncreaseHealth()
    {
        playerHealth += 1;
        if(playerHealth >= 100)
        {
            playerHealth = 100;
            CancelInvoke();
        }
        healthText.text = playerHealth.ToString() + "%";
    }
}
