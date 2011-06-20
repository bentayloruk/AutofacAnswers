using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core.Activators.Reflection;

namespace AutofacExamples
{
    public class SoAnswer6407530
    {
        public void DoAnswer()
        {
            var builder = new ContainerBuilder();
            
            //Use the custom IConstructorFinder
            builder.RegisterType<Example>().FindConstructorsWith(new NonObsoleteConstructorFinder());
            var container = builder.Build();

            //This will throw as Obsolete constructor not returned.
            var example = container.Resolve<Example>();
        }
    }

    internal class NonObsoleteConstructorFinder : IConstructorFinder
    {
        public IEnumerable<ConstructorInfo> FindConstructors(Type targetType)
        {
            //Find all constructors as Autofac does.
            var defaultFinder = new BindingFlagsConstructorFinder(BindingFlags.Public);
            var constructors = defaultFinder.FindConstructors(targetType);

            //Filter the Obsolete constructors.
            return constructors.Where(c => c.GetCustomAttributes(typeof(ObsoleteAttribute), true).Length == 0);
        }
    }

    public class Example
    {
        public Example(MyService service)
        {
            // ...
        }

        [Obsolete]
        public Example()
        {
            var service = new MyService();
        }
    }

    public class MyService
    {
    }
}