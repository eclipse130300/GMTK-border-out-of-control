using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI descriptionTxt;
    public TextMeshProUGUI hpInfo;
    public TextMeshProUGUI hpText;

    public Image timerImg;
    [SerializeField] Sprite gameImgTimer;
    [SerializeField] Sprite breakImgTimer;

    public GameObject gameOverPanel;

    public AudioClip snakeAudio;

    private Vector3 startHpInfoPosition;
    // Start is called before the first frame update
    void Start()
    {
        startHpInfoPosition = hpInfo.transform.position;
    }

    public void SetTimerImage(bool isGameRunning)
    {
        Sprite newSprite = isGameRunning == true ? gameImgTimer : breakImgTimer;
        timerImg.sprite = newSprite;
    }

    public void SetHpText(string text)
    {
        hpText.text = text;
    }

    public void SetTimerText(int secounds, Color color)
    {
        timerText.text = secounds.ToString();
        timerText.color = color;
    }

    public void SetDescriptionText(string text, Color color)
    {
        descriptionTxt.text = text;
        descriptionTxt.color = color;
    }

    public void ShowHpInfoTxt(string txt, Color color)
    {
        hpInfo.transform.position = startHpInfoPosition;

        hpInfo.text = txt;
        hpInfo.color = color;
        Sequence sequence = DOTween.Sequence();
        Tween fromAlpha = DOTween.ToAlpha(() => hpInfo.color, x => hpInfo.color = x, 1, 1);
        Tween toAlpha = DOTween.ToAlpha(() => hpInfo.color, x => hpInfo.color = x, 0, 1);
        sequence.Append(fromAlpha).Join(hpInfo.transform.DOMoveY(90,2)).Append(toAlpha);
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void PlaySnakeAudio()
    {
        SoundManager.Instance.AudiobackGround.clip = snakeAudio;
        SoundManager.Instance.AudiobackGround.volume = 0.7f;
        SoundManager.Instance.AudiobackGround.Play();
    }

}
