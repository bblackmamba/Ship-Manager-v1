using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Класс корабля
    /// </summary>
    public class Ship
    {
        public const int LOCATION_ID_ON_WAY = 1;
        public const int LOCATION_ID_LONDON = 2;
        public const int LOCATION_ID_NEW_YORK = 3;
        public const int LOCATION_ID_SYDNEY = 4;
        public const int LOCATION_ID_SHANGHAI = 5;
        public const int LOCATION_ID_VLADIVOSTOK = 6;


        public const string LOCATION_VALUE_ON_WAY = "в пути";
        public const string LOCATION_VALUE_LONDON = "Лондон";
        public const string LOCATION_VALUE_NEW_YORK = "Нью-Йорк";
        public const string LOCATION_VALUE_SYDNEY = "Сидней";
        public const string LOCATION_VALUE_SHANGHAI = "Шанхай";
        public const string LOCATION_VALUE_VLADIVOSTOK = "Владивосток";

        /// <summary>
        /// Название корабля
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Скорость корабля
        /// </summary>
        public int Speed { get; }

        /// <summary>
        /// Цена корабля
        /// </summary>
        public int Price { get; }

        /// <summary>
        /// Оплачен ли корабль
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Id будущей локации
        /// </summary>
        public int FutureLocationId { get; set; }

        /// <summary>
        /// Id локаци где находится корабль
        /// </summary>
        public int LocationId;

        /// <summary>
        /// Цена за плавание
        /// </summary>
        public int ArrivalPrice { get; set; }

        /// <summary>
        /// Дата прибытия
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name"></param>
        /// <param name="speed"></param>
        /// <param name="price"></param>
        /// <param name="startedLocationId"></param>
        public Ship(string name, int speed, int price, int startedLocationId)
        {
            if (startedLocationId == 1)
            {
                return;
            }

            Name = name;
            Speed = speed;
            Price = price;
            this.LocationId = startedLocationId;
            this.ArrivalDate = DateTime.MinValue;
            this.IsPaid = false;
        }

        /// <summary>
        /// Возвращение локации
        /// </summary>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        static public string GetLocationById(int LocationId)
        {
            switch (LocationId)
            {
                case LOCATION_ID_ON_WAY:
                    return LOCATION_VALUE_ON_WAY;
                case LOCATION_ID_LONDON:
                    return LOCATION_VALUE_LONDON;
                case LOCATION_ID_NEW_YORK:
                    return LOCATION_VALUE_NEW_YORK;
                case LOCATION_ID_SYDNEY:
                    return LOCATION_VALUE_SYDNEY;
                case LOCATION_ID_SHANGHAI:
                    return LOCATION_VALUE_SHANGHAI;
                case LOCATION_ID_VLADIVOSTOK:
                    return LOCATION_VALUE_VLADIVOSTOK;
                default:
                    return null;
            }
        }

        public string GetNowLocation()
        {
            return GetLocationById(LocationId);
        }

        public string GetFutureLocation()
        {
            return GetLocationById(FutureLocationId);
        }

        /// <summary>
        /// Установка локации
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="distance"></param>
        /// <param name="nowDate"></param>
        /// <param name="arrivalPrice"></param>
        public void SendSail(int futureLocationId, int distance, DateTime date, int arrivalPrice)
        {
            if (Speed == 0)
            {
                Console.WriteLine("Ошибка: скорость равна нулю");
                return;
            }

            int daysOnWay = distance / this.Speed;

            this.ArrivalDate = date.AddDays(daysOnWay);
            this.ArrivalPrice = arrivalPrice;
            this.FutureLocationId = futureLocationId;
            this.LocationId = LOCATION_ID_ON_WAY;
        }

        /// <summary>
        /// Отдает дистанцию между городами
        /// </summary>
        /// <param name="firstSity"></param>
        /// <param name="secondSity"></param>
        /// <returns></returns>
        static public int GetDistance(int firstSity, int secondSity)
        {
            string id = $"{firstSity}{secondSity}";
            switch (id)
            {
                case "23":
                    return 30;
                case "24":
                    return 60;
                case "25":
                    return 42;
                case "26":
                    return 48;
                case "32":
                    return 30;
                case "34":
                    return 48;
                case "35":
                    return 54;
                case "36":
                    return 60;
                case "42":
                    return 60;
                case "43":
                    return 48;
                case "45":
                    return 30;
                case "46":
                    return 36;
                case "52":
                    return 42;
                case "53":
                    return 54;
                case "54":
                    return 30;
                case "56":
                    return 18;
                case "62":
                    return 48;
                case "63":
                    return 60;
                case "64":
                    return 36;
                case "65":
                    return 18;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Прибытие
        /// </summary>
        /// <returns> Возвращвет сумму за плавание </returns>
        protected int Arrived()
        {
            this.LocationId = this.FutureLocationId;
            this.FutureLocationId = LOCATION_ID_ON_WAY;

            return this.ArrivalPrice;
        }


        /// <summary>
        /// покупка
        /// </summary>
        public void buy()
        {
            this.IsPaid = true;
        }

        public override string ToString()
        {
            return $"{Name}, \n" +
                $"скорость: {Speed}\n" +
                $"Цена: {Price}";
        }
    }
}
