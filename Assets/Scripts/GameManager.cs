using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreCreator scoreCreator;
    [SerializeField] Camera myCamera;
    [SerializeField] Counter counter;
    public Camera MyCamera => myCamera;

    public void Awake()
    {
        Time.timeScale = 0;
        counter.Init();
        counter.onCounterEnded += delegate { Time.timeScale = 1; };
    }
    
    public void AddScore(ScoreInfo info)
    {
        scoreCreator.CreateScore(info);
    }
}
