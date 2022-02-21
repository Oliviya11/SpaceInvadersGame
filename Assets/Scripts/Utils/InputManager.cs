using System;
using UnityEngine;

namespace Utils
{
    public class InputManager : MonoBehaviour
    {
        public static event Action OnRightArrowPressed;
        public static event Action OnLeftArrowPressed;
        public static event Action OnSpacePressed;

        public bool isPaused;
        
        void Update()
        {
            if (isPaused) return;
                
            if (Input.GetKey(KeyCode.RightArrow))
            {
                OnRightArrowPressed?.Invoke();
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                OnLeftArrowPressed?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                OnSpacePressed?.Invoke();
            }
        }
    }
}