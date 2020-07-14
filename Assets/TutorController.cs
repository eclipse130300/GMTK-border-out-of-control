using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorController : MonoBehaviour
{
    public AudioClip gameBG;
    public LevelLoader levelLoader;
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SoundManager.Instance.AudiobackGround.clip = gameBG;
            SoundManager.Instance.AudiobackGround.Play();
            SoundManager.Instance.AudiobackGround.volume = 0.1f;
            levelLoader.LoadScene(LevelLoader.gameSceneName);

        }
    }
}
