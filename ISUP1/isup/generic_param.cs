/* generic_param.c - ISUP encoder/decoder library.


using System.Collections;

public enum table_mode
{
    TABLE_SET = 0,
    TABLE_NONSET = 1
}

public class _value_string
{
    public int value;
    public string strptr;
}

public class _spec
{
    public string name;
    public int nbit; // Number of bits
    public int sbit; // Starting bit
    public table_mode state; // Flag which defines whether or not the table is used to dump the parameter
    public Dictionary<int, string> map = new Dictionary<int, string>(); // Private param mapping

    public _spec()
    {
        //for (int i = 0; i < DefineConstants.MAX_TABLE_MAP_ELEMENTS; i++)
        //{
        //    map[i] = new Dictionary<int, string>();
        //}
    }
}

