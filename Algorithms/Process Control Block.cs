class PCB { // recprd class that acts as a Process Control Block that contains the following information
    public int ProcessID;
    public double ArrivalTime;
    public double BurstTime;
    public double RemainingTime;
    public double StartTime = -1;
    public double CompletionTime;
    public double WaitingTime;
    public double TurnaroundTime;

    public PCB(int id, double at, double bt) { // constructor to initialize initial attributes of the process
        ProcessID = id;
        ArrivalTime = at;
        BurstTime = bt;
        RemainingTime = bt;
    }
}