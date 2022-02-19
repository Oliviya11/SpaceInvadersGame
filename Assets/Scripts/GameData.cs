using UnityEngine;

public class GameData : MonoBehaviour
{
    public int lives;
    public int score;

    void Awake()
    {
        lives = 3;
        score = 0;
    }
}
