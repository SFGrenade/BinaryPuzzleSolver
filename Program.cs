// See https://aka.ms/new-console-template for more information

namespace BinaryPuzzleSolver
{
    public class BinaryPuzzleSolver
    {
        public enum CellState
        {
            O,
            A,
            B
        }

        public BinaryPuzzleSolver()
        {
        }

        public BinaryPuzzleSolver(CellState[][] field)
        {
            _fieldHeight = field.Length;
            _fieldWidth = field[0].Length;
            Field = new BinaryPuzzleSolver.CellState[_fieldHeight][];
            for (int y = 0; y < _fieldHeight; y++)
            {
                Field[y] = new BinaryPuzzleSolver.CellState[_fieldWidth];
            }

            for (int y = 0; y < _fieldHeight; y++)
            {
                for (int x = 0; x < _fieldWidth; x++)
                {
                    Field[y][x] = field[y][x];
                }
            }
        }

        private int _fieldHeight = 8;
        private int _fieldWidth = 10;

        public CellState[][] Field { get; } =
        {
            new[] {CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O},
            new[] {CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O},
            new[] {CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O},
            new[] {CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O},
            new[] {CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O},
            new[] {CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O},
            new[] {CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O},
            new[] {CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O, CellState.O},
        };

        private CellState GetField(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return CellState.O;
            }

            if (y >= _fieldHeight || x >= _fieldWidth)
            {
                return CellState.O;
            }

            return Field[y][x];
        }

        private void SetField(int x, int y, CellState value)
        {
            if (x < 0 || y < 0)
            {
                return;
            }

            if (y >= _fieldHeight || x >= _fieldWidth)
            {
                return;
            }

            Field[y][x] = value;
        }

        private void SetFieldOtherValue(int x, int y, CellState compareValue)
        {
            SetField(x, y, compareValue == CellState.A ? CellState.B : CellState.A);
        }

        private bool CheckComplete()
        {
            bool ret = true;
            for (int y = 0; y < _fieldHeight; y++)
            {
                for (int x = 0; x < _fieldWidth; x++)
                {
                    ret &= GetField(x, y) != CellState.O;
                }
            }

            return ret;
        }

        private bool CheckDoubleAround(int x, int y)
        {
            if (GetField(x, y) != CellState.O)
            {
                return false;
            }

            bool foundPlacement = false;
            if (GetField(x - 2, y) != CellState.O && GetField(x - 2, y) == GetField(x - 1, y))
            {
                SetFieldOtherValue(x, y, GetField(x - 1, y));
                foundPlacement = true;
            }
            else if (GetField(x + 2, y) != CellState.O && GetField(x + 2, y) == GetField(x + 1, y))
            {
                SetFieldOtherValue(x, y, GetField(x + 1, y));
                foundPlacement = true;
            }
            else if (GetField(x, y - 2) != CellState.O && GetField(x, y - 2) == GetField(x, y - 1))
            {
                SetFieldOtherValue(x, y, GetField(x, y - 1));
                foundPlacement = true;
            }
            else if (GetField(x, y + 2) != CellState.O && GetField(x, y + 2) == GetField(x, y + 1))
            {
                SetFieldOtherValue(x, y, GetField(x, y + 1));
                foundPlacement = true;
            }

            return foundPlacement;
        }

        private bool CheckInBetween(int x, int y)
        {
            if (GetField(x, y) != CellState.O)
            {
                return false;
            }

            bool foundPlacement = false;
            if (GetField(x - 1, y) != CellState.O && GetField(x - 1, y) == GetField(x + 1, y))
            {
                SetFieldOtherValue(x, y, GetField(x - 1, y));
                foundPlacement = true;
            }
            else if (GetField(x, y - 1) != CellState.O && GetField(x, y - 1) == GetField(x, y + 1))
            {
                SetFieldOtherValue(x, y, GetField(x, y - 1));
                foundPlacement = true;
            }

            return foundPlacement;
        }

        private bool CheckRowCounting(int x, int y)
        {
            if (GetField(x, y) != CellState.O)
            {
                return false;
            }

            bool foundPlacement = false;
            int count1 = 0;
            int count2 = 0;
            for (int counterX = 0; counterX < _fieldWidth; counterX++)
            {
                if (GetField(counterX, y) == CellState.A)
                {
                    count1++;
                }
                else if (GetField(counterX, y) == CellState.B)
                {
                    count2++;
                }
            }

            if (count1 + count2 == _fieldWidth - 1)
            {
                SetField(x, y, count1 < count2 ? CellState.A : CellState.B);
                foundPlacement = true;
            }
            else if (count1 == _fieldWidth / 2)
            {
                SetField(x, y, CellState.B);
                foundPlacement = true;
            }
            else if (count2 == _fieldWidth / 2)
            {
                SetField(x, y, CellState.A);
                foundPlacement = true;
            }

            return foundPlacement;
        }

        private bool CheckColumnCounting(int x, int y)
        {
            if (GetField(x, y) != CellState.O)
            {
                return false;
            }

            bool foundPlacement = false;
            int count1 = 0;
            int count2 = 0;
            for (int counterY = 0; counterY < _fieldHeight; counterY++)
            {
                if (GetField(x, counterY) == CellState.A)
                {
                    count1++;
                }
                else if (GetField(x, counterY) == CellState.B)
                {
                    count2++;
                }
            }

            if (count1 + count2 == _fieldHeight - 1)
            {
                SetField(x, y, count1 < count2 ? CellState.A : CellState.B);
                foundPlacement = true;
            }
            else if (count1 == _fieldHeight / 2)
            {
                SetField(x, y, CellState.B);
                foundPlacement = true;
            }
            else if (count2 == _fieldHeight / 2)
            {
                SetField(x, y, CellState.A);
                foundPlacement = true;
            }

            return foundPlacement;
        }

        private bool ValidateDoubleAround(int x, int y)
        {
            if (GetField(x, y) == CellState.O)
            {
                return false;
            }

            CellState self = GetField(x, y);
            CellState left1 = GetField(x - 1, y);
            CellState left2 = GetField(x - 2, y);
            CellState right1 = GetField(x + 1, y);
            CellState right2 = GetField(x + 2, y);
            CellState up1 = GetField(x, y - 1);
            CellState up2 = GetField(x, y - 2);
            CellState down1 = GetField(x, y + 1);
            CellState down2 = GetField(x, y + 2);

            bool valid = true;
            if (left2 != CellState.O && left2 == left1 && left1 == self)
            {
                valid = false;
            }
            else if (right2 != CellState.O && right2 == right1 && right1 == self)
            {
                valid = false;
            }
            else if (up2 != CellState.O && up2 == up1 && up1 == self)
            {
                valid = false;
            }
            else if (down2 != CellState.O && down2 == down1 && down1 == self)
            {
                valid = false;
            }

            return valid;
        }

        private bool ValidateInBetween(int x, int y)
        {
            if (GetField(x, y) == CellState.O)
            {
                return false;
            }

            CellState self = GetField(x, y);
            CellState left1 = GetField(x - 1, y);
            CellState right1 = GetField(x + 1, y);
            CellState up1 = GetField(x, y - 1);
            CellState down1 = GetField(x, y + 1);

            bool valid = true;
            if (left1 != CellState.O && left1 == right1 && right1 == self)
            {
                valid = false;
            }
            else if (up1 != CellState.O && up1 == down1 && down1 == self)
            {
                valid = false;
            }

            return valid;
        }

        private bool ValidateRowCounting(int x, int y)
        {
            if (GetField(x, y) == CellState.O)
            {
                return false;
            }

            int count1 = 0;
            int count2 = 0;
            for (int counterX = 0; counterX < _fieldWidth; counterX++)
            {
                if (GetField(counterX, y) == CellState.A)
                {
                    count1++;
                }
                else if (GetField(counterX, y) == CellState.B)
                {
                    count2++;
                }
            }

            bool valid = true;
            if (count1 + count2 != _fieldWidth)
            {
                valid = false;
            }
            else if (count1 != _fieldWidth / 2)
            {
                valid = false;
            }
            else if (count2 != _fieldWidth / 2)
            {
                valid = false;
            }

            return valid;
        }

        private bool ValidateColumnCounting(int x, int y)
        {
            if (GetField(x, y) == CellState.O)
            {
                return false;
            }

            int count1 = 0;
            int count2 = 0;
            for (int counterY = 0; counterY < _fieldHeight; counterY++)
            {
                if (GetField(x, counterY) == CellState.A)
                {
                    count1++;
                }
                else if (GetField(x, counterY) == CellState.B)
                {
                    count2++;
                }
            }

            bool valid = true;
            if (count1 + count2 != _fieldHeight)
            {
                valid = false;
            }
            else if (count1 != _fieldHeight / 2)
            {
                valid = false;
            }
            else if (count2 != _fieldHeight / 2)
            {
                valid = false;
            }

            return valid;
        }

        private bool Validate(int x, int y)
        {
            CellState orig = GetField(x, y);
            bool ret = true;
            ret &= ValidateDoubleAround(x, y);
            ret &= ValidateInBetween(x, y);
            ret &= ValidateRowCounting(x, y);
            ret &= ValidateColumnCounting(x, y);
            return ret;
        }

        public void PrintField()
        {
            Console.WriteLine("=====================");
            for (int y = 0; y < _fieldHeight; y++)
            {
                Console.Write(" ");
                Console.WriteLine(String.Join('|', Field[y].Select(x => x switch
                {
                    CellState.A => "0",
                    CellState.B => "1",
                    _ => " "
                })));
            }

            Console.WriteLine("=====================");
        }

        public static bool CheckCompletable(BinaryPuzzleSolver puzzle)
        {
            for (int i = 0; i < puzzle._fieldHeight; i++)
            {
                for (int j = 0; j < puzzle._fieldWidth; j++)
                {
                    for (int y = 0; y < puzzle._fieldHeight; y++)
                    {
                        for (int x = 0; x < puzzle._fieldWidth; x++)
                        {
                            puzzle.CheckDoubleAround(x, y);
                        }
                    }

                    for (int y = 0; y < puzzle._fieldHeight; y++)
                    {
                        for (int x = 0; x < puzzle._fieldWidth; x++)
                        {
                            puzzle.CheckInBetween(x, y);
                        }
                    }

                    for (int y = 0; y < puzzle._fieldHeight; y++)
                    {
                        for (int x = 0; x < puzzle._fieldWidth; x++)
                        {
                            puzzle.CheckRowCounting(x, y);
                        }
                    }

                    for (int y = 0; y < puzzle._fieldHeight; y++)
                    {
                        for (int x = 0; x < puzzle._fieldWidth; x++)
                        {
                            puzzle.CheckColumnCounting(x, y);
                        }
                    }
                }
            }

            bool isCompleted = true;
            for (int y = 0; y < puzzle._fieldHeight; y++)
            {
                for (int x = 0; x < puzzle._fieldWidth; x++)
                {
                    isCompleted &= puzzle.Validate(x, y);
                }
            }

            return isCompleted;
        }

        public static bool CheckCompletable(CellState[][] field)
        {
            BinaryPuzzleSolver testPuzzle = new BinaryPuzzleSolver(field);
            return CheckCompletable(testPuzzle);
        }
    }

    public static class Program
    {
        public static double GetEmptyAmount(BinaryPuzzleSolver.CellState[][] field)
        {
            double total = 0;
            double empty = 0;
            for (int y = 0; y < field.Length; y++)
            {
                for (int x = 0; x < field[0].Length; x++)
                {
                    total += 1;
                    empty += field[y][x] == BinaryPuzzleSolver.CellState.O ? 1 : 0;
                }
            }
            return empty / total;
        }
        
        public static void Main()
        {
            const int chanceO = 60;
            const int chanceA = 20;
            const int chanceB = 20;

            const int width = 10;
            const int height = 8;
            
            Console.Write($"How few given symbols do you want? ({0.0.ToString("F1")} - {1.0.ToString("F1")}) (the less, the longer the generation takes): ");
            string? numberString = Console.ReadLine();
            double amountSymbols = 0.0;
            if (numberString is null || !double.TryParse(numberString, out amountSymbols))
            {
                Console.WriteLine($"ERROR! Can't read '{numberString}' as a number!");
                return;
            }
            if (amountSymbols < 0.0 || amountSymbols >= 1.0)
            {
                Console.WriteLine($"ERROR! Can't read '{numberString}' as a number between {0.0.ToString("F1")} and {1.0.ToString("F1")}!");
                return;
            }
            double amountEmpty = 1.0 - amountSymbols;
            Console.WriteLine($"Amount of symbols: {(width * height * amountSymbols).ToString("00")}");
            Console.WriteLine($"Amount of empty space: {(width * height * amountEmpty).ToString("00")}");

            BinaryPuzzleSolver.CellState[][] randomField = new BinaryPuzzleSolver.CellState[height][];
            for (int y = 0; y < height; y++)
            {
                randomField[y] = new BinaryPuzzleSolver.CellState[width];
            }

            Console.WriteLine("Please be patient, puzzle generation can take a while...");

            Random r = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int chance = r.Next(chanceO + chanceA + chanceB);
                    if (chance < chanceO)
                    {
                        randomField[y][x] = BinaryPuzzleSolver.CellState.O;
                    }
                    else if (chance < chanceO + chanceA)
                    {
                        randomField[y][x] = BinaryPuzzleSolver.CellState.A;
                    }
                    else
                    {
                        randomField[y][x] = BinaryPuzzleSolver.CellState.B;
                    }
                }
            }

            BinaryPuzzleSolver puzzle = new BinaryPuzzleSolver(randomField);
            while (!BinaryPuzzleSolver.CheckCompletable(puzzle))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int chance = r.Next(chanceO + chanceA + chanceB);
                        if (chance < chanceO)
                        {
                            randomField[y][x] = BinaryPuzzleSolver.CellState.O;
                        }
                        else if (chance < chanceO + chanceA)
                        {
                            randomField[y][x] = BinaryPuzzleSolver.CellState.A;
                        }
                        else
                        {
                            randomField[y][x] = BinaryPuzzleSolver.CellState.B;
                        }
                    }
                }

                puzzle = new BinaryPuzzleSolver(randomField);
            }
            puzzle = new BinaryPuzzleSolver(randomField);
            BinaryPuzzleSolver.CheckCompletable(puzzle);
            BinaryPuzzleSolver copyPuzzle = new BinaryPuzzleSolver(puzzle.Field);
            BinaryPuzzleSolver.CellState[][] fullField = copyPuzzle.Field;
            int lastX = -1;
            int lastY = -1;
            BinaryPuzzleSolver.CellState lastCell = BinaryPuzzleSolver.CellState.O;
            while (GetEmptyAmount(fullField) < amountEmpty)
            {
                while ((GetEmptyAmount(fullField) < amountEmpty) && BinaryPuzzleSolver.CheckCompletable(fullField))
                {
                    int y = r.Next(height);
                    int x = r.Next(width);
                    lastX = x;
                    lastY = y;
                    lastCell = fullField[y][x]; 
                    fullField[y][x] = BinaryPuzzleSolver.CellState.O;
                }
                if (!BinaryPuzzleSolver.CheckCompletable(fullField))
                {
                    fullField[lastY][lastX] = lastCell;
                }
            }

            BinaryPuzzleSolver solvable = new BinaryPuzzleSolver(fullField);
            Console.WriteLine("");
            Console.WriteLine("The rules are simple:");
            Console.WriteLine("- There are 0's and 1's");
            Console.WriteLine("- Only up to 2 of the same type can be next to each other horizontal and vertical");
            Console.WriteLine("    - (e.g. \"0011\" but not \"0001\")");
            Console.WriteLine("- A row or column contains the same number of 0's as of 1's");
            Console.WriteLine("");
            Console.WriteLine("The following puzzle is solvable:");
            solvable.PrintField();
            Console.Write("Press enter to see the solution.");
            Console.ReadLine();
            //BinaryPuzzleSolver solved = new BinaryPuzzleSolver(fullField);
            puzzle.PrintField();
        }
    }
}