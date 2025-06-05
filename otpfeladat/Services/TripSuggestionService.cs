using System;
using System.Collections.Generic;
using System.Linq;
using otpfeladat.Interfaces;
using otpfeladat.Models;

namespace otpfeladat.Services
{
    public class TripSuggestionService : ITripSuggestionService
    {
        public IEnumerable<Dtos> GetSuggestions(int passengerCount, int distance, List<Vehicle> vehicles)
        {
            var combos = GetVehicleCombinations(vehicles);

            var validCombos = combos.Where(c =>
                c.Sum(v => v.PassengerCapacity) >= passengerCount &&
                c.Sum(v => CalculateEffectiveRange(v, distance)) >= distance
            ).ToList();

            var suggestions = validCombos.Select(c => new Dtos
            {
                Vehicles = c.Select(v => new VehicleDto
                {
                    Id = v.Id,
                    PassengerCapacity = v.PassengerCapacity,
                    Range = v.Range,
                    Fuel = v.Fuel
                }).ToList(),
                Profit = CalculateProfit(c, passengerCount, distance)
            }).OrderByDescending(s => s.Profit);

            return suggestions;
        }

        private IEnumerable<List<Vehicle>> GetVehicleCombinations(List<Vehicle> vehicles, int maxCount = 3)
        {
            List<List<Vehicle>> results = new();

            foreach (var v1 in vehicles)
            {
                results.Add(new List<Vehicle> { v1 });
            }

            foreach (var v1 in vehicles)
            {
                foreach (var v2 in vehicles)
                {
                    results.Add(new List<Vehicle> { v1, v2 });
                }
            }

            foreach (var v1 in vehicles)
            {
                foreach (var v2 in vehicles)
                {
                    foreach (var v3 in vehicles)
                    {
                        results.Add(new List<Vehicle> { v1, v2, v3 });
                    }
                }
            }

            return results;
        }

        private int CalculateEffectiveRange(Vehicle vehicle, int distance)
        {
            if (vehicle.Fuel == FuelType.MildHybrid)
            {
                int cityDistance = Math.Min(distance, 50);
                int highwayDistance = Math.Max(distance - 50, 0);

                int cityRangeUsed = cityDistance / 2;
                int highwayRangeUsed = highwayDistance;

                return vehicle.Range - (cityRangeUsed + highwayRangeUsed);
            }

            return vehicle.Range;
        }

        private double CalculateProfit(List<Vehicle> vehicles, int passengerCount, int distance)
        {
            double travelFee = passengerCount * 2 * distance;

            int cityDistance = Math.Min(distance, 50);
            int highwayDistance = Math.Max(distance - 50, 0);
            int travelTimeMinutes = (cityDistance * 2) + highwayDistance;
            int halfHours = (int)Math.Ceiling(travelTimeMinutes / 30.0);

            travelFee += passengerCount * halfHours * 2;

            double refuelCost = 0;
            foreach (var vehicle in vehicles)
            {
                refuelCost += distance * (vehicle.Fuel == FuelType.PureElectric ? 1 : 2);
            }

            return travelFee - refuelCost;
        }

        
    }
}
