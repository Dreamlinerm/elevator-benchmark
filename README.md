# elevator-benchmark

ReadLargeDataset and parse instructions

## Input format

- Each elevator has the below capabilities:
  - Move up: u
  - Move down: d
  - Open door: o
  - Close door: c

## Additional Information

- minFloor: Number
- maxFloor: Number
- InitialFloor: Number
- InitialDoorStatus: o/c
  
- Challenge:
  - handling incorrect instructions (e.g. moving up from top floor)
  - ouputing the correct floor and door status
  - handling a big dataset


## Output format

- Ouput floor moved to: Number
- Output door status: o/o

## Example Output

```
0oc1oc2oc0o
```
