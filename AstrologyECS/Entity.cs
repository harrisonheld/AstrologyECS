using System;
using System.Collections.Generic;
using System.Linq;

namespace AstrologyECS
{
    public class Entity
    {
        public List<Component> components = new List<Component>();

        public void AddComponent(Component componentToAdd)
        {
            componentToAdd.Owner = this;
            components.Add(componentToAdd);
        }

        public bool RemoveComponent(Component componentToRemove)
        {
            return components.Remove(componentToRemove);
        }
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

            return true;
        }

        public bool HasComponent<T>()
            where T : Component
        {
            return components.OfType<T>().Count() > 0;
        }
        public bool HasComponent(Type type)
        {
            return components.Exists((Component comp) => (comp.GetType() == type));
        }

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
        public List<T> GetComponents<T>()
            where T : Component
        {
            return components.OfType<T>().ToList();
        }
    }
}