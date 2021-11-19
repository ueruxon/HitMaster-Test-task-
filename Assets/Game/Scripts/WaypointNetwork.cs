using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class WaypointNetwork : MonoBehaviour
    {
        public event Action ReachedFinish;
        
        [SerializeField] private List<Transform> _waypointList;

        private int _currentIndex = 0;
        
        public Transform GetNextWaypoint()
        {
            if (_waypointList.Count == 0)
            {
                return null;
            }
            
            Transform nextWaypoint = null;
            int nextIndex = _currentIndex + 1;

            if (nextIndex <= _waypointList.Count)
            {
                nextWaypoint = _waypointList[_currentIndex];
                _currentIndex = nextIndex;
                
                if (_currentIndex == _waypointList.Count)
                    ReachedFinish?.Invoke();
            }

            return nextWaypoint;
        }
    }
}
