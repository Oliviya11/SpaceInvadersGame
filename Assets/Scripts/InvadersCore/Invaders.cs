using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


namespace  InvadersCore
{
    public class Invaders : MonoBehaviour
{
    [SerializeField] InvadersData data;
    private InvadersLogic logic;

    public void Awake()
    {
        logic = new InvadersLogic(new InvadersLogic.Params()
        {
            data = data,
            parent = transform,
            camera = Camera.main,
        });
    }

    //????
    void OnHit(Invader invader)
    {
        /*invader.gameObject.SetActive(false);
        ScoreInfo scoreInfo = new ScoreInfo();
        scoreInfo.pos = gameManager.MyCamera.WorldToScreenPoint(invader.transform.position);
        scoreInfo.score = invader.ScoreForDestroy;
        scoreInfo.color = invader.Color;
        gameManager.AddScore(scoreInfo);
        destroyedInvadersNumber++;
        if (destroyedInvadersNumber >= invadersNumber)
        {
            onDestroyed?.Invoke();
        }*/
    }
    
    private void Update()
    {
        logic.Move();
    }
}

}
