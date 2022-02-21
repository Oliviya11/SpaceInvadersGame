using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace InvadersCore.Mothership
{
    public class MothershipLogic
    {
        public struct Params
        {
            public Transform parent;
            public InvadersData invadersData;
            public MothershipData mothershipData;
            public Action<Invader> OnDestroyed;
            public Camera camera;
        }

        Params _params;
        static Random RandomGen = new Random();
        GridMover gridMover;
        Invader mothership;
        public Invader Mothership => mothership;

        public MothershipLogic(Params p)
        {
            _params = p;
        }

        public void Create()
        {
            int creationProbability = RandomGen.Next(100);
            if (creationProbability < _params.mothershipData.ChanceOfAppearance)
            {
                Vector3 direction = Vector3.right;
                int directionProbability = RandomGen.Next(100);
                if (directionProbability < 50)
                {
                    direction = Vector3.left;
                }

                Invader mothership = Object.Instantiate(_params.mothershipData.Prefab, _params.parent);
                mothership.onDestroyed += _params.OnDestroyed;
                GridPlacer gridPlacer = new GridPlacer(new GridPlacer.Params()
                {
                    rows = _params.invadersData.Rows,
                    cols = _params.invadersData.Cols,
                    distanceBetweenTransforms = _params.invadersData.DistanceBetweenInvaders,
                    transforms = new List<Transform>() {mothership.transform}
                });
                gridPlacer.PlaceToCell(4, 0, _params.invadersData.Transform);

                Vector3 rightEdge = _params.camera.ViewportToWorldPoint(Vector3.right);
                rightEdge.x -= 1;

                Vector3 leftEdge = _params.camera.ViewportToWorldPoint(Vector3.zero);
                leftEdge.x += 1;
                
                if (direction == Vector3.right)
                {
                    Vector3 pos = mothership.transform.position;
                    pos.x = leftEdge.x;
                    mothership.transform.position = pos;
                }
                else
                {
                    Vector3 pos = mothership.transform.position;
                    pos.x = rightEdge.x;
                    mothership.transform.position = pos;
                }

                gridMover = new GridMover(new GridMover.Params()
                {
                     direction = direction,
                     transforms = new List<Transform>() {mothership.transform},
                     rightEdge = rightEdge,
                     leftEdge = leftEdge,
                     initialSpeed = 3,
                     transform = mothership.transform,
                     moveDownStep = 0,
                     speedStep = 0,
                     canMoveBack = false,
                     cam = _params.camera,
                     OutOfScreen = delegate { mothership.gameObject.SetActive(false); },
                });
                //mothership.GetComponent<MotherShipInvader>().Init(_params.camera);
            }
        }

        public void Move()
        {
            gridMover.Move();
        }
    }
}