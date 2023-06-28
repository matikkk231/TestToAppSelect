using System;
using System.Collections.Generic;
using Core.ECS.Component;

namespace Core.ECS.Entity
{
    public interface IEntity
    {
        public IEnumerable<IComponent> Components { get; }
        public IComponent GetComponent(Type type);
        public void AddComponent(IComponent additionalComponent);
        public void RemoveComponent(Type componentType);
    }
}