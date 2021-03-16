using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Steamship : Ship
    {
        public const string className = "Параход";
        public const int classId = 1;

        public int MaxNumberOfPassengers { get; }

        public int CountNowPassengers { get; set; }

        public int Fare { get; }

        public Steamship(string name, int price, int speed, int startedLocationId, int maxNumberOfPassengers, int fare)
            : base(name, speed, price, startedLocationId)
        {
            this.MaxNumberOfPassengers = maxNumberOfPassengers;
            this.Fare = fare;
        }

        /// <summary>
        /// Отправить в плавание
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="date"></param>
        /// <param name="countPassengers"></param>
        public int SendSail(int futureLocationId, DateTime date, int countPassengers)
        {
            if (countPassengers < 1 || countPassengers > this.MaxNumberOfPassengers || countPassengers < 1 || futureLocationId == LOCATION_ID_ON_WAY || futureLocationId == this.LocationId)
            {
                return 0;
            }
            else
            {
                int distance = GetDistance(this.LocationId, futureLocationId);

                this.CountNowPassengers = countPassengers;
                base.SendSail(futureLocationId, distance, date, this.Fare * distance);

                return CountNowPassengers;
            }
        }

        /// <summary>
        /// Прибытие
        /// </summary>
        /// <returns></returns>
        public int setArrived()
        {
            this.CountNowPassengers = 0;

            return this.Arrived();
        }

        public override string ToString()
        {
            return $"{className}\n" +
                $"{base.ToString()}\n" +
                $"макс.количество пассажиров: {MaxNumberOfPassengers}\n" +
                $"цена за проезд: {Fare}\n";
        }
    }
}
