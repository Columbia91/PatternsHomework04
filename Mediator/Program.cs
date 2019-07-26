using System;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagerMediator mediator = new ManagerMediator();
            Colleague factory = new Factory(mediator);
            Colleague courier = new Courier(mediator);
            Colleague shop = new Shop(mediator);

            mediator.Factory = factory;
            mediator.Courier = courier;
            mediator.Shop = shop;

            factory.Send("Есть продукция, необходимо доставить");
            courier.Send("Продукция доставлена, необходимо сбыть");
            shop.Send("Продукция успешно реализована");

            Console.Read();
        }
    }

    abstract class Mediator
    {
        public abstract void Send(string msg, Colleague colleague);
    }
    abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }

        public virtual void Send(string message)
        {
            mediator.Send(message, this);
        }
        public abstract void Notify(string message);
    }
    
    class Factory : Colleague
    {
        public Factory(Mediator mediator)
        : base(mediator)
        { }

        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение заводу: " + message);
        }
    }
    
    class Courier : Colleague
    {
        public Courier(Mediator mediator)
        : base(mediator)
        { }

        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение курьеру: " + message);
        }
    }
    
    class Shop : Colleague
    {
        public Shop(Mediator mediator)
        : base(mediator)
        { }

        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение магазину: " + message);
        }
    }

    class ManagerMediator : Mediator
    {
        public Colleague Factory { get; set; }
        public Colleague Courier { get; set; }
        public Colleague Shop { get; set; }
        public override void Send(string msg, Colleague colleague)
        {
            // если отправитель - завод, значит есть продукт
            // отправляем сообщение курьеру - доставить продукцию
            if (Factory == colleague)
                Courier.Notify(msg);
            // если отправитель - курьер, значит товар доставлен
            // отправляем сообщение в магазин
            else if (Courier == colleague)
                Shop.Notify(msg);
            // если отправитель - магазин, значит продукция сбыта
            // отправляем сообщение заводу
            else if (Shop == colleague)
                Factory.Notify(msg);
        }
    }
}