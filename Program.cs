namespace Simple_Hotel_Room_Management_Project
{
    internal class Program
    {
        // Constants and arrays to store hotel room data
        const int roomLimit = 10;   // Maximum number of rooms the system can handle
        static int[] roomNumbers = new int[roomLimit]; // Stores unique identifiers for each room
        static double[] roomRates = new double[roomLimit]; // Stores nightly rate for each room
        static bool[] isReserved = new bool[roomLimit]; // Tracks reservation status (true = reserved)
        static string[] guestNames = new string[roomLimit]; // Stores names of guests for reserved rooms
        static int[] nights = new int[roomLimit]; // Stores number of nights for each reservation
        static DateTime[] bookingDates = new DateTime[roomLimit]; // Stores when each reservation was made

        static int roomCount = 0; // Tracks how many rooms are currently in the system

        //================= main menu ===================
        static void Main()
        {
            // Continuous loop until user chooses to exit
            while (true)
            {
                // Display main menu options
                Console.WriteLine("\n============ Hotel Room Management System ============");
                Console.WriteLine("1. Add a new room");
                Console.WriteLine("2. View all rooms");
                Console.WriteLine("3. Reserve a room");
                Console.WriteLine("4. View all reservations");
                Console.WriteLine("5. Search reservation by guest name");
                Console.WriteLine("6. Find highest-paying guest");
                Console.WriteLine("7. Cancel a reservation by room number");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");

                // Process user selection
                switch (Console.ReadLine())
                {
                    case "1":
                        AddRoom(); // Add a new room to the system
                        break;
                    case "2":
                        ViewRooms(); // Display all rooms and their status
                        break;
                    case "3":
                        ReserveRoom(); // Make a room reservation
                        break;
                    case "4":
                        ViewReservations(); // Show all current reservations
                        break;
                    case "5":
                        SearchReservation(); // Find reservation by guest name
                        break;
                    case "6":
                        FindHighestPayingGuest(); // Identify guest with highest total payment
                        break;
                    case "7":
                        CancelReservation(); // Remove a reservation
                        break;
                    case "8":
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid option."); // Handle invalid input
                        break;
                }
            }
        }

        //================= add room ===================
        static void AddRoom()
        {
            Console.Clear();
            // Check if we've reached maximum room capacity
            if (roomCount >= roomLimit)
            {
                Console.WriteLine("Room limit reached.");
                return;
            }

            // Get and validate room number
            Console.Write("Enter room number: ");
            int roomNumber = int.Parse(Console.ReadLine());

            // Check for duplicate room numbers
            for (int i = 0; i < roomCount; i++)
            {
                if (roomNumbers[i] == roomNumber)
                {
                    Console.WriteLine("Room number already exists.");
                    return;
                }
            }

            // Store the new room number
            roomNumbers[roomCount] = roomNumber;

            // Get and validate room rate
            Console.Write("Enter room rate: ");
            double roomRate = double.Parse(Console.ReadLine());
            while (roomRate < 100) // Enforce minimum room rate
            {
                Console.WriteLine("Room rate must be at least 100.");
                Console.Write("Enter room rate: ");
                roomRate = double.Parse(Console.ReadLine());
                Console.ReadLine();
            }

            // Store room data and increment counter
            roomRates[roomCount] = roomRate;
            roomCount++;
            isReserved[roomCount] = false; // New rooms start as available
            Console.ReadLine();
        }

        //================= view all rooms ===================
        static void ViewRooms()
        {
            Console.Clear();
            // Loop through all rooms and display their information
            for (int i = 0; i < roomCount; i++)
            {
                Console.WriteLine("Room Number: " + roomNumbers[i]);
                Console.WriteLine("Room Rate: " + roomRates[i]);

                // Show reservation details if room is reserved
                if (isReserved[i] == true)
                {
                    Console.WriteLine("Status: the room is Reserved by:");
                    Console.WriteLine("Guest Name: " + guestNames[i]);
                    Console.WriteLine("Nights: " + nights[i]);
                    Console.WriteLine("Cost: " + (roomRates[i] * nights[i]));
                    Console.WriteLine("Booking Date: " + bookingDates[i]);
                }
                else
                {
                    Console.WriteLine("Status: Available");
                }
            }
            Console.ReadLine();
        }

        //================= reserve room ===================
        static void ReserveRoom()
        {
            Console.Clear();
            // Get guest information
            Console.Write("Enter guest name: ");
            string guestName = Console.ReadLine();
            guestName.ToUpper(); // Standardize name format

            // Get and validate number of nights
            Console.Write("Enter number of nights: ");
            int numbernights = int.Parse(Console.ReadLine());
            while (numbernights < 1) // Must reserve at least 1 night
            {
                Console.WriteLine("Number of nights must be at least 1.");
                Console.Write("Enter number of nights: ");
                numbernights = int.Parse(Console.ReadLine());
            }

            // Get desired room number
            Console.WriteLine("enter room number to reserve: ");
            int roomNumber = int.Parse(Console.ReadLine());

            // Find the room and process reservation
            for (int i = 0; i < roomCount; i++)
            {
                if (roomNumbers[i] == roomNumber)
                {
                    // Check if room is already reserved
                    if (isReserved[i] == true)
                    {
                        Console.WriteLine("Room is already reserved.");
                        return;
                    }

                    // Create the reservation
                    isReserved[i] = true;
                    guestNames[i] = guestName;
                    nights[i] = numbernights;
                    bookingDates[i] = DateTime.Now;

                    Console.WriteLine("Room reserved successfully.");
                    return;
                }
                else
                {
                    Console.WriteLine("Room not found.");
                    return;
                }
            }
            Console.ReadLine();
        }
        //================= view all reservations ===================
        static void ViewReservations()
        {
            Console.Clear();
            // Loop through all rooms and show reserved ones
            for (int i = 0; i < roomCount; i++)
            {
                if (isReserved[i] == true)
                {
                    Console.WriteLine("Room Number: " + roomNumbers[i]);
                    Console.WriteLine("Guest Name: " + guestNames[i]);
                    Console.WriteLine("Nights: " + nights[i]);
                    Console.WriteLine("Cost: " + (roomRates[i] * nights[i]));
                    Console.WriteLine("Booking Date: " + bookingDates[i]);
                    Console.WriteLine();
                }
            }
            Console.ReadLine();
        }

        //================= search reservation by guest name ===================
        static void SearchReservation()
        {
            Console.Clear();
            // Get guest name to search for
            Console.Write("Enter guest name to search: ");
            string guestName = Console.ReadLine();
            guestName.ToUpper(); // Standardize format for comparison

            // Search through all reservations
            for (int i = 0; i < roomCount; i++)
            {
                if (guestNames[i] == guestName)
                {
                    // Display matching reservation details
                    Console.WriteLine("Room Number: " + roomNumbers[i]);
                    Console.WriteLine("Nights: " + nights[i]);
                    Console.WriteLine("Cost: " + (roomRates[i] * nights[i]));
                    Console.WriteLine("Booking Date: " + bookingDates[i]);
                }
                else
                {
                    Console.WriteLine("No reservation found for this guest.");
                }
            }
            Console.ReadLine();
        }

        //================= find highest paying guest ===================
        static void FindHighestPayingGuest()
        {
            Console.Clear();
            double highestCost = 0;
            string highestGuest = null;

            // Check all reservations to find highest payment
            for (int i = 0; i < roomCount; i++)
            {
                if (isReserved[i] == true)
                {
                    double cost = roomRates[i] * nights[i];
                    if (cost > highestCost)
                    {
                        highestCost = cost;
                        highestGuest = guestNames[i];
                    }
                }
            }

            // Display results
            if (highestGuest != null)
            {
                Console.WriteLine("Highest paying guest: " + highestGuest);
                Console.WriteLine("Cost: " + highestCost);
            }
            else
            {
                Console.WriteLine("No reservations found.");
            }
            Console.ReadLine();
        }

        //================= cancel reservation ===================
        static void CancelReservation()
        {
            Console.Clear();
            // Get room number to cancel
            Console.Write("Enter room number to cancel reservation: ");
            int roomNumber = int.Parse(Console.ReadLine());

            // Find the room and cancel reservation
            for (int i = 0; i < roomCount; i++)
            {
                if (roomNumbers[i] == roomNumber)
                {
                    // Verify room is actually reserved
                    if (isReserved[i] == false)
                    {
                        Console.WriteLine("Room is not reserved.");
                        return;
                    }

                    // Clear reservation data
                    isReserved[i] = false;
                    guestNames[i] = null;
                    nights[i] = 0;
                    Console.WriteLine("Reservation cancelled successfully.");
                    return;
                }
            }
        }
    }
}