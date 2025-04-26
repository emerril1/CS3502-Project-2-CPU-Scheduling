using System;
using System.Collections.Generic;
using System.Linq;


class CPUScheduler { // main driver class
    static void Main(string[] args) {
        Console.WriteLine("CPU Scheduler Simulator\n");
        Console.WriteLine("1. Shortest Remaining Time First (SRTF)");
        Console.WriteLine("2. Highest Response Ratio Next (HRRN)");
        Console.Write("\nSelect Scheduling Algorithm: ");
        int choice = Convert.ToInt32(Console.ReadLine()); // takes in user input for choice of scheduling algorithm

        Console.Write("Enter the number of processes: ");
        string input = Console.ReadLine(); // takes in user input for choice of number of processes

        switch (choice) {  // switch block that executes one of the chosen algorithms
            case 1:
                SRTF srtf = new SRTF(input); // passes in number of processes to algorithm
                srtf.Execute(); // executes chosen algorithm
                break;
            case 2:
                HRRN hrrn = new HRRN(input); // passes in number of processes to algorithm
                hrrn.Execute(); // executes chosen algorithm
                break;
            default:
                Console.WriteLine("Invalid Option.");
                break;
        }
    }
}
