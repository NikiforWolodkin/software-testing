using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Aircompany.Planes
{
    public abstract class Plane
    {
        public string Model { get; private set; }
        public int MaxSpeed { get; private set; }
        public int MaxFlightDistance { get; private set; }
        public int MaxLoadCapacity { get; private set; }

        public Plane(string model, int maxSpeed, int maxFlightDistance, int maxLoadCapacity)
        {
            Model = model;
            MaxSpeed = maxSpeed;
            MaxFlightDistance = maxFlightDistance;
            MaxLoadCapacity = maxLoadCapacity;
        }

        public override string ToString()
        {
            return $"Plane{{model='{Model}', " +
                $"maxSpeed={MaxSpeed}, " +
                $"maxFlightDistance={MaxFlightDistance}, " +
                $"maxLoadCapacity={MaxLoadCapacity}}}";
        }

        public override bool Equals(object obj)
        {
            var plane = obj as Plane;
            return plane != null &&
                   Model == plane.Model &&
                   MaxSpeed == plane.MaxSpeed &&
                   MaxFlightDistance == plane.MaxFlightDistance &&
                   MaxLoadCapacity == plane.MaxLoadCapacity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Model, MaxSpeed, MaxFlightDistance, MaxLoadCapacity);
        }    
    }
}
