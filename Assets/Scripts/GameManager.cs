using System.Collections;
using InvadersCore;
using InvadersCore.Mothership;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreCreator scoreCreator;
    [SerializeField] Player player;
    [SerializeField] Invaders invaders;
    [SerializeField] Motherships motherships;
    [SerializeField] Camera myCamera;
    [SerializeField] Counter counter;
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;
    [SerializeField] Text winText;
    [SerializeField] Text loseText;
    [SerializeField] GameData gameData;
    [SerializeField] Vector3 playerPos;
    [SerializeField] Vector3 invadersPos;
    [SerializeField] Vector3 mothershipsPos;
    [SerializeField] GameObject endGamePopUp;
    Player currentPlayer;
    Invaders currentInvaders;
    Motherships currentMotherships;
    public Invaders Invaders => currentInvaders;
    public Camera MyCamera => myCamera;
    
    public void Awake()
    {
        Time.timeScale = 0;
        counter.OnCounterEnded += delegate { Time.timeScale = 1; };
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
        currentPlayer = Instantiate(player, playerPos, Quaternion.identity);
        currentPlayer.onDestroyed += ResetGame;
        currentInvaders = Instantiate(invaders, invadersPos, Quaternion.identity);
        currentInvaders.Init(OnHit, WinOnInvadersDestroyed, myCamera);
        currentMotherships = Instantiate(motherships, mothershipsPos, Quaternion.identity);
        currentMotherships.Init(currentInvaders, myCamera, OnHitMothership);
    }
    
    public void AddScore(ScoreInfo info)
    {
        scoreCreator.CreateScore(info);
        gameData.score += info.score;
        UpdateScore(gameData.score);
    }

    void OnHitMothership(Invader invader)
    {
        OnHit(invader);
        WinOnMothershipsDestroyed();
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

        if (currentMotherships != null)
        {
            Destroy(currentMotherships.gameObject);
        }
    }

    void WinOnInvadersDestroyed()
    {
        if (!currentMotherships.IsDestroyed()) return;
        Win();
    }

    void WinOnMothershipsDestroyed()
    {
        if (!currentMotherships.IsDestroyed()) return;
        if (!currentInvaders.IsDestroyed()) return;
        Win();
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
