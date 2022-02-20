using System;
using UnityEngine;

namespace Utils
{
    public class InputManager : MonoBehaviour
    {
        public static event Action OnRightArrowPressed;
        public static event Action OnLeftArrowPressed;
        public static event Action OnSpacePressed;
        
        void Update()
        {
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