//Part 1

using System.Text;

static string GetNumber(List<object[]> keyPad, (int x, int y) startingPoint) {
    var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");
    var inputString = new StringBuilder();

    var (X, Y) = (startingPoint.x, startingPoint.y);
    foreach (var line in input) {
        foreach (var c in line) {
            switch (c) {
                case 'U':
                    Y = Y - 1 >= 0 && keyPad[X][Y - 1] != null ? Y - 1 : Y;
                    break;
                case 'D':
                    Y = Y + 1 < keyPad.Count && keyPad[X][Y + 1] != null ? Y + 1 : Y;
                    break;
                case 'L':
                    X = X - 1 >= 0 && keyPad[X - 1][Y] != null ? X - 1 : X;
                    break;
                case 'R':
                    X = X + 1 < keyPad[Y].Length && keyPad[X + 1][Y] != null ? X + 1 : X;
                    break;
            }
        }
        inputString.Append(keyPad[Y][X]);
    }

    return inputString.ToString();
}

var shapeOne = new List<object[]> {
    new object[] { 1, 2, 3 },
    new object[] { 4, 5, 6 },
    new object[] { 7, 8, 9 }
};

Console.WriteLine(GetNumber(shapeOne, (1, 1)));
//45973

var shapeTwo = new List<object[]>
{
    new object[] { null, null, 1, null, null },
    new object[] { null, 2, 3, 4, null },
    new object[] { 5, 6, 7, 8, 9 },
    new object[] { null, 'A', 'B', 'C', null },
    new object[] { null, null, 'D', null, null }
};

Console.WriteLine(GetNumber(shapeTwo, (0, 2)));
//27CA4