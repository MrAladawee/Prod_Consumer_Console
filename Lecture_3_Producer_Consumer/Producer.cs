using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_3_Producer_Consumer
{
    public class Producer
    {
        // Producer'у необходимо сообщить, за какую часть он отвечает
        // Первый он, 2 или 3.

        private const int count = 5;
        private int num;

        public int Num => num; // свойство на чтение (узнать номер Producer'а)

        public Producer(int num)
        {
            this.num = num;
        }

        // Метод, запускающий процесс, за который отвечает Прод
        public void Start()
        {
            
            var t = new Thread(() =>
            {
                var iter = 0;
                while (iter++ < count)
                {
                    // Эмулируем случай, когда поток работает рандомное время
                    var r = new Random();
                    Thread.Sleep(r.Next(3000, 7000));
                    var result = r.Next(1000);
                    Console.WriteLine($"Producer №{num}: {result}");
                    CommonData.Put(num, result);
                }
            });
            t.IsBackground = true;
            t.Start();
        }

    }
}
