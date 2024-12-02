# elevator-benchmark

ReadLargeDataset and parse instructions

## Input format

- Each elevator has the below capabilities:
  - Move up: u
  - Move down: d
  - Open door: o
  - Close door: c

## Additional Information

- floor_min: Number
- floor_max: Number
- on_floor: Number
- InitialDoorStatus: o/c
  
- Challenge:
  - handling incorrect instructions (e.g. moving up from top floor)
  - outputing the correct floor and door status
  - handling a big dataset


## Output format

- Ouput floor and door status

## Examples

Input:
```
udddcccucccccocddddccuuuuoduucduoducoddc
```

Output:
```
7 false
```

## Time execution

```bash
time python python.py
```

```powershell
Measure-Command { node js.js }
```

## Run cpp

```bash
dotnet build
dotnet publish -c Release -r win-x64 --self-contained
```