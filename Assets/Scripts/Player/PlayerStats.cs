using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/* Assignment: Portal
/  Programmer: Owen Jones
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 04/07/2024
*/
public class PlayerStats : MonoBehaviour
{
    [SerializeField] float playerHealth;
    [SerializeField] TMP_Text healthText;

    [SerializeField] private bool invincible;
    AssignableAudio hitSound;

    private void Start()
    {
        hitSound = GetComponent<AssignableAudio>();
    }

    public void DecreaseHealth()
    {
        if (!invincible)
        {
            playerHealth -= 15;
            healthText.text = playerHealth.ToString() + "%";
            hitSound.onPlaySoundEvent.Invoke();
            if(playerHealth <= 0)
            {
                GetComponent<CharacterController>().enabled = false;
                GetComponentInChildren<PlayerUIManager>().deathScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                InvokeRepeating("IncreaseHealth", 3f, .01f);
            }
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
