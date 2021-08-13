# An Extremely Brief Overview of ECS
ECS stands for Entity-Component-System. It was invented to solve the problem of wide, deep, and confusing class hierarchies in video game development. Instead of inhereting behavior from a base class, entities acquire their behavior compositionally by having components added to them.

## What are Entities?
Entities are just bags of components. Their behavior is modified by adding or removing components.

## What are Components?
Components are plain old data. They contain public fields so their data can be accessed.

## What are Systems?
Systems are where behavior is implemented. There are many different systems. A system will only act on entities that have the right components. When you add or remove components from an entity, systems may choose to act on the entity or to ignore it.

## What is the EntityPool?
In AstrologyECS, the EntityPool is a static class that holds all the Entities. It is a conveinent place to access them.
```csharp
Entity entity = new Entity();
EntityPool.AddEntity(entity); // add an entity
EntityPool.RemoveEntity(entity); // remove an entity

// get all the entities in the pool
List<Entity> entities = EntityPool.GetEntities();
```
# Creating New Things!
## Creating a New Type of Component
Components should hold data, and only data. This means they should have no methods.
All of their properties should have public get and set accessors so their data can be manipulated.
```csharp
// All components must inherit from AstrologyECS.Component

// a position component with x and y coordinates.
class Position : AstrologyECS.Component
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
}

// Components don't even need to have data at all. They can be used as tags.
// You'll see an example of this in the next section.
class Immobile : AstrologyECS.Component { }
```

## Creating a new type of System
Systems must implement the `Filter` property and the `OperateOnEntity` method. Here is a basic implementation of a wind system that blows entities in the positive X direction. It only operates on entities that have a Position component, but excludes those that have the Immobile component. Systems automatically update what entities they are interested in, so you don't need to worry about that!
```csharp
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
```

# Making it all work
You can use `EntityPool.Tick()` to perform one tick. This means each of the systems in the EntityPool will be run once. Here's a demonstation using the new components and the new system we just created.
```csharp
// Register the system we just created.
EntityPool.AddSystem(new WindSystem());

// Create two entities with Position components. One of them has the Immobile component.
Entity entity1 = new Entity()
    .AddComponent(new Position());

Entity entity2 = new Entity()
    .AddComponent(new Position())
    .AddComponent(new Immobile());

// Add them to the EntityPool.
EntityPool.AddEntity(entity1);
EntityPool.AddEntity(entity2);

// Tick the EntityPool three times.
EntityPool.Tick();
EntityPool.Tick();
EntityPool.Tick();

// Verify that only the position of the first entity was changed.
// The second, being Immobile, should not have changed.
Position position1 = entity1.GetComponent<Position>();
Position position2 = entity2.GetComponent<Position>();
System.Console.WriteLine($"Entity 1: ({position1.X}, {position1.Y})");
System.Console.WriteLine($"Entity 2: ({position2.X}, {position2.Y})");
```
