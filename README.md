# CS 3502: Project 2 - CPU Scheduling

This project is designed to emulate a CPU scheduler with the ability to compare two scheduling algorithms. This is for the use of OwlTech, a hypothetical company that gave the task of implementing new algorithms to find the one with the most use for their needs. I built my CPU Scheduler from scratch to complete this request while still taking inspiration from their original implementation of a few scheduling algorithms. The new algorithms I decided to implement and compare were the Highest Response Ratio Next (HRRN) and Shortest Remaining Time First (SRTF). To get the feeling and function of CPU scheduling, I used a List as a ready queue to track what processes are up next. This simulator was created in the C# language using the .NET 8.0 runtime. The code itself was developed inside of the Visual Studio 2022 IDE but can be compiled and run anywhere.

Getting this simulator up and running is simple. The first thing you need is to have the .NET runtime installed with all the relevant dependencies set up for whichever IDE is in use. The next thing to download or copy the code from the four source code files given in this repository: Highest Response Ratio Next, Shortest Remaining Time First, Process Control Block, and CPU Scheduler. The first two files represent the algorithms themselves, while the next file represents a record class containing a Process Control Block, which holds information on the processes including Arrival Time, Burst Time, Remaining Time, etc. To run the simulator itself, simply run the CPU Scheduler file, as it is the main driver class, in either the terminal or console of the IDE itself. Doing this will pop up a prompt that gives the user the ability to choose which algorithm they wish to simulate, in this case, SRTF and HRRN. You are then prompted to enter how many processes are going to be used, and then the Arrival and Burst Time for each of them. After that, the program will output the results from the algorithm you selected. It will show you metrics such as Average Waiting Time and Average Turnaround Time. An example output is provided below:




![Screenshot 2025-04-23 220824](https://github.com/user-attachments/assets/b11ac372-ae5a-48bb-b1d3-64e62214259d)

Source Code and implementation I took inspiration from:

Francis Nweke. CPU Simulator GUI. https://github.com/FrancisNweke/CPU-Simulator-GUI
