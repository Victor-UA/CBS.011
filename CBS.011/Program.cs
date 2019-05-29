using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CBS._011
{
    //Написать класс "ThreadWithResult<TReturn>", который будет иметь методы/свойства:

    //- IsCompleted // Операция завершена или нет
    //- Success // Операция завершена успешно или нет
    //- Result // Результат операции
    //- Start() // Запуск операции

    //- Операцию для выполнения передавать в конструктор с помощью делегата
    //- Класс должен быть generic.Generic-параметр указывает тип результата операции.
    //- Result - должен выдавать результат операции или исключение если оно произошло внутри переданной операции
    //или если операция еще не запускалась



    //- Добавить возможность создания экземпляров с помощью записи: 
    //"var thread = ThreadWithResult.Create(()=>10)" (пример),
    //с автоматическим определением типа возвращаемого значения, с помощью переданного делегата.
    class Program
    {
        static void Main(string[] args)
        {
            var thread = ThreadWithResult.Create(() => 10);

            Console.WriteLine(thread);

            new Thread(thread.Start).Start();

            Console.WriteLine(thread);

            while (!thread.IsCompleted)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine(thread);
            Console.ReadKey();


        }
    }
}
