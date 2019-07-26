using System;

namespace Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            // человек в бассейне
            Man man = new Man();
            // он идет в раздевалку
            DressingRoom room = new DressingRoom();

            man.Clothes = "Футболка, шорты, кроссовки";

            // кладет свои вещи в сумку
            // и оставляет на хранение в раздевалку
            room.Bag = man.Undress();

            // одевает подходящие одежду и снаряжение
            man.Clothes = "Плавки, подводные очки";

            // после обратно переодевается
            man.Dress(room.Bag);
            room.Bag = man.Undress();
        }
    }

    class Man
    {
        public string Clothes { get; set; }
        public void Dress(Bag bag)
        {
            Clothes = bag.Contents;
        }
        public Bag Undress()
        {
            return new Bag(Clothes);
        }
    }

    class Bag
    {
        public string Contents { get; set; }
        public Bag(string contents)
        {
            this.Contents = contents;
        }
    }
    class DressingRoom
    {
        public Bag Bag { get; set; }
    }
}
