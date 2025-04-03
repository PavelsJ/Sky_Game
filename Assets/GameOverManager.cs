using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Image overlayImage;

    private void Start()
    {
        FadeOut();
    }

    private void OnEnable()
    {
        GameManager.OnEndRace += FadeIn;
    }

    private void OnDisable()
    {
        GameManager.OnEndRace -= FadeIn;
    }

    private void FadeIn()
    {
        gameOverPanel.SetActive(true);
    }

    private void FadeOut()
    {
        gameOverPanel.SetActive(false);
        overlayImage.CrossFadeAlpha(0f, 1f, false);
    }
    
    public void RestartGame()
    {
        StartCoroutine(RestartGameCoroutine());
    }

    private IEnumerator RestartGameCoroutine()
    {
        overlayImage.CrossFadeAlpha(1f, 1f, true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextGame()
    {
        StartCoroutine(NextGameCoroutine());
    }
    
    private IEnumerator NextGameCoroutine()
    {
        overlayImage.CrossFadeAlpha(1f, 1f, true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }
    
    private IEnumerator QuitGameCoroutine()
    {
        overlayImage.CrossFadeAlpha(1f, 1f, true);
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
