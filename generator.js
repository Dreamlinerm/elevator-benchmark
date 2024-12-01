const fs = require("fs");
const { randomInt } = require("crypto");

const instructions = ["u", "d", "o", "c"];
const characterAmount = 1_00_000_000;
const chunkSize = 10_000_000; // Write in chunks of 10 million characters

const writeStream = fs.createWriteStream("input.txt");

let written = 0;

function writeChunk() {
  let chunk = "";
  for (let i = 0; i < chunkSize; i++) {
    chunk += instructions[randomInt(4)];
  }
  return writeStream.write(chunk);
}

function write() {
  while (written < characterAmount) {
    if (!writeChunk()) {
      writeStream.once("drain", write);
      return;
    }
    written += chunkSize;
  }
  writeStream.end();
}

write();
