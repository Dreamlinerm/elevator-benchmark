using System;

namespace ConsoleApp1;

public class Elevator
{
    private readonly int floorMin;
    private readonly int floorMax;
    public int OnFloor { get; private set; }
    public bool DoorStatus  { get; private set; }
    //public string Output { get; set; }

    public Elevator(int floorMin, int floorMax, int onFloor = 0, bool doorStatus = true)
    {
        this.floorMin = floorMin;
        this.floorMax = floorMax;
        this.OnFloor = onFloor;
        this.DoorStatus = doorStatus;
        //this.Output = string.Empty;
    }

    public void ProcessChunk(ReadOnlySpan<char> chars)
    {
        foreach (var ch in chars)
        {
            ProcessInstruction(ch);
        }
    }

    private void ProcessInstruction(char instruction)
    {
        switch (instruction)
        {
            case 'u':
                MoveUp();
                break;
            case 'd':
                MoveDown();
                break;
            case 'o':
                OpenDoor();
                break;
            case 'c':
                CloseDoor();
                break;
        }
    }

    private void MoveUp()
    {
        if (DoorStatus && OnFloor < floorMax)
        {
            OnFloor += 1;
            //Output += onFloor;
        }
        /*else
        {
            //Output += "f";
        }*/
    }

    private void MoveDown()
    {
        if (DoorStatus && OnFloor > floorMin)
        {
            OnFloor -= 1;
            //Output += onFloor;
        }
        /*else
        {
            //Output += "f";
        }*/
    }

    private void OpenDoor()
    {
        if (DoorStatus)
        {
            DoorStatus = false;
            //Output += "o";
        }
        /*else
        {
            //Output += "f";
        }*/
    }

    private void CloseDoor()
    {
        if (!DoorStatus)
        {
            DoorStatus = true;
            //Output += "c";
        }
        /*else
        {
            //Output += "f";
        }*/
    }
}