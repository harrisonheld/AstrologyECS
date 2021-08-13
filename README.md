# AstrologyECS
My Entity-Component-System framework.

# What is the EntityPool?
The EntityPool is a static class that holds all the Entities. It is a conveinent place to access them.
```csharp
Entity entity = new Entity();
EntityPool.AddEntity(entity); // add an entity
EntityPool.RemoveEntity(entity); // remove an entity

// get all the entities in the pool
List<Entity> entities = EntityPool.GetEntities();
```

# What are Entities?
Entities are just bags of components. Their behavior is modified by adding or removing components.

# What are Components?
Components are plain old data. They contain public fields so their data can be accessed.

# What are Systems?
Systems are where behavior is implemented. There are many different systems. A system will only act on entities that have the right components. When you add or remove components from an entity, systems may choose to act on the entity or to ignore it.

# Creating a New Type of Component
Components should hold data, and only data. This means they should have no methods.
All of their properties should have public get and set accessors so their data can be manipulated.
```csharp
// All components must inherit from AstrologyECS.Component
class Position : AstrologyECS.Component
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
}
```
This is a simple position component, having X and Y properties. In this example, they are integers.
