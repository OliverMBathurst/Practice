using _File = System.IO.File;

var structure = BuildStructure(_File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt"));
while (structure.Parent != null)
    structure = structure.Parent;

SetDirectorySizes(structure);

static long PartOne(Directory root)
{
    var targetSize = 100000;
    var totalUnder100000 = 0L;
    if (root.Size <= targetSize)
        totalUnder100000 += root.Size;

    AddUnderTotal(root);

    void AddUnderTotal(Directory directory) {
        foreach (var dir in directory.Directories)
        {
            if (dir.Size <= targetSize)
                totalUnder100000 += dir.Size;

            AddUnderTotal(dir);
        }
    }

    return totalUnder100000;
}

var total = PartOne(structure);
Console.WriteLine(total); //1491614

var remainingToDelete = 30000000 - (70000000 - structure.Size);
var hashSetOfDirectorySizes = new HashSet<long>();

if (structure.Size >= remainingToDelete)
    hashSetOfDirectorySizes.Add(structure.Size);

void PartTwo(Directory root)
{
    foreach (var directory in root.Directories)
    {
        if (directory.Size >= remainingToDelete)
        {
            hashSetOfDirectorySizes.Add(directory.Size);
            PartTwo(directory);
        }
    }
}

PartTwo(structure);
Console.WriteLine(hashSetOfDirectorySizes.OrderBy(x => x).First()); //6400111

#region Helpers and classes
static Directory BuildStructure(string[] instructions)
{
    var structure = new Directory("/", null);

    foreach (var line in instructions)
    {
        var split = line.Split(" ");
        if (split[0] == "$")
        {
            if (split[1] == "ls")
            {
                foreach (var file in structure.Files)
                {
                    Console.WriteLine(file.Name);
                }
            }
            else if (split[1] == "cd")
            {
                if (split[2] == "/")
                {
                    while (structure.Parent != null)
                        structure = structure.Parent;
                }
                else if (split[2] == "..")
                {
                    structure = structure.Parent;
                }
                else
                {
                    var dir = structure.Directories.FirstOrDefault(x => x.Name == split[2]);
                    if (dir != null)
                    {
                        structure = dir;
                    }
                }
            }
        }
        else if (split[0] == "dir")
        {
            structure.AddDirectory(new Directory(split[1], structure));
        }
        else
        {
            structure.AddFile(new File(split[1], int.Parse(split[0])));
        }
    }

    return structure;
}

static long SetDirectorySizes(Directory root)
{
    var directorySize = 0L;
    foreach (var directory in root.Directories)
        directorySize += SetDirectorySizes(directory);

    foreach (var file in root.Files)
        directorySize += file.Size;

    root.SetSize(directorySize);

    return directorySize;
}


public sealed class Directory {
    public Directory(string name, Directory parent) 
    {
        Name = name;
        Parent = parent;
    }

    public string Name { get; private init; }

    public long Size { get; private set; }

    public Directory Parent { get; private init; }

    public List<Directory> Directories { get; } = new List<Directory>();

    public List<File> Files { get; } = new List<File>();

    public void AddDirectory(Directory d) => Directories.Add(d);

    public void AddFile(File f) => Files.Add(f);

    public void SetSize(long size) => Size = size;
}

public readonly struct File {
    public File(string name, int size) 
    {
        Name = name;
        Size = size;
    }

    public string Name { get; private init; }

    public int Size { get; private init; }
}

#endregion