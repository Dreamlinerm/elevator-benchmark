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

    this.output = "";
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
      this.output += this.on_floor;
    } else {
      this.output += "f";
    }
  }

  moveDown() {
    if (this.door_status && this.on_floor > this.floor_min) {
      this.on_floor -= 1;
      this.output += this.on_floor;
    } else {
      this.output += "f";
    }
  }

  openDoor() {
    if (this.door_status) {
      this.door_status = false;
      this.output += "o";
    } else {
      this.output += "f";
    }
  }

  closeDoor() {
    if (!this.door_status) {
      this.door_status = true;
      this.output += "c";
    } else {
      this.output += "f";
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
  writeStream.write(el.output, (err) => {
    if (err) throw err;
  });
  el.output = ""; // Clear the output after writing
});

readStream.on("end", () => {
  writeStream.write(el.output, (err) => {
    if (err) throw err;
  });
});

readStream.on("error", (err) => {
  throw err;
});

writeStream.on("error", (err) => {
  throw err;
});
