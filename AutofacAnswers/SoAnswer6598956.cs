using System.Reflection;
using Autofac;

namespace AutofacAnswers
{
    public class SoAnswer6598956
    {
        private IContainer _container;

        public void DoAnswer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(IHandle<>));

            _container = builder.Build();

            var commandHadler1 = _container.Resolve<IHandle<Command1>>();
            var commandHadler2 = _container.Resolve<IHandle<Command2>>();
        }
    }

    interface IHandle<T> where T : class
    {
        void Handle(T command);
    }

    class CommandHandler1 : IHandle<Command1>
    {
        public void Handle(Command1 command)
        {
            throw new System.NotImplementedException();
        }
    }

    class Command1 { }

    class CommandHandler2 : IHandle<Command2>
    {
        public void Handle(Command2 command)
        {
            throw new System.NotImplementedException();
        }
    }

    class Command2 { }

}