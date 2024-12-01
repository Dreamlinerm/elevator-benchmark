using System;
using System.IO;
using System.Threading.Tasks;

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

    public void ProcessChunk(char[] buffer, int charsRead)
    {
        for (int i = 0; i < charsRead; i++)
        {
            ProcessInstruction(buffer[i]);
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
    static async Task Main(string[] args)
    {
        const int bufferSize = 4096; // Adjust the chunk size as needed
        string inputFilePath = @"C:\Users\Marvin\Documents\Programmieren\elevator-benchmark\input.txt";
        string outputFilePath = @"C:\Users\Marvin\Documents\Programmieren\elevator-benchmark\output.txt";

        byte[] fileBytes = File.ReadAllBytes(inputFilePath);

        using (var memoryStream = new MemoryStream(fileBytes))
        using (var readStream = new StreamReader(memoryStream))
        using (var writeStream = new StreamWriter(outputFilePath))
        {
            var el = new Elevator(0, 10);
            char[] buffer = new char[bufferSize];
            int charsRead;
            var memoryBuffer = buffer.AsMemory(0, bufferSize);
            while ((charsRead = await readStream.ReadAsync(memoryBuffer)) > 0)
            {
                el.ProcessChunk(buffer, charsRead);
                await writeStream.WriteAsync(el.Output);
                el.Output = string.Empty; // Clear the output after writing
                                          // Console.WriteLine(el.Output); // Uncomment to see the output in the console
            }

            await writeStream.WriteAsync(el.Output); // Write any remaining output
        }
    }
}