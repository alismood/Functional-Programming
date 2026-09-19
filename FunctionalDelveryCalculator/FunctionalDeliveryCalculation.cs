using System;

namespace FunctionalDeliveryCalculator
{
    public enum DeliveryType { Pickup, Courier, DoorToDoor }
    public enum DeliveryZone { City, OutsideCity, Remote }

    class FunctionalDeliveryCalculation
    {
        // 1. Higher-Order Function (Expression-Bodied)
        static decimal ApplyRule(decimal price, Func<decimal, decimal> rule) => rule(price);

        // 2. Static Methods
        static decimal CalculateItemAdjustment(decimal price, int items)
        {
            if (items >= 8) return price * 1.20m;
            if (items >= 4) return price * 1.10m;
            return price;
        }

        static decimal CalculateZoneAdjustment(decimal price, DeliveryZone zone) =>
            zone switch
            {
                DeliveryZone.OutsideCity => price * 1.25m,
                _ => price // City and Remote apply no charge
            };

        static void Main()
        {
            // Safe User Input
            Console.WriteLine("Enter delivery price:");
            if (!decimal.TryParse(Console.ReadLine(), out var basePrice) || basePrice < 0)
            {
                Console.WriteLine("Error");
                return; // Early return on invalid input
            }

            Console.WriteLine("Enter number of items:");
            if (!int.TryParse(Console.ReadLine(), out var items) || items < 1)
            {
                Console.WriteLine("Error");
                return;
            }

            Console.WriteLine("Express delivery? (true/false):");
            if (!bool.TryParse(Console.ReadLine(), out var isExpress))
            {
                Console.WriteLine("Error");
                return;
            }

            Console.WriteLine("Delivery Type (Pickup, Courier, DoorToDoor):");
            if (!Enum.TryParse<DeliveryType>(Console.ReadLine(), true, out var deliveryType))
            {
                Console.WriteLine("Error");
                return;
            }

            Console.WriteLine("Delivery Zone (City, OutsideCity, Remote):");
            if (!Enum.TryParse<DeliveryZone>(Console.ReadLine(), true, out var deliveryZone))
            {
                Console.WriteLine("Error");
                return;
            }

            // 4. Lambda Expressions
            Func<decimal, DeliveryType, decimal> applyTypeRule = (p, t) => t switch
            {
                DeliveryType.Pickup => p * 0.80m,
                DeliveryType.DoorToDoor => p * 1.15m,
                _ => p
            };

            Func<decimal, bool, decimal> applyExpressRule = (p, e) => e ? p * 1.30m : p;

            // 5. Functional Pipeline (Immutable State)
            var priceAfterItems = ApplyRule(basePrice, p => CalculateItemAdjustment(p, items));
            var priceAfterType = ApplyRule(priceAfterItems, p => applyTypeRule(p, deliveryType));
            var priceAfterZone = ApplyRule(priceAfterType, p => CalculateZoneAdjustment(p, deliveryZone));
            var priceAfterExpress = ApplyRule(priceAfterZone, p => applyExpressRule(p, isExpress));
            var finalPrice = Math.Round(priceAfterExpress, 2);

            // 6. Output
            Console.WriteLine($"Final Delivery Cost: {finalPrice:0.00}");
        }
    }
} 