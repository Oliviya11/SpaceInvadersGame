using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] List<Invader> transforms;
    [SerializeField] private int rows, columns;
    [SerializeField] Vector3 direction = Vector3.right;
    [SerializeField] float moveDownStep = 0.8f;
    [SerializeField] float initialSpeed = 1f;
    [SerializeField] float speedStep = 0.5f;
    float speed;
    private float distanceBetweenTransforms = 3f;

    public void Awake()
    {
        Create();
    }

    void Create()
    {
        speed = initialSpeed;
        
        int k = 0;
        for (int i = 0; i < rows; ++i)
        {
            float width = distanceBetweenTransforms * (columns - 1);
            float height = distanceBetweenTransforms * (rows - 1);

            Vector2 centerPadding = new Vector2(-width * 0.5f, -height * 0.5f);
            Vector3 rowPosition = new Vector3(centerPadding.x, (distanceBetweenTransforms * i) + centerPadding.y, 0f);

            for (int j = 0; j < columns; ++j)
            {
                Invader tr = Instantiate(transforms[k++], transform);
                tr.onDestroyed += OnHit;
                Vector3 position = rowPosition;
                position.x += distanceBetweenTransforms * j;
                tr.transform.localPosition = position;
            }
        }
    }

    void OnHit(Invader invader)
    {
        invader.gameObject.SetActive(false);
        ScoreInfo scoreInfo = new ScoreInfo();
        scoreInfo.pos = gameManager.MyCamera.WorldToScreenPoint(invader.transform.position);
        scoreInfo.score = invader.ScoreForDestroy;
        scoreInfo.color = invader.Color;
        gameManager.AddScore(scoreInfo);
    }
    
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        Vector3 rightEdge = gameManager.MyCamera.ViewportToWorldPoint(Vector3.right);
        Vector3 leftEdge = gameManager.MyCamera.ViewportToWorldPoint(Vector3.zero);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeSelf) {
                continue;
            }

            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 1f))
            {
                UpdatePosition();
                break;
            }
            
            if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 1f))
            {
                UpdatePosition();
                break;
            }
        }
    }
    
    private void UpdatePosition()
    {
        direction = new Vector3(-direction.x, 0f, 0f);
        var transform1 = transform;
        Vector3 position = transform1.position;
        position.y -= moveDownStep;
        transform1.position = position;
        speed += speedStep;
    }
}
