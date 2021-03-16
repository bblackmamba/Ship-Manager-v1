using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace ship_manager_v_1
{
    public class Program
    {
        static public List<SailingShip> sailingShips= new List<SailingShip>();
        static public List<Steamship> steamships = new List<Steamship>();
        static public List<Corvette> corvettes = new List<Corvette>();

        static private DateTime nowDate = DateTime.Now;
        static private int Balance = 1500;

        static public void Fixture()
        {
            sailingShips.Add(new SailingShip("ыы", 130, 1, Ship.LOCATION_ID_LONDON, 200, 12));
            sailingShips.Add(new SailingShip("фф", 110, 2, Ship.LOCATION_ID_NEW_YORK, 200, 11));
            sailingShips.Add(new SailingShip("йй", 150, 3, Ship.LOCATION_ID_SYDNEY, 200, 8));
            sailingShips.Add(new SailingShip("ее", 170, 1, Ship.LOCATION_ID_SHANGHAI, 2000, 5));

            steamships.Add(new Steamship("ыы", 130, 2, Ship.LOCATION_ID_LONDON, 200, 12));
            steamships.Add(new Steamship("фф", 110, 2, Ship.LOCATION_ID_NEW_YORK, 200, 11));
            steamships.Add(new Steamship("йй", 150, 3, Ship.LOCATION_ID_SHANGHAI, 200, 8));
            steamships.Add(new Steamship("ее", 170, 1, Ship.LOCATION_ID_SYDNEY, 2000, 5));

            corvettes.Add(new Corvette("ыы", 130, 1, Ship.LOCATION_ID_LONDON, 20, 12));
            corvettes.Add(new Corvette("фф", 110, 2, Ship.LOCATION_ID_VLADIVOSTOK, 200, 11));
            corvettes.Add(new Corvette("йй", 150, 3, Ship.LOCATION_ID_NEW_YORK, 200, 8));
            corvettes.Add(new Corvette("ее", 170, 1, Ship.LOCATION_ID_SYDNEY, 2000, 5));
        }

        static void Main(string[] args)
        {
            if (nowDate == DateTime.MinValue)
            {
                Console.WriteLine("У вас стоит некорректное время, программа не может работать.");
                Console.ReadLine();

                return;
            }

            Fixture();
            bool ProgramIsActive = true;
            int Button;
            while (ProgramIsActive)
            {
                Console.WriteLine(nowDate.ToLongDateString());
                Console.WriteLine($"Ваш баланс: {Balance}");
                Console.WriteLine("\nВыберите действие: ");
                Console.WriteLine("\n1.Отправить корабль в плавание " +
                    "\n2.Просмотреть свои корабли " +
                    "\n3.Купить новый корабль " +
                    "\n4.Следущий день" +
                    "\n5.Закончить работу программы");
                switch (inputButton(1, 5))
                {
                    case 1:
                        Console.Clear();
                        sendSailShip();
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 2:
                        Console.Clear();
                        printIsPaidShip();
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 3:
                        Console.Clear();
                        buyShip();
                        Console.Clear();
                        break;

                    case 4:
                        nowDate = nowDate.AddDays(1);
                        Console.Clear();
                        printEventArrival();
                        break;

                    case 5:
                        Console.Clear();
                        ProgramIsActive = closeProgram();
                        break;

                    default:
                        Console.Clear(); 
                        return;
                }
            }
        }

        static public int selectTypeShip()
        {
            Console.WriteLine($"Какого типа корабль:\n" +
                $"1.{Steamship.className}\n" +
                $"2.{SailingShip.className}\n" +
                $"3.{Corvette.className}\n");

            return inputButton(1, 3);
        }

        static public void sendSailShip()
        {
            switch (selectTypeShip())
            {
                case Steamship.classId:
                    Console.Clear();
                    sendSailSteamship();
                    break;
                case SailingShip.classId:
                    Console.Clear();
                    sendSailSailingShip();
                    break;
                case Corvette.classId:
                    Console.Clear();
                    sendSailCorvette();
                    break;
                default:
                    return;
            }
        }

        static public void sendSailSteamship()
        {
            List<Steamship> steamshipsIsPaid = getIsPaidListSteamships();
            Console.WriteLine("Выберите нужный вам корабль:");

            if (printListSteamship(steamshipsIsPaid) == 0)
            {
                Console.WriteLine("У вас пока что нету ни одного парахода.");
                Console.ReadLine();

                return;
            }

            int id = inputButton(1, steamshipsIsPaid.Count()) - 1;

            Console.Clear();

            Steamship steamship = steamshipsIsPaid[id];
            int locationId;
            while (true)
            {
                locationId = selectLocation();
                if (locationId == steamship.LocationId)
                {
                    Console.Clear();
                    Console.WriteLine("Корабль уже в этом порту. Выберите коректный порт.\n");
                }
                else
                {
                    break;
                }
            }
            var rand = new Random();

            Console.WriteLine($"Количество пассажиров: {steamship.SendSail(locationId, nowDate, rand.Next(steamships[id].MaxNumberOfPassengers))}");
            Console.WriteLine($"{steamship.LocationId}");
        }

        static public void sendSailSailingShip()
        {
            List<SailingShip> sailingShipsIsPaid = getIsPaidListSailingShips();
            Console.WriteLine("Выберите нужный вам корабль:");

            if (printListSailingShip(sailingShipsIsPaid) == 0)
            {
                Console.WriteLine("У вас пока что нету ни одного парахода.");
                Console.ReadLine();

                return;
            }

            int id = inputButton(1, sailingShipsIsPaid.Count()) - 1;

            Console.Clear();

            SailingShip sailingShip = sailingShipsIsPaid[id];
            int locationId;

            while (true)
            {
                locationId = selectLocation();
                if (locationId == sailingShip.LocationId)
                {
                    Console.Clear();
                    Console.WriteLine("Корабль уже в этом порту. Выберите коректный порт.\n");
                }
                else
                {
                    break;
                }
            }

            var rand = new Random();
            Console.WriteLine($"Количество груза: {sailingShip.SendSail(locationId, nowDate, rand.Next(steamships[id].MaxNumberOfPassengers))}");
        }

        static public void sendSailCorvette()
        {
            List<Corvette> corvettesIsPaid = getIsPaidListCorvettes();
            Console.WriteLine("Выберите нужный вам корабль:");

            if (printListCorvette(corvettesIsPaid) == 0)
            {
                Console.WriteLine("У вас пока что нету ни одного парахода.");
                Console.ReadLine();

                return;
            }

            int id = inputButton(1, corvettesIsPaid.Count()) - 1;

            Console.Clear();

            Corvette corvette = corvettesIsPaid[id];
            int locationId;

            while (true)
            {
                locationId = selectLocation();
                if (locationId == corvette.LocationId)
                {
                    Console.Clear();
                    Console.WriteLine("Корабль уже в этом порту. Выберите коректный порт.\n");
                }
                else
                {
                    break;
                }
            }

            var rand = new Random();
            Console.WriteLine($"Срок аренды: {corvette.SendSail(locationId, nowDate, rand.Next(steamships[id].MaxNumberOfPassengers))}");
        }

        static public int selectLocation()
        {
            Console.WriteLine($"Куда отправить пароход(ведите номер города): \n" +
                $"{Ship.LOCATION_ID_LONDON}.{Ship.LOCATION_VALUE_LONDON}\n" +
                $"{Ship.LOCATION_ID_NEW_YORK}.{Ship.LOCATION_VALUE_NEW_YORK}\n" +
                $"{Ship.LOCATION_ID_SHANGHAI}.{Ship.LOCATION_VALUE_SHANGHAI}\n" +
                $"{Ship.LOCATION_ID_SYDNEY}.{Ship.LOCATION_VALUE_SYDNEY}\n" +
                $"{Ship.LOCATION_ID_VLADIVOSTOK}.{Ship.LOCATION_VALUE_VLADIVOSTOK}\n");

            return (inputButton(2, 6));
        }

        static public int printIsPaidShip()
        {
            int countShipsIsPaid = 0;
            countShipsIsPaid += printListSteamship(getIsPaidListSteamships());
            Console.WriteLine("\n");
            countShipsIsPaid += printListSailingShip(getIsPaidListSailingShips());
            Console.WriteLine("\n");
            countShipsIsPaid += printListCorvette(getIsPaidListCorvettes());
            Console.WriteLine("\n");

            if (countShipsIsPaid == 0)
            {
                Console.WriteLine("У вас нет купленых кораблей.");
            }

            return countShipsIsPaid;
        }

        static public List<Corvette> getIsPaidListCorvettes()
        {
            List<Corvette> resultList = new List<Corvette>();

            foreach (Corvette corvette in corvettes)
            {
                if (corvette.IsPaid)
                {
                    resultList.Add(corvette);
                }
            }

            if (resultList.Count == 0)
            {
                return null;
            }

            return resultList;
        }

        static public List<SailingShip> getIsPaidListSailingShips()
        {
            List<SailingShip> resultList = new List<SailingShip>();

            foreach (SailingShip sailingShip in sailingShips)
            {
                if (sailingShip.IsPaid)
                {
                    resultList.Add(sailingShip);
                }
            }

            if (resultList.Count == 0)
            {
                return null;
            }

            return resultList;
        }

        static public List<Steamship> getIsPaidListSteamships()
        {
            List<Steamship> resultList = new List<Steamship>();

            foreach (Steamship steamship in steamships)
            {
                if (steamship.IsPaid)
                {
                    resultList.Add(steamship);
                }
            }

            if (resultList.Count == 0)
            {
                return null;
            }

            return resultList;
        }
        static public int printListCorvette(List<Corvette> corvettes)
        {
            if (corvettes == null)
            {
                return 0;
            }

            for (int i = 0; i < corvettes.Count(); ++i)
            {
                Console.WriteLine($"{i + 1}.{corvettes[i].ToString()}\n" +
                    $"Местонахождение: {corvettes[i].GetNowLocation()}");

                if (corvettes[i].LocationId == Ship.LOCATION_ID_ON_WAY)
                {
                    Console.WriteLine($"На пути в {corvettes[i].GetFutureLocation()}");
                    Console.WriteLine($"Прибудет {corvettes[i].ArrivalDate.ToLongDateString()}");
                }

                Console.WriteLine("\n\n");
            }

            return corvettes.Count();
        }

        static public int printListSteamship(List<Steamship> steamships)
        {
            if (steamships == null)
            {
                return 0;
            }

            for (int i = 0; i < steamships.Count(); ++i)
            {
                Console.WriteLine($"{i + 1}.{steamships[i].ToString()}" +
                    $"Местонахождение: {steamships[i].GetNowLocation()}");

                if (steamships[i].LocationId == Ship.LOCATION_ID_ON_WAY)
                {
                    Console.WriteLine($"На пути в {steamships[i].GetFutureLocation()}");
                    Console.WriteLine($"Прибудет {steamships[i].ArrivalDate.ToLongDateString()}");
                }

                Console.WriteLine("\n\n");
            }

            return steamships.Count();
        }

        static public int printListSailingShip(List<SailingShip> sailingShips)
        {
            if (sailingShips == null)
            {
                return 0;
            }

            for (int i = 0; i < sailingShips.Count(); ++i)
            {
                Console.WriteLine($"{i + 1}.{sailingShips[i].ToString()}" +
                    $"Местонахождение: {sailingShips[i].GetNowLocation()}");

                if (sailingShips[i].LocationId == Ship.LOCATION_ID_ON_WAY)
                {
                    Console.WriteLine($"На пути в {sailingShips[i].GetFutureLocation()}");
                    Console.WriteLine($"Прибудет {sailingShips[i].ArrivalDate.ToLongDateString()}");
                }

                Console.WriteLine("\n\n");
            }

            return sailingShips.Count();
        }

        static public void buyShip()
        { 
            switch (selectTypeShip())
            {
                case Steamship.classId:
                    Console.Clear();
                    buySteamship();
                    break;
                case SailingShip.classId:
                    Console.Clear();
                    buySailingShip();
                    break;
                case Corvette.classId:
                    Console.Clear();
                    buyCorvette();
                    break;
                default:
                    Console.Clear();
                    return;
            }

        }

        static public void buySteamship()
        {
            Console.WriteLine("Выберите корабль который хотите приобрести(введя его номер): \n");

            for (int i = 0; i < steamships.Count; ++i)
            {
                Console.WriteLine($"{i + 1}.{steamships[i].ToString()}");
                Console.WriteLine("\n\n");
            }

            int button = inputButton(1, steamships.Count) - 1;

            if (steamships[button].IsPaid) 
            {
                return;
            } 

            if (steamships[button].Price > Balance)
            {
                Console.WriteLine("Недостаточно средств.");
                return;
            }

            steamships[button].IsPaid = true;
            Balance -= steamships[button].Price;
        }

        static public void buySailingShip()
        {
            Console.WriteLine("Выберите корабль который хотите приобрести(введя его номер): \n");

            for (int i = 0; i < sailingShips.Count; ++i)
            {
                Console.WriteLine($"{i + 1}.{sailingShips[i].ToString()}");
                Console.WriteLine("\n\n");
            }

            int button = inputButton(1, sailingShips.Count) - 1;

            if (sailingShips[button].IsPaid)
            {
                return;
            }

            if (sailingShips[button].Price > Balance)
            {
                Console.WriteLine("Недостаточно средств.");
                return;
            }

            sailingShips[button].IsPaid = true;
            Balance -= steamships[button].Price;
        }

        static public void buyCorvette()
        {
            Console.WriteLine("Выберите корабль который хотите приобрести(введя его номер): \n");

            for (int i = 0; i < corvettes.Count; ++i)
            {
                Console.WriteLine($"{i + 1}.{corvettes[i].ToString()}");
                Console.WriteLine("\n\n");
            }

            int button = inputButton(1, corvettes.Count) - 1;

            if (corvettes[button].IsPaid)
            {
                return;
            }

            if (corvettes[button].Price > Balance)
            {
                Console.WriteLine("Недостаточно средств.");
                return;
            }

            corvettes[button].IsPaid = true;
            Balance -= corvettes[button].Price;
        }

        static public void printEventArrival()
        {
            foreach(SailingShip sailingShip in sailingShips)
            {
                if (sailingShip.ArrivalDate == nowDate)
                {
                    Balance += sailingShip.ArrivalPrice;
                    Console.WriteLine($"Корабль {sailingShip.Name} окончил плавание\n" +
                        $"Ваш баланс пополнился на {sailingShip.ArrivalPrice}\n");
                }
            }

            foreach (Steamship steamship in steamships)
            {
                if (steamship.ArrivalDate == nowDate)
                {
                    Balance += steamship.ArrivalPrice;
                    Console.WriteLine($"Корабль {steamship.Name} окончил плавание\n" +
                        $"Ваш баланс пополнился на {steamship.ArrivalPrice}\n");
                }
            }

            foreach (Corvette corvette in corvettes)
            {
                if (corvette.ArrivalDate == nowDate)
                {
                    Balance += corvette.ArrivalPrice;
                    Console.WriteLine($"Корабль {corvette.Name} окончил плавание\n" +
                        $"Ваш баланс пополнился на {corvette.ArrivalPrice}\n");
                }
            }
        }

        static private int inputButton(int minValue, int maxValue)
        {
            int Button;

            Console.WriteLine("Ведите выбранный вариант: ");

            while (true)
            {
                Button = Convert.ToInt32(Console.ReadLine());
                if (Button < minValue || Button > maxValue)
                {
                    Console.WriteLine("Введено неверное значение. Повторите ввод: ");
                }
                else
                {
                    Console.WriteLine("\n");
                    return Button;
                }
            }
        }

        static private bool closeProgram()
        {
            Console.WriteLine("Вы уверены что хотите закрыть программу? \n1.Да \n2.Нет");

            if (inputButton(1, 2) == 1)
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}
