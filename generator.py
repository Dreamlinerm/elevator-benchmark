instructions = ["u", "d", "o", "c"]

from random import choice


characterAmount = 1000

with open("input.txt", "w") as file:
    for _ in range(characterAmount):
        file.write(choice(instructions))
