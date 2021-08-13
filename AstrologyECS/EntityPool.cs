using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyECS
{
    public class EntityPool
    {
        private List<Entity> entities = new List<Entity>();
        private List<System> systems = new List<System>();

        public void Clear()
        {
            entities.Clear();
        }

        /// <summary>
        /// Add an entity to the pool.
        /// </summary>
        public void AddEntity(Entity toAdd)
        {
            entities.Add(toAdd);
        }

        public void Tick()
        {
            foreach(System system in systems)
            {
                system.Run();
            }
        }
    }
}
