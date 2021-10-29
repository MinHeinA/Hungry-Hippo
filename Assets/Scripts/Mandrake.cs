using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Mandrake : MonoBehaviour
{
    [SerializeField]
    Light2D light2D;

    public AudioSource audioSrc;
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponentInChildren<Light2D>();
        light2D.enabled = false;
        CallAudio();
    }
    public void PlayShriek()
    {
        AudioSource.PlayClipAtPoint(audioClips[audioClips.Length - 1], transform.position);
    }

    void CallAudio()
    {
        Invoke("RandomSounds", 3);
    }

    void RandomSounds()
    {
        audioSrc.clip = audioClips[Random.Range(0, audioClips.Length - 1)];
        audioSrc.Play();
        CallAudio();
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Flashlight")
        {
            light2D.enabled = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flashlight")
        {
             light2D.enabled = false;
        }
    }
}
