import mmap


class Elevator:
    """
    Elevator class: represents the elevator
    It can do following:
    - Move up
    - Move down
    - Open door
    - Close door
    """

    def __init__(self, floor_min, floor_max, on_floor=0, door_status="c"):
        self.floor_min = floor_min
        self.floor_max = floor_max
        self.on_floor = on_floor
        self.door_status: bool = door_status == "c"

    def process_chunk(self, chunk):
        for instruction in chunk:
            self.process_instruction(instruction)

    def process_instruction(self, instruction):
        if instruction == "u":
            self.move_up()
        elif instruction == "d":
            self.move_down()
        elif instruction == "o":
            self.open_door()
        elif instruction == "c":
            self.close_door()

    def move_up(self):
        if self.door_status and self.on_floor < self.floor_max:
            self.on_floor += 1

    def move_down(self):
        if self.door_status and self.on_floor > self.floor_min:
            self.on_floor -= 1

    def open_door(self):
        if self.door_status:
            self.door_status = False

    def close_door(self):
        if not self.door_status:
            self.door_status = True


chunk_size = 1_000  # Adjust the chunk size as needed
with open("input.txt", "r+b") as file:
    el = Elevator(0, 10)
    with mmap.mmap(file.fileno(), 0, access=mmap.ACCESS_READ) as mm:
        for i in range(0, len(mm), chunk_size):
            chunk = mm[i : i + chunk_size]
            el.process_chunk(chunk.decode("utf-8"))


with open("output.txt", "w") as output_file:
    output_file.write(str(el.on_floor) + " " + str(el.door_status))
