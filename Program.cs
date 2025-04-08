namespace Simple_Hotel_Room_Management_Project
{
    internal class Program
    {
        const int roomLimit = 10;   // Maximum number of rooms
        static int[] roomNumbers = new int[roomLimit]; // Array to store room numbers
        static double[] roomRates = new double[roomLimit]; // Array to store room rates
        static bool[] isReserved = new bool[roomLimit]; // Array to store reservation status
        static string[] guestNames = new string[roomLimit]; // Array to store guest names
        static int[] nights = new int[roomLimit]; // Array to store number of nights
        static DateTime[] bookingDates = new DateTime[roomLimit]; // Array to store booking dates

        static int roomCount = 0;

        static void Main()
        {
            while (true)
            {
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

                switch (Console.ReadLine())
                {
                    case "1":
                        AddRoom();
                        break;
                    case "2":
                        ViewRooms();
                        break;
                    case "3":
                        ReserveRoom();
                        break;
                    case "4":
                        ViewReservations();
                        break;
                    case "5":
                        SearchReservation();
                        break;
                    case "6":
                        FindHighestPayingGuest();
                        break;
                    case "7":
                        CancelReservation();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        /// ===================add a new room=====================
        static void AddRoom()
        {
            Console.Clear();
            if (roomCount >= roomLimit)
            {
                Console.WriteLine("Room limit reached.");
                return;
            }

            Console.Write("Enter room number: ");
            int roomNumber = int.Parse(Console.ReadLine());
            for (int i = 0; i < roomCount; i++)
            {
                if (roomNumbers[i] == roomNumber)
                {
                    Console.WriteLine("Room number already exists.");
                    return;
                }
            }
            roomNumbers[roomCount] = roomNumber;
            Console.Write("Enter room rate: ");
            double roomRate = double.Parse(Console.ReadLine());
            while (roomRate < 100)
            {
                Console.WriteLine("Room rate must be at least 100.");
                Console.Write("Enter room rate: ");
                roomRate = double.Parse(Console.ReadLine());
                Console.ReadLine();
               
            }


            roomRates[roomCount] = roomRate;
            roomCount++;
            isReserved[roomCount] = false;
            Console.ReadLine();


        }

        /// ===================view all rooms=====================
        static void ViewRooms()
        {
            Console.Clear();
            for (int i = 0; i < roomCount; i++)
            {
                Console.WriteLine("Room Number: " + roomNumbers[i]);
                Console.WriteLine("Room Rate: " + roomRates[i]);

                if (isReserved[i] == true)
                {
                    Console.WriteLine("Status: the room is Reserved by :");
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

        /// ===================reserve a room=====================
        static void ReserveRoom()
        {
            Console.Clear();
            Console.Write("Enter guest name: ");
            string guestName = Console.ReadLine();
            guestName.ToUpper();
            Console.Write("Enter number of nights: ");
            int numbernights = int.Parse(Console.ReadLine());

            while (numbernights < 1)
            {
                Console.WriteLine("Number of nights must be at least 1.");
                Console.Write("Enter number of nights: ");
                numbernights = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("enter room number to reserve: ");
            int roomNumber = int.Parse(Console.ReadLine());
            for (int i = 0; i < roomCount; i++)
            {
                if (roomNumbers[i] == roomNumber)
                {
                    if (isReserved[i] == true)
                    {
                        Console.WriteLine("Room is already reserved.");
                        return;
                    }

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

        /// ===================view all reservations=====================
        static void ViewReservations()
        {
            Console.Clear();
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

        /// ===================search reservation by guest name=====================
        static void SearchReservation()
        {
            Console.Clear();
            Console.Write("Enter guest name to search: ");
            string guestName = Console.ReadLine();
            guestName.ToUpper();
;
            for (int i = 0; i < roomCount; i++)
            {
                if (guestNames[i] == guestName)
                {
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

        /// ===================find highest paying guest=====================
        static void FindHighestPayingGuest()
        {
            Console.Clear();
            double highestCost = 0;
            string highestGuest = null;
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

        /// ===================cancel reservation by room number=====================
        static void CancelReservation()
        {
            Console.Clear();
            Console.Write("Enter room number to cancel reservation: ");
            int roomNumber = int.Parse(Console.ReadLine());

            for (int i = 0; i < roomCount; i++)
            {
                if (roomNumbers[i] == roomNumber)
                {
                    if (isReserved[i] == false)
                    {
                        Console.WriteLine("Room is not reserved.");
                        return;
                    }
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