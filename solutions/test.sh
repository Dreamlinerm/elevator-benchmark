#!/bin/bash

# Define min and max floors
minFloor=1
maxFloor=10

# Function to handle elevator instructions
handle_instruction() {
  local instruction=$1
  case $instruction in
    U) echo "Move up";;
    D) echo "Move down";;
    O) echo "Open door";;
    C) echo "Close door";;
    S) echo "Display status";;
    *) echo "Invalid instruction";;
  esac
}

# Read instructions from input
echo "Enter elevator instructions (U, D, O, C, S), separated by spaces:"
read -a instructions

# Process each instruction
for instruction in "${instructions[@]}"; do
  handle_instruction $instruction
done