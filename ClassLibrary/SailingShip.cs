using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class SailingShip : Ship
    {
        public const string className = "Парусник";
        public const int classId = 2;

        public int MaxLoadWeight { get; }

        public int NowLoadWeight { get; set; }

        public int Fare { get; }

        public SailingShip(string name, int price, int speed, int startedLocationId, int maximumLoadWeight, int fare)
            : base(name, speed, price, startedLocationId)
        {
            this.MaxLoadWeight = maximumLoadWeight;
            this.Fare = fare;
        }

        public int SendSail(int futureLocationId, DateTime date, int loadWeight)
        {
            if (loadWeight < 1 || loadWeight > this.MaxLoadWeight || loadWeight < 1 || futureLocationId == LOCATION_ID_ON_WAY || futureLocationId == this.LocationId)
            {
                return 0;
            }
            else
            {
                int distance = GetDistance(this.LocationId, futureLocationId);

                this.NowLoadWeight = loadWeight;
                this.SendSail(futureLocationId, distance, date, this.Fare * distance);

                return NowLoadWeight;
            }
        }

        /// <summary>
        /// Прибытие
        /// </summary>
        /// <returns></returns>
        public int setArrived()
        {
            this.NowLoadWeight = 0;

            return this.Arrived();
        }

        public override string ToString()
        {
            return $"{className}\n" +
                $"{base.ToString()}\n" +
                $"макс. масса груза: {MaxLoadWeight}\n" +
                $"цена за единицу груза: {Fare}\n";

        }
    }
}
