using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class GridMover
    {
        public struct Params
        {
            public Vector3 direction;
            public List<Transform> transforms;
            public Vector3 rightEdge;
            public Vector3 leftEdge;
            public float initialSpeed;
            public Transform transform;
            public float moveDownStep;
            public float speedStep;
            public bool canMoveBack;
            public Camera cam;
            public Action OutOfScreen;
        }

        Params _params;
        private Vector3 direction;
        private float speed;

        public GridMover(Params p)
        {
            _params = p;
           direction = _params.direction;
           speed = _params.initialSpeed;
        }

        public void Move()
        {
            _params.transform.position += direction * speed * Time.deltaTime;

            for (int i = 0; i < _params.transforms.Count; ++i)
            {
                Transform transform = _params.transforms[i];
                if (!transform.gameObject.activeSelf) {
                    continue;
                }

                if (_params.canMoveBack)
                {
                    if (direction == Vector3.right && transform.position.x >= _params.rightEdge.x)
                    {
                        UpdatePosition();
                        break;
                    }

                    if (direction == Vector3.left && transform.position.x <= _params.leftEdge.x)
                    {
                        UpdatePosition();
                        break;
                    }
                }

                if (!IsOnScreen(transform))
                {
                    _params.OutOfScreen?.Invoke();
                }
            }
        }

        bool IsOnScreen(Transform tr)
        {
            Vector3 screenPoint = _params.cam.WorldToViewportPoint(tr.position);
            return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }
        
        void UpdatePosition()
        {
            direction = new Vector3(-direction.x, 0f, 0f);
            var transform1 = _params.transform;
            Vector3 position = transform1.position;
            position.y -= _params.moveDownStep;
            transform1.position = position;
            speed += _params.speedStep;
        }
    }
}