using System;
using Core.Animals.Strategy;
using Core.CollisionBehaviours;
using Core.Configs.Animals;
using Core.Configs.Movements;
using Core.Movements;
using Core.Services.Cameras;
using Core.Services.Configs;
using Core.Services.Configs.Std;
using UI.Animals;
using UnityEngine;
using Utils;
using Utils.Const;
using VContainer;

namespace Core.Animals
{
	[SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class Animal : MonoBehaviour
    {
        public event Action<Animal, Animal> OnCollision;
        public event Action<Animal> OnDeath;
        
        [SerializeField] private AnimalType animalType;
        [SerializeField] private Transform visual;
        [SerializeField] private TastyLabel tastyLabel;

        private Vector3 _direction;
        private float _stayTimer;
        private CollisionBehaviourType _collisionBehaviour;
        
        private IMovementStrategy _movementStrategy;
        private TurnBackHandler _outOfBoundsHandler;

        private IStrategyResolver _strategyResolver;
        private ICameraHandler _cameraHandler;
        private AnimalsConfig _animalsConfig;
        private MovementsConfig _movementConfig;

        public CollisionBehaviourType CollisionBehaviour => _collisionBehaviour;
        public AnimalType Type => animalType;
        public Rigidbody Rigidbody { get; private set; }
        public Vector3 Direction { set => _direction = value.WithYZero(); }

        [Inject]
        private void Construct(IStrategyResolver strategyResolver, 
            ICameraHandler cameraHandler,
            AnimalsConfigProvider animalsConfig,
            MovementsConfigProvider movementConfig)
        {
            _cameraHandler = cameraHandler;
            _strategyResolver = strategyResolver;
            _animalsConfig = animalsConfig.GetConfig();
            _movementConfig = movementConfig.GetConfig();
        }

        public void Initialize(AnimalData data)
        {
            _collisionBehaviour = data.collisionBehavior;

            _direction = transform.forward;
            _direction.y = 0;
            _direction.Normalize();
            
            tastyLabel.Initialize(_animalsConfig.TastyLabelDuration);

            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.linearVelocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
            _stayTimer = 0f;
            
            var movementConfig = _movementConfig.Movements[data.movement];
            _movementStrategy = _strategyResolver.ResolveMovement(data.movement, movementConfig);
            _outOfBoundsHandler = new TurnBackHandler(_animalsConfig);
        }

        public void ShowTastySign() => tastyLabel.Show();

        public void Despawn()
        {
            OnDeath?.Invoke(this);
        }

        private void FixedUpdate()
        {
            _movementStrategy?.Tick(Rigidbody, Time.deltaTime, ref _direction);
            _outOfBoundsHandler.Tick(Rigidbody, _cameraHandler.Bounds, ref _direction);
            
            OrientToDirection();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out Animal animal)) return;
            
            OnCollision?.Invoke(this, animal);
            _stayTimer = 0f;
        }
        
        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.layer != (int)LayerName.Animal) return;

            _stayTimer += Time.fixedDeltaTime;

            if (!(_stayTimer >= _animalsConfig.CollisionCheckInterval)) 
                return;
            
            _stayTimer = 0f;
            OnCollision?.Invoke(this, other.gameObject.GetComponent<Animal>());
        }

        private void OnDestroy()
        {
            OnDeath?.Invoke(this);
        }

        private void OrientToDirection()
        {
            _direction.y = 0f;
            
            if (_direction.sqrMagnitude < _animalsConfig.MagnitudeThreshold) 
                return;

            var targetRot = Quaternion.LookRotation(_direction, Vector3.up);

            visual.rotation = Quaternion.Slerp(visual.rotation, targetRot, _animalsConfig.AnimalRotationSpeed * Time.fixedDeltaTime);
        }
    }
}