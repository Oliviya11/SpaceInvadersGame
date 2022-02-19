using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace InvadersCore
{
    public class InvadersLogic
    {
        public struct Params
        {
            public InvadersData data;
            public Transform parent;
            public Action OnHit, OnDestroyed;
            public Camera camera;
        }
        Params _params;
        int destroyedInvadersNumber = 0;
        int invadersNumber = 0;
        GridMover gridMover;

        public InvadersLogic(Params p)
        {
            _params = p;
            invadersNumber = _params.data.Rows * _params.data.Cols;
            List<Transform> transforms = Create();
            new GridPlacer(new GridPlacer.Params()
            {
                rows = _params.data.Rows,
                cols = _params.data.Cols,
                distanceBetweenTransforms = _params.data.DistanceBetweenInvaders,
                transforms = transforms
            });
            gridMover = new GridMover(new GridMover.Params()
            {
                direction = _params.data.Direction,
                transforms = transforms,
                rightEdge = _params.camera.ViewportToWorldPoint(Vector3.right),
                leftEdge = _params.camera.ViewportToWorldPoint(Vector3.left),
                initialSpeed = _params.data.InitialSpeed,
                transform = _params.parent,
                moveDownStep = _params.data.MoveDownStep,
                speedStep = _params.data.SpeedStep
            });
        }

        public void Move()
        {
            gridMover.Move();
        }

        List<Transform> Create()
        {
            List<Transform> transforms = new List<Transform>();
            for (int i = 0; i < _params.data.Transforms.Count; ++i)
            {
                Invader tr = Object.Instantiate(_params.data.Transforms[i], _params.parent);
                tr.onDestroyed += OnHit;
                transforms.Add(tr.transform);
            }

            return transforms;
        }

        void OnHit(Invader invader)
        {
            invader.gameObject.SetActive(false);
            _params.OnHit?.Invoke();
            destroyedInvadersNumber++;
            if (destroyedInvadersNumber >= invadersNumber)
            {
                _params.OnDestroyed?.Invoke();
            }
        }
    }
}