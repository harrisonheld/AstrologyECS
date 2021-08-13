using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyECS
{
    abstract class System
    {
        // filter used to find relevant components for the System
        protected ComponentFilter Filter { get; }
        private List<Entity> entities = new List<Entity>();

        /// <summary>
        /// Test if an entity should be in this system. Adds or removes it accordingly.
        /// </summary>
        internal void TestEntity(Entity entity)
        {
            if (Filter.Match(entity))
            {
                if (!entities.Contains(entity))
                    entities.Add(entity);
            }
            else // !Filter.Match(entity)
            {
                entities.Remove(entity);
            }
        }

        public void Run()
        {
            foreach (Entity e in entities)
                OperateOnEntity(e);
        }

        protected abstract void OperateOnEntity(Entity entity);
    }
}
