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

- Ouput floor moved to: Number
- Output door status: o/c
- Output invalid instruction: f

## Examples

Input:
```
udddcccucccccocddddccuuuuoduucduoducoddc
```

Output:
```
10fffff1fffffoc0fffff1234offfc34offcoffc
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