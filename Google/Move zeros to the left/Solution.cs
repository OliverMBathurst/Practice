var integers = new[] { 1, 0, 6, 8, 0, 2, 5, 8, 0, 6, 0, 10 };
int readHead = integers.Length - 1, writeHead = readHead;

while (readHead >= 0)
{
    if (integers[readHead] != 0)
    {
        if (readHead != writeHead)
            integers[writeHead] = integers[readHead];
        
        writeHead--;
    }

    readHead--;
}

while (writeHead >= 0)
{
    integers[writeHead] = 0;
    writeHead--;
}

foreach (var integer in integers)
    Console.Write($"{integer} ");