using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button stopGame;
    public Button continueGame;
    public Button exitLevel;
    public Button restartLevel;
    public Button nextLevel;

    public GameObject main;
    public GameObject menu;
    public GameObject gameOver;
    public GameObject levelComplete;

    public bool GameHasEnded { get; set; }
    public Pages Page { get; set; }

    private void Start()
    {
        SetPage(Pages.Main);
        stopGame.onClick.AddListener(() => SetPage(Pages.Menu));
        continueGame.onClick.AddListener(() => SetPage(Pages.Main));
        restartLevel.onClick.AddListener(Restart);
        nextLevel.onClick.AddListener(Restart);
        exitLevel.onClick.AddListener(() => SceneManager.LoadScene("Levels"));
    }

    private void SetPage(Pages page)
    {
        Debug.Log($"Set page: {page}");
        
        main.SetActive(false);
        menu.SetActive(false);
        gameOver.SetActive(false);
        levelComplete.SetActive(false);
        
        switch (page)
        {
            case Pages.Main:
                main.SetActive(true);
                break;
            case Pages.Menu:
                menu.SetActive(true);
                break;
            case Pages.GameOver:
                gameOver.SetActive(true);
                break;
            case Pages.LevelComplete:
                levelComplete.SetActive(true);
                break;

            default:
                throw new ArgumentException(nameof(page));
        }

        Time.timeScale = page != Pages.Main ? 0 : 1;
        
        Page = page;
    }

    public void EndGame()
    {
        if (GameHasEnded) return;
        GameHasEnded = true;
        SetPage(Pages.GameOver);
        Debug.Log("Game over");
    }

    public void LevelComplete()
    {
        if (!int.TryParse(SceneManager.GetActiveScene().name, out var completedLevel)) return;
        SetPage(Pages.LevelComplete);
        PlayerPrefs.SetInt("CompletedLevel", completedLevel);
    }

    private void Restart()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene($"Scenes/Levels/{SceneManager.GetActiveScene().name}");
        SetPage(Pages.Main);
    }
}