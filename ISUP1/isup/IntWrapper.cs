public class IntWrapper
{
    public int Value = 0;

    public IntWrapper()
    {
    }

    public IntWrapper(int val)
    {
        this.Value = val;
    }

    public static IntWrapper[] CreateArray(int length)
    {
        IntWrapper[] array = new IntWrapper[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = new IntWrapper();
        }
        return array;
    }
}