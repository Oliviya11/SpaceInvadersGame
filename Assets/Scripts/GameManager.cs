using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] GameData gameData;
    [SerializeField] Vector3 playerPos;
    [SerializeField] Vector3 invadersPos;
    Player currentPlayer;
    Invaders currentInvaders;
    public Camera MyCamera => myCamera;

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
    }

    void CreateLevel()
    {
        currentPlayer = Instantiate(player, playerPos, Quaternion.identity, transform);
        currentPlayer.onDestroyed += ResetGame;
        currentInvaders = Instantiate(invaders, invadersPos, quaternion.identity, transform);
        currentInvaders.gameManager = this;
    }
    
    public void AddScore(ScoreInfo info)
    {
        scoreCreator.CreateScore(info);
        gameData.score += info.score;
        UpdateScore(gameData.score);
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
        --gameData.lives;
        if (gameData.lives > 0)
        {
            UpdateLives(gameData.lives);
            Destroy(currentInvaders.gameObject);
            CreateLevel();
            counter.Reset();
        }
    }
}
