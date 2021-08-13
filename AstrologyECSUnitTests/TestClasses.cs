using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AstrologyECS;

namespace AstrologyECSUnitTests
{
    class Position : AstrologyECS.Component
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Immobile : AstrologyECS.Component { }



    class WindSystem : AstrologyECS.System
    {
        // This filter is used to pick and choose what kind of entities this system should operate on.
        protected override ComponentFilter Filter => new ComponentFilter()
            .AddNecessary(typeof(Position))
            .AddForbidden(typeof(Immobile));

        // This method is run on every entity that passes through the filter.
        protected sealed override void OperateOnEntity(Entity entity)
        {
            // the entity will be moved +1 in the X direction.
            entity.GetComponent<Position>().X += 1;
        }
    }
}
