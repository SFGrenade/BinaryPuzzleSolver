# Binary Puzzle Solver

This is a generator of a type of binary puzzle.  

The rules are simple:  
- There are 0's and 1's
- Only up to 2 of the same type can be next to each other (e.g. "0011" but not "0001")
- A row or column contains the same number of 0's as of 1's

It can look like this:
```
=====================
  | |0| |1|1| |0| |
  | | |1| | | | | |0
 1| | | | | | | | |
  | |1| | | |1|1| |0
  |0|0| | |0| | | |
  | | | | | |1| |1|
  |1| |1|1| | | | |
  |1| | | |1|0| | |0
=====================
```
