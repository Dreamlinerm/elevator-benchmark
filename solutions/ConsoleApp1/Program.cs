using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1;

public static class Program
{
    private static async Task Main(string[] args)
    {
        const int bufferSize = 4096; // Adjust the chunk size as needed
        const string inputFilePath = @"C:\Users\Marvin\Documents\Programmieren\elevator-benchmark\input.txt";
        const string outputFilePath = @"C:\Users\Marvin\Documents\Programmieren\elevator-benchmark\output.txt";
        var optionsRead = new FileStreamOptions()
        {
            Mode = FileMode.Open,
            Access = FileAccess.Read,
            BufferSize = bufferSize,
            Share = FileShare.Read
        };

        var optionsWrite = new FileStreamOptions()
        {
            Mode = FileMode.Create,
            Access = FileAccess.Write,
            BufferSize = bufferSize,
            Share = FileShare.Read
        };

        using var readStream = new StreamReader(inputFilePath,Encoding.UTF8, false, optionsRead);
        await using var writeStream = new StreamWriter(outputFilePath, Encoding.UTF8, optionsWrite);
        var el = new Elevator(0, 10);
        var buffer = new char[bufferSize];
        int charsRead;
        var memoryBuffer = buffer.AsMemory(0, bufferSize);
        while ((charsRead = await readStream.ReadAsync(memoryBuffer).ConfigureAwait(false)) > 0)
        {
            el.ProcessChunk(memoryBuffer.Span[..charsRead]);
            //await writeStream.WriteAsync(el.Output);
            //el.Output = string.Empty; // Clear the output after writing
            // Console.WriteLine(el.Output); // Uncomment to see the output in the console
        }

        await writeStream.WriteAsync(el.DoorStatus.ToString()+el.OnFloor); // Write any remaining output
    }
}