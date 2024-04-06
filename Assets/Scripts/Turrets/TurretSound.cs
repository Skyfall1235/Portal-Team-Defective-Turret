using UnityEngine;
using UnityEngine.Events;
/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 04/04/2024
*/
public class TurretSound : MonoBehaviour
{
    private AudioSource _turretAudio;
    [SerializeField] bool playIdleSounds;
    [SerializeField] AudioClip[] idleSounds;

    public UnityEvent onShootTurretEvent;
    private void Awake()
    {
        _turretAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(playIdleSounds)
        {
            InvokeRepeating("PlayTurretIdleSound", 4f, 4f);
        }
    }

    public void PlayTurretSound(AudioClip sound)
    {
        _turretAudio.PlayOneShot(sound);
    }

    private void PlayTurretIdleSound()
    {
        int index = Random.Range(0, idleSounds.Length);
        _turretAudio.PlayOneShot(idleSounds[index]);
    }
}
