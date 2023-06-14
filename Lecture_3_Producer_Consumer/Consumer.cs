using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_3_Producer_Consumer
{
    public class Consumer
    {

        public void Start()
        {
            var t = new Thread( () =>
            {
                while (true)
                {
                    var _tResult = CommonData.Get();
                    var r = new Random();
                    Thread.Sleep(r.Next(2000, 5000)); // эмуляция длительности работы программы
                    var result = $"{_tResult[0]} + {_tResult[1]} + {_tResult[2]} = {_tResult.Sum()}";
                    Console.WriteLine(result);
                }
            });
            t.IsBackground = true;
            t.Start();
        }

    }
}
