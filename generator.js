const fs = require("fs");
const { randomInt } = require("crypto");

const instructions = ["u", "d", "o", "c"];
const characterAmount = 1_000_000_000;

const writeStream = fs.createWriteStream("input.txt");

for (let i = 0; i < characterAmount; i++) {
  writeStream.write(instructions[randomInt(4)]);
}

writeStream.end();
