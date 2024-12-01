class Elevator {
  /**
   * Elevator class: represents the elevator
   * It can do following:
   * - Move up
   * - Move down
   * - Open door
   * - Close door
   */
  constructor(floor_min, floor_max, on_floor = 0, door_status = "c") {
    this.floor_min = floor_min;
    this.floor_max = floor_max;
    this.on_floor = on_floor;
    this.door_status = door_status === "c";
  }

  processChunk(chunk) {
    for (let instruction of chunk) {
      this.processInstruction(instruction);
    }
  }

  processInstruction(instruction) {
    if (instruction === "u") {
      this.moveUp();
    } else if (instruction === "d") {
      this.moveDown();
    } else if (instruction === "o") {
      this.openDoor();
    } else if (instruction === "c") {
      this.closeDoor();
    }
  }

  moveUp() {
    if (this.door_status && this.on_floor < this.floor_max) {
      this.on_floor += 1;
    }
  }

  moveDown() {
    if (this.door_status && this.on_floor > this.floor_min) {
      this.on_floor -= 1;
    }
  }

  openDoor() {
    if (this.door_status) {
      this.door_status = false;
    }
  }

  closeDoor() {
    if (!this.door_status) {
      this.door_status = true;
    }
  }
}

const fs = require("fs");
// const chunkSize = 1024; // Adjust the chunk size as needed

const readStream = fs.createReadStream("input.txt", {
  encoding: "utf8",
  //   highWaterMark: chunkSize,
});
const writeStream = fs.createWriteStream("output.txt");

const el = new Elevator(0, 10);

readStream.on("data", (chunk) => {
  el.processChunk(chunk);
});

readStream.on("end", () => {
  writeStream.write(el.on_floor + " " + el.door_status, (err) => {
    if (err) throw err;
  });
});

readStream.on("error", (err) => {
  throw err;
});

writeStream.on("error", (err) => {
  throw err;
});
