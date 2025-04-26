using System;
using System.Collections.Generic;
using System.Linq;

class HRRN {
    private string userInput;
    public HRRN(string input) { userInput = input; } // accepts user input that represents the number of processes

    public void Execute() { // function that executes the algorithm and prints the results
        int numOfProcesses = Convert.ToInt32(userInput); // stores the number of processes
        List<PCB> processes = new List<PCB>(); // list that holds the information of the processes

        Console.WriteLine("\nHighest Response Ratio Next Scheduling\n");
        for (int i = 0; i < numOfProcesses; i++) { // stores information about each process from user input
            Console.Write($"Enter Arrival Time for P{i + 1}: ");
            double at = Convert.ToDouble(Console.ReadLine());
            Console.Write($"Enter Burst Time for P{i + 1}: ");
            double bt = Convert.ToDouble(Console.ReadLine());
            processes.Add(new PCB(i + 1, at, bt)); // adds and stores the process to the Process Control Block
        }

        double currentTime = 0;  // initialzes the current time of CPU scheduler
        int completed = 0; // tracks the number of processes completed
        double totalWaitingTime = 0; // initializes the total waiting time to calculate the average
        double totalTurnaroundTime = 0; // initializes the total turnaround time to calculate the average
        double totalBurstTime = processes.Sum(p => p.BurstTime); // calculates the total burst time which helps in calculating CPU utilization

        while (completed < numOfProcesses) { // main loop for scheduling algorithm
            List<PCB> readyQueue = processes.Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0).ToList(); // selects the processes that have not yet completed but have arrived

            if (readyQueue.Count > 0)
            {
                PCB current = readyQueue.OrderByDescending(p => ((currentTime - p.ArrivalTime + p.BurstTime) / (double)p.BurstTime))
                    .ThenBy(p => p.ArrivalTime).First(); // selects process with the highest response ratio ((WaitingTime + BurstTime) / BurstTime))

                if (current.StartTime == -1) { // if process is executing for the first time, set start time of the process to the current time
                    current.StartTime = currentTime;
                }

                currentTime += current.BurstTime; // sets the current time to the burst time of current process
                current.CompletionTime = currentTime; // sets the completion time to current time
                current.TurnaroundTime = current.CompletionTime - current.ArrivalTime; // calculates the turnaround time for the current process
                current.WaitingTime = current.TurnaroundTime - current.BurstTime; // calculates the waiting time for the current process
                current.RemainingTime = 0; // sets the remaining time to zero since process finished

                totalWaitingTime += current.WaitingTime; // calculates the the total waiting time
                totalTurnaroundTime += current.TurnaroundTime; // calculates the total turnaround time
                completed++; // increments number of processes completed
            }
            else { // increments time if no process if ready (CPU idle time)
                currentTime++;
            }
        }

        double avgWT = totalWaitingTime / numOfProcesses; // calculates the average waiting time of all processes
        double avgTT = totalTurnaroundTime / numOfProcesses; // calculates the average turnaround time of all processes
        double cpuUtilization = (totalBurstTime / currentTime) * 100; // calculates the CPU utilization 
        double throughput = numOfProcesses / currentTime; // calculates the total throughput

        Console.WriteLine("\nProcess Execution Results:\n"); // prints the results of the scheduling algorithm simulation
        foreach (var p in processes.OrderBy(p => p.ProcessID)) {
            Console.WriteLine($"Process {p.ProcessID} -> Arrival Time: {p.ArrivalTime}, Burst Time: {p.BurstTime}, Waiting Time: {p.WaitingTime}, Turnaround Time: {p.TurnaroundTime}, Completion Time: {p.CompletionTime}"); // summary of each process
        }
        Console.WriteLine($"\nAverage Waiting Time = {avgWT:F2} sec(s)");
        Console.WriteLine($"Average Turnaround Time = {avgTT:F2} sec(s)");
        Console.WriteLine($"CPU Utilization = {cpuUtilization:F2}%");
        Console.WriteLine($"Throughput = {throughput:F2} process(es)/sec");
    }
}