/* Assignment: Portal
/  Programmer: Owen Jones
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 03/28/2024
*/
using UnityEngine;
using UnityEngine.Events;

public class AssignableAudio : MonoBehaviour
{
    private AudioSource playerAudio;

    public UnityEvent onPlaySoundEvent;

    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        playerAudio.PlayOneShot(sound);
    }
}
