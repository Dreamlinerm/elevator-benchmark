#!/bin/bash

# Global variables
floor_min=0
floor_max=10
on_floor=0
door_status=true
output=""

process_chunk() {
  local chunk=$1
  for (( i=0; i<${#chunk}; i++ )); do
    process_instruction "${chunk:$i:1}"
  done
}

process_instruction() {
  local instruction=$1
  case $instruction in
    u) move_up ;;
    d) move_down ;;
    o) open_door ;;
    c) close_door ;;
  esac
}

move_up() {
  if $door_status && [ $on_floor -lt $floor_max ]; then
    ((on_floor++))
    output+=$on_floor
  else
    output+="f"
  fi
}

move_down() {
  if $door_status && [ $on_floor -gt $floor_min ]; then
    ((on_floor--))
    output+=$on_floor
  else
    output+="f"
  fi
}

open_door() {
  if $door_status; then
    door_status=false
    output+="o"
  else
    output+="f"
  fi
}

close_door() {
  if ! $door_status; then
    door_status=true
    output+="c"
  else
    output+="f"
  fi
}

# Read from input.txt and process the instructions
while IFS= read -r -n1 char; do
  process_chunk "$char"
done < input.txt

# Write the output to output.txt
echo -n "$output" > output.txt