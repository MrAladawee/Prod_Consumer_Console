using Lecture_3_Producer_Consumer;

Consumer consumer = new Consumer();

Producer p1 = new Producer(0);
Producer p2 = new Producer(1);
Producer p3 = new Producer(2);

consumer.Start();

p1.Start();
p2.Start();
p3.Start();

// Если первый сгенерирует число 323, второй - 593, третий 201, то получим = 1117, то есть на окне: 323 + 593 + 201 = 1117

Console.ReadKey(); // Предотвратим завершение потока до момента исполнения потоков (они фоновые)