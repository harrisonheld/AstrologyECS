using Microsoft.VisualStudio.TestTools.UnitTesting;
using AstrologyECS;

namespace AstrologyECSUnitTests
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void AddMillionEntities()
        {
            EntityPool pool = new EntityPool();

            for(int i = 0; i < 1000000; i++)
            {
                Entity entity = new Entity();
                pool.AddEntity(entity);
            }

            pool.Tick();
        }
    }
}
