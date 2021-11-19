using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private readonly int Run = Animator.StringToHash("Run");

        public event Action ReachedToWaypoint;

        [Header("References")]
        [SerializeField] private Radar _radar;
        [SerializeField] private InputHandler _input;
        [SerializeField] private WaypointNetwork _waypointNetwork;
        [SerializeField] private Animator _animator;
        [Space(2)] 
        [SerializeField] private float _speed;

        private NavMeshAgent _agent;
        private bool _isMoving;
        private bool _canMove = true;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _speed;
        }

        private void Start()
        {
            _input.Touched += OnTouched;
            _radar.PlaceCleaned += OnPlaceCleaned;
        }

        private void Update()
        {
            if (_isMoving)
            {
                if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
                {
                    _isMoving = false;
                    _canMove = false;
                    
                    _animator.SetBool(Run, false);
                    
                    ReachedToWaypoint?.Invoke();
                }
            }
        }

        private void OnPlaceCleaned() => _canMove = true;

        private void OnTouched()
        {
            if (_canMove && _isMoving == false) 
                StartMoving();
        }

        private void StartMoving()
        {
            _isMoving = true;
            MoveTo();
        }

        private void MoveTo()
        {
            Transform target = _waypointNetwork.GetNextWaypoint();
            if (target != null)
            {
                _agent.SetDestination(target.position);
                _animator.SetBool(Run, true);
            }
            else
                StopMoving();
        }

        private void StopMoving()
        {
            _agent.isStopped = false;
            _isMoving = false;

            _animator.SetBool(Run, false);
        }

        private void OnDisable()
        {
            _radar.PlaceCleaned -= OnPlaceCleaned;
            _input.Touched -= OnTouched;
        }
    }
}