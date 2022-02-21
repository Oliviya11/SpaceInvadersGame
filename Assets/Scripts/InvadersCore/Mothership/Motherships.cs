using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace InvadersCore.Mothership
{
    public class Motherships : MonoBehaviour
    {
        [SerializeField] MothershipData mothershipData;
        List<MothershipLogic> mothershipLogics = new List<MothershipLogic>();
        bool isInited = false;
        Invaders invaders;
        Camera camera;
        private Action<Invader> OnDestroyed;
        public void Init(Invaders someInvaders, Camera cam, Action<Invader> someOnDestroyed)
        {
            invaders = someInvaders;
            camera = cam;
            OnDestroyed = someOnDestroyed;
            InvokeRepeating(nameof(Appear), mothershipData.ApperanceDeltaTime,  mothershipData.ApperanceDeltaTime);
            isInited = true;
        }

        void Appear()
        {
            MothershipLogic mothershipLogic = new MothershipLogic(new MothershipLogic.Params()
            {
                invadersData = invaders.Data,
                mothershipData = mothershipData,
                camera = camera,
                parent = transform,
                OnDestroyed = OnDestroyed,
            });
            mothershipLogic.Create();
            mothershipLogics.Add(mothershipLogic);
        }

        void Update()
        {
            if (isInited)
            {
                for (int i = 0; i < mothershipLogics.Count; ++i)
                {
                    mothershipLogics[i].Move();
                }
            }
        }

        public bool IsDestroyed()
        {
            for (int i = 0; i < mothershipLogics.Count; i++)
            {
                if (mothershipLogics[i].Mothership.gameObject.activeSelf)
                {
                    return false;
                }
            }

            return true;
        }
    }
}