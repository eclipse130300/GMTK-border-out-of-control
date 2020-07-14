using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;
    public AudioSource AudiobackGround;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
/*        audioSource = GetComponent<AudioSource>();*/


    }

    public void PlayClipAtPoint(AudioClip clip, Vector3 point)
    {
        AudioSource.PlayClipAtPoint(clip, point);
    }

    public void PlayRandomClipAtPoint(AudioClip[] clips, Vector3 point)
    {
        int randomNum = Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[randomNum], point);
    }

    public void LoopAudio(AudioClip clip, float duration)
    {
        StartCoroutine(FixedDurationAudioLoop(clip, duration));
    }

    IEnumerator FixedDurationAudioLoop(AudioClip clip, float duration)
    {
        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitForSeconds(duration);
        audioSource.Stop();
        audioSource.clip = null;
    }
}
