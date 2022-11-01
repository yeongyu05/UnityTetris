public class Block
{
    public bool[,] I = new bool[5, 5] {
        { false, false, true, false, false },
        { false, false, true, false, false },
        { false, false, true, false, false },
        { false, false, true, false, false },
        { false, false, false, false, false },
    };
    public bool[,] O = new bool[5, 5] {
        { false, true, true, false, false },
        { false, true, true, false, false },
        { false, false, false, false, false },
        { false, false, false, false, false },
        { false, false, false, false, false },
    };

    public int Column;
    public int Row;
    public bool[,] Shape;
}
