using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyECS
{
    public static class EntityPool
    {
        private static List<Entity> entities = new List<Entity>();
        private static List<System> systems = new List<System>();
        private static HashSet<Entity> entitiesToBePolled = new HashSet<Entity>();

        public static void Clear()
        {
            entities.Clear();
        }

        public static List<Entity> GetEntities()
        {
            return entities;
        }

        /// <summary>
        /// Add an entity to the pool.
        /// </summary>
        public static void AddEntity(Entity toAdd)
        {
            entities.Add(toAdd);
        }

        /// <summary>
        /// Remove an entity from the pool.
        /// </summary>
        /// <returns>True if the entity was successfully removed, otherwise false.</returns>
        public static bool RemoveEntity(Entity toRemove)
        {
            return entities.Remove(toRemove);
        }

        /// <summary>
        /// Add a new system to the pool.
        /// </summary>
        public static void AddSystem(System toAdd)
        {
            systems.Add(toAdd);
        }

        /// <summary>
        /// Remove a system from the pool.
        /// </summary>
        /// <returns>True if the system was successfully removed, otherwise false.</returns>
        public static bool RemoveSystem(System toRemove)
        {
            return systems.Remove(toRemove);
        }


        /// <summary>
        /// Adds the entity to the set of entities to polled by the systems.
        /// </summary>
        internal static void QueueSystemsPoll(Entity entity)
        {
            entitiesToBePolled.Add(entity);
        }

        /// <summary>
        /// Run each system once.
        /// </summary>
        public static void Tick()
        {
            // update each system so it has the correct entities
            foreach(Entity entity in entitiesToBePolled)
            {
                foreach(System system in systems)
                {
                    system.TestEntity(entity);
                }
            }
            entitiesToBePolled.Clear();

            // run all systems
            foreach(System system in systems)
            {
                system.Run();
            }
        }
    }
}
