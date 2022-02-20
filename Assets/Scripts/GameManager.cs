using System.Collections;
using InvadersCore;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreCreator scoreCreator;
    [SerializeField] Player player;
    [SerializeField] Invaders invaders;
    [SerializeField] Camera myCamera;
    [SerializeField] Counter counter;
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;
    [SerializeField] Text winText;
    [SerializeField] Text loseText;
    [SerializeField] GameData gameData;
    [SerializeField] Vector3 playerPos;
    [SerializeField] Vector3 invadersPos;
    [SerializeField] GameObject endGamePopUp;
    Player currentPlayer;
    Invaders currentInvaders;

    public void Awake()
    {
        Time.timeScale = 0;
        counter.Init();
        counter.onCounterEnded += delegate { Time.timeScale = 1; };
        gameData.score = 0;
        UpdateScore(gameData.score);
        gameData.lives = 3;
        UpdateLives(gameData.lives);
        CreateLevel();
        HideGameStatus();
        endGamePopUp.SetActive(false);
    }

    void HideGameStatus()
    {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
    }

    void CreateLevel()
    {
        currentPlayer = Instantiate(player, playerPos, Quaternion.identity, transform);
        currentPlayer.onDestroyed += ResetGame;
        currentInvaders = Instantiate(invaders, invadersPos, quaternion.identity, transform);
        currentInvaders.Init(OnHit, Win, myCamera);
    }
    
    public void AddScore(ScoreInfo info)
    {
        scoreCreator.CreateScore(info);
        gameData.score += info.score;
        UpdateScore(gameData.score);
    }
    
    void OnHit(Invader invader)
    {
        invader.gameObject.SetActive(false);
        ScoreInfo scoreInfo = new ScoreInfo();
        scoreInfo.pos = myCamera.WorldToScreenPoint(invader.transform.position);
        scoreInfo.score = invader.ScoreForDestroy;
        scoreInfo.color = invader.Color;
        AddScore(scoreInfo);
    }

    void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    void ResetGame()
    {
        Time.timeScale = 0;
        --gameData.lives;
        if (gameData.lives > 0)
        {
            UpdateLives(gameData.lives);
            ClearInvaders();
            CreateLevel();
            counter.Reset();
        }
        else
        {
            Lose();
        }
    }

    void ClearInvaders()
    {
        if (currentInvaders != null)
        {
            Destroy(currentInvaders.gameObject);
        }
    }

    void Win()
    {
        Time.timeScale = 1;
        winText.gameObject.SetActive(true);
        StartCoroutine(ShowEndGamePopUp());
    }

    void Lose()
    {
        Time.timeScale = 1;
        loseText.gameObject.SetActive(true);
        StartCoroutine(ShowEndGamePopUp());
    }

    IEnumerator ShowEndGamePopUp()
    {
        yield return new WaitForSeconds(1f);
        ClearInvaders();
        HideGameStatus();
        endGamePopUp.SetActive(true);
    }
}
