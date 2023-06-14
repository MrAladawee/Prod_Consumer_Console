using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_3_Producer_Consumer
{
    // Статик чтобы CommonData присутствовал единственным образом общим для Producer и Consumer, которые работают
    // с таким одним общим объектом.

    public static class CommonData
    {

        private static int _maxQueueLenght = 2; // Ограничение на размер очереди
                                                // чтобы ресурсы не тратить на генератцию
        private static Queue<int>[] _tResults = { new(), new(), new() }; // т.к у нас 3 продьюсера по условию
                                                  // то массив хранит 3 значения от каждого 

        private static object locker = new object();

        // Записывает в массив данные
        public static void Put(int index, int value)
        {
            if (index >= 0 && index < _tResults.Length)
            {
                lock (locker)
                {
                    while (_tResults[index].Count >= _maxQueueLenght)
                    {
                        Console.WriteLine($"[ERROR] Producer {index}: Slot not ready yet by Consumer.");
                        Monitor.Wait(locker); // Пока Consumer не возьмет ячейку - Put ждет
                    }
                    Console.WriteLine($"Producer {index}: Add result {value}.");
                    _tResults[index].Enqueue(value);
                    Monitor.PulseAll(locker);
                }
            }
        }


        // Получаем целиком весь массив

        public static int[] Get()
        {
            lock (locker)
            {

                while (!_tResults.All(value => value.Count > 0))
                {
                    Console.WriteLine("Consumer: Data not ready yet.");
                    Monitor.Wait(locker); // Поток засыпает, но блокировка снимается => Lock в Put оставляем
                }

                Console.WriteLine("Consumer: Data is ready. Comparing");

                var res = new[] {_tResults[0].Dequeue(), _tResults[1].Dequeue(), _tResults[2].Dequeue()};
                Console.WriteLine("Consumer: Call Produce empty slot.");
                Monitor.PulseAll(locker);

                return res;
            }
        }


    }
}
