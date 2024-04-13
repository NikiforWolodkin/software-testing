using Aircompany.Models;
using System;

namespace Aircompany.Planes
{
    public class MilitaryPlane : Plane
    {
        public MilitaryType Type { get; private set; }

        public MilitaryPlane(string model, int maxSpeed, int maxFlightDistance, int maxLoadCapacity, MilitaryType type)
            : base(model, maxSpeed, maxFlightDistance, maxLoadCapacity)
        {
            Type = type;
        }

        public override string ToString()
        {
            return $"Plane{{model='{Model}', " +
                $"maxSpeed={MaxSpeed}, " +
                $"maxFlightDistance={MaxFlightDistance}, " +
                $"maxLoadCapacity={MaxLoadCapacity}, " +
                $"type={Type}}}";
        }

        public override bool Equals(object obj)
        {
            var plane = obj as MilitaryPlane;
            return plane != null &&
                   base.Equals(obj) &&
                   Type == plane.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Model, MaxSpeed, MaxFlightDistance, MaxLoadCapacity, Type);
        }
    }
}
