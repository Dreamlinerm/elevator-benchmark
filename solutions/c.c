#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct
{
    int floor_min;
    int floor_max;
    int on_floor;
    int door_status; // 1 for closed, 0 for open
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
    }
}

void moveDown(Elevator *el)
{
    if (el->door_status && el->on_floor > el->floor_min)
    {
        el->on_floor -= 1;
    }
}

void openDoor(Elevator *el)
{
    if (el->door_status)
    {
        el->door_status = 0;
    }
}

void closeDoor(Elevator *el)
{
    if (!el->door_status)
    {
        el->door_status = 1;
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

    Elevator el = {0, 10, 0, 1};
    char chunk[1024];

    while (fgets(chunk, sizeof(chunk), inputFile) != NULL)
    {
        processChunk(&el, chunk);
        // write floorstatus and doorstatus to output
        fprintf(outputFile, "%d %d\n", el.on_floor, el.door_status);
    }

    fclose(inputFile);
    fclose(outputFile);

    return EXIT_SUCCESS;
}