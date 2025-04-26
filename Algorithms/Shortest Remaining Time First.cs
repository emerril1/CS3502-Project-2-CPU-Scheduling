using System;
using System.Collections.Generic;
using System.Linq;

class SRTF {
    private string userInput;
    public SRTF(string input) { userInput = input; } // accepts user input that represents the number of processes

    public void Execute() { // function that executes the algorithm and prints the results
        int numOfProcesses = Convert.ToInt32(userInput); // stores the number of processes
        List<PCB> processes = new List<PCB>(); // list that holds the information of the processes

        Console.WriteLine("\nShortest Remaining Time First Scheduling\n");
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

        List<PCB> readyQueue = new List<PCB>(); // stores the ready processes that have arrived, but are not finished

        while (completed < numOfProcesses) {
            foreach (var p in processes.Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0 && !readyQueue.Contains(p))) { // adds processes that have just arrived to the queue
                readyQueue.Add(p);
            }

            if (readyQueue.Count > 0) {
                PCB current = readyQueue.OrderBy(p => p.RemainingTime).ThenBy(p => p.ArrivalTime).First(); // selects the process that has the shortest remaining time left

                if (current.StartTime == -1) {  // if process is executing for the first time, set start time of the process to the current time
                    current.StartTime = currentTime;
                }
                current.RemainingTime -= 1; // executes process for one unit of time for preemptive simulation
                currentTime += 1; // increments time

                foreach (var p in processes.Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0 && p != current)) { // increases the waiting time of other processes
                    p.WaitingTime += 1;
                }

                if (current.RemainingTime == 0) { // calculates following metrics if the current process finishes
                    current.CompletionTime = currentTime; // sets the completion time
                    current.TurnaroundTime = current.CompletionTime - current.ArrivalTime; // sets the turnaround time
                    current.WaitingTime = current.TurnaroundTime - current.BurstTime; // sets the waiting time

                    totalWaitingTime += current.WaitingTime; // calculates the the total waiting time
                    totalTurnaroundTime += current.TurnaroundTime; // calculates the total turnaround time
                    completed++; // increments number of processes completed
                    readyQueue.Remove(current); // removes the finished process from queue
                }
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