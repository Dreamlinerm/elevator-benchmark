#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct
{
    int floor_min;
    int floor_max;
    int on_floor;
    int door_status; // 1 for closed, 0 for open
    char output[1024];
} Elevator;

void processInstruction(Elevator *el, char instruction);

void processChunk(Elevator *el, const char *chunk)
{
    for (int i = 0; i < strlen(chunk); i++)
    {
        processInstruction(el, chunk[i]);
    }
}

void moveUp(Elevator *el)
{
    if (el->door_status && el->on_floor < el->floor_max)
    {
        el->on_floor += 1;
        sprintf(el->output + strlen(el->output), "%d", el->on_floor);
    }
    else
    {
        strcat(el->output, "f");
    }
}

void moveDown(Elevator *el)
{
    if (el->door_status && el->on_floor > el->floor_min)
    {
        el->on_floor -= 1;
        sprintf(el->output + strlen(el->output), "%d", el->on_floor);
    }
    else
    {
        strcat(el->output, "f");
    }
}

void openDoor(Elevator *el)
{
    if (el->door_status)
    {
        el->door_status = 0;
        strcat(el->output, "o");
    }
    else
    {
        strcat(el->output, "f");
    }
}

void closeDoor(Elevator *el)
{
    if (!el->door_status)
    {
        el->door_status = 1;
        strcat(el->output, "c");
    }
    else
    {
        strcat(el->output, "f");
    }
}

void processInstruction(Elevator *el, char instruction)
{
    if (instruction == 'u')
    {
        moveUp(el);
    }
    else if (instruction == 'd')
    {
        moveDown(el);
    }
    else if (instruction == 'o')
    {
        openDoor(el);
    }
    else if (instruction == 'c')
    {
        closeDoor(el);
    }
}

int main()
{
    FILE *inputFile = fopen("input.txt", "r");
    FILE *outputFile = fopen("output.txt", "w");

    if (inputFile == NULL || outputFile == NULL)
    {
        perror("Error opening file");
        return EXIT_FAILURE;
    }

    // Get the file size
    fseek(inputFile, 0, SEEK_END);
    long fileSize = ftell(inputFile);
    fseek(inputFile, 0, SEEK_SET);

    // Allocate memory for the entire file
    char *fileContent = (char *)malloc(fileSize + 1);
    if (fileContent == NULL)
    {
        perror("Error allocating memory");
        fclose(inputFile);
        fclose(outputFile);
        return EXIT_FAILURE;
    }

    // Read the entire file into memory
    fread(fileContent, 1, fileSize, inputFile);
    fileContent[fileSize] = '\0'; // Null-terminate the string

    Elevator el = {0, 10, 0, 1, ""};
    processChunk(&el, fileContent);
    fprintf(outputFile, "%s", el.output);

    // Clean up
    free(fileContent);
    fclose(inputFile);
    fclose(outputFile);

    return EXIT_SUCCESS;
}