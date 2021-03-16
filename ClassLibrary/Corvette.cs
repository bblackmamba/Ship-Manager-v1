using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Corvette : Ship
    {
        public const string className = "Корвет";
        public const int classId = 3;

        public int MaximumRentalPeriod { get; }

        public int NowRentalPeriod { get; set; }

        public int RentalPrice { get; }

        public Corvette(string name, int price, int speed, int startedLocationId, int maximumRentalPeriod, int rentalPrice)
        : base(name, speed, price, startedLocationId)
        {
            MaximumRentalPeriod = maximumRentalPeriod;
            RentalPrice = rentalPrice;
        }

        public int SendSail(int futureLocationId, DateTime date, int rentalPeriod)
        {
            if (rentalPeriod < 1 || rentalPeriod > this.MaximumRentalPeriod || rentalPeriod < 1 || futureLocationId == LOCATION_ID_ON_WAY || futureLocationId == this.LocationId)
            {
                return 0;
            }
            else
            {
                int distance = GetDistance(this.LocationId, futureLocationId);

                this.NowRentalPeriod = rentalPeriod;
                this.SendSail(futureLocationId, distance, date.AddDays(rentalPeriod), this.RentalPrice * distance);

                return NowRentalPeriod;
            }
        }

        public override string ToString()
        {
            return $"{className}\n" +
                $"{base.ToString()}\n" +
                $"макс. срок аренды: {MaximumRentalPeriod}\n" +
                $"цена за день аренды: {RentalPrice}\n";
        }
    }
}
