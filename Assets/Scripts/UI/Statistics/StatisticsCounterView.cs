using System;
using Core.Animals;
using Core.Animals.Spawner;
using Core.CollisionBehaviours;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI.Statistics
{
    public class StatisticsCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI deadPrey;
        [SerializeField] private TextMeshProUGUI deadPredators;
        
        private IAnimalsSpawner _animalsSpawner;
        
        private int _preyCount;
        private int _predatorsCount;

        [Inject]
        private void Construct(IAnimalsSpawner animalsSpawner)
        {
            _animalsSpawner = animalsSpawner;
            _animalsSpawner.OnAnimalDied += UpdateCounters;
        }

        private void Start()
        {
            UpdateText();
        }

        private void UpdateCounters(CollisionBehaviourType type)
        {
            switch (type)
            {
                case CollisionBehaviourType.Prey:
                    _preyCount++;
                    break;
                case CollisionBehaviourType.Predator:
                    _predatorsCount++;
                    break;
            }

            UpdateText();
        }

        private void UpdateText()
        {
            deadPrey.text = "Dead prey: " + _preyCount;
            deadPredators.text = "Dead predators: " + _predatorsCount;
        }

        private void OnDestroy()
        {
            _animalsSpawner.OnAnimalDied -= UpdateCounters;
        }
    }
}
