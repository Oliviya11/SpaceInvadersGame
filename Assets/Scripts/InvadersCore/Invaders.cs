using System;
using UnityEngine;


namespace  InvadersCore
{
    public class Invaders : MonoBehaviour
{
    [SerializeField] InvadersData data;
    InvadersLogic logic;
    bool isInited = false;
    public InvadersData Data => data;

    public void Init(Action<Invader> OnHit, Action OnDestroyed, Camera myCamera)
    {
        logic = new InvadersLogic(new InvadersLogic.Params()
        {
            data = data,
            parent = transform,
            camera = myCamera,
            OnHit = OnHit,
            OnDestroyed = OnDestroyed,
        });
        isInited = true;
    }

    public bool IsDestroyed()
    {
        return logic.IsDestroyed();
    }

    private void Update()
    {
        if (isInited)
        {
            logic.Move();
        }
    }
}

}
