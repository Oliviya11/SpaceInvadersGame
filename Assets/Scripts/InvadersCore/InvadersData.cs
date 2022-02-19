using System;
using System.Collections.Generic;
using UnityEngine;

namespace InvadersCore
{
    [Serializable]
    public class InvadersData
    {
        [SerializeField] List<Invader> transforms;
        [SerializeField] private int rows, cols;
        [SerializeField] Vector3 direction = Vector3.right;
        [SerializeField] float moveDownStep = 0.8f;
        [SerializeField] float initialSpeed = 1f;
        [SerializeField] float speedStep = 0.5f;
        [SerializeField] float distanceBetweenInvaders = 3;

        public List<Invader> Transforms => transforms;
        public int Rows => rows;
        public int Cols => cols;
        public Vector3 Direction => direction;
        public float MoveDownStep => moveDownStep;
        public float InitialSpeed => initialSpeed;
        public float SpeedStep => speedStep;
        public float DistanceBetweenInvaders => distanceBetweenInvaders;
    }
}