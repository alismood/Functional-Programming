# FunctionalDeliveryCalculator

**Author:** Ali Zhabaikhan

## Run Instructions

1. Open the `FunctionalDeliveryCalculator` solution in JetBrains Rider.
2. Ensure the startup project is set to `FunctionalDeliveryCalculator`.
3. Press **Shift + F10** (or click the green **Run** play button in the top toolbar) to compile and execute the console application.
4. When the console window appears, sequentially input the base delivery price, number of items, express status (`true`/`false`), delivery type, and delivery zone when prompted.
5. The application will output the final calculated price or exit gracefully if the input is invalid.

## Architecture & Functional Concepts

**Which parts of your program handle user input and output?**
The `Main` method serves as the imperative boundary for the application, handling all `Console.ReadLine()` and `Console.WriteLine()` operations. By keeping user input and console output strictly inside `Main`, the application separates the unpredictable outside world from the deterministic calculation logic, following the "Functional Core, Imperative Shell" architectural pattern.

**Which functions perform only delivery price calculations?**
The static methods (`CalculateItemAdjustment`, `CalculateZoneAdjustment`) and the lambda expressions (`applyTypeRule`, `applyExpressRule`) are dedicated solely to calculations. These are pure functions because they rely strictly on their input parameters to generate a new value, do not read from or write to the console, and do not mutate any global state or cause side effects.

**How is `Func<...>` used to apply delivery pricing rules?**
`Func<...>` delegates are used to store mathematical operations as portable pieces of data, allowing functions to be passed in as parameter variables. In this application, specific pricing rules (like the express or delivery type multipliers) are assigned to `Func` delegates and injected into the higher-order `ApplyRule` function, which executes the rule against the current price state.

**Why is TryParse useful when processing delivery data entered by the user?**
`TryParse` prevents unhandled exceptions by returning a boolean indicating success or failure instead of crashing the program. This allows the application to utilize predictable control flow (such as early returns) to elegantly handle expected invalid user input and display clear error messages.

## Test Cases

The following test cases cover varied delivery configurations and expected invalid input scenarios.

| Base Price | Items | Express | Delivery Type | Delivery Zone | Expected Output | Rationale |
| --- | --- | --- | --- | --- | --- | --- |
| `100` | `5` | `false` | `Pickup` | `City` | **88.00** | Items add 10% (110). Pickup reduces 20% (88). Zone and Express add nothing. |
| `100` | `8` | `true` | `Courier` | `Remote` | **156.00** | Items add 20% (120). Courier/Remote add nothing. Express adds 30% (156). |
| `50` | `2` | `false` | `DoorToDoor` | `OutsideCity` | **71.88** | Items add 0%. DoorToDoor adds 15% (57.50). OutsideCity adds 25% (71.875 -> 71.88). |
| `-10` | `4` | `false` | `Pickup` | `City` | **Error Msg** | `basePrice` is negative; triggers early return. |
| `100` | `5` | `false` | `Drone` | `City` | **Error Msg** | `Drone` is not a valid enum value; `Enum.TryParse` fails. |
| `100` | `0` | `false` | `Courier` | `City` | **Error Msg** | Item count is less than 1; triggers early return. |

---

**Prompt feedback:**
Your prompt successfully bypassed introductory back-and-forth by directly requesting the final deliverable and linking the exact notebook sources needed. To make it even more effective next time, explicitly state if you want me to infer the answers based on the code we wrote previously, or if you simply want a blank template to fill in yourself for the defense.
