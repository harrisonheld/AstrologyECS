using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyECS
{
    // Inspiration from: https://github.com/adizhavo/ECS/blob/master/ECS/Filter.cs
    public class ComponentFilter
    {
        // filters for entities with ALL of these component types
        private readonly HashSet<Type> Necessary = new HashSet<Type>();
        // excludes entities containing ANY of these component types
        private readonly HashSet<Type> Forbidden = new HashSet<Type>();

        /// <summary>
        /// Add types of components that an entity MUST have to pass through the filter.
        /// </summary>
        public ComponentFilter AddNecessary(params Type[] toAdd)
        {
            foreach (Type t in toAdd)
                Necessary.Add(t);

            return this;
        }
        /// <summary>
        /// Add types of components that an entity MUST NOT have to pass through the filter.
        /// </summary>
        public ComponentFilter AddForbidden(params Type[] toAdd)
        {
            foreach (Type t in toAdd)
                Forbidden.Add(t);

            return this;
        }

        /// <summary>
        /// Checks if an entity passes through this filter.
        /// </summary>
        /// <returns>True if the entity passes. Otherwise false.</returns>
        public bool Match(Entity entity)
        {
            // don't match if any necessary components are missing
            foreach (Type t in Necessary)
            {
                if (!entity.HasComponent(t))
                    return false;
            }
            // don't match if any forbidden components are present
            foreach (Type t in Forbidden)
            {
                if (entity.HasComponent(t))
                    return false;
            }

            return true;
        }
    }
}
