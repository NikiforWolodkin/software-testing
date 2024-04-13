using Aircompany.Models;
using Aircompany.Planes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aircompany
{
    public class Airport
    {
        public List<Plane> _planes;

        public Airport(IEnumerable<Plane> planes)
        {
            _planes = planes.ToList();
        }

        public List<PassengerPlane> PassengerPlanes 
            => _planes.OfType<PassengerPlane>().ToList();

        public List<MilitaryPlane> MilitaryPlanes 
            => _planes.OfType<MilitaryPlane>().ToList();

        public PassengerPlane PassengerPlaneWithMaxPassengersCapacity 
            => PassengerPlanes.Aggregate((firstPlane, secondPlane) 
                => firstPlane.PassengerCapacity > secondPlane.PassengerCapacity ? firstPlane : secondPlane);

        public List<MilitaryPlane> TransportMilitaryPlanes 
            => MilitaryPlanes.Where(plane => plane.Type == MilitaryType.Transport).ToList();

        public Airport SortByMaxDistance()
        {
            return new Airport(_planes.OrderBy(plane => plane.MaxFlightDistance));
        }

        public Airport SortByMaxSpeed()
        {
            return new Airport(_planes.OrderBy(plane => plane.MaxSpeed));
        }

        public Airport SortByMaxLoadCapacity()
        {
            return new Airport(_planes.OrderBy(plane => plane.MaxLoadCapacity));
        }

        public IEnumerable<Plane> Planes => _planes;

        public override string ToString()
        {
            return $"Airport{{planes={string.Join(", ", _planes.Select(plane => plane.Model))}}}";
        }
    }
}
