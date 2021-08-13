using System;
using System.Collections.Generic;
using System.Linq;

namespace AstrologyECS
{
    public class Entity
    {
        public List<Component> components = new List<Component>();

        /// <summary>
        /// Add a component to this entity.
        /// </summary>
        public Entity AddComponent(Component componentToAdd)
        {
            componentToAdd.Owner = this;
            components.Add(componentToAdd);

            EntityPool.QueueSystemsPoll(this);

            return this;
        }

        /// <summary>
        /// Remove a component from the entity.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public bool RemoveComponent(Component componentToRemove)
        {
            bool successful = components.Remove(componentToRemove);

            if (successful)
                EntityPool.QueueSystemsPoll(this);

            return successful;
        }
        /// <summary>
        /// Removes all components of the specified type from the entity.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public bool RemoveComponentsOfType<T>()
            where T : Component
        {
            List<T> componentsOfType = GetComponents<T>();

            // if no components of type, return false
            if (componentsOfType.Count == 0)
                return false;

            // otherwise, remove them and return true
            foreach (T component in GetComponents<T>())
            {
                RemoveComponent(component as Component);
            }

            EntityPool.QueueSystemsPoll(this);
            return true;
        }


        /// <summary>
        /// Tests if the entity has any components of the specified type.
        /// </summary>
        public bool HasComponent<T>()
            where T : Component
        {
            return components.OfType<T>().Count() > 0;
        }
        /// <summary>
        /// Tests if the entity has any components of the specified type.
        /// </summary>
        public bool HasComponent(Type type)
        {
            return components.Exists((Component comp) => (comp.GetType() == type));
        }

        /// <summary>
        /// Returns the first component of the specified type.
        /// </summary>
        public T GetComponent<T>()
            where T : Component
        {
            T component = GetComponents<T>().FirstOrDefault();

            if (component == null)
            {
                string exceptionMessage = $"This entity does not have a component of type {typeof(T).Name}.";
                throw (new Exception(exceptionMessage));
            }

            return component;
        }
        /// <summary>
        /// Returns all components of the specified type.
        /// </summary>
        public List<T> GetComponents<T>()
            where T : Component
        {
            return components.OfType<T>().ToList();
        }
    }
}