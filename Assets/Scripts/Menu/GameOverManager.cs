using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameWonScreen;
    [SerializeField] TextMeshProUGUI currentScore;
    int score = 0;
    private bool dead = false;

    public void SetGameOver()
    {
        dead = true;
        gameOverScreen.SetActive(true);
        currentScore.text = ":(((";
        Time.timeScale = 0;
    }

    public void SetGameWon()
    {
        gameWonScreen.SetActive(true);
        currentScore.text = "Score : " + score.ToString();
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void addScore()
    {
        score = score + 1;
        if(score == 2 & !dead)
        {
            SetGameWon();
        }
    }
}
