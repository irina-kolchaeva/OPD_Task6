// See https://aka.ms/new-console-template for more information

namespace OPDTask6
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Выберите действие: ");
            Console.WriteLine("1 - добавить запись в список дел");
            Console.WriteLine("2 - вывести все данные из бд");
            Console.WriteLine("3 - изменить дату сдачи задания из списка дел");
            Console.WriteLine("4 - удалить данные");
            Console.WriteLine("0 - выход из программы");
            while(true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': Add();break;
                    case '2': Read(); break;
                    case '3': Update(); break;
                    case '4': Remove(); break;
                    case '0': System.Environment.Exit(0); break;
                    default: break;
                }
            }
        }

        static void Add()
        {
            Console.WriteLine("\n*************************************************************************************************");
            Console.WriteLine("Введите дату, когда нужно сдать задание: ");
            string date = Console.ReadLine();
            Console.WriteLine("Введите задание: ");
            string name = Console.ReadLine();
            //bool a = Int32.TryParse(Console.ReadLine(), out age);
            //if (name2 != null && a)
            if (date != null)
            {
                using (MiniApp app = new MiniApp())
                {
                    app.Tasks.Add(new Todo_list() { Date = date, Name = name });
                    app.SaveChanges();
                    Console.WriteLine("Данные успешно добавлены!");
                }
            }
            else
            {
                Console.WriteLine("Введите все данные!!!");
                Add();
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню\n");
            if (char.ToLower(Console.ReadKey(true).KeyChar) != null) Main();
           
        }

        static void Read()
        {
            Console.WriteLine("\n*************************************************************************************************");
            using (MiniApp app = new MiniApp())
            {
                var tasks = app.Tasks.ToList();
                if (tasks.Count > 0)
                {
                    Console.WriteLine("  Id" + "".PadRight(4) +
                        "Дата сдачи".PadRight(14) + "Задание");
                    foreach(var u in tasks)
                    {
                        Console.WriteLine("".PadRight(2) + u.Id + "".PadRight(5) + u.Date.PadRight(14) + u.Name);
                    }
                }
                else
                {
                    Console.WriteLine("Список дел пуст");
                }
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню\n");
            if (char.ToLower(Console.ReadKey(true).KeyChar) != null) Main();
            
        }

        static void Update()
        {
            Console.WriteLine("\n*************************************************************************************************");
            Console.WriteLine("Введите Id объекта, имя которого вы хотите обновить: ");
            int id;
            bool check = Int32.TryParse(Console.ReadLine(), out id);    
            if(check)
            {
                Todo_list task = null;
                using (MiniApp app = new MiniApp())
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
                            Console.WriteLine("Данные успешно изменены!");
                        }
                        app.SaveChanges();

                    }
                }
            }
            else
            {
                Console.WriteLine("Неккоректно введенное Id");
                Update();
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню\n");
            if (char.ToLower(Console.ReadKey(true).KeyChar) != null) Main();
            
        }

        static void Remove()
        {
            Console.WriteLine("\n*************************************************************************************************");
            Console.WriteLine("Введите Id объекта, которого вы хотите удалить: ");
            int id;
            bool check = Int32.TryParse(Console.ReadLine(), out id);
            if (check)
            {
                Todo_list task = null;
                using(MiniApp app = new MiniApp())
                {
                    try
                    {
                        task = app.Tasks.Find(id);
                        app.Tasks.Remove(task);
                        app.SaveChanges();
                        Console.WriteLine("Данные успешно удалены!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Id отсутствует в базе данных");
                    }
                }
            }
            else
            {
                Console.WriteLine("Введено некорректное значение");
                Remove();
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню\n");
            if (char.ToLower(Console.ReadKey(true).KeyChar) != null) Main();
            
        }
    }
}
