﻿using System;
using System.IO;

class Elevator
{
    private int floorMin;
    private int floorMax;
    private int onFloor;
    private bool doorStatus;
    public string Output { get; set; }

    public Elevator(int floorMin, int floorMax, int onFloor = 0, string doorStatus = "c")
    {
        this.floorMin = floorMin;
        this.floorMax = floorMax;
        this.onFloor = onFloor;
        this.doorStatus = doorStatus == "c";
        this.Output = string.Empty;
    }

    public void ProcessChunk(string chunk)
    {
        foreach (char instruction in chunk)
        {
            ProcessInstruction(instruction);
        }
    }

    private void ProcessInstruction(char instruction)
    {
        if (instruction == 'u')
        {
            MoveUp();
        }
        else if (instruction == 'd')
        {
            MoveDown();
        }
        else if (instruction == 'o')
        {
            OpenDoor();
        }
        else if (instruction == 'c')
        {
            CloseDoor();
        }
    }

    private void MoveUp()
    {
        if (doorStatus && onFloor < floorMax)
        {
            onFloor += 1;
            Output += onFloor;
        }
        else
        {
            Output += "f";
        }
    }

    private void MoveDown()
    {
        if (doorStatus && onFloor > floorMin)
        {
            onFloor -= 1;
            Output += onFloor;
        }
        else
        {
            Output += "f";
        }
    }

    private void OpenDoor()
    {
        if (doorStatus)
        {
            doorStatus = false;
            Output += "o";
        }
        else
        {
            Output += "f";
        }
    }

    private void CloseDoor()
    {
        if (!doorStatus)
        {
            doorStatus = true;
            Output += "c";
        }
        else
        {
            Output += "f";
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        const int chunkSize = 1000; // Adjust the chunk size as needed
        string inputFilePath = @"C:\Users\Marvin\Documents\Programmieren\elevator-benchmark\input.txt";
        string outputFilePath = @"C:\Users\Marvin\Documents\Programmieren\elevator-benchmark\output.txt";

        using (var readStream = new StreamReader(inputFilePath))
        using (var writeStream = new StreamWriter(outputFilePath))
        {
            var el = new Elevator(0, 10);
            char[] buffer = new char[chunkSize];
            int bytesRead;

            while ((bytesRead = readStream.Read(buffer, 0, chunkSize)) > 0)
            {
                string chunk = new string(buffer, 0, bytesRead);
                el.ProcessChunk(chunk);
                writeStream.Write(el.Output);
                el.Output = string.Empty; // Clear the output after writing
                                          // Console.WriteLine(el.Output); // Uncomment to see the output in the console
                Console.WriteLine("Processing...");
            }

            writeStream.Write(el.Output); // Write any remaining output
        }
    }
}