using System;
using System.Collections.Generic;

namespace Core.ECS.Entity
{
    public interface IEntityManager
    {
        public List<IEntity> GetEntitiesOfGroup(List<Type> needfulGroup);
        public IEntity CreateEntity();
        public void RemoveEntity(IEntity entity);
    }
}