using System;

namespace Aircompany.Planes
{
    public class PassengerPlane : Plane
    {
        public int PassengerCapacity { get; private set; }

        public PassengerPlane(string model, int maxSpeed, int maxFlightDistance, int maxLoadCapacity, int passengersCapacity)
            :base(model, maxSpeed, maxFlightDistance, maxLoadCapacity)
        {
            PassengerCapacity = passengersCapacity;
        }

        public override string ToString()
        {
            return $"Plane{{model='{Model}', " +
                $"maxSpeed={MaxSpeed}, " +
                $"maxFlightDistance={MaxFlightDistance}, " +
                $"maxLoadCapacity={MaxLoadCapacity}, " +
                $"passengerCapacity={PassengerCapacity}}}";
        }

        public override bool Equals(object obj)
        {
            var plane = obj as PassengerPlane;
            return plane != null &&
                   base.Equals(obj) &&
                   PassengerCapacity == plane.PassengerCapacity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Model, MaxSpeed, MaxFlightDistance, MaxLoadCapacity, PassengerCapacity);
        }
    }
}
