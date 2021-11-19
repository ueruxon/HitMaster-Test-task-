using System;
using UnityEngine;

namespace Game.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        public event Action Touched;

        private Camera _camera;
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Touched?.Invoke();
            }
        }

        public Vector3 GetTouchOnScreen() => 
            Input.mousePosition;
    }
}
