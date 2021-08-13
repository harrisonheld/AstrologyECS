using Microsoft.VisualStudio.TestTools.UnitTesting;
using AstrologyECS;

using System.Collections.Generic;

namespace AstrologyECSUnitTests
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void AddMillionEntities()
        {
            for(int i = 0; i < 1000000; i++)
            {
                Entity entity = new Entity();
                EntityPool.AddEntity(entity);
            }
        }

        [TestMethod]
        public void AddAndRemoveEntity()
        {
            Entity entity = new Entity();
            EntityPool.AddEntity(entity);
            EntityPool.RemoveEntity(entity);

            // get all the entities in the pool
            List<Entity> entities = EntityPool.GetEntities();
            Assert.IsTrue(entities.Count == 0);
        }

        [TestMethod]
        public void AddAComponent()
        {
            Entity entity = new Entity();
            Position positionComp = new Position();
            entity.AddComponent(positionComp);
        }

        [TestMethod]
        public void TestComponentFilter()
        {
            ComponentFilter filter = new ComponentFilter()
                .AddNecessary(typeof(Position))
                .AddForbidden(typeof(Immobile));

            Entity entity = new Entity()
                .AddComponent(new Position());

            Assert.IsTrue(filter.Match(entity));

            entity.AddComponent(new Immobile());
            Assert.IsFalse(filter.Match(entity));
        }

        [TestMethod]
        public void TestSystems()
        {
            EntityPool.AddSystem(new WindSystem());

            Entity entity1 = new Entity()
                .AddComponent(new Position());

            Entity entity2 = new Entity()
                .AddComponent(new Position())
                .AddComponent(new Immobile());

            EntityPool.AddEntity(entity1);
            EntityPool.AddEntity(entity2);

            EntityPool.Tick();
            EntityPool.Tick();
            EntityPool.Tick();

            // Entity 1 should have moved
            Assert.AreEqual(3, entity1.GetComponent<Position>().X);
            // Entity 2, having the Immobile component, should not have moved.
            Assert.AreEqual(0, entity2.GetComponent<Position>().X);
        }
    }
}
