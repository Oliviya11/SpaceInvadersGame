using System;
using UnityEngine;

namespace InvadersCore.Mothership
{
    [Serializable]
    public class MothershipData
    {
        [SerializeField] Invader prefab;
        [SerializeField] float apperanceDeltaTime = 4;
        [SerializeField] int chanceOfAppearance= 99;

        public Invader Prefab => prefab;
        public float ApperanceDeltaTime => apperanceDeltaTime;
        public int ChanceOfAppearance => chanceOfAppearance;
    }
}