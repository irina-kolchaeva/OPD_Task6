using System;

namespace OPDTask6
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("СПИСОК ДЕЛ");
            Console.WriteLine("1 - показать все запланированные дела");
            Console.WriteLine("2 - добавить запись в список дел");
            Console.WriteLine("3 - изменить дату сдачи задания из списка дел");
            Console.WriteLine("4 - удалить запись");
            Console.WriteLine("0 - выход из программы");
            while(true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': Read(); break;
                    case '2': Add();break;
                    case '3': Update(); break;
                    case '4': Remove(); break;
                    case '0': Environment.Exit(0); break;
                    default: break;
                }
            }
        }

        static void Read()
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            using (MiniApp app = new())
            {
                var tasks = app.Tasks.ToList();
                if (tasks.Count > 0)
                {
                    Console.WriteLine("  Id" + "".PadRight(4) + "Дата сдачи".PadRight(14) + "Задание");
                    foreach (var u in tasks)
                    {
                        Console.WriteLine("".PadRight(2) + u.Id + "".PadRight(5) + u.Date.PadRight(14) + u.Name);
                    }
                }
                else
                {
                    Console.WriteLine("Список дел пуст");
                }
            }
        }

        static void Add()
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Введите дату, когда нужно сдать задание: ");
            string date = Console.ReadLine();
            Console.WriteLine("Введите задание: ");
            string name = Console.ReadLine();
            if (date != "" && name != "")
            {
                using (MiniApp app = new())
                {
                    app.Tasks.Add(new Todo_list() { Date = date, Name = name });
                    app.SaveChanges();
                    Console.WriteLine("Запись успешно добавлена!");
                }
            }
            else
            {
                Console.WriteLine("Введите все данные для записи!");
                Add();
            }
        }

        static void Update()
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Введите Id записи, дату выполнения которой нужно обновить: ");
            int id;
            bool check = Int32.TryParse(Console.ReadLine(), out id);
            if (check)
            {
                Todo_list task = null;
                using (MiniApp app = new())
                {
                    task = app.Tasks.Find(id);
                    if (task != null)
                    {
                        Console.WriteLine("Введите новую дату сдачи");
                        string date = Console.ReadLine();
                        if (date != null)
                        {
                            task.Date = date;
                            app.Tasks.Update(task);
                            Console.WriteLine("Запись успешно изменена!");
                        }
                        app.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Id отсутствует в базе данных");
                    }
                }
            }
            else
            {
                Console.WriteLine("Неккоректно введено Id");
                Update();
            }           
        }

        static void Remove()
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Введите Id объекта, которого вы хотите удалить: ");
            int id;
            bool check = Int32.TryParse(Console.ReadLine(), out id);
            if (check)
            {
                Todo_list task = null;
                using(MiniApp app = new())
                {
                    try
                    {
                        task = app.Tasks.Find(id);
                        app.Tasks.Remove(task);
                        app.SaveChanges();
                        Console.WriteLine("Данные успешно удалены!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Id отсутствует в базе данных");
                    }
                }
            }
            else
            {
                Console.WriteLine("Неккоректно введено Id");
                Remove();
            }
        }
    }
}
