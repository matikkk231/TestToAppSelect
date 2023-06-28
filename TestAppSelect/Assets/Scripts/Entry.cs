using System.Collections.Generic;
using Core.ECS.Entity;
using Core.ECS.System;
using DefaultNamespace.Area.Logic.System;
using UnityEngine;

public class Entry : MonoBehaviour
{
    private IEntityManager _entityManager;
    private List<ISystem> _systems;
    private List<ISystem> _initializeSystems;

    private void Awake()
    {
        _entityManager = new EntityManager();
        _systems = new List<ISystem>();
        _initializeSystems = new List<ISystem>();

        CreateSystems();
    }

    private void Start()
    {
        foreach (var system in _initializeSystems)
        {
            system.Execute();
        }
    }

    private void Update()
    {
        foreach (var system in _systems)
        {
            system.Execute();
        }
    }

    private void CreateSystems()
    {
        _initializeSystems.Add(new CardDeckCreatorSystem(_entityManager));
    }
}