using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

public static class Globals
{


    // --------------------------------------------------------------------------------
    public static isupss7_optional[] iam_names_itu = Arrays.PadReferenceTypeArrayWithDefaultInstances<isupss7_optional>(DefineConstants.MAX_OPTIONAL_PARAMS, new isupss7_optional[] { isupss7_optional.CALLING_PARTY_NUMBER, isupss7_optional.ORIGINAL_CALLED_NUMBER, isupss7_optional.GENERIC_DIGITS, isupss7_optional.PROPAGATION_DELAY_COUNTER, isupss7_optional.NONE });
    public static isupss7_optional[] iam_names_ansi = Arrays.PadReferenceTypeArrayWithDefaultInstances<isupss7_optional>(DefineConstants.MAX_OPTIONAL_PARAMS, new isupss7_optional[] { isupss7_optional.CALLING_PARTY_NUMBER, isupss7_optional.CHARGE_NUMBER, isupss7_optional.GENERIC_DIGITS, isupss7_optional.GENERIC_NAME, isupss7_optional.JURISDICTION_INFORMATION, isupss7_optional.ORIGINAL_CALLED_NUMBER, isupss7_optional.ORIGINATING_LINE_INFORMATION, isupss7_optional.NONE });
    public static isupss7_optional[] acm_names_ansi = Arrays.PadReferenceTypeArrayWithDefaultInstances<isupss7_optional>(DefineConstants.MAX_OPTIONAL_PARAMS, new isupss7_optional[] { isupss7_optional.NONE });
    public static isupss7_optional[] acm_names_itu = Arrays.PadReferenceTypeArrayWithDefaultInstances<isupss7_optional>(DefineConstants.MAX_OPTIONAL_PARAMS, new isupss7_optional[] { isupss7_optional.NONE });
    // --------------------------------------------------------------------------------


    // --------------------------------------------------------------------------------
    // Attach the text 'add' to the parameter 'dumped'. It mallocs the necessary space.
    // 'fill_size' sets the minimum size to be filled.

    public static int Attach(ref string[] dumped, ref string add, int fill_size)
    {
        if (add != null)
        {
            int size = (fill_size > add.Length) ? fill_size : add.Length;
            if (dumped != null)
            {
                int ori_dumped_length = dumped.Length;
                Array.Resize(ref dumped, ori_dumped_length + size + 1);
                Array.Fill(dumped, " ", ori_dumped_length, size);
                dumped[ori_dumped_length + size] = null;

                // Copiar caracteres de la cadena 'add' a los elementos del arreglo 'dumped'
                for (int i = 0; i < add.Length; i++)
                {
                    dumped[ori_dumped_length + i] = add[i].ToString();
                }
            }
            else
            {
                dumped = new string[size + 1];
                Array.Fill(dumped, " ");
                dumped[size] = null;

                // Copiar caracteres de la cadena 'add' al arreglo 'dumped'
                for (int i = 0; i < add.Length; i++)
                {
                    dumped[i] = add[i].ToString();
                }
            }
        }
        return 1;
    }


    public static int attach_separator()
    {
        return attach_separator_generic();
    }


    public static int attach_separator_2()
	{
	  return attach_separator_generic();
	}

    public static int attach_line(ref string[] dumped, ref string name, ref string bits, ref string hex, ref string[] human)
    {
        attach(name);
        attach(bits);
        attach(hex);
        attach(human.ToString());
        attach("\n");
        return 1;
    }


    public static int attach_title(string sname)
    {
        attach(sname);
        attach("\n");
        return 1;
    }

    public static int attach(string str)
    {
        Console.WriteLine(str);
        return 1;
    }


    public static int attach_separator_generic()
    {
        attach("\n");
        return 1;
    }

    public static int encode_param(ref byte[] buffer, int offset, _spec s, ref int value)
    {
        buffer[offset] = (byte)(buffer[offset] | (value << s.sbit)); // Left shift
        return 1;
    }

    public static int decode_param(byte[] buffer, int offset, _spec s, ref int value)
    {
        int ret = -1;
        int tmp = (buffer[offset] >> s.sbit) & bitmasks[s.nbit]; // Right shift
        if (BelongsInt(tmp, ConvertDictionaryToValueStringArray(s.map)) != 0)
        {
            value = tmp;
            ret = 1;
        }
        return ret;
    }

    public static _value_string[] ConvertDictionaryToValueStringArray(Dictionary<int, string> dictionary)
    {
        _value_string[] valueStrings = new _value_string[dictionary.Count];
        int index = 0;

        foreach (var kvp in dictionary)
        {
            valueStrings[index] = new _value_string
            {
                value = kvp.Key,
                strptr = kvp.Value
            };
            index++;
        }

        return valueStrings;
    }


    public static int encode_sparam(ref byte[] buffer, int offset, _spec[] specs, IntWrapper[] places)
    {
        int i = 0;
        int offset_tmp = offset;
        buffer[offset_tmp] = 0; // initialize the buffer

        while (specs[i].nbit != DefineConstants.NONSET)
        {
            encode_param(ref buffer, offset_tmp, specs[i], ref places[i].Value);
            if ((specs[i].nbit + specs[i].sbit) == 8)
            {
                offset_tmp += 1;
                buffer[offset_tmp] = 0; // initialize the buffer position
            }
            i = i + 1;
        }
        return 1;
    }


    public static int decode_sparam(ref byte[] buffer, int offset, _spec[] specs, ref IntWrapper[] places)
    {
        int i = 0;
        int ret = 1;
        int offset_tmp = 0; // Start at the given offset
        while (specs[i].nbit != DefineConstants.NONSET && ret > 0)
        {
            ret = decode_param(buffer, offset + offset_tmp, specs[i], ref places[i].Value);
            if (ret > 0)
            {
                if (specs[i].nbit + specs[i].sbit == 8)
                {
                    offset_tmp += 1;
                }
                i++;
            }
            else
            {
                break;
            }
        }
        return ret;
    }

    public static int set_param(_spec[] specs, IntWrapper[] places, ref string index, int value)
    {
        int i = 0;
        int ret = -1;
        
        while (specs[i].nbit != DefineConstants.NONSET && ret < 0)
        {
            if (string.Compare(index, specs[i].name) == 0 && BelongsInt(value, ConvertDictionaryToValueStringArray(specs[i].map)) != 0)
            {
                //var intElem = Convert.ToInt16(((char)value).ToString());
                places[i].Value = value;
                ret = 1;
            }
            i++;
        }
        return ret;
    }

    public static int get_param(_spec[] specs, IntWrapper[] places, ref string index, ref int value)
    {
        int i = 0;
        int ret = -1;
        while ((specs[i].nbit != DefineConstants.NONSET) && (ret < 0))
        {
            if (string.Equals(index, specs[i].name))
            {
                value = places[i].Value;
                ret = 1;
            }
            i = i + 1;
        }
        return ret;
    }

    public static int encode_number(ref byte[] buffer, int offset, string[] addresses)
    {
        int numbers = addresses == null ? 0 : addresses.Length;
        int i = 0;
        int n1 = 0;
        int n2 = 0;
        IntWrapper[] addresses_places = { new IntWrapper(), new IntWrapper() };
        
        while (numbers > 1)
        {
            addresses_places[0].Value = Convert.ToInt32(addresses[i + 0]) - 0; // Converting from char to int.
            addresses_places[1].Value = Convert.ToInt32(addresses[i + 1]) - 0; // Converting from char to int.
            encode_sparam(ref buffer, offset, addresses_spec, addresses_places); // Assuming this method exists and has been properly converted to C#
            offset += 1;
            i += 2;
            numbers -= 2;
        }
        if (numbers == 1)
        {
            addresses_places[0].Value = Convert.ToInt32(addresses[addresses.Length - 1]) - 0; // Converting from char to int.
            addresses_places[1].Value = 0;
            encode_sparam(ref buffer, offset, addresses_spec, addresses_places); // Assuming this method exists and has been properly converted to C#
        }
        return 1;
    }


    public static int decode_number(byte[] buffer, int offset, ref string[] addressesOrig, int ndigits)
    {
        int i = 0;
        int n1 = 0;
        int n2 = 0;
        int numbers = ndigits;
        int ret = 1;

        var addressesTmp = new string[ndigits];

        if (addressesTmp == null)
        {
            return -1;
        }

        while ((numbers > 0) && (ret > 0))
        {
            IntWrapper[] addressesPlaces = { new IntWrapper(), new IntWrapper() }; // Declaración de la variable dentro del bucle.

            if (decode_sparam(ref buffer, offset, addresses_spec, ref addressesPlaces) > 0)
            {
                n1 = addressesPlaces[0].Value; // Actualización de los valores.
                n2 = addressesPlaces[1].Value;

                addressesTmp[i] = Convert.ToString(n1);
                if (numbers != 1)
                {
                    addressesTmp[i + 1] = Convert.ToString(n2);
                }

                i += 2;
                offset += 1;
                numbers -= 2;
            }
            else
            {
                ret = -1;
            }
        }

        if(i > 0) {
            addressesOrig = addressesTmp;    
        }

        return ret;
    }

    public static bool IsNumeric(string s)
    {
        if (string.IsNullOrEmpty(s) || s.Trim().Length == 0)
        {
            return false;
        }

        double result;
        return double.TryParse(s, out result);
    }

    public static int decode_ia5(byte[] buffer, int offset, ref char[] characters, int nchars)
    {
        int numbers = nchars;
        int ret = 1;
        int i = 0;

        characters = new char[nchars];

        // Initialization with specific character (using '0' as a placeholder for INIT_INT_TO_CHAR)
        for (int j = 0; j < nchars; j++)
        {
            characters[j] = (char)DefineConstants.INIT_INT_TO_CHAR; // assuming INIT_INT_TO_CHAR is '0'
        }

        while (numbers > 0 && ret > 0)
        {
            int int_tmp = (int)buffer[offset + i];
            char char_tmp = (char)int_tmp;
            characters[i] = char_tmp;
            i += 1;
            numbers -= 1;
        }

        return ret;
    }


    public static int encode_ia5(ref byte[] buffer, int offset, char[] characters)
    {
        int numbers = characters == null ? 0 : characters.Length;
        int i = 0;
        int intTmp = 0;
        char charTmp = ' ';

        while (numbers > 0)
        {
            charTmp = characters[i];
            intTmp = (int)charTmp;
            buffer[offset + i] = (byte)intTmp; // Using offset parameter
            i += 1;
            numbers -= 1;
        }

        return 1; // You may need to return a suitable value
    }

    internal static int[] bitmasks = {DefineConstants.MASK_NOTUSED, DefineConstants.MASK_01, DefineConstants.MASK_03, DefineConstants.MASK_07, DefineConstants.MASK_0f, DefineConstants.MASK_1f, DefineConstants.MASK_3f, DefineConstants.MASK_7f, DefineConstants.MASK_ff};

    public static int BelongsInt(int elem, _value_string[] map)
    {
        int i = 0;
        int found = 0;
        int intElem = -1; 

        //if (convElemStr)
        //{
        //    intElem = Convert.ToInt16(((char)elem).ToString());
        //}

        while (map[i].value != DefineConstants.NONSET && found == 0)
        {
            if (map[i].value == elem)
            {
                found = 1;
            }
            else
            {
                i += 1;
            }
        }

        return found;
    }


    // --------------------------------------------------------------------------------
    // From: stackoverflow.com @Christoph. (Modified @lmartin)
    public static string int2bin(int i)
    {
        int bits = sizeof(int) * 8;
        char[] str = new char[bits + 1];

        str[bits] = '\0';
        uint u = (uint)i;
        for (; bits > 0; bits--, u >>= 1)
        {
            str[bits - 1] = (u & 1) != 0 ? '1' : '0';
        }

        return new string(str);
    }


    public static _spec[] addresses_spec =
 {
    new _spec()
    {
        name = "address_right",
        nbit = 4,
        sbit = 0,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
        {
            {0, null},
            {1, null},
            {2, null},
            {3, null},
            {4, null},
            {5, null},
            {6, null},
            {7, null},
            {8, null},
            {9, null},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "address_left",
        nbit = 4,
        sbit = 4,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
        {
            {0, null},
            {1, null},
            {2, null},
            {3, null},
            {4, null},
            {5, null},
            {6, null},
            {7, null},
            {8, null},
            {9, null},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};

    public static int create_tree_isupss7lib(ref _isupss7_tree tree, isupss7_variant variant, isupss7_message_type message_type)
    {
        tree = new _isupss7_tree();
       
        tree.variant = variant;
        create_message_type(out tree.s_message_type);
        set_message_type(tree.s_message_type, message_type);

        switch (message_type)
        {
            case isupss7_message_type.ISUPSS7_IAM:
                if (create_iam(out tree.s_union.s_iam) < 0)
                {
                    destroy_tree_isupss7lib(tree);
                    return -1; // Error: Fallo al crear IAM
                }
                if (set_default_iam(tree.s_union.s_iam) < 0)
                {
                    destroy_iam(ref tree.s_union.s_iam);
                    destroy_tree_isupss7lib(tree);
                    return -1; // Error: Fallo al establecer valores predeterminados IAM
                }
                break;
            case isupss7_message_type.ISUPSS7_SAM:
                if (create_sam(out tree.s_union.s_sam) < 0)
                {
                    destroy_tree_isupss7lib(tree);
                    return -1; // Error: Fallo al crear SAM
                }
                if (set_default_sam(tree.s_union.s_sam) < 0)
                {
                    destroy_sam(tree.s_union.s_sam);
                    destroy_tree_isupss7lib(tree);
                    return -1; // Error: Fallo al establecer valores predeterminados SAM
                }
                break;
            case isupss7_message_type.ISUPSS7_ACM:
                if (create_acm(out tree.s_union.s_acm) < 0)
                {
                    destroy_tree_isupss7lib(tree);
                    return -1; // Error: Fallo al crear ACM
                }
                if (set_default_acm(tree.s_union.s_acm) < 0)
                {
                    destroy_acm(ref tree.s_union.s_acm);
                    destroy_tree_isupss7lib(tree);
                    return -1; // Error: Fallo al establecer valores predeterminados ACM
                }
                break;
            default:
                destroy_message_type(ref tree.s_message_type);
                destroy_tree_isupss7lib(tree);
                return -1; // Error: Tipo de mensaje no válido
        }
        return 1; // Éxito
    }

    public static int destroy_tree_isupss7lib(_isupss7_tree tree)
    {
        //if (tree == null)
        //{
        //    return -1; // Error: Árbol nulo
        //}

        isupss7_message_type message_type = (isupss7_message_type)tree.s_message_type.p_message_type.Value;

        switch (message_type)
        {
            case isupss7_message_type.ISUPSS7_IAM:
                if (destroy_iam(ref tree.s_union.s_iam) < 0)
                {
                    return -1; // Error al destruir IAM
                }
                break;
            case isupss7_message_type.ISUPSS7_SAM:
                if (destroy_sam(tree.s_union.s_sam) < 0)
                {
                    return -1; // Error al destruir SAM
                }
                break;
            case isupss7_message_type.ISUPSS7_ACM:
                if (destroy_acm(ref tree.s_union.s_acm) < 0)
                {
                    return -1; // Error al destruir ACM
                }
                break;
            default:
                return -1; // Tipo de mensaje no válido
        }

        if (destroy_message_type(ref tree.s_message_type) < 0)
        {
            return -1; // Error al destruir el tipo de mensaje
        }

        return 1; // Éxito
    }

    public static int decode_tree_isupss7lib(ref _isupss7_tree tree, isupss7_variant variant, byte[] buffer, int size)
    {
        tree = new _isupss7_tree();
        // Asumiendo que hay una forma de establecer todos los campos de tree en cero
        tree.variant = variant;
        create_message_type(out tree.s_message_type);
        if (decode_message_type(tree.s_message_type, ref buffer, 0) < 0)
        {
            goto error;
        }
        else
        {
            isupss7_message_type message_type = (isupss7_message_type) tree.s_message_type.p_message_type.Value;
            switch (message_type)
            {
                case isupss7_message_type.ISUPSS7_IAM:
                    if (create_iam(out tree.s_union.s_iam) < 0) { goto error; }
                    if (decode_iam(ref tree.s_union.s_iam, variant, buffer) < 0) { goto error; }
                    break;
                case isupss7_message_type.ISUPSS7_SAM:
                    break;
                //decode_sam(buffer, size, tree.s_sam, variant); break;
                case isupss7_message_type.ISUPSS7_ACM:
                    if (create_acm(out tree.s_union.s_acm) < 0) { goto error; }
                    if (decode_acm(ref tree.s_union.s_acm, variant, buffer) < 0) { goto error; }
                    break;
                default:
                    break;
            }
        }
        return 1;
    error:
        destroy_message_type(ref tree.s_message_type);
        // Suponiendo que hay una manera de liberar tree
        return -1;
    }

    /// <summary>
    /// este método se llama desde ambos mains
    /// </summary>
    public static int encode_tree_isupss7lib(ref byte[] buffer, ref int size, ref _isupss7_tree tree)
    {
        buffer = new byte[DefineConstants.MAX_SIZE_ENCODED_BUFF + 1];
        Array.Clear(buffer, 0, buffer.Length);
        isupss7_variant variant = tree.variant;
        isupss7_message_type message_type = (isupss7_message_type)tree.s_message_type.p_message_type.Value;
        size = 0;

        // First encode the message_type.
        encode_message_type(ref buffer, ref size, tree.s_message_type);

        // Different encoders for different message_types.
        switch (message_type)
        {
            case isupss7_message_type.ISUPSS7_IAM:
                encode_iam(ref buffer, ref size, tree.s_union.s_iam, variant);
                break;
            case isupss7_message_type.ISUPSS7_SAM:
                encode_sam(ref buffer, ref size, ref tree.s_union.s_sam, variant);
                break;
            case isupss7_message_type.ISUPSS7_ACM:
                encode_acm(ref buffer, ref size, tree.s_union.s_acm, variant);
                break;
            default:
                break;
        }
         
        Array.Resize(ref buffer, size);

        return 1;
    }



    public static int get_value_tree_isupss7lib(_isupss7_tree[] tree, ref string index, string[] value, int pos)
    {
        return value_tree(tree[0], ref index, 1, value, pos);
    }

    public static int value_tree(_isupss7_tree tree, ref string index, int op, string[] value, int pos)
    {
        if (value == null)
        {
            goto error;
        }
        if ((op == 0) && (value.Length < 1))
        {
            goto error;
        }
        isupss7_variant variant = tree.variant;
        isupss7_message_type message_type = (isupss7_message_type)tree.s_message_type.p_message_type.Value;
        int ret = -1;
        const string delims = "=>";
        string first_index = null;
        string second_index = null;
        string third_index = null;
        string index_cpy = index;
        if (index_cpy == null)
        {
            goto error;
        }
        string[] tokens = index_cpy.Split(delims);
        if (tokens.Length != 3)
        {
            goto error;
        }
        first_index = tokens[0];
        second_index = tokens[1];
        third_index = tokens[2];
        if ((first_index == null) || (second_index == null) || (third_index == null))
        {
            goto error;
        }
        if ((string.Compare("iam", first_index) == 0) && (message_type == isupss7_message_type.ISUPSS7_IAM))
        {
            ret = value_iam(tree.s_union.s_iam, ref second_index, ref third_index, variant, op, value, pos);
        }
        if ((string.Compare("acm", first_index) == 0) && (message_type == isupss7_message_type.ISUPSS7_ACM))
        {
            ret = value_acm(tree.s_union.s_acm, ref second_index, ref third_index, variant, op, value, pos);
        }
        return ret;

    error:
        return -1;
    }

    public static int value_acm(_acm acm, ref string second_index, ref string third_index, isupss7_variant variant, int op, string[] value, int pos)
    {
        int ret = 1;
        if (string.Compare("backward_call_indicators", second_index) == 0)
        {
            ret = value_backward_call_indicators(acm.s_backward_call_indicators, third_index, op, value, pos);
        }
        else
        {
            // Optional
            // Updating itu structures
            string[] strings_itu = new string[DefineConstants.MAX_OPTIONAL_PARAMS];
            object[][] places_itu = new object[DefineConstants.MAX_OPTIONAL_PARAMS][];
            // Updating ansi structures
            string[] strings_ansi = new string[DefineConstants.MAX_OPTIONAL_PARAMS];
            object[][] places_ansi = new object[DefineConstants.MAX_OPTIONAL_PARAMS][];
            switch (variant)
            {
                case isupss7_variant.ITU:
                    create_strings_optional(acm_names_itu, strings_itu);
                    acm_update_places_optionals_itu(acm, places_itu);
                    ret = value_optional(places_itu, acm_names_itu, strings_itu, ref second_index, ref third_index, op, value);
                    destroy_strings_optional(acm_names_itu, strings_itu);
                    break;
                case isupss7_variant.ANSI:
                    create_strings_optional(acm_names_ansi, strings_ansi);
                    acm_update_places_optionals_ansi(acm, places_ansi);
                    ret = value_optional(places_ansi, acm_names_ansi, strings_ansi, ref second_index, ref third_index, op, value);
                    destroy_strings_optional(acm_names_ansi, strings_ansi);
                    break;
            }
        }
        return ret;
    }

    public static int create_strings_optional(isupss7_optional[] names, string[] strings)
    {
        int i = 0;
        while (names[i] != isupss7_optional.NONE)
        {
            switch (names[i])
            {
                case isupss7_optional.CALLING_PARTY_NUMBER:
                    strings[i] = "calling_party_number";
                    break;
                case isupss7_optional.PROPAGATION_DELAY_COUNTER:
                    strings[i] = "propagation_delay_counter";
                    break;
                case isupss7_optional.GENERIC_DIGITS:
                    strings[i] = "generic_digits";
                    break;
                case isupss7_optional.JURISDICTION_INFORMATION:
                    strings[i] = "jurisdiction_information";
                    break;
                case isupss7_optional.CHARGE_NUMBER:
                    strings[i] = "charge_number";
                    break;
                case isupss7_optional.ORIGINATING_LINE_INFORMATION:
                    strings[i] = "originating_line_information";
                    break;
                case isupss7_optional.ORIGINAL_CALLED_NUMBER:
                    strings[i] = "original_called_number";
                    break;
                case isupss7_optional.GENERIC_NAME:
                    strings[i] = "generic_name";
                    break;
                default:
                    break;
            }
            i += 1;
        }
        return 1;
    }

    public static int create_backward_call_indicators(ref _backward_call_indicators backward_call_indicators)
    {
        backward_call_indicators = new _backward_call_indicators();
        if (backward_call_indicators == null) { goto error; }

        // Asumiendo que el tipo _backward_call_indicators es una estructura o clase con campos que son tipos primitivos,
        // el siguiente fragmento de código inicializa estos campos con un valor predefinido.
        foreach (var field in backward_call_indicators.GetType().GetFields())
        {
            if (field.FieldType == typeof(int))
            {
                field.SetValue(backward_call_indicators, DefineConstants.INIT_INT_TO_CHAR);
            }
            // Agregar aquí más condiciones para otros tipos, si es necesario.
        }

        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int destroy_backward_call_indicators(ref _backward_call_indicators backward_call_indicators)
    {
        backward_call_indicators = null;
        return 1;
    }


    // --------------------------------------------------------------------------------

    public static int dump_backward_call_indicators(string[] dumped, _backward_call_indicators backward_call_indicators)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_backward_call_indicators(backward_call_indicators, places) < 0)
        {
            goto error;
        }

        attach_separator();
        attach_title("backward_call_indicators");
       
        return 1;
    error:
        return -1;
    }


    // --------------------------------------------------------------------------------

    public static int encode_backward_call_indicators(ref byte[] buffer, int offset, _backward_call_indicators backward_call_indicators)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_backward_call_indicators(backward_call_indicators, places) < 0)
        {
            goto error;
        }

        if (encode_sparam(ref buffer, offset, backward_call_indicators_spec, places) < 0)
        {
            goto error;
        }

        return 1;

    error:
        return -1;
    }


    // --------------------------------------------------------------------------------

    public static int decode_backward_call_indicators(_backward_call_indicators backward_call_indicators, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_backward_call_indicators(backward_call_indicators, places) < 0)
        {
            goto error;
        }
        if (decode_sparam(ref buffer, offset, backward_call_indicators_spec, ref places) < 0)
        {
            goto error;
        }
        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int value_backward_call_indicators(_backward_call_indicators backward_call_indicators, string index, int op, string[] value, int pos)
	{
	  int ret = -1;
	  switch (op)
	  {
	  case 0:
		  ret = set_backward_call_indicators(backward_call_indicators, index, value, pos);
		  break;
	  case 1:
		  ret = get_backward_call_indicators(backward_call_indicators, index, value);
		  break;
	  };
	  return ret;
	}

// --------------------------------------------------------------------------------

	public static int set_default_backward_call_indicators(_backward_call_indicators backward_call_indicators)
	{
	  byte[] tmp = {0x55, 0x66};
	  return decode_backward_call_indicators(backward_call_indicators, ref tmp, 0);
	}



    // --------------------------------------------------------------------------------
    public static _spec[] backward_call_indicators_spec =
{
    new _spec()
    {
        name = "charge_indicator",
        nbit = 2,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "no indication"},
            {1, "no charge"},
            {2, "charge"},
            {3, "spare"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "called_partys_status_indicator",
        nbit = 2,
        sbit = 2,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "no indication"},
            {1, "subscriber free"},
            {2, "connect when free (national use)"},
            {3, "spare"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "called_partys_category_indicator",
        nbit = 2,
        sbit = 4,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "no indicator"},
            {1, "ordinary subscriber"},
            {2, "payphone"},
            {3, "spare"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "end_to_end_method_indicator",
        nbit = 2,
        sbit = 6,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "no end-to-end method available (only link-by-link method available)"},
            {1, "pass-along method available (national use)"},
            {2, "SCCP method available"},
            {3, "pass-along and SCCP methods available (national use)"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "interworking_indicator",
        nbit = 1,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "no interworking encountered (Signalling System No. 7 all the way)"},
            {1, "interworking encountered"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "end_to_end_information_indicator",
        nbit = 1,
        sbit = 1,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "no end-to-end information available"},
            {1, "end-to-end information available"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "isdn_user_part_indicator",
        nbit = 1,
        sbit = 2,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "ISDN user part not used all the way"},
            {1, "ISDN user part used all the way"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "holding_indicator",
        nbit = 1,
        sbit = 3,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "Holding not requested"},
            {1, "Holding requested"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "isdn_access_indicator",
        nbit = 1,
        sbit = 4,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "terminating access non-ISDN"},
            {1, "terminating access ISDN"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "echo_control_device_indicator",
        nbit = 1,
        sbit = 5,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "incoming half echo control device not included"},
            {1, "incoming half echo control device included"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = "sccp_method_indicator",
        nbit = 2,
        sbit = 6,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "no indication"},
            {1, "connectionless method available (national use)"},
            {2, "connection oriented method available"},
            {3, "connectionless and connection oriented methods available (national use)"},
            {DefineConstants.NONSET, ""}
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};




    // --------------------------------------------------------------------------------
    public static int update_places_backward_call_indicators(_backward_call_indicators backward_call_indicators, IntWrapper[] places)
    {
        places[0] = backward_call_indicators.p_charge_indicator;
        places[1] = backward_call_indicators.p_called_partys_status_indicator;
        places[2] = backward_call_indicators.p_called_partys_category_indicator;
        places[3] = backward_call_indicators.p_end_to_end_method_indicator;
        places[4] = backward_call_indicators.p_interworking_indicator;
        places[5] = backward_call_indicators.p_end_to_end_information_indicator;
        places[6] = backward_call_indicators.p_isdn_user_part_indicator;
        places[7] = backward_call_indicators.p_holding_indicator;
        places[8] = backward_call_indicators.p_isdn_access_indicator;
        places[9] = backward_call_indicators.p_echo_control_device_indicator;
        places[10] = backward_call_indicators.p_sccp_method_indicator;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int set_backward_call_indicators(_backward_call_indicators backward_call_indicators, string index, string[] value, int pos)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;

        if (IsNumeric(value[pos]))
        {
            value_tmp = Convert.ToInt32(value[pos].ToString());
        }
        else
        {
            goto error;
        }

        if (update_places_backward_call_indicators(backward_call_indicators, places) < 0) goto error;
        if (set_param(backward_call_indicators_spec, places, ref index, value_tmp) < 0) goto error;

        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------
    public static int get_backward_call_indicators(_backward_call_indicators backward_call_indicators, string index, string[] value)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;
        if (update_places_backward_call_indicators(backward_call_indicators, places) < 0) goto error;
        if (get_param(backward_call_indicators_spec, places, ref index, ref value_tmp) < 0) goto error;
        value = new string[DefineConstants.MAX_DIGITS_GET + 1];
        for (int i = 0; i < value.Length; i++)
            value[i] = Convert.ToString(DefineConstants.INIT_INT_TO_CHAR);
        string valueStr = value_tmp.ToString();
        value = new string[1] { valueStr };
        return 1;

    error:
        return -1;
    }




    // --------------------------------------------------------------------------------

    public static int create_called_party_number(ref _called_party_number called_party_number)
{
        called_party_number = new _called_party_number();
        if (called_party_number == null)
        {
            goto error;
        }

        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int destroy_called_party_number(_called_party_number called_party_number)
    {
        called_party_number = null;
        return 1;
    }

   
    // --------------------------------------------------------------------------------

    public static int get_length_encode_called_party_number(_called_party_number called_party_number)
    {
        int digits = called_party_number.address_signals == null ? 0 : called_party_number.address_signals.Length;
        int digits_nbytes = (digits % 2 == 0) ? (digits / 2) : (digits / 2) + 1;
        int nbytes = 1 + 2 + digits_nbytes;
        return nbytes;
    }


     



    // --------------------------------------------------------------------------------

    public static int decode_called_party_number(_called_party_number called_party_number, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        try
        {
            if (update_places_called_party_number(called_party_number, places) < 0)
                throw new Exception("Error in update_places_called_party_number");

            if (decode_sparam(ref buffer, offset + 1, called_party_number_spec, ref places) < 0)
                throw new Exception("Error in decode_sparam");

            int ndigits = ((buffer[offset] - 2) * 2) - called_party_number.p_odd_even_indicator.Value;
            decode_number(buffer, offset + 3, ref called_party_number.address_signals, ndigits);
            return 1;
        }
        catch
        {
            destroy_called_party_number(called_party_number);
            return -1;
        }
    }


    // --------------------------------------------------------------------------------

    public static int value_called_party_number(_called_party_number called_party_number, ref string index, int op, string[] value, int pos)
    {
        int ret = -1;
        switch (op)
        {
            case 0:
                ret = set_called_party_number(called_party_number, index, value, pos);
                break;
            case 1:
                ret = get_called_party_number(called_party_number, index, value);
                break;
        }
        return ret;
    }

    // --------------------------------------------------------------------------------

    public static int set_default_called_party_number(_called_party_number called_party_number)
    {
        byte[] tmp = { 0x06, 0x81, 0x10, 0x11, 0x22, 0x33, 0x04 };
        return decode_called_party_number(called_party_number, ref tmp, 0);
    }

    public static _spec[] called_party_number_spec =
    {
    new _spec()
    {
        name = "nature_of_address_indicator",
        nbit = 7,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            { 0, "spare" },
            { 1, "subscriber number (national use)" },
            { 2, "unknown (national use)" },
            { 3, "national (significant) number" },
            { 4, "international number" },
            { 5, "network-specific number (national use)" },
            { 6, "network routing number in national (significant) number format (national use)" },
            { 7, "network routing number in network-specific number format (national use)" },
            { 8, "network routing number concatenated with Called Directory Number (national use)" },
            { 9, "spare" },
            { 10, "spare" },
            { 11, "spare" },
            { 12, "spare" },
            { 13, "spare" },
            { 14, "spare" },
            { 15, "spare" },
            { 16, "spare" },
            { 17, "spare" },
            { 18, "spare" },
            { 19, "spare" },
            { 20, "spare" },
            { 21, "spare" },
            { 22, "spare" },
            { 23, "spare" },
            { 24, "spare" },
            { 25, "spare" },
            { 26, "spare" },
            { 27, "spare" },
            { 28, "spare" },
            { 29, "spare" },
            { 30, "spare" },
            { 31, "spare" },
            { 32, "spare" },
            { 33, "spare" },
            { 34, "spare" },
            { 35, "spare" },
            { 36, "spare" },
            { 37, "spare" },
            { 38, "spare" },
            { 39, "spare" },
            { 40, "spare" },
            { 41, "spare" },
            { 42, "spare" },
            { 43, "spare" },
            { 44, "spare" },
            { 45, "spare" },
            { 46, "spare" },
            { 47, "spare" },
            { 48, "spare" },
            { 49, "spare" },
            { 50, "spare" },
            { 51, "spare" },
            { 52, "spare" },
            { 53, "spare" },
            { 54, "spare" },
            { 55, "spare" },
            { 56, "spare" },
            { 57, "spare" },
            { 58, "spare" },
            { 59, "spare" },
            { 60, "spare" },
            { 61, "spare" },
            { 62, "spare" },
            { 63, "spare" },
            { 64, "spare" },
            { 65, "spare" },
            { 66, "spare" },
            { 67, "spare" },
            { 68, "spare" },
            { 69, "spare" },
            { 70, "spare" },
            { 71, "spare" },
            { 72, "spare" },
            { 73, "spare" },
            { 74, "spare" },
            { 75, "spare" },
            { 76, "spare" },
            { 77, "spare" },
            { 78, "spare" },
            { 79, "spare" },
            { 80, "spare" },
            { 81, "spare" },
            { 82, "spare" },
            { 83, "spare" },
            { 84, "spare" },
            { 85, "spare" },
            { 86, "spare" },
            { 87, "spare" },
            { 88, "spare" },
            { 89, "spare" },
            { 90, "spare" },
            { 91, "spare" },
            { 92, "spare" },
            { 93, "spare" },
            { 94, "spare" },
            { 95, "spare" },
            { 96, "spare" },
            { 97, "spare" },
            { 98, "spare" },
            { 99, "spare" },
            { 100, "spare" },
            { 101, "spare" },
            { 102, "spare" },
            { 103, "spare" },
            { 104, "spare" },
            { 105, "spare" },
            { 106, "spare" },
            { 107, "spare" },
            { 108, "spare" },
            { 109, "spare" },
            { 110, "spare" },
            { 111, "spare" },
            { 112, "spare" },
            { 113, "spare" },
            { 114, "spare" },
            { 115, "spare" },
            { 116, "spare" },
            { 117, "spare" },
            { 118, "spare" },
            { 119, "spare" },
            { 120, "spare" },
            { 121, "spare" },
            { 122, "spare" },
            { 123, "spare" },
            { 124, "spare" },
            { 125, "spare" },
            { 126, "spare" },
            { 127, "spare" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "odd_even_indicator",
        nbit = 1,
        sbit = 7,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            { 0, "even number of address signals" },
            { 1, "odd number of address signals" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "numbering_plan_indicator",
        nbit = 3,
        sbit = 4,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            { 0, "spare" },
            { 1, "ISDN (Telephony) numbering plan (ITU-T Recommendation E.164)" },
            { 2, "spare" },
            { 3, "Data numbering plan (ITU-T Recommendation X.121) (national use)" },
            { 4, "Telex numbering plan (ITU-T Recommendation F.69) (national use)" },
            { 5, "reserved for national use" },
            { 6, "reserved for national use" },
            { 7, "spare" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "internal_network_number_indicator",
        nbit = 1,
        sbit = 7,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            { 0, "routing to internal network number allowed" },
            { 1, "routing to internal network number not allowed" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};



    // --------------------------------------------------------------------------------

    public static int update_places_called_party_number(_called_party_number called_party_number, IntWrapper[] places)
    {
        places[0] = called_party_number.p_nature_of_address_indicator;
        places[1] = called_party_number.p_odd_even_indicator;
        places[2] = called_party_number.p_numbering_plan_indicator;
        places[3] = called_party_number.p_internal_network_number_indicator;
        return 1;
    }


    // --------------------------------------------------------------------------------
    public static int set_called_party_number(_called_party_number called_party_number, string index, string[] value, int pos)
    {
        try
        {
            if (index == "odd_even_indicator")
            {
                throw new Exception("Invalid index");
            }

            if (index == "address_signals")
            {
                if (!IsNumeric(value[pos].ToString()))
                    throw new Exception("Value is not numeric");

                called_party_number.address_signals = new string[] { value[pos] };

                if (called_party_number.address_signals == null)
                    throw new Exception("Failed to duplicate address signals");

                // Update the odd even param
                int size = value.Length;
                called_party_number.p_odd_even_indicator.Value = (size % 2 == 0) ? 0 : 1;
            }
            else
            {
                IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
                int valueTmp = -1;

                if (IsNumeric(value[pos].ToString()))
                    valueTmp = int.Parse(new string(value[pos].ToString()));
                else
                    throw new Exception("Value is not numeric");

                if (update_places_called_party_number(called_party_number, places) < 0)
                    throw new Exception("update_places_called_party_number failed");

                if (set_param(called_party_number_spec, places, ref index, valueTmp) < 0)
                    throw new Exception("set_param failed");
            }

            return 1;
        }
        catch
        {
            return -1;
        }
    }


    // --------------------------------------------------------------------------------
    public static int get_called_party_number(_called_party_number called_party_number, string index, string[] value)
    {
        try
        {
            value = new string[0];  // Initialize value

            if (index == "odd_even_indicator")
                throw new ArgumentException("Invalid index: odd_even_indicator");

            if (index == "address_signals")
            {
                value = called_party_number.address_signals;
                if (value == null)
                    throw new Exception("Failed to duplicate address_signals");
            }
            else
            {
                IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
                int value_tmp = -1;

                if (update_places_called_party_number(called_party_number, places) < 0)
                    throw new Exception("update_places_called_party_number failed");

                if (get_param(called_party_number_spec, places, ref index, ref value_tmp) < 0)
                    throw new Exception("GetParam failed");

                value = new string[DefineConstants.MAX_DIGITS_GET + 1];  // Replace MAX_DIGITS_GET with the actual value
                for (int i = 0; i < value.Length; i++)
                    value[i] = DefineConstants.INIT_INT_TO_CHAR.ToString();  // Replace INIT_INT_TO_CHAR with the actual value

                string valueTmpString = value_tmp.ToString();
                Array.Copy(valueTmpString.ToCharArray(), value, valueTmpString.Length);
            }

            return 1;
        }
        catch
        {
            return -1;
        }
    }

    // --------------------------------------------------------------------------------

    public static int create_calling_party_category(ref _calling_party_category calling_party_category)
    {
        calling_party_category = new _calling_party_category();
        if (calling_party_category == null)
        {
            goto error;
        }

        // Suponiendo que el tipo _calling_party_category es una estructura o clase con campos que son tipos primitivos,
        // el siguiente fragmento de código inicializa estos campos con un valor predefinido.
        foreach (var field in calling_party_category.GetType().GetFields())
        {
            if (field.FieldType == typeof(int))
            {
                field.SetValue(calling_party_category, DefineConstants.INIT_INT_TO_CHAR);
            }
            // Agregar aquí más condiciones para otros tipos, si es necesario.
        }

        return 1;

    error:
        return -1;
    }




    // --------------------------------------------------------------------------------

    public static int destroy_calling_party_category(ref _calling_party_category calling_party_category)
    {
        calling_party_category = null;
        return 1;
    }



    // --------------------------------------------------------------------------------

    public static int encode_calling_party_category(ref byte[] buffer, int offset, ref _calling_party_category calling_party_category)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_calling_party_category(calling_party_category, ref places) < 0) goto error;
        if (encode_sparam(ref buffer, offset, calling_party_category_spec, places) < 0) goto error; // Use offset here
        return 1;
    error:
        return -1;
    }


    // --------------------------------------------------------------------------------

    public static int value_calling_party_category(ref _calling_party_category calling_party_category, ref string index, int op, string[] value, int pos)
	{
	  int ret = -1;
	  switch (op)
	  {
	  case 0:
		  ret = set_calling_party_category(calling_party_category, index, value, pos);
		  break;
	  case 1:
		  ret = get_calling_party_category(ref calling_party_category, index, value);
		  break;
	  };
	  return ret;
	}

    public static int set_calling_party_category(_calling_party_category calling_party_category, string index, string[] value, int pos)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;
        if (IsNumeric(value[pos].ToString()))
        {
            value_tmp = Convert.ToInt32(value[pos].ToString());
        }
        else
        {
            goto error;
        }
        if (update_places_calling_party_category(calling_party_category, ref places) < 0)
        {
            goto error;
        }
        if (set_param(calling_party_category_spec, places, ref index, value_tmp) < 0)
        {
            goto error;
        }
        return 1;

    error:
        return -1;
    }


    public static int get_calling_party_category(ref _calling_party_category calling_party_category, string index, string[] value)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int valueTmp = -1;
        if (update_places_calling_party_category(calling_party_category, ref places) < 0)
        {
            value = null;
            return -1;
        }
        if (get_param(calling_party_category_spec, places, ref index, ref valueTmp) < 0)
        {
            value = null;
            return -1;
        }

        value = new string[DefineConstants.MAX_DIGITS_GET + 1];
        for (int i = 0; i < DefineConstants.MAX_DIGITS_GET + 1; i++)
        {
            value[i] = Convert.ToString(DefineConstants.INIT_INT_TO_CHAR);
        }

        string valueString = valueTmp.ToString();
        if (valueString.Length > DefineConstants.MAX_DIGITS_GET)
        {
            value = null;
            return -1;
        }

        value = new string[] { valueString };  
        return 1;
    }


    // --------------------------------------------------------------------------------

    public static int SetDefaultCallingPartyCategory(_calling_party_category calling_party_category)
    {
        byte[] tmp = new byte[] { 0x0e };
        return decode_calling_party_category(ref calling_party_category, ref tmp, 0);
    }

    public static int decode_calling_party_category(ref _calling_party_category calling_party_category,ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_calling_party_category(calling_party_category, ref places) < 0) goto error;
        if (decode_sparam(ref buffer, offset, calling_party_category_spec, ref places) < 0) goto error;
        return 1;
    error:
        return -1;
    }


    public static _spec[] calling_party_category_spec =
    {
    new _spec()
    {
        name = "calling_party_category",
        nbit = 8,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            { 0, "calling party's category unknown at this time (national use)" },
            { 1, "operator, language French" },
            { 2, "operator, language English" },
            { 3, "operator, language German" },
            { 4, "operator, language Russian" },
            { 5, "operator, language Spanish" },
            { 6, "available to Administrations" },
            { 7, "available to Administrations" },
            { 8, "available to Administrations" },
            { 9, "reserved (national use)" },
            { 10, "ordinary calling subscriber" },
            { 11, "calling subscriber with priority" },
            { 12, "data call (voice band data)" },
            { 13, "test call" },
            { 14, "IEPS call marking for preferential call set up" },
            { 15, "payphone" },
            { 16, "spare" },
            { 17, "spare" },
            { 18, "spare" },
            { 19, "spare" },
            { 20, "spare" },
            { 21, "spare" },
            { 22, "spare" },
            { 23, "spare" },
            { 24, "spare" },
            { 25, "spare" },
            { 26, "spare" },
            { 27, "spare" },
            { 28, "spare" },
            { 29, "spare" },
            { 30, "spare" },
            { 31, "spare" },
            { 32, "spare" },
            { 33, "spare" },
            { 34, "spare" },
            { 35, "spare" },
            { 36, "spare" },
            { 37, "spare" },
            { 38, "spare" },
            { 39, "spare" },
            { 40, "spare" },
            { 41, "spare" },
            { 42, "spare" },
            { 43, "spare" },
            { 44, "spare" },
            { 45, "spare" },
            { 46, "spare" },
            { 47, "spare" },
            { 48, "spare" },
            { 49, "spare" },
            { 50, "spare" },
            { 51, "spare" },
            { 52, "spare" },
            { 53, "spare" },
            { 54, "spare" },
            { 55, "spare" },
            { 56, "spare" },
            { 57, "spare" },
            { 58, "spare" },
            { 59, "spare" },
            { 60, "spare" },
            { 61, "spare" },
            { 62, "spare" },
            { 63, "spare" },
            { 64, "spare" },
            { 65, "spare" },
            { 66, "spare" },
            { 67, "spare" },
            { 68, "spare" },
            { 69, "spare" },
            { 70, "spare" },
            { 71, "spare" },
            { 72, "spare" },
            { 73, "spare" },
            { 74, "spare" },
            { 75, "spare" },
            { 76, "spare" },
            { 77, "spare" },
            { 78, "spare" },
            { 79, "spare" },
            { 80, "spare" },
            { 81, "spare" },
            { 82, "spare" },
            { 83, "spare" },
            { 84, "spare" },
            { 85, "spare" },
            { 86, "spare" },
            { 87, "spare" },
            { 88, "spare" },
            { 89, "spare" },
            { 90, "spare" },
            { 91, "spare" },
            { 92, "spare" },
            { 93, "spare" },
            { 94, "spare" },
            { 95, "spare" },
            { 96, "spare" },
            { 97, "spare" },
            { 98, "spare" },
            { 99, "spare" },
            { 100, "spare" },
            { 101, "spare" },
            { 102, "spare" },
            { 103, "spare" },
            { 104, "spare" },
            { 105, "spare" },
            { 106, "spare" },
            { 107, "spare" },
            { 108, "spare" },
            { 109, "spare" },
            { 110, "spare" },
            { 111, "spare" },
            { 112, "spare" },
            { 113, "spare" },
            { 114, "spare" },
            { 115, "spare" },
            { 116, "spare" },
            { 117, "spare" },
            { 118, "spare" },
            { 119, "spare" },
            { 120, "spare" },
            { 121, "spare" },
            { 122, "spare" },
            { 123, "spare" },
            { 124, "spare" },
            { 125, "spare" },
            { 126, "spare" },
            { 127, "spare" },
            { 128, "spare" },
            { 129, "spare" },
            { 130, "spare" },
            { 131, "spare" },
            { 132, "spare" },
            { 133, "spare" },
            { 134, "spare" },
            { 135, "spare" },
            { 136, "spare" },
            { 137, "spare" },
            { 138, "spare" },
            { 139, "spare" },
            { 140, "spare" },
            { 141, "spare" },
            { 142, "spare" },
            { 143, "spare" },
            { 144, "spare" },
            { 145, "spare" },
            { 146, "spare" },
            { 147, "spare" },
            { 148, "spare" },
            { 149, "spare" },
            { 150, "spare" },
            { 151, "spare" },
            { 152, "spare" },
            { 153, "spare" },
            { 154, "spare" },
            { 155, "spare" },
            { 156, "spare" },
            { 157, "spare" },
            { 158, "spare" },
            { 159, "spare" },
            { 160, "spare" },
            { 161, "spare" },
            { 162, "spare" },
            { 163, "spare" },
            { 164, "spare" },
            { 165, "spare" },
            { 166, "spare" },
            { 167, "spare" },
            { 168, "spare" },
            { 169, "spare" },
            { 170, "spare" },
            { 171, "spare" },
            { 172, "spare" },
            { 173, "spare" },
            { 174, "spare" },
            { 175, "spare" },
            { 176, "spare" },
            { 177, "spare" },
            { 178, "spare" },
            { 179, "spare" },
            { 180, "spare" },
            { 181, "spare" },
            { 182, "spare" },
            { 183, "spare" },
            { 184, "spare" },
            { 185, "spare" },
            { 186, "spare" },
            { 187, "spare" },
            { 188, "spare" },
            { 189, "spare" },
            { 190, "spare" },
            { 191, "spare" },
            { 192, "spare" },
            { 193, "spare" },
            { 194, "spare" },
            { 195, "spare" },
            { 196, "spare" },
            { 197, "spare" },
            { 198, "spare" },
            { 199, "spare" },
            { 200, "spare" },
            { 201, "spare" },
            { 202, "spare" },
            { 203, "spare" },
            { 204, "spare" },
            { 205, "spare" },
            { 206, "spare" },
            { 207, "spare" },
            { 208, "spare" },
            { 209, "spare" },
            { 210, "spare" },
            { 211, "spare" },
            { 212, "spare" },
            { 213, "spare" },
            { 214, "spare" },
            { 215, "spare" },
            { 216, "spare" },
            { 217, "spare" },
            { 218, "spare" },
            { 219, "spare" },
            { 220, "spare" },
            { 221, "spare" },
            { 222, "spare" },
            { 223, "spare" },
            { 224, "spare" },
            { 225, "spare" },
            { 226, "spare" },
            { 227, "spare" },
            { 228, "spare" },
            { 229, "spare" },
            { 230, "spare" },
            { 231, "spare" },
            { 232, "spare" },
            { 233, "spare" },
            { 234, "spare" },
            { 235, "spare" },
            { 236, "spare" },
            { 237, "spare" },
            { 238, "spare" },
            { 239, "spare" },
            { 240, "spare" },
            { 241, "spare" },
            { 242, "spare" },
            { 243, "spare" },
            { 244, "spare" },
            { 245, "spare" },
            { 246, "spare" },
            { 247, "spare" },
            { 248, "spare" },
            { 249, "spare" },
            { 250, "spare" },
            { 251, "spare" },
            { 252, "spare" },
            { 253, "spare" },
            { 254, "spare" },
            { 255, "spare" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};



    // --------------------------------------------------------------------------------
    public static int update_places_calling_party_category(_calling_party_category calling_party_category, ref IntWrapper[] places)
    {
        places[0] = calling_party_category.p_calling_party_category;
        return 1;
    }

    // --------------------------------------------------------------------------------
    
    // --------------------------------------------------------------------------------
    public static int GetCallingPartyCategory(_calling_party_category[] calling_party_category, ref string index, string[] value)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;

        if (update_places_calling_party_category(calling_party_category[0], ref places) < 0)
        {
            goto error;
        }

        if (get_param(calling_party_category_spec, places, ref index, ref value_tmp) < 0)
        {
            goto error;
        }

        value[0] = value_tmp.ToString();

        return 1;

    error:
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int create_forward_call_indicators(ref _forward_call_indicators forward_call_indicators)
    {
        forward_call_indicators = new _forward_call_indicators();
        if (forward_call_indicators == null)
        {
            goto error;
        }

        // Aquí suponemos que el tipo _forward_call_indicators es una estructura o clase con campos que son tipos primitivos.
        // El siguiente fragmento de código inicializa estos campos con un valor predefinido, similar al comportamiento de memset en C.
        foreach (var field in forward_call_indicators.GetType().GetFields())
        {
            if (field.FieldType == typeof(int))
            {
                field.SetValue(forward_call_indicators, DefineConstants.INIT_INT_TO_CHAR);
            }
            // Agregar aquí más condiciones para otros tipos, si es necesario
        }

        return 1;

    error:
        return -1;
    }


    // --------------------------------------------------------------------------------

    public static int destroy_forward_call_indicators(ref _forward_call_indicators forward_call_indicators)
    {
        forward_call_indicators = null;
        return 1;
    }


    // --------------------------------------------------------------------------------

    public static int encode_forward_call_indicators(ref byte[] buffer, int offset, ref _forward_call_indicators forward_call_indicators)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_forward_call_indicators(forward_call_indicators, ref places) < 0) goto error;
        if (encode_sparam(ref buffer, offset, forward_call_indicators_spec, places) < 0) goto error; // Use offset here
        return 1;
    error:
        return -1;
    }


    // --------------------------------------------------------------------------------

    public static int decode_forward_call_indicators(_forward_call_indicators forward_call_indicators, ref byte[] buffer, int offset)
	{
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_forward_call_indicators(forward_call_indicators, ref places) < 0)
	    {
		    goto error;
	    }
	    if (decode_sparam(ref buffer, offset, forward_call_indicators_spec, ref places) < 0)
	    {
		    goto error;
	    }
	    return 1;
	 
        error:
	      return -1;
	}

    // --------------------------------------------------------------------------------

    public static int value_forward_call_indicators(_forward_call_indicators forward_call_indicators, ref string index, int op, string[] value, int pos)
    {
        int ret = -1;
        switch (op)
        {
            case 0:
                ret = set_forward_call_indicators(forward_call_indicators, ref index, value, pos);
                break;
            case 1:
                ret = get_forward_call_indicators(forward_call_indicators, ref index, value);
                break;
        }
        return ret;
    }

    // --------------------------------------------------------------------------------

    public static int set_default_forward_call_indicators(_forward_call_indicators forward_call_indicators)
    {
        byte[] tmp = { 0x60, 0x10 };
        return decode_forward_call_indicators(forward_call_indicators, ref tmp, 0);
    }


    // --------------------------------------------------------------------------------
    public static _spec[] forward_call_indicators_spec =
{
    new _spec()
    {
        name = "national_international_call_indicator",
        nbit = 1,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "Call to be treated as a national call" },
            { 1, "Call to be treated as an international call" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "end_to_end_method_indicator",
        nbit = 2,
        sbit = 1,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "No End-to-end method available (only link-by-link method available)" },
            { 1, "Pass-along method available (national use)" },
            { 2, "SCCP method available" },
            { 3, "Pass-along and SCCP method available (national use)" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "interworking_indicator",
        nbit = 1,
        sbit = 3,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "No interworking encountered (No.7 signaling all the way)" },
            { 1, "Interworking encountered" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "end_to_end_information_indicator",
        nbit = 1,
        sbit = 4,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "No end-to-end information available" },
            { 1, "End-to-end information available" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "isdn_user_part_indicator",
        nbit = 1,
        sbit = 5,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "ISDN user part not used all the way" },
            { 1, "ISDN user part used all the way" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "isdn_user_part_preference_indicator",
        nbit = 2,
        sbit = 6,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "ISDN user part preferred all the way" },
            { 1, "ISDN user part not required all the way" },
            { 2, "ISDN user part required all the way" },
            { 3, "Spare" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "isdn_access_indicator",
        nbit = 1,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "Originating access non-ISDN" },
            { 1, "Originating access ISDN" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "sccp_method_indicator",
        nbit = 2,
        sbit = 1,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "No indication" },
            { 1, "Connectionless method available (national use)" },
            { 2, "Connection-oriented method available" },
            { 3, "Connectionless and -oriented method available (national use)" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "ported_number_translation_indicator",
        nbit = 1,
        sbit = 4,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "Number not translated" },
            { 1, "Number translated" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "query_on_release_attempt_indicator",
        nbit = 1,
        sbit = 5,
        state = table_mode.TABLE_SET,
        map =
        {
            { 0, "No QoR routing attempt in progress" },
            { 1, "QoR routing attempt in progress" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = {}
    }
};



    // --------------------------------------------------------------------------------
    public static int update_places_forward_call_indicators(_forward_call_indicators forward_call_indicators, ref IntWrapper[] places)
    {
        places[0] = forward_call_indicators.p_national_international_call_indicator;
        places[1] = forward_call_indicators.p_end_to_end_method_indicator;
        places[2] = forward_call_indicators.p_interworking_indicator;
        places[3] = forward_call_indicators.p_end_to_end_information_indicator;
        places[4] = forward_call_indicators.p_isdn_user_part_indicator;
        places[5] = forward_call_indicators.p_isdn_user_part_preference_indicator;
        places[6] = forward_call_indicators.p_isdn_access_indicator;
        places[7] = forward_call_indicators.p_sccp_method_indicator;
        places[8] = forward_call_indicators.p_ported_number_translation_indicator;
        places[9] = forward_call_indicators.p_query_on_release_attempt_indicator;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int set_forward_call_indicators(_forward_call_indicators forward_call_indicators, ref string index, string[] value, int pos)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;
        if (IsNumeric(value[pos].ToString()))
        {
            value_tmp = Convert.ToInt32(value[pos].ToString());
            if (update_places_forward_call_indicators(forward_call_indicators, ref places) < 0)
            {
                goto error;
            }
            if (set_param(forward_call_indicators_spec, places, ref index, value_tmp) < 0)
            {
                goto error;
            }
            return 1;
        }

    error:
        return -1;
    }

    // --------------------------------------------------------------------------------
    public static int get_forward_call_indicators(_forward_call_indicators forward_call_indicators, ref string index, string[] value)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS); // Cambiado de null a un arreglo vacío
        int value_tmp = -1;

        if (update_places_forward_call_indicators(forward_call_indicators, ref places) < 0)
        {
            goto error;
        }

        if (get_param(forward_call_indicators_spec, places, ref index, ref value_tmp) < 0)
        {
            goto error;
        }

        value = new string[DefineConstants.MAX_DIGITS_GET + 1];
        for (int i = 0; i < value.Length; i++)
        {
            value[i] = Convert.ToString(DefineConstants.INIT_INT_TO_CHAR);
        }

        string tempValue = value_tmp.ToString();
        Array.Copy(tempValue.ToCharArray(), value, tempValue.Length);

        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------
    public static int create_nature_of_connection_indicators(ref _nature_of_connection_indicators nature_of_connection_indicators)
    {
        nature_of_connection_indicators = new _nature_of_connection_indicators();
        if (nature_of_connection_indicators == null)
        {
            goto error;
        }

        // Suponiendo que _nature_of_connection_indicators es una estructura con campos de tipo entero
        // y que INIT_INT_TO_CHAR es una constante definida para la inicialización.
        //foreach (FieldInfo field in typeof(_nature_of_connection_indicators).GetFields())
        //{
        //    field.SetValue(nature_of_connection_indicators, DefineConstants.INIT_INT_TO_CHAR);
        //}

        return 1;

    error:
        return -1;
    }




    // --------------------------------------------------------------------------------

    public static int destroy_nature_of_connection_indicators(ref _nature_of_connection_indicators nature_of_connection_indicators)
    {
        nature_of_connection_indicators = null;
        return 1;
    }


    // --------------------------------------------------------------------------------

    public static int encode_nature_of_connection_indicators(ref byte[] buffer, int index, _nature_of_connection_indicators nature_of_connection_indicators)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_nature_of_connection_indicators(ref nature_of_connection_indicators, ref places) < 0) goto error;
        if (encode_sparam(ref buffer, index, nature_of_connection_indicators_spec, places) < 0) goto error;
        return 1;
    error:
        return -1;

    }

    // --------------------------------------------------------------------------------

    public static int decode_nature_of_connection_indicators(ref _nature_of_connection_indicators nature_of_connection_indicators, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_nature_of_connection_indicators(ref nature_of_connection_indicators, ref places) < 0)
        {
            goto error;
        }

        if (decode_sparam(ref buffer, offset, nature_of_connection_indicators_spec, ref places) < 0)
        {
            goto error;
        }

        return 1;

    error:
        return -1;
    }

    private static int update_places_nature_of_connection_indicators(ref _nature_of_connection_indicators nature_of_connection_indicators, ref IntWrapper[] places)
    {
        places[0] = nature_of_connection_indicators.p_satellite_indicator;
        places[1] = nature_of_connection_indicators.p_continuity_check_indicator;
        places[2] = nature_of_connection_indicators.p_echo_control_device_indicator;
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int set_default_nature_of_connection_indicators(_nature_of_connection_indicators[] nature_of_connection_indicators)
    {
        byte[] tmp = new byte[1] { 0x05 };
        return decode_nature_of_connection_indicators(ref nature_of_connection_indicators[0], ref tmp, 0);
    }

    public static _spec[] nature_of_connection_indicators_spec =
    {
    new _spec()
    {
        name = "satellite_indicator",
        nbit = 2,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            { 0, "No Satellite circuit in connection" },
            { 1, "One Satellite circuit in connection" },
            { 2, "Two Satellite circuits in connection" },
            { 3, "spare" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "continuity_check_indicator",
        nbit = 2,
        sbit = 2,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            { 0, "Continuity check not required" },
            { 1, "Continuity check required on this circuit" },
            { 2, "Continuity check performed on a previous circuit" },
            { 3, "spare" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = "echo_control_device_indicator",
        nbit = 1,
        sbit = 4,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            { 0, "Echo control device not included" },
            { 1, "Echo control device included" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};

     
    // --------------------------------------------------------------------------------
    public static int set_nature_of_connection_indicators(_nature_of_connection_indicators nature_of_connection_indicators, ref string index, string[] value, int pos)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        int valueTmp = -1;
        if (IsNumeric(value[pos].ToString()))
        {
            valueTmp = Convert.ToInt16(value[pos].ToString());
        }
        else
        {
            return -1;
        }

        if (update_places_nature_of_connection_indicators(ref nature_of_connection_indicators, ref places) < 0)
        {
            return -1;
        }

        if (set_param(nature_of_connection_indicators_spec, places, ref index, valueTmp) < 0)
        {
            return -1;
        }


        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int get_nature_of_connection_indicators(_nature_of_connection_indicators nature_of_connection_indicators, string index, string[] value)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int valueTmp = -1;
        if (update_places_nature_of_connection_indicators(ref nature_of_connection_indicators, ref places) < 0)
        {
            value = null;
            return -1;
        }
        if (get_param(nature_of_connection_indicators_spec, places, ref index, ref valueTmp) < 0)
        {
            value = null;
            return -1;
        }

        value = new string[DefineConstants.MAX_DIGITS_GET + 1];
        for (int i = 0; i < DefineConstants.MAX_DIGITS_GET + 1; i++)
        {
            value[i] = Convert.ToString(DefineConstants.INIT_INT_TO_CHAR);
        }

        string valueString = valueTmp.ToString();
        if (valueString.Length > DefineConstants.MAX_DIGITS_GET)
        {
            value = null;
            return -1;
        }

        value = new string[1] { valueString };
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int create_transmission_medium_requirement(ref _transmission_medium_requirement transmission_medium_requirement)
    {
        transmission_medium_requirement = new _transmission_medium_requirement();
        if (transmission_medium_requirement == null)
        {
            goto error;
        }

        // Asumiendo que el tipo _transmission_medium_requirement es una estructura o clase con campos que son tipos primitivos,
        // el siguiente fragmento de código inicializa estos campos con un valor predefinido.
        foreach (var field in transmission_medium_requirement.GetType().GetFields())
        {
            if (field.FieldType == typeof(int))
            {
                field.SetValue(transmission_medium_requirement, DefineConstants.INIT_INT_TO_CHAR);
            }
            // Agregar aquí más condiciones para otros tipos, si es necesario.
        }

        return 1;

    error:
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int destroy_transmission_medium_requirement(ref _transmission_medium_requirement transmission_medium_requirement)
    {
        transmission_medium_requirement = null;
        return 1;
    }


    // --------------------------------------------------------------------------------

    public static int encode_transmission_medium_requirement(ref byte[] buffer, int offset, _transmission_medium_requirement transmission_medium_requirement)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_transmission_medium_requirement(transmission_medium_requirement, ref places) < 0) goto error;
        if (encode_sparam(ref buffer, offset, transmission_medium_requirement_spec, places) < 0) goto error; // Use offset here
        return 1;
    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int decode_transmission_medium_requirement(_transmission_medium_requirement transmission_medium_requirement, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_transmission_medium_requirement(transmission_medium_requirement, ref places) < 0)
        {
            return -1;
        }
        if (decode_sparam(ref buffer, offset, transmission_medium_requirement_spec, ref places) < 0)
        {
            return -1;
        }
        return 1;
    }

     


    // --------------------------------------------------------------------------------

    public static int value_transmission_medium_requirement(_transmission_medium_requirement transmission_medium_requirement, ref string index, int op, string[] value, int pos)
    {
        int ret = -1;
        switch (op)
        {
            case 0:
                ret = set_transmission_medium_requirement(transmission_medium_requirement, index, value, pos);
                break;
            case 1:
                ret = get_transmission_medium_requirement(transmission_medium_requirement, ref index, value);
                break;
            default:
                ret = -1; // Asegúrate de manejar adecuadamente otros valores de "op" si es necesario
                break;
        }
        return ret;
    }



    // --------------------------------------------------------------------------------

    public static int set_default_transmission_medium_requirement(_transmission_medium_requirement transmission_medium_requirement)
    {
        byte[] tmp = new byte[1] { 0x03 };
        return decode_transmission_medium_requirement(transmission_medium_requirement, ref tmp, 0);
    }

    public static _spec[] transmission_medium_requirement_spec =
    {
    new _spec()
    {
        name = "transmission_medium_requirement",
        nbit = 8,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            { 0, "speech" },
            { 1, "spare" },
            { 2, "64 kbit/s unrestricted" },
            { 3, "3.1 kHz audio" },
            { 4, "reserved for alternate speech (service 2)/64 kbit/s unrestricted (service 1)" },
            { 5, "reserved for alternate 64 kbit/s unrestricted (service 1)/speech (service 2)" },
            { 6, "64 kbit/s preferred" },
            { 7, "2x64 kbit/s unrestricted" },
            { 8, "384 kbit/s unrestricted" },
            { 9, "1536 kbit/s unrestricted" },
            { 10, "1920 kbit/s unrestricted" },
            { 11, "spare" },
            { 12, "spare" },
            { 13, "spare" },
            { 14, "spare" },
            { 15, "spare" },
            { 16, "3x64 kbit/s unrestricted" },
            { 17, "4x64 kbit/s unrestricted" },
            { 18, "5x64 kbit/s unrestricted" },
            { 19, "spare" },
            { 20, "7x64 kbit/s unrestricted" },
            { 21, "8x64 kbit/s unrestricted" },
            { 22, "9x64 kbit/s unrestricted" },
            { 23, "10x64 kbit/s unrestricted" },
            { 24, "11x64 kbit/s unrestricted" },
            { 25, "12x64 kbit/s unrestricted" },
            { 26, "13x64 kbit/s unrestricted" },
            { 27, "14x64 kbit/s unrestricted" },
            { 28, "15x64 kbit/s unrestricted" },
            { 29, "16x64 kbit/s unrestricted" },
            { 30, "17x64 kbit/s unrestricted" },
            { 31, "18x64 kbit/s unrestricted" },
            { 32, "19x64 kbit/s unrestricted" },
            { 33, "20x64 kbit/s unrestricted" },
            { 34, "21x64 kbit/s unrestricted" },
            { 35, "22x64 kbit/s unrestricted" },
            { 36, "23x64 kbit/s unrestricted" },
            { 37, "spare" },
            { 38, "25x64 kbit/s unrestricted" },
            { 39, "26x64 kbit/s unrestricted" },
            { 40, "27x64 kbit/s unrestricted" },
            { 41, "28x64 kbit/s unrestricted" },
            { 42, "29x64 kbit/s unrestricted" },
            { DefineConstants.NONSET, null }
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};

    // --------------------------------------------------------------------------------
    //C++ TO C# CONVERTER TASK: C# does not have an equivalent to pointers to value types:
    //ORIGINAL LINE: int update_places_transmission_medium_requirement(struct _transmission_medium_requirement** transmission_medium_requirement, int* places[])
    public static int update_places_transmission_medium_requirement(_transmission_medium_requirement transmission_medium_requirement, ref IntWrapper[] places)
    {
        places[0] = transmission_medium_requirement.p_transmission_medium_requirement;
        return 1;
    }


    // --------------------------------------------------------------------------------
    public static int set_transmission_medium_requirement(_transmission_medium_requirement transmission_medium_requirement, string index, string[] value, int pos)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;

        try
        {
            if (IsNumeric(value[pos].ToString()))
                value_tmp = int.Parse(new string(value[0].ToString()));
            else
                throw new Exception("Value is not numeric");

            if (update_places_transmission_medium_requirement(transmission_medium_requirement, ref places) < 0)
                throw new Exception("update_places_transmission_medium_requirement failed");

            if (set_param(transmission_medium_requirement_spec, places, ref index, value_tmp) < 0)
                throw new Exception("set_param failed");

            return 1;
        }
        catch
        {
            return -1;
        }
    }

    // --------------------------------------------------------------------------------
    public static int get_transmission_medium_requirement(_transmission_medium_requirement transmission_medium_requirement, ref string index, string[] value)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;

        if (update_places_transmission_medium_requirement(transmission_medium_requirement, ref places) < 0)
        {
            goto error;
        }

        if (get_param(transmission_medium_requirement_spec, places, ref index, ref value_tmp) < 0)
        {
            goto error;
        }

        value = new string[DefineConstants.MAX_DIGITS_GET + 1];
        for (int i = 0; i < value.Length; i++)
        {
            value[i] = Convert.ToString(DefineConstants.INIT_INT_TO_CHAR);
        }

        string tempValue = value_tmp.ToString();
        Array.Copy(tempValue.ToCharArray(), value, tempValue.Length);

        return 1;

    error:
        return -1;
    }


    // --------------------------------------------------------------------------------

    public static int create_user_service_information(ref _user_service_information user_service_information)
    {
        user_service_information = new _user_service_information();
        if (user_service_information == null)
        {
            goto error;
        }

        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int destroy_user_service_information(ref _user_service_information user_service_information)
    {
        user_service_information = null;
        return 1;
    }



    // --------------------------------------------------------------------------------

    public static int get_length_encode_user_service_information(_user_service_information user_service_information)
    {
        int nbytes = 2; // TODO

        if (user_service_information.p_2_extension_indicator.Value == 0)
        {
            nbytes += 1;

            if (user_service_information.p_2a_extension_indicator.Value == 0)
            {
                nbytes += 1;
            }
        }

        if (user_service_information.p_2_information_transfer_rate.Value == 24)
        {
            nbytes += 1;
        }

        // Tenemos 21 octetos

        return 1 + nbytes;
    }

    // --------------------------------------------------------------------------------

    public static int encode_user_service_information(ref byte[] buffer, int offset, _user_service_information user_service_information)
    {
        // The entire parameter length minus the actual size byte.
        buffer[offset] = (byte)(get_length_encode_user_service_information(user_service_information) - 1);
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_user_service_information(user_service_information, ref places) < 0) goto error;
        if (encode_sparam(ref buffer, offset + 1, user_service_information_spec, places) < 0) goto error;
        
        //2a
        if (user_service_information.p_2_extension_indicator.Value == 0)
        {
            IntWrapper[] places_2a = new IntWrapper[DefineConstants.MAX_NPARAMS];

            if (update_places_user_service_information_2a(user_service_information, ref places_2a) < 0) goto error;
            if (encode_sparam(ref buffer, offset + 3, user_service_information_spec_2a, places_2a) < 0) goto error;
            
            //2b
            if (user_service_information.p_2a_extension_indicator.Value == 0)
            {
                IntWrapper[] places_2b = new IntWrapper[DefineConstants.MAX_NPARAMS];
                if (update_places_user_service_information_2b(ref user_service_information, ref places_2b) < 0) goto error;
                if (encode_sparam(ref buffer, offset + 4, user_service_information_spec_2b, places_2b) < 0) goto error;
            }
        }
        //21
        if (user_service_information.p_2_information_transfer_rate.Value == 24)
        {
            IntWrapper[] places_21 = new IntWrapper[DefineConstants.MAX_NPARAMS];
            if (update_places_user_service_information_21(user_service_information, ref places_21) < 0) goto error;
            if (encode_sparam(ref buffer, offset + 5, user_service_information_spec_21, places_21) < 0) goto error;
        }
        return 1;
    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int decode_user_service_information(ref _user_service_information user_service_information, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_user_service_information(user_service_information, ref places) < 0)
        {
            goto error;
        }

        if (decode_sparam(ref buffer, offset + 1, user_service_information_spec, ref places) < 0)
        {
            goto error;
        }

        //2a
        if (user_service_information.p_2_extension_indicator.Value == 0)
        {
            IntWrapper[] places_2a = new IntWrapper[DefineConstants.MAX_NPARAMS];

            if (update_places_user_service_information_2a(user_service_information, ref places_2a) < 0)
            {
                goto error;
            }

            if (decode_sparam(ref buffer, offset + 3, user_service_information_spec_2a, ref places_2a) < 0)
            {
                goto error;
            }

            //2b
            if (user_service_information.p_2a_extension_indicator.Value == 0)
            {
                IntWrapper[] places_2b = new IntWrapper[DefineConstants.MAX_NPARAMS];

                if (update_places_user_service_information_2b(ref user_service_information, ref places_2b) < 0)
                {
                    goto error;
                }
                 
                if (decode_sparam(ref buffer, offset + 4, user_service_information_spec_2b, ref places_2b) < 0)
                {
                    goto error;
                }
            }
        }

        //21
        if (user_service_information.p_2_information_transfer_rate.Value == 24)
        {
            IntWrapper[] places_21 = new IntWrapper[DefineConstants.MAX_NPARAMS];

            if (update_places_user_service_information_21(user_service_information, ref places_21) < 0)
            {
                goto error;
            }

            if (decode_sparam(ref buffer, offset + 5, user_service_information_spec_21, ref places_21) < 0)
            {
                goto error;
            }
        }

        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int value_user_service_information(_user_service_information[] user_service_information, ref string index, int op, string[] value)
    {
        int ret = -1;
        switch (op)
        {
            case 0:
                ret = set_user_service_information(user_service_information, ref index, value);
                break;
            case 1:
                ret = get_user_service_information(user_service_information, ref index, value);
                break;
        }
        return ret;
    }


    // --------------------------------------------------------------------------------

    public static int set_default_user_service_information(ref _user_service_information user_service_information)
    {
        byte[] tmp = { 0x02, 0x80, 0x80 };
        return decode_user_service_information(ref user_service_information, ref tmp, 0);
    }

    public static _spec[] user_service_information_spec =
    {
    new _spec()
    {
        name = "1_information_transfer_capability",
        nbit = 5,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "Speech"},
            {8, "Unrestricted digital information"},
            {9, "Restricted digital information"},
            {16, "3.1 kHz audio information"},
            {17, "7 kHz audio"},
            {18, "15 kHz audio"},
            {24, "Video"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "1_coding_standard",
        nbit = 2,
        sbit = 5,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "ITU-T"},
            {1, "ISO/IEC"},
            {2, "National standard"},
            {3, "Network standard"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "1_extension_indicator",
        nbit = 1,
        sbit = 7,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {1, "last octet"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "2_information_transfer_rate",
        nbit = 5,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "Packet mode calls"},
            {16, "64kbps"},
            {17, "2 * 64 kbps"},
            {19, "384 kbps"},
            {20, "1472 kbps (national ansi only)"},
            {21, "1536 kbps"},
            {23, "1920 kbps"},
            {24, "Multirate (64 kbps based)"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "2_transfer_mode",
        nbit = 2,
        sbit = 5,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "Circuit mode"},
            {1, "reserved"},
            {2, "packet mode"},
            {3, "reserved"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "2_extension_indicator",
        nbit = 1,
        sbit = 7,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "Information continues in the next octet"},
            {1, "last octet"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};



    public static _spec[] user_service_information_spec_2a =
{
    new _spec()
    {
        name = "2a_establishment",
        nbit = 2,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "demand"},
            {1, "reserved"},
            {2, "reserved"},
            {3, "reserved"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "2a_configuration",
        nbit = 2,
        sbit = 2,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "point-to-point"},
            {1, "reserved"},
            {2, "reserved"},
            {3, "reserved"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "2a_structure",
        nbit = 3,
        sbit = 4,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "default"},
            {1, "8 kHz integrity"},
            {2, "reserved"},
            {3, "reserved"},
            {4, "service data unit integrity"},
            {5, "reserved"},
            {6, "reserved"},
            {7, "unstructured"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "2a_extension_indicator",
        nbit = 1,
        sbit = 7,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "Information continues in the next octet"},
            {1, "last octet"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};

    public static _spec[] user_service_information_spec_2b =
    {
    new _spec()
    {
        name = "2b_information_transfer_rate",
        nbit = 5,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "Packet mode calls"},
            {16, "64kbps"},
            {17, "2 * 64 kbps"},
            {19, "384 kbps"},
            {20, "1472 kbps (national ansi only)"},
            {21, "1536 kbps"},
            {23, "1920 kbps"},
            {24, "Multirate (64 kbps based)"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "2b_symmetry",
        nbit = 2,
        sbit = 5,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "bi-directional symmetric"},
            {1, "reserved"},
            {2, "reserved"},
            {3, "reserved"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "2b_extension_indicator",
        nbit = 1,
        sbit = 7,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "Information continues in the next octet"},
            {1, "last octet"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};

    public static _spec[] user_service_information_spec_21 =
    {
    new _spec()
    {
        name = "21_rate_multiplier",
        nbit = 7,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "reserved"},
            {1, "reserved"},
            {2, "reserved"},
            {3, "reserved"},
            {4, "reserved"},
            {5, "reserved"},
            {6, "reserved"},
            {7, "reserved"},
            {8, "reserved"},
            {9, "reserved"},
            {10, "reserved"},
            {11, "reserved"},
            {12, "reserved"},
            {13, "reserved"},
            {14, "reserved"},
            {15, "reserved"},
            {16, "reserved"},
            {17, "reserved"},
            {18, "reserved"},
            {19, "reserved"},
            {20, "reserved"},
            {21, "reserved"},
            {22, "reserved"},
            {23, "reserved"},
            {24, "No procedures specified for U. S. networks"},
            {25, "No procedures specified for U. S. networks"},
            {26, "No procedures specified for U. S. networks"},
            {27, "No procedures specified for U. S. networks"},
            {28, "No procedures specified for U. S. networks"},
            {29, "No procedures specified for U. S. networks"},
            {30, "No procedures specified for U. S. networks"},
            {31, "reserved"},
            {32, "reserved"},
            {33, "reserved"},
            {34, "reserved"},
            {35, "reserved"},
            {36, "reserved"},
            {37, "reserved"},
            {38, "reserved"},
            {39, "reserved"},
            {40, "reserved"},
            {41, "reserved"},
            {42, "reserved"},
            {43, "reserved"},
            {44, "reserved"},
            {45, "reserved"},
            {46, "reserved"},
            {47, "reserved"},
            {48, "reserved"},
            {49, "reserved"},
            {50, "reserved"},
            {51, "reserved"},
            {52, "reserved"},
            {53, "reserved"},
            {54, "reserved"},
            {55, "reserved"},
            {56, "reserved"},
            {57, "reserved"},
            {58, "reserved"},
            {59, "reserved"},
            {60, "reserved"},
            {61, "reserved"},
            {62, "reserved"},
            {63, "reserved"},
            {64, "reserved"},
            {65, "reserved"},
            {66, "reserved"},
            {67, "reserved"},
            {68, "reserved"},
            {69, "reserved"},
            {70, "reserved"},
            {71, "reserved"},
            {72, "reserved"},
            {73, "reserved"},
            {74, "reserved"},
            {75, "reserved"},
            {76, "reserved"},
            {77, "reserved"},
            {78, "reserved"},
            {79, "reserved"},
            {80, "reserved"},
            {81, "reserved"},
            {82, "reserved"},
            {83, "reserved"},
            {84, "reserved"},
            {85, "reserved"},
            {86, "reserved"},
            {87, "reserved"},
            {88, "reserved"},
            {89, "reserved"},
            {90, "reserved"},
            {91, "reserved"},
            {92, "reserved"},
            {93, "reserved"},
            {94, "reserved"},
            {95, "reserved"},
            {96, "reserved"},
            {97, "reserved"},
            {98, "reserved"},
            {99, "reserved"},
            {100, "reserved"},
            {101, "reserved"},
            {102, "reserved"},
            {103, "reserved"},
            {104, "reserved"},
            {105, "reserved"},
            {106, "reserved"},
            {107, "reserved"},
            {108, "reserved"},
            {109, "reserved"},
            {110, "reserved"},
            {111, "reserved"},
            {112, "reserved"},
            {113, "reserved"},
            {114, "reserved"},
            {115, "reserved"},
            {116, "reserved"},
            {117, "reserved"},
            {118, "reserved"},
            {119, "reserved"},
            {120, "reserved"},
            {121, "reserved"},
            {122, "reserved"},
            {123, "reserved"},
            {124, "reserved"},
            {125, "reserved"},
            {126, "reserved"},
            {127, "reserved"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "21_extension_indicator",
        nbit = 1,
        sbit = 7,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "Information continues in the next octet"},
            {1, "last octet"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};

    // --------------------------------------------------------------------------------
    public static int update_places_user_service_information(_user_service_information user_service_information, ref IntWrapper[] places)
    {
        places[0] = user_service_information.p_1_information_transfer_capability;
        places[1] = user_service_information.p_1_coding_standard;
        places[2] = user_service_information.p_1_extension_indicator;
        places[3] = user_service_information.p_2_information_transfer_rate;
        places[4] = user_service_information.p_2_transfer_mode;
        places[5] = user_service_information.p_2_extension_indicator;
        return 1;
    }


    // --------------------------------------------------------------------------------
    public static int update_places_user_service_information_2a(_user_service_information user_service_information, ref IntWrapper[] places)
    {
        places[0] = user_service_information.p_2a_establishment;
        places[1] = user_service_information.p_2a_configuration;
        places[2] = user_service_information.p_2a_structure;
        places[3] = user_service_information.p_2a_extension_indicator;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int update_places_user_service_information_2b(ref _user_service_information user_service_information, ref IntWrapper[] places)
    {
        places[0] = user_service_information.p_2b_information_transfer_rate;
        places[1] = user_service_information.p_2b_symmetry;
        places[2] = user_service_information.p_2b_extension_indicator;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int update_places_user_service_information_21(_user_service_information user_service_information, ref IntWrapper[] places)
    {
        places[0] = user_service_information.p_21_rate_multiplier;
        places[1] = user_service_information.p_21_extension_indicator;
        return 1;
    }


    // --------------------------------------------------------------------------------
    public static int set_user_service_information(_user_service_information[] user_service_information, ref string index, string[] value)
    {
        int value_tmp = -1;
        if (!IsNumeric(value[0]))
        {
            goto error;
        }

        value_tmp = Convert.ToInt32(value[0]);

        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_user_service_information(user_service_information[0], ref places) < 0)
        {
            goto error;
        }

        if (set_param(user_service_information_spec, places, ref index, value_tmp) > 0)
        {
            goto done;
        }

        IntWrapper[] places_2a = new IntWrapper[DefineConstants.MAX_NPARAMS];
        if (update_places_user_service_information_2a(user_service_information[0], ref places_2a) < 0)
        {
            goto error;
        }

        if (set_param(user_service_information_spec_2a, places_2a, ref index, value_tmp) > 0)
        {
            goto done;
        }

        IntWrapper[] places_2b = new IntWrapper[DefineConstants.MAX_NPARAMS];
        if (update_places_user_service_information_2b(ref user_service_information[0], ref places_2b) < 0)
        {
            goto error;
        }

        if (set_param(user_service_information_spec_2b, places_2b, ref index, value_tmp) > 0)
        {
            goto done;
        }

        IntWrapper[] places_21 = new IntWrapper[DefineConstants.MAX_NPARAMS];
        if (update_places_user_service_information_21(user_service_information[0], ref places_21) < 0)
        {
            goto error;
        }

        if (set_param(user_service_information_spec_21, places_21, ref index, value_tmp) > 0)
        {
            goto done;
        }

    error:
        return -1;
    done:
        return 1;
    }


    // --------------------------------------------------------------------------------
    public static int get_user_service_information(_user_service_information[] user_service_information, ref string index, string[] value)
    {
        int value_tmp = -1;

        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_user_service_information(user_service_information[0], ref places) < 0)
        {
            goto error;
        }

        if (get_param(user_service_information_spec, places, ref index, ref value_tmp) > 0)
        {
            goto done;
        }

        IntWrapper[] places_2a = new IntWrapper[DefineConstants.MAX_NPARAMS];
        if (update_places_user_service_information_2a(user_service_information[0], ref places_2a) < 0)
        {
            goto error;
        }

        if (get_param(user_service_information_spec_2a, places_2a, ref index, ref value_tmp) > 0)
        {
            goto done;
        }

        IntWrapper[] places_2b = new IntWrapper[DefineConstants.MAX_NPARAMS];
        if (update_places_user_service_information_2b(ref user_service_information[0], ref places_2b) < 0)
        {
            goto error;
        }

        if (get_param(user_service_information_spec_2b, places_2b, ref index, ref value_tmp) > 0)
        {
            goto done;
        }

        IntWrapper[] places_21 = new IntWrapper[DefineConstants.MAX_NPARAMS];
        if (update_places_user_service_information_21(user_service_information[0], ref places_21) < 0)
        {
            goto error;
        }

        if (get_param(user_service_information_spec_21, places_21, ref index, ref value_tmp) > 0)
        {
            goto done;
        }

    error:
        return -1;

    done:
        value[0] = value_tmp.ToString();
        return 1;
    }


    // --------------------------------------------------------------------------------

    public static int create_message_type(out _message_type message_type)
    {
        message_type = new _message_type();
        //for (int i = 0; i < message_type.Length; i++)
        //{
        //    message_type[i] = new _message_type();
        //}
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int destroy_message_type(ref _message_type message_type)
    {
        // Código para liberar la memoria asignada a message_type
        message_type = null;
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int encode_message_type(ref byte[] buffer, ref int size, _message_type message_type)
    {
        IntWrapper[] message_type_places = new IntWrapper[1];
        message_type_places[0] = message_type.p_message_type;
        encode_sparam(ref buffer, 0, message_type_spec, message_type_places);
        size += 1;
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int decode_message_type(_message_type message_type, ref byte[] buffer, int offset)
    {
        IntWrapper[] message_type_places = new IntWrapper[1];
        message_type_places[0] = message_type.p_message_type;
        int result = decode_sparam(ref buffer, offset, message_type_spec, ref message_type_places);
        return result;
    }

    // --------------------------------------------------------------------------------

    public static int set_message_type(_message_type message_type, isupss7_message_type type)
    {
        message_type.p_message_type = new IntWrapper((byte)type);
        return 1;
    }

    public static _spec[] message_type_spec =
    {
    new _spec()
    {
        name = "message_type",
        nbit = 8,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {1, "Initial address - IAM"},
            {2, "Subsequent address - SAM"},
            {3, "Information request (national use) - INR"},
            {4, "__todo__"},
            {5, "Continuity - COT"},
            {6, "Address complete - ACM"},
            {7, "Connect - CON"},
            {8, "__todo__"},
            {9, "Answer - ANM"},
            {10, "__todo__"},
            {11, "__todo__"},
            {12, "Release - REL"},
            {13, "Suspend - SUS"},
            {14, "Resume - RES"},
            {15, "__todo__"},
            {16, "Release complete - RLC"},
            {17, "Continuity Check Request - CCR"},
            {18, "Reset Circuit - RSC"},
            {19, "Blocking - BLK"},
            {20, "Unblocking - UBL"},
            {21, "Blocking acknowledgement - BLA"},
            {22, "Unblocking acknowledgement - UBA"},
            {23, "Circuit group reset - GRS"},
            {24, "Circuit group blocking - CGB"},
            {25, "Circuit group unblocking - CGU"},
            {26, "Circuit group blocking acknowledgement - CGA"},
            {27, "Circuit group unblocking acknowledgement - CUA"},
            {28, "__todo__"},
            {29, "__todo__"},
            {30, "__todo__"},
            {31, "__todo__"},
            {32, "__todo__"},
            {33, "__todo__"},
            {34, "__todo__"},
            {35, "__todo__"},
            {36, "__todo__"},
            {37, "__todo__"},
            {38, "__todo__"},
            {39, "__todo__"},
            {40, "__todo__"},
            {41, "Circuit group reset acknowledgement - GRA"},
            {42, "__todo__"},
            {43, "__todo__"},
            {44, "Call progress - CPR"},
            {45, "__todo__"},
            {46, "Unequipped CIC - UEC"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};





    // --------------------------------------------------------------------------------

    public static int create_acm(out _acm acm)
    {
        acm = new _acm();

        // Asumiendo que el siguiente método inicializa los campos dentro de la estructura acm a INIT_INT_TO_CHAR
        // Si este no es el comportamiento deseado, este bloque debe ser ajustado
        // para reflejar la inicialización correcta.
        acm.s_backward_call_indicators = new _backward_call_indicators(); // Asumiendo que esto es una estructura o clase.

        // Crear las estructuras.
        if (create_backward_call_indicators(ref acm.s_backward_call_indicators) < 0)
        {
            goto error_backward_call_indicators;
        }

        // Opcionales
        object[] places_itu = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
        acm_update_places_optionals_itu(acm, places_itu);
        if (update_null_optional(places_itu, acm_names_itu) < 0)
        {
            goto last_error;
        }
        object[] places_ansi = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
        acm_update_places_optionals_ansi(acm, places_ansi);
        if (update_null_optional(places_ansi, acm_names_ansi) < 0)
        {
            goto last_error;
        }
        // Éxito
        return 1;

    // Tenemos un error al crear cualquiera de las estructuras
    last_error:
        destroy_backward_call_indicators(ref acm.s_backward_call_indicators);
    error_backward_call_indicators:
        return -1; // Aquí ya se creó la estructura acm, no se puede liberar como en C. El recolector de basura se encargará de ello.
    }



    // --------------------------------------------------------------------------------

    public static int destroy_acm(ref _acm acm)
    {
        int ret = 1;
        if (destroy_backward_call_indicators(ref acm.s_backward_call_indicators) < 0)
        {
            ret = -1;
        }
        // Optionals
        int ret_opt_itu = 1;
        int ret_opt_ansi = 1;
        object[][] places_itu = new object[DefineConstants.MAX_OPTIONAL_PARAMS][];
        acm_update_places_optionals_itu(acm, places_itu);
        object[][] places_ansi = new object[DefineConstants.MAX_OPTIONAL_PARAMS][];
        acm_update_places_optionals_ansi(acm, places_ansi);
        // Destroy all possible optional parameters.
        ret_opt_itu = destroy_optional(places_itu, acm_names_itu);
        ret_opt_ansi = destroy_optional(places_ansi, acm_names_ansi);
        // Freeing the global structure.
        //acm = null;
        return (ret == 1) ? (ret_opt_itu * ret_opt_ansi) : ret;
    }

    // --------------------------------------------------------------------------------

    public static int encode_acm(ref byte[] buffer, ref int size, _acm acm, isupss7_variant variant)
    {
        
        encode_backward_call_indicators(ref buffer, 1, acm.s_backward_call_indicators);
        size += 2;

        // Optional
        object[] placesItu = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
        acm_update_places_optionals_itu(acm, placesItu);

        object[] placesAnsi = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
        acm_update_places_optionals_ansi(acm, placesAnsi);

        if ((variant == isupss7_variant.ITU) && exists_optional(placesItu, acm_names_itu))
        {
            // At least an optional parameter found.
            buffer[3] = 1;
            encode_optional(ref buffer, 4, ref size, placesItu, acm_names_itu);
        }
        else if ((variant == isupss7_variant.ANSI) && exists_optional(placesAnsi, acm_names_ansi))
        {
            buffer[3] = 1;
            encode_optional(ref buffer, 4, ref size, placesAnsi, acm_names_ansi);
        }
        else
        {
            // No optional part found
            buffer[3] = 0;
            size += 1;
        }

        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int decode_acm(ref _acm acm, isupss7_variant variant, byte[] buffer)
    {
        decode_backward_call_indicators(acm.s_backward_call_indicators, ref buffer, 1);
        
        // Optional
        if (buffer[3] == 0)
        {
            Console.WriteLine("No optional part");
        }
        else
        {
            int optionalOffset = 3 + buffer[3];
            
            dynamic[] places_itu = new dynamic[DefineConstants.MAX_OPTIONAL_PARAMS];
            acm_update_places_optionals_itu(acm, places_itu);

            dynamic[] places_ansi = new dynamic[DefineConstants.MAX_OPTIONAL_PARAMS];
            acm_update_places_optionals_ansi(acm, places_ansi);

            switch (variant)
            {
                case isupss7_variant.ITU:
                    decode_optional(buffer, optionalOffset, ref places_itu, acm_names_itu);
                    break;
                case isupss7_variant.ANSI:
                    decode_optional(buffer, optionalOffset, ref places_ansi, acm_names_ansi);
                    break;
            }
        }
        return 1;
    }

    // --------------------------------------------------------------------------------

    


    public static int set_default_acm(_acm acm)
    {
        set_default_backward_call_indicators(acm.s_backward_call_indicators);
        // No opcionales por defecto.
        return 1;
    }

    public static int acm_update_places_optionals_itu(_acm acm, object[] places)
	{
	  //places[0] = (void **)&((*acm)->s_calling_party_number);
	  return 1;
	}
   
    public static int acm_update_places_optionals_ansi(_acm acm, object[] places)
    {
        //places[0] = (void **)&((*acm)->s_calling_party_number);
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int create_iam(out _iam iam)
    {
        iam = new _iam();

        // Inicializar la estructura
        // Puede que necesites adaptar este código para ajustarlo a los campos y tipos específicos en tu estructura
        foreach (var field in iam.GetType().GetFields())
        {
            if (field.FieldType == typeof(int))
            {
                field.SetValue(iam, DefineConstants.INIT_INT_TO_CHAR);
            }
        }

        // Crear las estructuras
        if (create_nature_of_connection_indicators(ref iam.s_nature_of_connection_indicators) < 0) { goto error_nature_of_connection_indicators; }
        if (create_forward_call_indicators(ref iam.s_forward_call_indicators) < 0) { goto error_forward_call_indicators; }
        if (create_calling_party_category(ref iam.s_calling_party_category) < 0) { goto error_calling_party_category; }
        if (create_transmission_medium_requirement(ref iam.s_transmission_medium_requirement) < 0) { goto error_transmission_medium_requirement; }
        if (create_user_service_information(ref iam.s_user_service_information) < 0) { goto error_user_service_information; }
        if (create_called_party_number(ref iam.s_called_party_number) < 0) { goto error_called_party_number; }

        // Optionals.
        object[] places_itu = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
        iam_update_places_optionals_itu(iam, places_itu);
        if (update_null_optional(places_itu, iam_names_itu) < 0) { goto error_called_party_number; }
        
        object[] places_ansi = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
        iam_update_places_optionals_ansi(iam, places_ansi);
        if (update_null_optional(places_ansi, iam_names_ansi) < 0) { goto error_called_party_number; }

        return 1;

        // We have an error while creating any of the structures
        error_called_party_number: destroy_user_service_information(ref iam.s_user_service_information);
        error_user_service_information: destroy_transmission_medium_requirement(ref iam.s_transmission_medium_requirement);
        error_transmission_medium_requirement: destroy_calling_party_category(ref iam.s_calling_party_category);
        error_calling_party_category: destroy_forward_call_indicators(ref iam.s_forward_call_indicators);
        error_forward_call_indicators: destroy_nature_of_connection_indicators(ref iam.s_nature_of_connection_indicators);
        error_nature_of_connection_indicators:  // Liberar la memoria
        error: return -1;
    }

    // --------------------------------------------------------------------------------

    public static int destroy_iam(ref _iam iam)
    {
        int ret = 1;
        if (destroy_nature_of_connection_indicators(ref iam.s_nature_of_connection_indicators) < 0)
        {
            ret = -1;
        }
        if (destroy_forward_call_indicators(ref iam.s_forward_call_indicators) < 0)
        {
            ret = -1;
        }
        if (destroy_calling_party_category(ref iam.s_calling_party_category) < 0)
        {
            ret = -1;
        }
        if (destroy_transmission_medium_requirement(ref iam.s_transmission_medium_requirement) < 0)
        {
            ret = -1;
        } //ITU
        if (destroy_user_service_information(ref iam.s_user_service_information) < 0)
        {
            ret = -1;
        } //ANSI
        if (destroy_called_party_number(iam.s_called_party_number) < 0)
        {
            ret = -1;
        }
        // Optionals
        int ret_opt_itu = 1;
        int ret_opt_ansi = 1;
        object[][] places_itu = new object[DefineConstants.MAX_OPTIONAL_PARAMS][];
        iam_update_places_optionals_itu(iam, places_itu);
        object[][] places_ansi = new object[DefineConstants.MAX_OPTIONAL_PARAMS][];
        iam_update_places_optionals_ansi(iam, places_ansi);
        // Destroy all possible optional parameters.
        ret_opt_itu = destroy_optional(places_itu, iam_names_itu);
        ret_opt_ansi = destroy_optional(places_ansi, iam_names_ansi);
        // Freeing the global structure.
        //iam = null;
        return (ret == 1) ? (ret_opt_itu * ret_opt_ansi) : ret;
    }

    // --------------------------------------------------------------------------------
   

    public static int encode_iam(ref byte[] buffer, ref int size, _iam iam, isupss7_variant variant)
    {
        int pointer_optional_part = 0;
        int offset_optional = 0;

        encode_nature_of_connection_indicators(ref buffer, 1, iam.s_nature_of_connection_indicators);
        encode_forward_call_indicators(ref buffer, 2, ref iam.s_forward_call_indicators);
        encode_calling_party_category(ref buffer, 4, ref iam.s_calling_party_category);

        int offset_called_party_number = 0;

        switch (variant)
        {
            case isupss7_variant.ITU:
                encode_transmission_medium_requirement(ref buffer, 5, iam.s_transmission_medium_requirement);
                buffer[6] = (byte)(1 + 1);
                pointer_optional_part = get_length_encode_called_party_number(iam.s_called_party_number) + 1;
                offset_called_party_number = 6 + buffer[6];
                offset_optional = 7 + pointer_optional_part;
                break;
            case isupss7_variant.ANSI:
                buffer[5] = (byte)(1 + 1 + 1);
                buffer[6] = (byte)(1 + get_length_encode_user_service_information(iam.s_user_service_information) + 1);
                
                pointer_optional_part =
                    get_length_encode_user_service_information(iam.s_user_service_information) +
                    get_length_encode_called_party_number(iam.s_called_party_number) + 1;

                encode_user_service_information(ref buffer, 8, iam.s_user_service_information);
                offset_called_party_number = 6 + buffer[6];
                offset_optional = 7 + pointer_optional_part;
                break;
        }
        
        encode_called_party_number(ref buffer, offset_called_party_number, ref iam.s_called_party_number);
        size += buffer.Length - (buffer.Length - offset_optional);

        // Optional
        object[] places_itu = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
        iam_update_places_optionals_itu(iam, places_itu);

        object[] places_ansi = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
        iam_update_places_optionals_ansi(iam, places_ansi);

        if ((variant == isupss7_variant.ITU) && (exists_optional(places_itu, iam_names_itu)))
        {
            buffer[7] = (byte)pointer_optional_part;
            encode_optional(ref buffer, offset_optional, ref size, places_itu, iam_names_itu);
        }
        else if ((variant == isupss7_variant.ANSI) && (exists_optional(places_ansi, iam_names_ansi)))
        {
            buffer[7] = (byte)pointer_optional_part;
            encode_optional(ref buffer, offset_optional, ref size, places_ansi, iam_names_ansi);
        }
        else
        {
            buffer[7] = 0;
            size -= 1;
        }
        return 1;
    }


    // --------------------------------------------------------------------------------

    public static int decode_iam(ref _iam iam, isupss7_variant variant, byte[] buffer)
    {
        decode_nature_of_connection_indicators(ref iam.s_nature_of_connection_indicators, ref buffer, 1);
        decode_forward_call_indicators(iam.s_forward_call_indicators, ref buffer, 2);
        decode_calling_party_category(ref iam.s_calling_party_category, ref buffer, 4);

        switch (variant)
        {
            case isupss7_variant.ITU:
                decode_transmission_medium_requirement(iam.s_transmission_medium_requirement, ref buffer, 5);
                break;
            case isupss7_variant.ANSI:
                decode_user_service_information(ref iam.s_user_service_information, ref buffer, 5 + buffer[5]);
                break;
        }

        decode_called_party_number(iam.s_called_party_number, ref buffer, 6 + buffer[6]);

        // Optional
        if (buffer[7] == 0)
        {
            Console.WriteLine("No optional part");
        }
        else
        {
            int optionalOffset = 7 + buffer[7];

            dynamic[] places_itu = new dynamic[DefineConstants.MAX_OPTIONAL_PARAMS];
            iam_update_places_optionals_itu(iam, places_itu);
            
            dynamic[] places_ansi = new dynamic[DefineConstants.MAX_OPTIONAL_PARAMS];
            iam_update_places_optionals_ansi(iam, places_ansi);

            switch (variant)
            {
                case isupss7_variant.ITU:
                    decode_optional(buffer, optionalOffset, ref places_itu, iam_names_itu);
                    iam_update_places_optionals_itu_Inverted(ref iam, places_itu);
                    break;
                case isupss7_variant.ANSI:
                    decode_optional(buffer, optionalOffset, ref places_ansi, iam_names_ansi);
                    iam_update_places_optionals_ansi_Inverted(ref iam, places_ansi);
                    break;
            }
        }

        return 1;
    }


    public static int value_nature_of_connection_indicators(_nature_of_connection_indicators nature_of_connection_indicators, ref string index, int op, string[] value, int pos)
    {
        int ret = -1;
        switch (op)
        {
            case 0:
                ret = set_nature_of_connection_indicators(nature_of_connection_indicators, ref index, value, pos);
                break;
            case 1:
                ret = get_nature_of_connection_indicators(nature_of_connection_indicators, index, value);
                break;
        }
        return ret;
    }

    // --------------------------------------------------------------------------------

    public static int value_iam(_iam iam, ref string second_index, ref string third_index, isupss7_variant variant, int op, string[] value, int pos)
    {
        int ret = 1;
        if (second_index == "nature_of_connection_indicators")
        {
            ret = value_nature_of_connection_indicators(iam.s_nature_of_connection_indicators, ref third_index, op, value, pos);
        }
        else if (second_index == "forward_call_indicators")
        {
            ret = value_forward_call_indicators(iam.s_forward_call_indicators, ref third_index, op, value, pos);
        }
        else if (second_index == "calling_party_category")
        {
            ret = value_calling_party_category(ref iam.s_calling_party_category, ref third_index, op, value, pos);
        }
        else if (second_index == "transmission_medium_requirement" && variant == isupss7_variant.ITU)
        {
            ret = value_transmission_medium_requirement(iam.s_transmission_medium_requirement, ref third_index, op, value, pos);
        }
        else if (second_index == "user_service_information" && variant == isupss7_variant.ANSI)
        {
            // ret = value_user_service_information(ref iam[0].s_user_service_information, third_index, op, value); TODO
        }
        else if (second_index == "called_party_number")
        {
            ret = value_called_party_number(iam.s_called_party_number, ref third_index, op, value, pos);
        }
        else
        {
            // Optional
            // Updating itu structures
            string[] strings_itu = new string[DefineConstants.MAX_OPTIONAL_PARAMS];
            object[] places_itu = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
            
            // Updating ansi structures
            string[] strings_ansi = new string[DefineConstants.MAX_OPTIONAL_PARAMS];
            object[] places_ansi = new object[DefineConstants.MAX_OPTIONAL_PARAMS];
            
            switch (variant)
            {
                case isupss7_variant.ITU:
                    create_strings_optional(iam_names_itu, strings_itu);
                    iam_update_places_optionals_itu(iam, places_itu);
                    
                    ret = value_optional(places_itu, iam_names_itu, strings_itu, ref second_index, ref third_index, op, value);
                    destroy_strings_optional(iam_names_itu, strings_itu);
                    break;
                case isupss7_variant.ANSI:
                    create_strings_optional(iam_names_ansi, strings_ansi);
                    iam_update_places_optionals_ansi(iam, places_ansi);
                    
                    ret = value_optional(places_ansi, iam_names_ansi, strings_ansi, ref second_index, ref third_index, op, value);
                    destroy_strings_optional(iam_names_ansi, strings_ansi);
                    break;
            }
        }
        return ret;
    }

    // --------------------------------------------------------------------------------

    public static int set_default_iam(_iam iam)
    {
        set_default_nature_of_connection_indicators(ref iam.s_nature_of_connection_indicators);
        set_default_forward_call_indicators(iam.s_forward_call_indicators);
        set_default_calling_party_category(ref iam.s_calling_party_category);
        set_default_transmission_medium_requirement(iam.s_transmission_medium_requirement); //ITU
        set_default_user_service_information(ref iam.s_user_service_information); //ANSI
        set_default_called_party_number(iam.s_called_party_number);
        // No optionals by default.
        return 1;
    }

    public static int set_default_calling_party_category(ref _calling_party_category calling_party_category)
    {
        byte[] tmp = new byte[] { 0x0e };
        return decode_calling_party_category(ref calling_party_category, ref tmp, 0);
    }


    public static int set_default_nature_of_connection_indicators(ref _nature_of_connection_indicators nature_of_connection_indicators)
    {
        byte[] tmp = new byte[1] { 0x05 };
        return decode_nature_of_connection_indicators(ref nature_of_connection_indicators, ref tmp, 0);
    }


    // --------------------------------------------------------------------------------
    public static int iam_update_places_optionals_itu(_iam iam, object[] places)
    {
        places[0] = iam.s_calling_party_number;
        places[1] = iam.s_original_called_number;
        places[2] = iam.s_generic_digits;
        places[3] = iam.s_propagation_delay_counter;
        return 1;
    }

    public static int iam_update_places_optionals_itu_Inverted(ref _iam iam, object[] places)
    {
        iam.s_calling_party_number = (_calling_party_number) places[0];
        iam.s_original_called_number = (_original_called_number) places[1];
        iam.s_generic_digits = (_generic_digits) places[2];
        iam.s_propagation_delay_counter = (_propagation_delay_counter) places[3];
        return 1;
    }

    public static int iam_update_places_optionals_ansi(_iam iam, object[] places)
    {
        places[0] = iam.s_calling_party_number;
        places[1] = iam.s_charge_number;
        places[2] = iam.s_generic_digits;
        places[3] = iam.s_generic_name;
        places[4] = iam.s_jurisdiction_information;
        places[5] = iam.s_original_called_number;
        places[6] = iam.s_originating_line_information;
        return 1;
    }

    public static int iam_update_places_optionals_ansi_Inverted(ref _iam iam, object[] places)
    {
        iam.s_calling_party_number = (_calling_party_number) places[0];
        iam.s_charge_number = (_charge_number) places[1];
        iam.s_generic_digits = (_generic_digits) places[2];
        iam.s_generic_name = (_generic_name) places[3];
        iam.s_jurisdiction_information = (_jurisdiction_information) places[4];
        iam.s_original_called_number = (_original_called_number) places[5];
        iam.s_originating_line_information = (_originating_line_information) places[6];
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int create_sam(out _sam sam)
    {
        sam = new _sam();
        //Array.Fill(sam, new _sam());
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int destroy_sam(_sam sam)
    {
        // No se realiza ninguna operación en este método
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int dump_sam(char[] dumped, _sam sam, isupss7_variant variant)
    {
        // No se realiza ninguna operación en este método
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int encode_sam(ref byte[] buffer, ref int size, ref _sam sam, isupss7_variant variant)
    {
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int set_default_sam(_sam sam)
	{
	  return 1;
	}



    // --------------------------------------------------------------------------------

    public static int create_calling_party_number(ref _calling_party_number calling_party_number)
    {
        calling_party_number = new _calling_party_number();
        if (calling_party_number == null)
        {
            goto error;
        }
        calling_party_number.address_signals = null;
        return 1;

    error:
        calling_party_number = null;
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int destroy_calling_party_number(ref _calling_party_number calling_party_number)
    {
        // Liberar la memoria de calling_party_number.address_signals
        // Nota: No se necesita usar 'free' en C# ya que la recolección de basura (garbage collection) se encarga de liberar la memoria automáticamente.
        calling_party_number.address_signals = null;

        // Liberar la memoria de calling_party_number
        // Nota: No se necesita usar 'free' en C# para liberar memoria de estructuras.
        calling_party_number = null;

        return 1;
    }


    public static int get_length_encode_calling_party_number(ref _calling_party_number calling_party_number)
    {
        int digits = calling_party_number.address_signals == null ? 0 : calling_party_number.address_signals.Length;
        int digits_nbytes = (digits % 2 == 0) ? (digits / 2) : (digits / 2) + 1;
        int nbytes = 1 + 1 + 2 + digits_nbytes;
        return nbytes;
    }



    // --------------------------------------------------------------------------------

    public static int encode_called_party_number(ref byte[] buffer, int offset, ref _called_party_number called_party_number)
    {
        // The entire parameter length minus the actual size byte (buffer[0]).
        buffer[offset] = (byte)(get_length_encode_called_party_number(called_party_number) - 1);
        
        int digits = called_party_number.address_signals == null ? 0 : called_party_number.address_signals.Length;
        called_party_number.p_odd_even_indicator.Value = (digits % 2) == 0 ? 0 : 1;

        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_called_party_number(called_party_number, places) < 0)
        {
            goto error;
        }

        // This is 2 Bytes length.
        if (encode_sparam(ref buffer, 1 + offset, called_party_number_spec, places) < 0) { goto error; }
        if (encode_number(ref buffer, 3 + offset, called_party_number.address_signals) < 0) { goto error; }

        return 1;

        error:
            return -1;
    }


    // --------------------------------------------------------------------------------

    public static int decode_calling_party_number(ref _calling_party_number calling_party_number, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        try
        {
            if (update_places_calling_party_number(ref calling_party_number, ref places) < 0)
                throw new Exception("Error in update_places_calling_party_number");

            if (decode_sparam(ref buffer, offset + 1, calling_party_number_spec, ref places) < 0)
                throw new Exception("Error in decode_sparam");

            int ndigits = ((buffer[offset] - 2) * 2) - calling_party_number.p_odd_even_indicator.Value;

            if (decode_number(buffer, offset + 3, ref calling_party_number.address_signals, ndigits) < 0)
                throw new Exception("Error in decode_number");

            return 1;
        }
        catch
        {
            destroy_calling_party_number(ref calling_party_number);
            return -1;
        }
    }


    // --------------------------------------------------------------------------------

    public static int value_calling_party_number(_calling_party_number calling_party_number, ref string index, int op, string[] value, int pos)
	{
	  int ret = -1;
	  switch (op)
	  {
	  case 0:
		  ret = set_calling_party_number(calling_party_number, index, value, pos);
		  break;
	  case 1:
		  ret = get_calling_party_number(calling_party_number, index, value);
		  break;
	  };
	  return ret;
	}

    // --------------------------------------------------------------------------------

    public static int set_default_calling_party_number(ref _calling_party_number calling_party_number)
    {
        byte[] tmp = { 0x06, 0x81, 0x10, 0x22, 0x11, 0x33, 0x04 };
        return decode_calling_party_number(ref calling_party_number, ref tmp, 0);
    }

    // --------------------------------------------------------------------------------
    public static _spec[] calling_party_number_spec =
{
    new _spec()
    {
        name = "nature_of_address_indicator",
        nbit = 7,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>
        {
            {0, "spare"},
            {1, "subscriber number (national use)"},
            {2, "unknown (national use)"},
            {3, "national (significant) number"},
            {4, "international number"},
            {5, "spare"},
            {6, "spare"},
            {7, "spare"},
            {8, "spare"},
            {9, "spare"},
            {10, "spare"},
            {11, "spare"},
            {12, "spare"},
            {13, "spare"},
            {14, "spare"},
            {15, "spare"},
            {16, "spare"},
            {17, "spare"},
            {18, "spare"},
            {19, "spare"},
            {20, "spare"},
            {21, "spare"},
            {22, "spare"},
            {23, "spare"},
            {24, "spare"},
            {25, "spare"},
            {26, "spare"},
            {27, "spare"},
            {28, "spare"},
            {29, "spare"},
            {30, "spare"},
            {31, "spare"},
            {32, "spare"},
            {33, "spare"},
            {34, "spare"},
            {35, "spare"},
            {36, "spare"},
            {37, "spare"},
            {38, "spare"},
            {39, "spare"},
            {40, "spare"},
            {41, "spare"},
            {42, "spare"},
            {43, "spare"},
            {44, "spare"},
            {45, "spare"},
            {46, "spare"},
            {47, "spare"},
            {48, "spare"},
            {49, "spare"},
            {50, "spare"},
            {51, "spare"},
            {52, "spare"},
            {53, "spare"},
            {54, "spare"},
            {55, "spare"},
            {56, "spare"},
            {57, "spare"},
            {58, "spare"},
            {59, "spare"},
            {60, "spare"},
            {61, "spare"},
            {62, "spare"},
            {63, "spare"},
            {64, "spare"},
            {65, "spare"},
            {66, "spare"},
            {67, "spare"},
            {68, "spare"},
            {69, "spare"},
            {70, "spare"},
            {71, "spare"},
            {72, "spare"},
            {73, "spare"},
            {74, "spare"},
            {75, "spare"},
            {76, "spare"},
            {77, "spare"},
            {78, "spare"},
            {79, "spare"},
            {80, "spare"},
            {81, "spare"},
            {82, "spare"},
            {83, "spare"},
            {84, "spare"},
            {85, "spare"},
            {86, "spare"},
            {87, "spare"},
            {88, "spare"},
            {89, "spare"},
            {90, "spare"},
            {91, "spare"},
            {92, "spare"},
            {93, "spare"},
            {94, "spare"},
            {95, "spare"},
            {96, "spare"},
            {97, "spare"},
            {98, "spare"},
            {99, "spare"},
            {100, "spare"},
            {101, "spare"},
            {102, "spare"},
            {103, "spare"},
            {104, "spare"},
            {105, "spare"},
            {106, "spare"},
            {107, "spare"},
            {108, "spare"},
            {109, "spare"},
            {110, "spare"},
            {111, "spare"},
            {112, "spare"},
            {113, "non_unique_subscriber_number"},
            {114, "reserved_for_national_use"},
            {115, "non_unique_international_number"},
            {116, "spare"},
            {117, "spare"},
            {118, "spare"},
            {119, "spare"},
            {120, "spare"},
            {121, "spare"},
            {122, "spare"},
            {123, "spare"},
            {124, "spare"},
            {125, "spare"},
            {126, "spare"},
            {127, "spare"},
        }
    },
    new _spec()
    {
        name = "odd_even_indicator",
        nbit = 1,
        sbit = 7,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>
        {
            {0, "even number of address signals"},
            {1, "odd number of address signals"},
        }
    },
    new _spec()
    {
        name = "screening_indicator",
        nbit = 2,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>
        {
            {0, "user provided, not verified (national use)"},
            {1, "user provided, verified and passed"},
            {2, "user provided, verified and failed (national use)"},
            {3, "network provided"},
        }
    },
    new _spec()
    {
        name = "address_presentation_restricted_indicator",
        nbit = 2,
        sbit = 2,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>
        {
            {0, "presentation allowed"},
            {1, "presentation restricted"},
            {2, "address not available (Note 1) (national use)"},
            {3, "reserved for restriction by the network"},
        }
    },
    new _spec()
    {
        name = "numbering_plan_indicator",
        nbit = 3,
        sbit = 4,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>
        {
            {0, "spare"},
            {1, "ISDN (Telephony) numbering plan (ITU-T Recommendation E.164)"},
            {2, "spare"},
            {3, "Data numbering plan (ITU-T Recommendation X.121) (national use)"},
            {4, "Telex numbering plan (ITU-T Recommendation F.69) (national use)"},
            {5, "reserved for national use"},
            {6, "reserved for national use"},
            {7, "spare"},
        }
    },
    new _spec()
    {
        name = "number_incomplete_indicator (NI)",
        nbit = 1,
        sbit = 7,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>
        {
            {0, "complete"},
            {1, "incomplete"},
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};

    // --------------------------------------------------------------------------------
    public static int update_places_calling_party_number(ref _calling_party_number calling_party_number, ref IntWrapper[] places)
    {
        places[0] = calling_party_number.p_nature_of_address_indicator;
        places[1] = calling_party_number.p_odd_even_indicator;
        places[2] = calling_party_number.p_screening_indicator;
        places[3] = calling_party_number.p_address_presentation_restricted_indicator;
        places[4] = calling_party_number.p_numbering_plan_indicator;
        places[5] = calling_party_number.p_number_incomplete_indicator;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int set_calling_party_number(_calling_party_number calling_party_number, string index, string[] value, int pos)
    {
        if (index == "odd_even_indicator")
        {
            return -1;
        }
        if (index == "address_signals")
        {
            int numericValue;
            if (!IsNumeric(value[pos]))
            {
                return -1;
            }

            calling_party_number.address_signals = value;
            if (calling_party_number.address_signals == null)
            {
                return -1;
            }

            // Actualizar el parámetro odd even
            int size = value.Length;
            calling_party_number.p_odd_even_indicator.Value = (size % 2 == 0) ? 0 : 1;
        }
        else
        {
            IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
            int valueTmp = -1;
            if (!IsNumeric(value[pos]))
            {
                return -1;
            }

            if (update_places_calling_party_number(ref calling_party_number, ref places) < 0)
            {
                return -1;
            }
            if (set_param(calling_party_number_spec, places, ref index, valueTmp) < 0)
            {
                return -1;
            }
        }
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int get_calling_party_number(_calling_party_number calling_party_number, string index, string[] value)
    {
        value = null;
        if (index == "odd_even_indicator")
        {
            return -1;
        }
        if (index == "address_signals")
        {
            // Suponiendo que AddressSignals es una propiedad de tipo string en la clase/struct CallingPartyNumber
            value = calling_party_number.address_signals;
            if (value == null)
            {
                return -1;
            }
        }
        else
        {
            IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
            int valueTmp = -1;
            if (update_places_calling_party_number(ref calling_party_number, ref places) < 0)
            {
                return -1;
            }
            if (get_param(calling_party_number_spec, places, ref index, ref valueTmp) < 0)
            {
                return -1;
            }

            value = new string[DefineConstants.MAX_DIGITS_GET + 1];
            for (int i = 0; i < DefineConstants.MAX_DIGITS_GET + 1; i++)
            {
                value[i] = Convert.ToString(DefineConstants.INIT_INT_TO_CHAR);
            }

            string valueString = valueTmp.ToString();
            if (valueString.Length > DefineConstants.MAX_DIGITS_GET)
            {
                return -1;
            }

            value = new string[1] { valueString };
        }
        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int create_charge_number(ref _charge_number charge_number)
    {
        charge_number = new _charge_number();
        if (charge_number == null)
        {
            goto error;
        }
        //for (int i = 0; i < charge_number.Length; i++)
        //{
        //    charge_number[i] = new _charge_number();
        //    // Establece los valores predeterminados de cada propiedad del objeto _charge_number si es necesario.
        //    charge_number[i].address_signals = null;
        //}
        return 1;
    error:
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int destroy_charge_number(_charge_number charge_number)
    {
        // No es necesario liberar la memoria en C#, ya que el recolector de basura se encarga de ello
        // Elimina las siguientes líneas:
        // free(charge_number.address_signals);
        // freecharge_number;

        charge_number = null;
        return 1;
    }


    public static int get_length_encode_charge_number(_charge_number charge_number)
    {
        int digits = charge_number.address_signals == null ? 0 : charge_number.address_signals.Length;
        int digits_nbytes = (digits % 2 == 0) ? digits / 2 : (digits / 2) + 1;
        int nbytes = 1 + 1 + 2 + digits_nbytes;
        return nbytes;
    }

    // --------------------------------------------------------------------------------

    public static int encode_charge_number(ref byte[] buffer, int offset, _charge_number charge_number)
    {
        // The entire parameter length minus the name of the parameter and the size byte.
        buffer[offset] = (byte)(get_length_encode_charge_number(charge_number) - 1 - 1);

        int digits = charge_number.address_signals == null ? 0 : charge_number.address_signals.Length;
        charge_number.p_odd_even_indicator.Value = (digits % 2 == 0) ? 0 : 1;

        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_charge_number(charge_number, places) < 0)
        {
            goto error;
        }

        // This is 2 Bytes length.
        if (encode_sparam(ref buffer, 1 + offset, charge_number_spec, places) < 0)
        {
            goto error;
        }

        if (encode_number(ref buffer, 3 + offset, charge_number.address_signals) < 0)
        {
            goto error;
        }
        return 1;
    error:
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int decode_charge_number(_charge_number charge_number, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        try
        {
            if (update_places_charge_number(charge_number, places) < 0)
                throw new Exception("Error in update_places_charge_number");

            if (decode_sparam(ref buffer, offset + 1, charge_number_spec, ref places) < 0)
                throw new Exception("Error in decode_sparam");

            int ndigits = ((buffer[offset] - 2) * 2) - charge_number.p_odd_even_indicator.Value;

            if (decode_number(buffer, offset + 3, ref charge_number.address_signals, ndigits) < 0)
                throw new Exception("Error in decode_number");

            return 1;
        }
        catch
        {
            destroy_charge_number(charge_number);
            return -1;
        }
    }

    // --------------------------------------------------------------------------------

    public static int value_charge_number(_charge_number charge_number, ref string index, int op, string[] value, int pos)
    {
        int ret = -1;
        switch (op)
        {
            case 0:
                ret = set_charge_number(charge_number, index, value, pos);
                break;
            case 1:
                ret = get_charge_number(ref charge_number, index, value);
                break;
        }
        return ret;
    }

    // --------------------------------------------------------------------------------

    public static int set_default_charge_number(_charge_number charge_number)
    {
        byte[] tmp = { 0x04, 0x03, 0x30, 0x53, 0x97 };
        return decode_charge_number(charge_number, ref tmp, 0);
    }

    // --------------------------------------------------------------------------------
    public static _spec[] charge_number_spec =
{
    new _spec()
    {
        name = "nature_of_address_indicator",
        nbit = 7,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "spare"},
            {1, "ANI of the calling party; subscriber number"},
            {2, "ANI not available or not provided"},
            {3, "ANI of the calling party; national number"},
            {4, "spare"},
            {5, "ANI of the called party; subscriber number"},
            {6, "ANI of the called party; no number present"},
            {7, "ANI of the called party; national number"},
            {8, "spare"},
            {9, "spare" },
            {10, "spare" },
            {11, "spare" },
            {12, "spare" },
            {13, "spare" },
            {14, "spare" },
            {15, "spare" },
            {16, "spare" },
            {17, "spare" },
            {18, "spare" },
            {19, "spare" },
            {20, "spare" },
            {21, "spare" },
            {22, "spare" },
            {23, "spare" },
            {24, "spare" },
            {25, "spare" },
            {26, "spare" },
            {27, "spare" },
            {28, "spare" },
            {29, "spare" },
            {30, "spare" },
            {31, "spare" },
            {32, "spare" },
            {33, "spare" },
            {34, "spare" },
            {35, "spare" },
            {36, "spare" },
            {37, "spare" },
            {38, "spare" },
            {39, "spare" },
            {40, "spare" },
            {41, "spare" },
            {42, "spare" },
            {43, "spare" },
            {44, "spare" },
            {45, "spare" },
            {46, "spare" },
            {47, "spare" },
            {48, "spare" },
            {49, "spare" },
            {50, "spare" },
            {51, "spare" },
            {52, "spare" },
            {53, "spare" },
            {54, "spare" },
            {55, "spare" },
            {56, "spare" },
            {57, "spare" },
            {58, "spare" },
            {59, "spare" },
            {60, "spare" },
            {61, "spare" },
            {62, "spare" },
            {63, "spare" },
            {64, "spare" },
            {65, "spare" },
            {66, "spare" },
            {67, "spare" },
            {68, "spare" },
            {69, "spare" },
            {70, "spare" },
            {71, "spare" },
            {72, "spare" },
            {73, "spare" },
            {74, "spare" },
            {75, "spare" },
            {76, "spare" },
            {77, "spare" },
            {78, "spare" },
            {79, "spare" },
            {80, "spare" },
            {81, "spare" },
            {82, "spare" },
            {83, "spare" },
            {84, "spare" },
            {85, "spare" },
            {86, "spare" },
            {87, "spare" },
            {88, "spare" },
            {89, "spare" },
            {90, "spare" },
            {91, "spare" },
            {92, "spare" },
            {93, "spare" },
            {94, "spare" },
            {95, "spare" },
            {96, "spare" },
            {97, "spare" },
            {98, "spare" },
            {99, "spare" },
            {100, "spare" },
            {101, "spare" },
            {102, "spare" },
            {103, "spare" },
            {104, "spare" },
            {105, "spare" },
            {106, "spare" },
            {107, "spare" },
            {108, "spare" },
            {109, "spare" },
            {110, "spare" },
            {111, "spare" },
            {112, "spare" },
            {113, "spare" },
            {114, "spare" },
            {115, "spare" },
            {116, "spare" },
            {117, "spare" },
            {118, "spare" },
            {119, "spare" },
            {120, "spare" },
            {121, "spare" },
            {122, "spare" },
            {123, "spare" },
            {124, "spare" },
            {125, "reserved for network specific use"},
            {126, "reserved for network specific use"},
            {127, "reserved for network specific use"},
            { DefineConstants.NONSET, null}
        }
    }
    ,
        new _spec()
        {
            name = "odd_even_indicator",
            nbit = 1,
            sbit = 7,
            state = table_mode.TABLE_SET,
            map = new Dictionary<int, string>()
            {
                { 0, "even number of address signals"},
                 { 1, "odd number of address signals"},
                 { DefineConstants.NONSET, null}
            }
        }
        ,
         new _spec()
        {
            name = "numbering_plan_indicator",
            nbit = 3,
            sbit = 4,
            state = table_mode.TABLE_SET,
            map = new Dictionary<int, string>()
            {
                { 0, "unknown (no interpretation)"},
                 { 1, "ISDN (Telephony) numbering plan (Recommendation E.164)"},
                 { 2, "spare (no interpretation)"},
                 { 3, "reserved (ITU-T: Data numbering plan)"},
                 { 4, "reserved (ITU-T: Telex numbering plan)"},
                 { 5, "Private numbering plan"},
                 { 6, "spare (no interpretation)"},
                 { 7, "spare (no interpretation)"},
                 { DefineConstants.NONSET, null}
            }
        }
         ,
         new _spec()
         {
             name = null,
             nbit = DefineConstants.NONSET,
             sbit = DefineConstants.NONSET,
             state = table_mode.TABLE_NONSET,
             map = null
         }
};



    // --------------------------------------------------------------------------------
    //C++ TO C# CONVERTER TASK: C# does not have an equivalent to pointers to value types:
    //ORIGINAL LINE: int update_places_charge_number(struct _charge_number** charge_number, int* places[])
    public static int update_places_charge_number(_charge_number charge_number, IntWrapper[] places)
    {
        places[0] = charge_number.p_nature_of_address_indicator;
        places[1] = charge_number.p_odd_even_indicator;
        places[2] = charge_number.p_numbering_plan_indicator;
        return 1;
    }


    // --------------------------------------------------------------------------------
    public static int set_charge_number(_charge_number charge_number, string index, string[] value, int pos)
    {
        try
        {
            if (index == "odd_even_indicator")
            {
                throw new Exception("Invalid index");
            }

            if (index == "address_signals")
            {
                if (!IsNumeric(value[0].ToString()))
                    throw new Exception("Value is not numeric");

                charge_number.address_signals = (string[])value.Clone();

                if (charge_number.address_signals == null)
                    throw new Exception("Failed to duplicate address signals");

                // Update the odd even param
                int size = value.Length;
                charge_number.p_odd_even_indicator.Value = (size % 2 == 0) ? 0 : 1;
            }
            else
            {
                IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
                int valueTmp = -1;

                if (IsNumeric(value[0].ToString()))
                    valueTmp = int.Parse(value[pos]);
                else
                    throw new Exception("Value is not numeric");

                if (update_places_charge_number(charge_number, places) < 0)
                    throw new Exception("update_places_charge_number failed");

                if (set_param(charge_number_spec, places, ref index, valueTmp) < 0)
                    throw new Exception("set_param failed");
            }

            return 1;
        }
        catch
        {
            return -1;
        }
    }




    // --------------------------------------------------------------------------------
    public static int get_charge_number(ref _charge_number charge_number, string index, string[] value)
    {
        try
        {
            if (index == "odd_even_indicator")
                throw new ArgumentException("Invalid index: odd_even_indicator");

            if (index == "address_signals")
            {
                value = charge_number.address_signals;
                if (value == null)
                    throw new Exception("Failed to duplicate address_signals");
            }
            else
            {
                IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
                int value_tmp = -1;

                if (update_places_charge_number(charge_number, places) < 0)
                    throw new Exception("update_places_charge_number failed");

                if (get_param(charge_number_spec, places, ref index, ref value_tmp) < 0)
                    throw new Exception("GetParam failed");

                value = new string[DefineConstants.MAX_DIGITS_GET + 1];  // Replace MAX_DIGITS_GET with the actual value
                for (int i = 0; i < value.Length; i++)
                    value[i] = DefineConstants.INIT_INT_TO_CHAR.ToString();  // Replace INIT_INT_TO_CHAR with the actual value

                string valueTmpString = value_tmp.ToString();
                Array.Copy(valueTmpString.ToCharArray(), value, valueTmpString.Length);
            }

            return 1;
        }
        catch
        {
            return -1;
        }
    }



    // --------------------------------------------------------------------------------

    public static int create_generic_digits(ref _generic_digits generic_digits)
    {
        generic_digits = new _generic_digits();
        if (generic_digits == null)
        {
            goto error;
        }
        generic_digits.digits = null;
        return 1;
    error:
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int destroy_generic_digits(_generic_digits generic_digits)
    {
        // No se necesita la función 'free' en C#, ya que el recolector de basura se encarga de la administración de memoria.
        // Simplemente establecemos los elementos a null para liberar la referencia.
        //generic_digits.digits = null;
        generic_digits = null;
        return 1;
    }


    public static int get_length_encode_generic_digits(_generic_digits generic_digits)
    {
        int digits = generic_digits.digits == null ? 0 : generic_digits.digits.Length;
        int digits_nbytes = (digits % 2 == 0) ? (digits / 2) : (digits / 2) + 1;
        int nbytes = 1 + 1 + 1 + digits_nbytes;
        Console.WriteLine("length generic digits: " + nbytes);
        return nbytes;
    }

    // --------------------------------------------------------------------------------

    public static int encode_generic_digits(ref byte[] buffer, int offset, _generic_digits generic_digits)
    {
        int length = get_length_encode_generic_digits(generic_digits);
        buffer[offset] = (byte)(length - 1 - 1);
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS); // Reemplazar 'null' por 'new int[DefineConstants.MAX_NPARAMS]'
        
        if (update_places_generic_digits(generic_digits, places) < 0)
        {
            goto error;
        }

        if (encode_sparam(ref buffer, 1 + offset, generic_digits_spec, places) < 0)
        {
            goto error;
        }
        if (encode_number(ref buffer, 2 + offset, generic_digits.digits) < 0)
        {
            goto error;
        }
        return 1;
    error:
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int decode_generic_digits(_generic_digits generic_digits, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        try
        {
            if (update_places_generic_digits(generic_digits, places) < 0)
                throw new Exception("Error in update_places_generic_digits");
             
            if (decode_sparam(ref buffer, offset + 1, generic_digits_spec, ref places) < 0)
                throw new Exception("Error in decode_sparam");

            int ndigits = ((buffer[offset] - 1) * 2) - generic_digits.p_encoding_scheme.Value;

            if (decode_number(buffer, offset + 2, ref generic_digits.digits, ndigits) < 0)
                throw new Exception("Error in decode_number");

            return 1;
        }
        catch
        {
            return -1;
        }
    }


    // --------------------------------------------------------------------------------

    public static int value_generic_digits(_generic_digits generic_digits, ref string index, int op, string[] value)
    {
        int ret = -1;
        switch (op)
        {
            case 0:
                ret = set_generic_digits(generic_digits, ref index, value);
                break;
            case 1:
                ret = get_generic_digits(generic_digits, ref index, value);
                break;
        }
        return ret;
    }

    // --------------------------------------------------------------------------------

    public static int set_default_generic_digits(_generic_digits generic_digits)
    {
        byte[] tmp = { 0x04, 0x00, 0x64, 0x98, 0x30 };
        return decode_generic_digits(generic_digits, ref tmp, 0);
    }

    // --------------------------------------------------------------------------------
    public static _spec[] generic_digits_spec =
{
    new _spec()
    {
        name = "type_of_digit",
        nbit = 5,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "reserved for account code"},
            {1, "reserved for authorisation code"},
            {2, "reserved for private networking travelling class mark"},
            {3, "reserved for business communication group identity"},
            {4, "spare"},
            {5, "spare"},
            {6, "spare"},
            {7, "spare"},
            {8, "spare"},
            {9, "spare"},
            {10, "spare"},
            {11, "spare"},
            {12, "spare"},
            {13, "location identification number"},
            {14, "spare"},
            {15, "bill-to number"},
            {16, "spare"},
            {17, "spare"},
            {18, "spare"},
            {19, "spare"},
            {20, "spare"},
            {21, "spare"},
            {22, "spare"},
            {23, "spare"},
            {24, "spare"},
            {25, "spare"},
            {26, "spare"},
            {27, "spare"},
            {28, "spare"},
            {29, "spare"},
            {30, "spare"},
            {31, "reserved for extension"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "encoding_scheme",
        nbit = 3,
        sbit = 5,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "BCD even: (even number of digits)"},
            {1, "BCD odd: (odd number of digits)"},
            {2, "IA5 Character"},
            {3, "Binary coded"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};

    // --------------------------------------------------------------------------------
    public static int update_places_generic_digits(_generic_digits generic_digits, IntWrapper[] places)
    {
        places[0] = generic_digits.p_type_of_digit;
        places[1] = generic_digits.p_encoding_scheme;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int set_generic_digits(_generic_digits generic_digits, ref string index, string[] value)
    {
        try
        {
            if (index == "digits")
            {
                if (!IsNumeric(value[0].ToString()))
                    throw new Exception("Value is not numeric");

                generic_digits.digits = (string[])value.Clone();

                if (generic_digits.digits == null)
                    throw new Exception("Failed to duplicate digits");

                // Update the odd even param
                int size = value.Length;
                generic_digits.p_encoding_scheme.Value = (size % 2 == 0) ? 0 : 1;
            }
            else
            {
                IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
                int value_tmp = -1;

                if (IsNumeric(value[0].ToString()))
                    value_tmp = int.Parse(new string(value[0].ToString()));
                else
                    throw new Exception("Value is not numeric");

                if (update_places_generic_digits(generic_digits, places) < 0)
                    throw new Exception("update_places_generic_digits failed");

                if (set_param(generic_digits_spec, places, ref index, value_tmp) < 0)
                    throw new Exception("set_param failed");
            }

            return 1;
        }
        catch
        {
            return -1;
        }
    }

    // --------------------------------------------------------------------------------
    public static int get_generic_digits(_generic_digits generic_digits, ref string index, string[] value)
    {
        try
        {
            if (index == "digits")
            {
                value = generic_digits.digits;
                if (value == null)
                    throw new Exception("Failed to duplicate digits");
            }
            else
            {
                IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
                int value_tmp = -1;

                if (update_places_generic_digits(generic_digits, places) < 0)
                    throw new Exception("UpdatePlacesGenericDigits failed");

                if (get_param(generic_digits_spec, places, ref index, ref value_tmp) < 0)
                    throw new Exception("GetParam failed");

                value = new string[DefineConstants.MAX_DIGITS_GET + 1];  // Replace MAX_DIGITS_GET with the actual value
                for (int i = 0; i < value.Length; i++)
                    value[i] = DefineConstants.INIT_INT_TO_CHAR.ToString();  // Replace INIT_INT_TO_CHAR with the actual value

                string valueTmpString = value_tmp.ToString();
                Array.Copy(valueTmpString.ToCharArray(), value, valueTmpString.Length);
            }

            return 1;
        }
        catch
        {
            return -1;
        }
    }

    // --------------------------------------------------------------------------------

    public static int create_generic_name(ref _generic_name generic_name)
    {
        generic_name = new _generic_name();
        if (generic_name == null)
        {
            goto error;
        }
        //for (int i = 0; i < generic_name.Length; i++)
        //{
        //    generic_name[i] = new _generic_name();
        //    generic_name[i].characters = null;
        //}
        return 1;

    error:
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int destroy_generic_name(_generic_name generic_name)
    {
        // No se necesita llamar a "free" en C# para liberar la memoria
        // Asumiendo que "characters" es un array de caracteres, no se necesita liberar la memoria manualmente

        // Si la intención es liberar la memoria del array "generic_name" en sí, debes usar "new" para crearlo
        // y luego asignarle null para permitir que el recolector de basura lo limpie automáticamente

        generic_name = null;
        return 1;
    }


    // --------------------------------------------------------------------------------

    public static int get_length_encode_generic_name(_generic_name generic_name)
    {
        int chars = generic_name.characters == null ? 0 : generic_name.characters.Length;
        int chars_nbytes = chars;
        int nbytes = 1 + 1 + 1 + chars_nbytes;
        return nbytes;
    }

    // --------------------------------------------------------------------------------

    public static int encode_generic_name(ref byte[] buffer, int offset, _generic_name generic_name)
    {
        // The entire parameter length minus the name of the parameter and the size byte.
        buffer[offset] = (byte)(get_length_encode_generic_name(generic_name) - 1 - 1);
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_generic_name(generic_name, places) < 0)
        {
            goto error;
        }

        // This is 1 Byte length.
        if (encode_sparam(ref buffer, 1 + offset, generic_name_spec, places) < 0)
        {
            goto error;
        }
        if (encode_ia5(ref buffer, 2 + offset, generic_name.characters) < 0)
        {
            goto error;
        }
        return 1;

    error:
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int decode_generic_name(_generic_name generic_name, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        try
        {
            if (update_places_generic_name(generic_name, places) < 0)
                throw new Exception("Error in update_places_generic_name");

            if (decode_sparam(ref buffer, offset + 1, generic_name_spec, ref places) < 0)
                throw new Exception("Error in decode_sparam");

            int nchars = buffer[offset] -1;

            if (decode_ia5(buffer, offset + 2, ref generic_name.characters, nchars) < 0)
                throw new Exception("Error in decode_ia5");

            return 1;
        }
        catch
        {
            return -1;
        }
    }
    

    // --------------------------------------------------------------------------------

    public static int value_generic_name(_generic_name generic_name, ref string index, int op, string[] value, int pos)
    {
        int ret = -1;
        switch (op)
        {
            case 0:
                ret = set_generic_name(generic_name, ref index, value, pos);
                break;
            case 1:
                ret = get_generic_name(generic_name, ref index, value);
                break;
        }
        return ret;
    }

    // --------------------------------------------------------------------------------

    public static int set_default_generic_name(_generic_name generic_name)
    {
        byte[] tmp = { 0x09, 0x00, 0x71, 0x77, 0x65, 0x72, 0x74, 0x79, 0x79, 0x79 };
        return decode_generic_name(generic_name, ref tmp, 0);
    }


    // --------------------------------------------------------------------------------
    public static _spec[] generic_name_spec =
{
    new _spec()
    {
        name = "presentation",
        nbit = 2,
        sbit = 0,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "presentation allowed"},
            {1, "presentation restricted"},
            {2, "blocking toggle"},
            {3, "no indication"}
        }
    },
    new _spec()
    {
        name = "availability",
        nbit = 1,
        sbit = 4,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "name available/unknown"},
            {1, "name not available"}
        }
    },
    new _spec()
    {
        name = "type_of_name",
        nbit = 3,
        sbit = 5,
        state = table_mode.TABLE_SET,
        map = new Dictionary<int, string>()
        {
            {0, "spare"},
            {1, "calling name"},
            {2, "original called name"},
            {3, "redirecting name"},
            {4, "connected name"}
        }
    },
    new _spec()
    {
        name = null,
        nbit = DefineConstants.NONSET,
        sbit = DefineConstants.NONSET,
        state = table_mode.TABLE_NONSET,
        map = new Dictionary<int, string>()
    }
};

    // --------------------------------------------------------------------------------
    public static int update_places_generic_name(_generic_name generic_name, IntWrapper[] places)
    {
        places[0] = generic_name.p_presentation;
        places[1] = generic_name.p_availability;
        places[2] = generic_name.p_type_of_name;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int set_generic_name(_generic_name generic_name, ref string index, string[] value, int pos)
    {
        try
        {
            if (index == "characters")
            {
                generic_name.characters = (char[])value.Clone();

                if (generic_name.characters == null)
                    throw new Exception("Failed to duplicate characters");
            }
            else
            {
                IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
                int value_tmp = -1;

                if (IsNumeric(value[0].ToString()))
                    value_tmp = int.Parse(value[pos]);
                else
                    throw new Exception("Value is not numeric");

                if (update_places_generic_name(generic_name, places) < 0)
                    throw new Exception("update_places_generic_name failed");

                if (set_param(generic_name_spec, places, ref index, value_tmp) < 0)
                    throw new Exception("set_param failed");
            }

            return 1;
        }
        catch
        {
            return -1;
        }
    }

    // --------------------------------------------------------------------------------
    public static int get_generic_name(_generic_name generic_name, ref string index, string[] value)
    {
        try
        {
            value = new string[0];  // Initialize value

            if (index == "characters")
            {
                value = (string[])generic_name.characters.Clone();
                if (value == null)
                    throw new Exception("Failed to duplicate characters");
            }
            else
            {
                IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
                int value_tmp = -1;

                if (update_places_generic_name(generic_name, places) < 0)
                    throw new Exception("UpdatePlacesGenericName failed");

                if (get_param(generic_name_spec, places, ref index, ref value_tmp) < 0)
                    throw new Exception("GetParam failed");

                value = new string[DefineConstants.MAX_DIGITS_GET + 1];  // Replace MAX_DIGITS_GET with the actual value
                for (int i = 0; i < value.Length; i++)
                    value[i] = DefineConstants.INIT_INT_TO_CHAR.ToString();  // Replace INIT_INT_TO_CHAR with the actual value

                string valueTmpString = value_tmp.ToString();
                Array.Copy(valueTmpString.ToCharArray(), value, valueTmpString.Length);
            }

            return 1;
        }
        catch
        {
            return -1;
        }
    }

    // --------------------------------------------------------------------------------

    public static int create_jurisdiction_information(ref _jurisdiction_information jurisdiction_information)
    {
        jurisdiction_information = new _jurisdiction_information();
        if (jurisdiction_information == null)
        {
            goto error;
        }
        jurisdiction_information.address_signals = null;
        return 1;

    error:
        return -1;
    }

    // --------------------------------------------------------------------------------

    public static int destroy_jurisdiction_information(_jurisdiction_information jurisdiction_information)
    {
        // No es necesario liberar la memoria en C#
        // jurisdiction_information.address_signals = null;
        // jurisdiction_information = null;
        return 1;
    }
    

    public static int get_length_encode_jurisdiction_information(_jurisdiction_information jurisdiction_information)
    {
        int digits = jurisdiction_information.address_signals == null ? 0 : jurisdiction_information.address_signals.Length;
        int digits_nbytes = (digits % 2 == 0) ? (digits / 2) : (digits / 2) + 1;
        int nbytes = 1 + 1 + digits_nbytes;
        return nbytes;
    }

    // --------------------------------------------------------------------------------

    public static int encode_jurisdiction_information(ref byte[] buffer, int offset, _jurisdiction_information jurisdiction_information)
    {
        int length = get_length_encode_jurisdiction_information(jurisdiction_information);
        buffer[offset] = (byte)(length - 1 - 1);
        if (encode_number(ref buffer, 1 + offset, jurisdiction_information.address_signals) < 0)
        {
            goto error;
        }
        return 1;

    error:
        return -1;
    }

    // --------------------------------------------------------------------------------
   
    public static int decode_jurisdiction_information(ref _jurisdiction_information jurisdiction_information, byte[] buffer, int offset)
    {
        int ndigits = buffer[offset] * 2;

        if (decode_number(buffer, offset + 1, ref jurisdiction_information.address_signals, ndigits) < 0)
        {
            return -1;
        }

        return 1;
    }

    // --------------------------------------------------------------------------------

    public static int value_jurisdiction_information(_jurisdiction_information jurisdiction_information, ref string index, int op, string[] value, int pos)
    {
        int ret = -1;
        switch (op)
        {
            case 0:
                ret = set_jurisdiction_information(jurisdiction_information, index, value, pos);
                break;
            case 1:
                ret = get_jurisdiction_information(jurisdiction_information, index, value, pos);
                break;
        }
        return ret;
    }

    // --------------------------------------------------------------------------------

    public static int set_default_jurisdiction_information(_jurisdiction_information jurisdiction_information)
    {
        byte[] tmp = { 0x02, 0x11, 0x22 };
        return decode_jurisdiction_information(ref jurisdiction_information, tmp, 0);
    }



    // --------------------------------------------------------------------------------
    public static int set_jurisdiction_information(_jurisdiction_information jurisdiction_information, string index, string[] value, int pos)
    {
        if (index == null || index != "address_signals")
        {
            return -1;
        }

        if (!IsNumeric(value[pos]))
        {
            return -1;
        }

        jurisdiction_information.address_signals = value;
        if (jurisdiction_information.address_signals == null)
        {
            return -1;
        }

        return 1;
    }




    // --------------------------------------------------------------------------------
    public static int get_jurisdiction_information(_jurisdiction_information jurisdiction_information, string index, string[] value, int pos)
    {
        if (index == null) goto error;
        if (index != "address_signals") goto error;

        if (!IsNumeric(value[pos])) goto error;

        if (jurisdiction_information.address_signals != null)
        {
            jurisdiction_information.address_signals = null;
        }

        jurisdiction_information.address_signals = value;

        if (jurisdiction_information.address_signals == null) goto error;

        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int create_original_called_number(ref _original_called_number original_called_number)
    {
        original_called_number = new _original_called_number();
        if (original_called_number == null)
        {
            goto error;
        }
        //Array.Clear(original_called_number, 0, original_called_number.Length);
        original_called_number.address_signals = null;
        return 1;
    error:
        return -1;
    }


    // --------------------------------------------------------------------------------

    public static int destroy_original_called_number(_original_called_number original_called_number)
    {
        // No se requiere el uso de 'free' en C#
        // Puedes simplemente asignar null a los campos necesarios
        original_called_number.address_signals = null;
        original_called_number = null;
        return 1;
    }


    public static int get_length_encode_original_called_number(_original_called_number original_called_number)
    {
        int digits = original_called_number.address_signals == null ? 0 : original_called_number.address_signals.Length;
        int digits_nbytes = (digits % 2 == 0) ? (digits / 2) : (digits / 2) + 1;
        int nbytes = 1 + 1 + 2 + digits_nbytes;
        return nbytes;
    }


    // --------------------------------------------------------------------------------

    public static int encode_original_called_number(ref byte[] buffer, int offset, _original_called_number original_called_number)
    {
        buffer[offset] = (byte)(get_length_encode_original_called_number(original_called_number) - 1 - 1);
        int digits = original_called_number.address_signals == null ? 0 : original_called_number.address_signals.Length;

        original_called_number.p_odd_even_indicator.Value = (digits % 2) == 0 ? 0 : 1;
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_original_called_number(original_called_number, places) < 0)
        {
            goto error;
        }
        if (encode_sparam(ref buffer, 1 + offset, original_called_number_spec, places) < 0)
        {
            goto error;
        }
        if (encode_number(ref buffer, 3 + offset, original_called_number.address_signals) < 0)
        {
            goto error;
        }
        return 1;

    error:
        return -1;
    }


    // --------------------------------------------------------------------------------
    
    public static int decode_original_called_number(ref _original_called_number original_called_number, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_original_called_number(original_called_number, places) < 0)
        {
            goto error;
        }

        if (decode_sparam(ref buffer, offset + 1, original_called_number_spec, ref places) < 0)
        {
            goto error;
        }

        int ndigits = ((buffer[offset] - 2) * 2) - original_called_number.p_odd_even_indicator.Value;
        if (decode_number(buffer, offset + 3, ref original_called_number.address_signals, ndigits) < 0)
        {
            goto error;
        }

        return 1;

    error:
        destroy_original_called_number(original_called_number);
        return -1;
    }




    // --------------------------------------------------------------------------------

    public static int value_original_called_number(_original_called_number original_called_number, string index, int op, string[] value, int pos)
	{
	  int ret = -1;
	  switch (op)
	  {
	  case 0:
		  ret = set_original_called_number(original_called_number, index, value, pos);
		  break;
	  case 1:
		  ret = get_original_called_number(original_called_number, index, value);
		  break;
	  };
	  return ret;
	}

// --------------------------------------------------------------------------------

	public static int set_default_original_called_number(_original_called_number original_called_number)
	{
	  byte[] tmp = {0x06, 0x81, 0x10, 0x22, 0x11, 0x33, 0x04};
	  return decode_original_called_number(ref original_called_number, ref tmp, 0);
	}



    // --------------------------------------------------------------------------------
    public static _spec[] original_called_number_spec =
{
    new _spec()
    {
        name = "nature_of_address_indicator", nbit = 7, sbit = 0, state = table_mode.TABLE_SET, map =
        {
            { 0, "spare"},
             { 1, "subscriber number (national use)"},
             { 2, "unknown (national use)"},
             { 3, "national (significant) number"},
             { 4, "international number"},
             { 5, "spare"}, { 6, "spare"}, { 7, "spare"}, { 8, "spare"}, { 9, "spare"}, { 10, "spare"}, { 11, "spare"}, { 12, "spare"}, { 13, "spare"}, { 14, "spare"}, { 15, "spare"}, { 16, "spare"}, { 17, "spare"}, { 18, "spare"}, { 19, "spare"}, { 20, "spare"}, { 21, "spare"}, { 22, "spare"}, { 23, "spare"}, { 24, "spare"}, { 25, "spare"}, { 26, "spare"}, { 27, "spare"}, { 28, "spare"}, { 29, "spare"}, { 30, "spare"}, { 31, "spare"}, { 32, "spare"}, { 33, "spare"}, { 34, "spare"}, { 35, "spare"}, { 36, "spare"}, { 37, "spare"}, { 38, "spare"}, { 39, "spare"}, { 40, "spare"}, { 41, "spare"}, { 42, "spare"}, { 43, "spare"}, { 44, "spare"}, { 45, "spare"}, { 46, "spare"}, { 47, "spare"}, { 48, "spare"}, { 49, "spare"}, { 50, "spare"}, { 51, "spare"}, { 52, "spare"}, { 53, "spare"}, { 54, "spare"}, { 55, "spare"}, { 56, "spare"}, { 57, "spare"}, { 58, "spare"}, { 59, "spare"}, { 60, "spare"}, { 61, "spare"}, { 62, "spare"}, { 63, "spare"}, { 64, "spare"}, { 65, "spare"}, { 66, "spare"}, { 67, "spare"}, { 68, "spare"}, { 69, "spare"}, { 70, "spare"}, { 71, "spare"}, { 72, "spare"}, { 73, "spare"}, { 74, "spare"}, { 75, "spare"}, { 76, "spare"}, { 77, "spare"}, { 78, "spare"}, { 79, "spare"}, { 80, "spare"}, { 81, "spare"}, { 82, "spare"}, { 83, "spare"}, { 84, "spare"}, { 85, "spare"}, { 86, "spare"}, { 87, "spare"}, { 88, "spare"}, { 89, "spare"}, { 90, "spare"}, { 91, "spare"}, { 92, "spare"}, { 93, "spare"}, { 94, "spare"}, { 95, "spare"}, { 96, "spare"}, { 97, "spare"}, { 98, "spare"}, { 99, "spare"}, { 100, "spare"}, { 101, "spare"}, { 102, "spare"}, { 103, "spare"}, { 104, "spare"}, { 105, "spare"}, { 106, "spare"}, { 107, "spare"}, { 108, "spare"}, { 109, "spare"}, { 110, "spare"}, { 111, "spare"}, { 112, "spare"},
             {113, "non_unique_subscriber_number"},
             {114, "reserved_for_national_use"},
             {115, "non_unique_international_number"},
             {116, "spare"}, {117, "spare"}, {118, "spare"}, {119, "spare"}, {120, "spare"}, {121, "spare"}, {122, "spare"}, {123, "spare"}, {124, "spare"}, {125, "spare"}, {126, "spare"}, {127, "spare"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "odd_even_indicator", nbit = 1, sbit = 7, state = table_mode.TABLE_SET, map =
        {
            {0, "even number of address signals"},
            {1, "odd number of address signals"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "address_presentation_restricted_indicator", nbit = 2, sbit = 2, state = table_mode.TABLE_SET, map =
        {
            {0, "presentation allowed"},
            {1, "presentation restricted"},
            {2, "address not available (Note 1) (national use)"},
            {3, "reserved for restriction by the network"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec()
    {
        name = "numbering_plan_indicator", nbit = 3, sbit = 4, state = table_mode.TABLE_SET, map =
        {
            {0, "spare"},
            {1, "ISDN (Telephony) numbering plan (ITU-T Recommendation E.164)"},
            {2, "spare"},
            {3, "Data numbering plan (ITU-T Recommendation X.121) (national use)"},
            {4, "Telex numbering plan (ITU-T Recommendation F.69) (national use)"},
            {5, "reserved for national use"},
            {6, "reserved for national use"},
            {7, "spare"},
            {DefineConstants.NONSET, null}
        }
    },
    new _spec() {name = null, nbit = DefineConstants.NONSET, sbit = DefineConstants.NONSET, state = table_mode.TABLE_NONSET, map = {}}
};


    // --------------------------------------------------------------------------------
    public static int update_places_original_called_number(_original_called_number original_called_number, IntWrapper[] places)
    {
        places[0] = original_called_number.p_nature_of_address_indicator;
        places[1] = original_called_number.p_odd_even_indicator;
        places[2] = original_called_number.p_address_presentation_restricted_indicator;
        places[3] = original_called_number.p_numbering_plan_indicator;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int set_original_called_number(_original_called_number original_called_number, string index, string[] value, int pos)
    {
        if (index == "odd_even_indicator")
        {
            return -1;
        }
        string stringValue = value[pos];
        if (index == "address_signals")
        {
            int numericValue;
            if (!int.TryParse(stringValue, out numericValue))
            {
                return -1;
            }

            original_called_number.address_signals = new string[] { stringValue };
            if (original_called_number.address_signals == null)
            {
                return -1;
            }

            // Update the odd even param.
            int size = stringValue.Length;
            original_called_number.p_odd_even_indicator.Value = (size % 2 == 0) ? 0 : 1;
        }
        else
        {
            IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
            int valueTmp = -1;
            if (int.TryParse(stringValue, out valueTmp))
            {
                return -1;
            }

            if (update_places_original_called_number(original_called_number, places) < 0)
            {
                return -1;
            }
            if (set_param(original_called_number_spec, places, ref index, valueTmp) < 0)
            {
                return -1;
            }
        }
        return 1;
    }



    // --------------------------------------------------------------------------------
    public static int get_original_called_number(_original_called_number original_called_number, string index, string[] value)
    {
        if (index == "odd_even_indicator") goto error;

        if (index == "address_signals")
        {
            value = original_called_number.address_signals;
            if (value == null) goto error;
        }
        else
        {
            IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
            int value_tmp = -1;
            if (update_places_original_called_number(original_called_number, places) < 0) goto error;
            if (get_param(original_called_number_spec, places, ref index, ref value_tmp) < 0) goto error;
            value = new string[DefineConstants.MAX_DIGITS_GET + 1];
            for (int i = 0; i < value.Length; i++)
                value[i] = Convert.ToString(DefineConstants.INIT_INT_TO_CHAR);
            string valueStr = value_tmp.ToString();
            value = new string[1] { valueStr };
        }
        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int create_originating_line_information(ref _originating_line_information originating_line_information)
    {
        originating_line_information = new _originating_line_information();
        if (originating_line_information == null)
        {
            goto error;
        }
        //Array.Clear(originating_line_information, 0, originating_line_information.Length);
        return 1;
    error:
        return -1;
    }


    // --------------------------------------------------------------------------------

    public static int destroy_originating_line_information(ref _originating_line_information originating_line_information)
    {
        // C# doesn't have an equivalent to 'free' in C#
        originating_line_information = null;
        return 1;
    }



    public static int get_length_encode_originating_line_information(_originating_line_information originating_line_information)
	{
	  int param_id_byte = 1;
	  int length_byte = 1;
	  int nbytes = param_id_byte + length_byte + 1;
	  return nbytes;
	}

    // --------------------------------------------------------------------------------

    public static int encode_originating_line_information(ref byte[] buffer, int offset, _originating_line_information originating_line_information)
    {
        // The entire parameter length minus the name of the parameter and the size byte.
        buffer[offset] = (byte)(get_length_encode_originating_line_information(originating_line_information) - 2);
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_originating_line_information(originating_line_information, places) < 0) goto error;
        // This is 2 Bytes length.
        if (encode_sparam(ref buffer, 1 + offset, originating_line_information_spec, places) < 0) goto error; // Skips the first byte
        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int decode_originating_line_information(_originating_line_information originating_line_information, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        if (update_places_originating_line_information(originating_line_information, places) < 0)
        {
            goto error;
        }
        if (decode_sparam(ref buffer, offset + 1, originating_line_information_spec, ref places) < 0)
        {
            goto error;
        }
        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int value_originating_line_information(_originating_line_information originating_line_information, string index, int op, string[] value, int pos)
	{
	  int ret = -1;
	  switch (op)
	  {
	  case 0:
		  ret = set_originating_line_information(originating_line_information, index, value, pos);
		  break;
	  case 1:
		  ret = get_originating_line_information(originating_line_information, index, value);
		  break;
	  };
	  return ret;
	}

// --------------------------------------------------------------------------------

	public static int set_default_originating_line_information(_originating_line_information originating_line_information)
	{
        byte[] tmp = { 0x01, 0x21, 0 };
        return decode_originating_line_information(originating_line_information, ref tmp, 0);
	}


	// --------------------------------------------------------------------------------
	public static _spec[] originating_line_information_spec =
	{
		new _spec()
		{
			name = "originating_line_information", nbit = 8, sbit = 0, state = table_mode.TABLE_SET, map =
			{
				{0, "Plain Old Telephone Service (POTS)"},
				{1, "Multiparty line (more than 2)"},
				{2, "ANI failure"},
				{3, "unassigned"},
				{4, "unassigned"},
				{5, "unassigned"},
				{6, "Station Level Rating"},
				{7, "special operator handling required"},
				{8, "unassigned"},
				{9, "unassigned"},
				{10, "not assignable - conflict with 10X test code"},
				{11, "unassigned"},
				{12, "not assignable - conflict with international outpulsing code"},
				{13, "not assignable - conflict with international outpulsing code"},
				{14, "not assignable - conflict with international outpulsing code"},
				{15, "not assignable - conflict with international outpulsing code"},
				{16, "not assignable - conflict with international outpulsing code"},
				{17, "not assignable - conflict with international outpulsing code"},
				{18, "not assignable - conflict with international outpulsing code"},
				{19, "not assignable - conflict with international outpulsing code"},
				{20, "not assignable - Automatic Identified Outward Dialing (AIOD)"},
				{21, "unassigned"},
				{22, "unassigned"},
				{23, "coin or non-coin on calls using database access"},
				{24, "800 service call"},
				{25, "800 service call from a pay station"},
				{26, "unassigned"},
				{27, "coin"},
				{28, "unassigned"},
				{29, "prison/inmate service"},
				{30, "intercept (blank)"},
				{31, "intercept (trouble)"},
				{32, "intercept (regular)"},
				{33, "unassigned"},
				{34, "telco operator handled call"},
				{35, "unassigned"},
				{36, "CPE"},
				{37, "unassigned"},
				{38, "unassigned"},
				{39, "unassigned"},
				{40, "unrestricted use - locally determined by carrier"},
				{41, "unrestricted use - locally determined by carrier"},
				{42, "unrestricted use - locally determined by carrier"},
				{43, "unrestricted use - locally determined by carrier"},
				{44, "unrestricted use - locally determined by carrier"},
				{45, "unrestricted use - locally determined by carrier"},
				{46, "unrestricted use - locally determined by carrier"},
				{47, "unrestricted use - locally determined by carrier"},
				{48, "unrestricted use - locally determined by carrier"},
				{49, "unrestricted use - locally determined by carrier"},
				{50, "unassigned"},
				{51, "unassigned"},
				{52, "OUTward Wide Area Telecommunications Service (OUTWATS)"},
				{53, "unassigned"},
				{54, "unassigned"},
				{55, "unassigned"},
				{56, "unassigned"},
				{57, "unassigned"},
				{58, "unassigned"},
				{59, "unassigned"},
				{60, "TRS"},
				{61, "cellular/wireless PCS (type 1)"},
				{62, "cellular/wireless PCS (type 2)"},
				{63, "cellular/wireless PCS (roaming)"},
				{64, "unassigned"},
				{65, "unassigned"},
				{66, "TRS"},
				{67, "TRS"},
				{68, "unassigned"},
				{69, "unassigned"},
				{70, "private paystations"},
				{71, "unassigned"},
				{72, "unassigned"},
				{73, "unassigned"},
				{74, "unassigned"},
				{75, "unassigned"},
				{76, "unassigned"},
				{77, "unassigned"},
				{78, "unassigned"},
				{79, "unassigned"},
				{80, "reserved for future expansion to 3-digit code"},
				{81, "reserved for future expansion to 3-digit code"},
				{82, "reserved for future expansion to 3-digit code"},
				{83, "reserved for future expansion to 3-digit code"},
				{84, "reserved for future expansion to 3-digit code"},
				{85, "reserved for future expansion to 3-digit code"},
				{86, "reserved for future expansion to 3-digit code"},
				{87, "reserved for future expansion to 3-digit code"},
				{88, "reserved for future expansion to 3-digit code"},
				{89, "reserved for future expansion to 3-digit code"},
				{90, "unassigned"},
				{91, "unassigned"},
				{92, "unassigned"},
				{93, "access for private virtual network types of service"},
				{94, "unassigned"},
				{95, "unassignable - conflict with test codes 958 and 959"},
				{96, "unassigned"},
				{97, "unassigned"},
				{98, "unassigned"},
				{99, "unassigned"},
				{100, "reserved for future expansion (no interpretation)"},
				{101, "reserved for future expansion (no interpretation)"},
				{102, "reserved for future expansion (no interpretation)"},
				{103, "reserved for future expansion (no interpretation)"},
				{104, "reserved for future expansion (no interpretation)"},
				{105, "reserved for future expansion (no interpretation)"},
				{106, "reserved for future expansion (no interpretation)"},
				{107, "reserved for future expansion (no interpretation)"},
				{108, "reserved for future expansion (no interpretation)"},
				{109, "reserved for future expansion (no interpretation)"},
				{110, "reserved for future expansion (no interpretation)"},
				{111, "reserved for future expansion (no interpretation)"},
				{112, "reserved for future expansion (no interpretation)"},
				{113, "reserved for future expansion (no interpretation)"},
				{114, "reserved for future expansion (no interpretation)"},
				{115, "reserved for future expansion (no interpretation)"},
				{116, "reserved for future expansion (no interpretation)"},
				{117, "reserved for future expansion (no interpretation)"},
				{118, "reserved for future expansion (no interpretation)"},
				{119, "reserved for future expansion (no interpretation)"},
				{120, "reserved for future expansion (no interpretation)"},
				{121, "reserved for future expansion (no interpretation)"},
				{122, "reserved for future expansion (no interpretation)"},
				{123, "reserved for future expansion (no interpretation)"},
				{124, "reserved for future expansion (no interpretation)"},
				{125, "reserved for future expansion (no interpretation)"},
				{126, "reserved for future expansion (no interpretation)"},
				{127, "reserved for future expansion (no interpretation)"},
				{128, "reserved for future expansion (no interpretation)"},
				{129, "reserved for future expansion (no interpretation)"},
				{130, "reserved for future expansion (no interpretation)"},
				{131, "reserved for future expansion (no interpretation)"},
				{132, "reserved for future expansion (no interpretation)"},
				{133, "reserved for future expansion (no interpretation)"},
				{134, "reserved for future expansion (no interpretation)"},
				{135, "reserved for future expansion (no interpretation)"},
				{136, "reserved for future expansion (no interpretation)"},
				{137, "reserved for future expansion (no interpretation)"},
				{138, "reserved for future expansion (no interpretation)"},
				{139, "reserved for future expansion (no interpretation)"},
				{140, "reserved for future expansion (no interpretation)"},
				{141, "reserved for future expansion (no interpretation)"},
				{142, "reserved for future expansion (no interpretation)"},
				{143, "reserved for future expansion (no interpretation)"},
				{144, "reserved for future expansion (no interpretation)"},
				{145, "reserved for future expansion (no interpretation)"},
				{146, "reserved for future expansion (no interpretation)"},
				{147, "reserved for future expansion (no interpretation)"},
				{148, "reserved for future expansion (no interpretation)"},
				{149, "reserved for future expansion (no interpretation)"},
				{150, "reserved for future expansion (no interpretation)"},
				{151, "reserved for future expansion (no interpretation)"},
				{152, "reserved for future expansion (no interpretation)"},
				{153, "reserved for future expansion (no interpretation)"},
				{154, "reserved for future expansion (no interpretation)"},
				{155, "reserved for future expansion (no interpretation)"},
				{156, "reserved for future expansion (no interpretation)"},
				{157, "reserved for future expansion (no interpretation)"},
				{158, "reserved for future expansion (no interpretation)"},
				{159, "reserved for future expansion (no interpretation)"},
				{160, "reserved for future expansion (no interpretation)"},
				{161, "reserved for future expansion (no interpretation)"},
				{162, "reserved for future expansion (no interpretation)"},
				{163, "reserved for future expansion (no interpretation)"},
				{164, "reserved for future expansion (no interpretation)"},
				{165, "reserved for future expansion (no interpretation)"},
				{166, "reserved for future expansion (no interpretation)"},
				{167, "reserved for future expansion (no interpretation)"},
				{168, "reserved for future expansion (no interpretation)"},
				{169, "reserved for future expansion (no interpretation)"},
				{170, "reserved for future expansion (no interpretation)"},
				{171, "reserved for future expansion (no interpretation)"},
				{172, "reserved for future expansion (no interpretation)"},
				{173, "reserved for future expansion (no interpretation)"},
				{174, "reserved for future expansion (no interpretation)"},
				{175, "reserved for future expansion (no interpretation)"},
				{176, "reserved for future expansion (no interpretation)"},
				{177, "reserved for future expansion (no interpretation)"},
				{178, "reserved for future expansion (no interpretation)"},
				{179, "reserved for future expansion (no interpretation)"},
				{180, "reserved for future expansion (no interpretation)"},
				{181, "reserved for future expansion (no interpretation)"},
				{182, "reserved for future expansion (no interpretation)"},
				{183, "reserved for future expansion (no interpretation)"},
				{184, "reserved for future expansion (no interpretation)"},
				{185, "reserved for future expansion (no interpretation)"},
				{186, "reserved for future expansion (no interpretation)"},
				{187, "reserved for future expansion (no interpretation)"},
				{188, "reserved for future expansion (no interpretation)"},
				{189, "reserved for future expansion (no interpretation)"},
				{190, "reserved for future expansion (no interpretation)"},
				{191, "reserved for future expansion (no interpretation)"},
				{192, "reserved for future expansion (no interpretation)"},
				{193, "reserved for future expansion (no interpretation)"},
				{194, "reserved for future expansion (no interpretation)"},
				{195, "reserved for future expansion (no interpretation)"},
				{196, "reserved for future expansion (no interpretation)"},
				{197, "reserved for future expansion (no interpretation)"},
				{198, "reserved for future expansion (no interpretation)"},
				{199, "reserved for future expansion (no interpretation)"},
				{200, "reserved for future expansion (no interpretation)"},
				{201, "reserved for future expansion (no interpretation)"},
				{202, "reserved for future expansion (no interpretation)"},
				{203, "reserved for future expansion (no interpretation)"},
				{204, "reserved for future expansion (no interpretation)"},
				{205, "reserved for future expansion (no interpretation)"},
				{206, "reserved for future expansion (no interpretation)"},
				{207, "reserved for future expansion (no interpretation)"},
				{208, "reserved for future expansion (no interpretation)"},
				{209, "reserved for future expansion (no interpretation)"},
				{210, "reserved for future expansion (no interpretation)"},
				{211, "reserved for future expansion (no interpretation)"},
				{212, "reserved for future expansion (no interpretation)"},
				{213, "reserved for future expansion (no interpretation)"},
				{214, "reserved for future expansion (no interpretation)"},
				{215, "reserved for future expansion (no interpretation)"},
				{216, "reserved for future expansion (no interpretation)"},
				{217, "reserved for future expansion (no interpretation)"},
				{218, "reserved for future expansion (no interpretation)"},
				{219, "reserved for future expansion (no interpretation)"},
				{220, "reserved for future expansion (no interpretation)"},
				{221, "reserved for future expansion (no interpretation)"},
				{222, "reserved for future expansion (no interpretation)"},
				{223, "reserved for future expansion (no interpretation)"},
				{224, "reserved for future expansion (no interpretation)"},
				{225, "reserved for future expansion (no interpretation)"},
				{226, "reserved for future expansion (no interpretation)"},
				{227, "reserved for future expansion (no interpretation)"},
				{228, "reserved for future expansion (no interpretation)"},
				{229, "reserved for future expansion (no interpretation)"},
				{230, "reserved for future expansion (no interpretation)"},
				{231, "reserved for future expansion (no interpretation)"},
				{232, "reserved for future expansion (no interpretation)"},
				{233, "reserved for future expansion (no interpretation)"},
				{234, "reserved for future expansion (no interpretation)"},
				{235, "reserved for future expansion (no interpretation)"},
				{236, "reserved for future expansion (no interpretation)"},
				{237, "reserved for future expansion (no interpretation)"},
				{238, "reserved for future expansion (no interpretation)"},
				{239, "reserved for future expansion (no interpretation)"},
				{240, "reserved for future expansion (no interpretation)"},
				{241, "reserved for future expansion (no interpretation)"},
				{242, "reserved for future expansion (no interpretation)"},
				{243, "reserved for future expansion (no interpretation)"},
				{244, "reserved for future expansion (no interpretation)"},
				{245, "reserved for future expansion (no interpretation)"},
				{246, "reserved for future expansion (no interpretation)"},
				{247, "reserved for future expansion (no interpretation)"},
				{248, "reserved for future expansion (no interpretation)"},
				{249, "reserved for future expansion (no interpretation)"},
				{250, "reserved for future expansion (no interpretation)"},
				{251, "reserved for future expansion (no interpretation)"},
				{252, "reserved for future expansion (no interpretation)"},
				{253, "reserved for future expansion (no interpretation)"},
				{254, "reserved for future expansion (no interpretation)"},
				{255, "reserved for future expansion (no interpretation)"},
				{DefineConstants.NONSET, null}
			}
		},
		new _spec() {name = null, nbit = DefineConstants.NONSET, sbit = DefineConstants.NONSET, state = table_mode.TABLE_NONSET, map = {}}
	};

    // --------------------------------------------------------------------------------
    public static int update_places_originating_line_information(_originating_line_information originating_line_information, IntWrapper[] places)
    {
        places[0] = originating_line_information.p_originating_line_information;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int set_originating_line_information(_originating_line_information originating_line_information, string index, string[] value, int pos)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        string stringValue = value[pos];
        int valueTmp;
        if (!int.TryParse(stringValue, out valueTmp))
        {
            return -1;
        }

        if (update_places_originating_line_information(originating_line_information, places) < 0)
        {
            return -1;
        }
        if (set_param(originating_line_information_spec, places, ref index, valueTmp) < 0)
        {
            return -1;
        }

        return 1;
    }



    // --------------------------------------------------------------------------------
    public static int get_originating_line_information(_originating_line_information originating_line_information, string index, string[] value)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;

        if (update_places_originating_line_information(originating_line_information, places) < 0)
        {
            goto error;
        }

        if (get_param(originating_line_information_spec, places, ref index, ref value_tmp) < 0)
        {
            goto error;
        }

        value = new string[DefineConstants.MAX_DIGITS_GET + 1];
        for (int i = 0; i < value.Length; i++)
        {
            value[i] = Convert.ToString(DefineConstants.INIT_INT_TO_CHAR);
        }

        string tempValue = value_tmp.ToString();
        Array.Copy(tempValue.ToCharArray(), value, tempValue.Length);

        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int create_propagation_delay_counter(ref _propagation_delay_counter propagation_delay_counter)
    {
        propagation_delay_counter = new _propagation_delay_counter();
        if (propagation_delay_counter == null)
        {
            goto error;
        }
        // In C#, there's no need to explicitly initialize memory as C++ does with memset.
        // The memory is automatically initialized to default values for the data type.
        return 1;

    error:
        return -1;
    }


    // --------------------------------------------------------------------------------

    public static int destroy_propagation_delay_counter(ref _propagation_delay_counter propagation_delay_counter)
    {
        //Array.Clear(propagation_delay_counter, 0, propagation_delay_counter.Length);
        propagation_delay_counter = null;
        return 1;
    }


    public static int get_length_encode_propagation_delay_counter(_propagation_delay_counter propagation_delay_counter)
	{
	  int param_id_byte = 1;
	  int length_byte = 1;
	  int nbytes = param_id_byte + length_byte + 2;
	  return nbytes;
	}

    // --------------------------------------------------------------------------------

    public static int encode_propagation_delay_counter(ref byte[] buffer, int offset, _propagation_delay_counter propagation_delay_counter)
    {
        buffer[offset] = (byte)(get_length_encode_propagation_delay_counter(propagation_delay_counter) - 1 - 1);
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_propagation_delay_counter(propagation_delay_counter, places) < 0) return -1;

        // Esto es una longitud de 2 Bytes.
        if (encode_sparam(ref buffer, 1 + offset, propagation_delay_counter_spec, places) < 0) return -1;

        return 1;
    }


    // --------------------------------------------------------------------------------

    public static int decode_propagation_delay_counter(_propagation_delay_counter propagation_delay_counter, ref byte[] buffer, int offset)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_propagation_delay_counter(propagation_delay_counter, places) < 0)
        {
            goto error;
        }

        if (decode_sparam(ref buffer, offset + 1, propagation_delay_counter_spec, ref places) < 0)
        {
            goto error;
        }

        return 1;

    error:
        return -1;
    }



    // --------------------------------------------------------------------------------

    public static int value_propagation_delay_counter(_propagation_delay_counter propagation_delay_counter, ref string index, int op, string[] value, int pos)
	{
	  int ret = -1;
	  switch (op)
	  {
	  case 0:
		  ret = set_propagation_delay_counter(propagation_delay_counter, index, value, pos);
		  break;
	  case 1:
		  ret = get_propagation_delay_counter(propagation_delay_counter, index, value);
		  break;
	  };
	  return ret;
	}

    // --------------------------------------------------------------------------------

    public static int set_default_propagation_delay_counter(_propagation_delay_counter propagation_delay_counter)
    {
        byte[] tmp = { 0x02, 0x11, 0x11 };
        return decode_propagation_delay_counter(propagation_delay_counter, ref tmp, 0);
    }


    // --------------------------------------------------------------------------------
    public static _spec[] propagation_delay_counter_spec =
	{
		new _spec()
		{
			name = "msb", nbit = 8, sbit = 0, state = table_mode.TABLE_SET, map =
			{
				{0, "0"},
				{1, "1"},
				{2, "2"},
				{3, "3"},
				{4, "4"},
				{5, "5"},
				{6, "6"},
				{7, "7"},
				{8, "8"},
				{9, "9"},
				{10, "10"},
				{11, "11"},
				{12, "12"},
				{13, "13"},
				{14, "14"},
				{15, "15"},
				{16, "16"},
				{17, "17"},
				{18, "18"},
				{19, "19"},
				{20, "20"},
				{21, "21"},
				{22, "22"},
				{23, "23"},
				{24, "24"},
				{25, "25"},
				{26, "26"},
				{27, "27"},
				{28, "28"},
				{29, "29"},
				{30, "30"},
				{31, "31"},
				{32, "32"},
				{33, "33"},
				{34, "34"},
				{35, "35"},
				{36, "36"},
				{37, "37"},
				{38, "38"},
				{39, "39"},
				{40, "40"},
				{41, "41"},
				{42, "42"},
				{43, "43"},
				{44, "44"},
				{45, "45"},
				{46, "46"},
				{47, "47"},
				{48, "48"},
				{49, "49"},
				{50, "50"},
				{51, "51"},
				{52, "52"},
				{53, "53"},
				{54, "54"},
				{55, "55"},
				{56, "56"},
				{57, "57"},
				{58, "58"},
				{59, "59"},
				{60, "60"},
				{61, "61"},
				{62, "62"},
				{63, "63"},
				{64, "64"},
				{65, "65"},
				{66, "66"},
				{67, "67"},
				{68, "68"},
				{69, "69"},
				{70, "70"},
				{71, "71"},
				{72, "72"},
				{73, "73"},
				{74, "74"},
				{75, "75"},
				{76, "76"},
				{77, "77"},
				{78, "78"},
				{79, "79"},
				{80, "80"},
				{81, "81"},
				{82, "82"},
				{83, "83"},
				{84, "84"},
				{85, "85"},
				{86, "86"},
				{87, "87"},
				{88, "88"},
				{89, "89"},
				{90, "90"},
				{91, "91"},
				{92, "92"},
				{93, "93"},
				{94, "94"},
				{95, "95"},
				{96, "96"},
				{97, "97"},
				{98, "98"},
				{99, "99"},
				{100, "100"},
				{101, "101"},
				{102, "102"},
				{103, "103"},
				{104, "104"},
				{105, "105"},
				{106, "106"},
				{107, "107"},
				{108, "108"},
				{109, "109"},
				{110, "110"},
				{111, "111"},
				{112, "112"},
				{113, "113"},
				{114, "114"},
				{115, "115"},
				{116, "116"},
				{117, "117"},
				{118, "118"},
				{119, "119"},
				{120, "120"},
				{121, "121"},
				{122, "122"},
				{123, "123"},
				{124, "124"},
				{125, "125"},
				{126, "126"},
				{127, "127"},
				{128, "128"},
				{129, "129"},
				{130, "130"},
				{131, "131"},
				{132, "132"},
				{133, "133"},
				{134, "134"},
				{135, "135"},
				{136, "136"},
				{137, "137"},
				{138, "138"},
				{139, "139"},
				{140, "140"},
				{141, "141"},
				{142, "142"},
				{143, "143"},
				{144, "144"},
				{145, "145"},
				{146, "146"},
				{147, "147"},
				{148, "148"},
				{149, "149"},
				{150, "150"},
				{151, "151"},
				{152, "152"},
				{153, "153"},
				{154, "154"},
				{155, "155"},
				{156, "156"},
				{157, "157"},
				{158, "158"},
				{159, "159"},
				{160, "160"},
				{161, "161"},
				{162, "162"},
				{163, "163"},
				{164, "164"},
				{165, "165"},
				{166, "166"},
				{167, "167"},
				{168, "168"},
				{169, "169"},
				{170, "170"},
				{171, "171"},
				{172, "172"},
				{173, "173"},
				{174, "174"},
				{175, "175"},
				{176, "176"},
				{177, "177"},
				{178, "178"},
				{179, "179"},
				{180, "180"},
				{181, "181"},
				{182, "182"},
				{183, "183"},
				{184, "184"},
				{185, "185"},
				{186, "186"},
				{187, "187"},
				{188, "188"},
				{189, "189"},
				{190, "190"},
				{191, "191"},
				{192, "192"},
				{193, "193"},
				{194, "194"},
				{195, "195"},
				{196, "196"},
				{197, "197"},
				{198, "198"},
				{199, "199"},
				{200, "200"},
				{201, "201"},
				{202, "202"},
				{203, "203"},
				{204, "204"},
				{205, "205"},
				{206, "206"},
				{207, "207"},
				{208, "208"},
				{209, "209"},
				{210, "210"},
				{211, "211"},
				{212, "212"},
				{213, "213"},
				{214, "214"},
				{215, "215"},
				{216, "216"},
				{217, "217"},
				{218, "218"},
				{219, "219"},
				{220, "220"},
				{221, "221"},
				{222, "222"},
				{223, "223"},
				{224, "224"},
				{225, "225"},
				{226, "226"},
				{227, "227"},
				{228, "228"},
				{229, "229"},
				{230, "230"},
				{231, "231"},
				{232, "232"},
				{233, "233"},
				{234, "234"},
				{235, "235"},
				{236, "236"},
				{237, "237"},
				{238, "238"},
				{239, "239"},
				{240, "240"},
				{241, "241"},
				{242, "242"},
				{243, "243"},
				{244, "244"},
				{245, "245"},
				{246, "246"},
				{247, "247"},
				{248, "248"},
				{249, "249"},
				{250, "250"},
				{251, "251"},
				{252, "252"},
				{253, "253"},
				{254, "254"},
				{255, "255"}
			}
		},
		new _spec()
		{
			name = "lsb", nbit = 8, sbit = 0, state = table_mode.TABLE_SET, map =
			{
				{0, "0"},
				{1, "1"},
				{2, "2"},
				{3, "3"},
				{4, "4"},
				{5, "5"},
				{6, "6"},
				{7, "7"},
				{8, "8"},
				{9, "9"},
				{10, "10"},
				{11, "11"},
				{12, "12"},
				{13, "13"},
				{14, "14"},
				{15, "15"},
				{16, "16"},
				{17, "17"},
				{18, "18"},
				{19, "19"},
				{20, "20"},
				{21, "21"},
				{22, "22"},
				{23, "23"},
				{24, "24"},
				{25, "25"},
				{26, "26"},
				{27, "27"},
				{28, "28"},
				{29, "29"},
				{30, "30"},
				{31, "31"},
				{32, "32"},
				{33, "33"},
				{34, "34"},
				{35, "35"},
				{36, "36"},
				{37, "37"},
				{38, "38"},
				{39, "39"},
				{40, "40"},
				{41, "41"},
				{42, "42"},
				{43, "43"},
				{44, "44"},
				{45, "45"},
				{46, "46"},
				{47, "47"},
				{48, "48"},
				{49, "49"},
				{50, "50"},
				{51, "51"},
				{52, "52"},
				{53, "53"},
				{54, "54"},
				{55, "55"},
				{56, "56"},
				{57, "57"},
				{58, "58"},
				{59, "59"},
				{60, "60"},
				{61, "61"},
				{62, "62"},
				{63, "63"},
				{64, "64"},
				{65, "65"},
				{66, "66"},
				{67, "67"},
				{68, "68"},
				{69, "69"},
				{70, "70"},
				{71, "71"},
				{72, "72"},
				{73, "73"},
				{74, "74"},
				{75, "75"},
				{76, "76"},
				{77, "77"},
				{78, "78"},
				{79, "79"},
				{80, "80"},
				{81, "81"},
				{82, "82"},
				{83, "83"},
				{84, "84"},
				{85, "85"},
				{86, "86"},
				{87, "87"},
				{88, "88"},
				{89, "89"},
				{90, "90"},
				{91, "91"},
				{92, "92"},
				{93, "93"},
				{94, "94"},
				{95, "95"},
				{96, "96"},
				{97, "97"},
				{98, "98"},
				{99, "99"},
				{100, "100"},
				{101, "101"},
				{102, "102"},
				{103, "103"},
				{104, "104"},
				{105, "105"},
				{106, "106"},
				{107, "107"},
				{108, "108"},
				{109, "109"},
				{110, "110"},
				{111, "111"},
				{112, "112"},
				{113, "113"},
				{114, "114"},
				{115, "115"},
				{116, "116"},
				{117, "117"},
				{118, "118"},
				{119, "119"},
				{120, "120"},
				{121, "121"},
				{122, "122"},
				{123, "123"},
				{124, "124"},
				{125, "125"},
				{126, "126"},
				{127, "127"},
				{128, "128"},
				{129, "129"},
				{130, "130"},
				{131, "131"},
				{132, "132"},
				{133, "133"},
				{134, "134"},
				{135, "135"},
				{136, "136"},
				{137, "137"},
				{138, "138"},
				{139, "139"},
				{140, "140"},
				{141, "141"},
				{142, "142"},
				{143, "143"},
				{144, "144"},
				{145, "145"},
				{146, "146"},
				{147, "147"},
				{148, "148"},
				{149, "149"},
				{150, "150"},
				{151, "151"},
				{152, "152"},
				{153, "153"},
				{154, "154"},
				{155, "155"},
				{156, "156"},
				{157, "157"},
				{158, "158"},
				{159, "159"},
				{160, "160"},
				{161, "161"},
				{162, "162"},
				{163, "163"},
				{164, "164"},
				{165, "165"},
				{166, "166"},
				{167, "167"},
				{168, "168"},
				{169, "169"},
				{170, "170"},
				{171, "171"},
				{172, "172"},
				{173, "173"},
				{174, "174"},
				{175, "175"},
				{176, "176"},
				{177, "177"},
				{178, "178"},
				{179, "179"},
				{180, "180"},
				{181, "181"},
				{182, "182"},
				{183, "183"},
				{184, "184"},
				{185, "185"},
				{186, "186"},
				{187, "187"},
				{188, "188"},
				{189, "189"},
				{190, "190"},
				{191, "191"},
				{192, "192"},
				{193, "193"},
				{194, "194"},
				{195, "195"},
				{196, "196"},
				{197, "197"},
				{198, "198"},
				{199, "199"},
				{200, "200"},
				{201, "201"},
				{202, "202"},
				{203, "203"},
				{204, "204"},
				{205, "205"},
				{206, "206"},
				{207, "207"},
				{208, "208"},
				{209, "209"},
				{210, "210"},
				{211, "211"},
				{212, "212"},
				{213, "213"},
				{214, "214"},
				{215, "215"},
				{216, "216"},
				{217, "217"},
				{218, "218"},
				{219, "219"},
				{220, "220"},
				{221, "221"},
				{222, "222"},
				{223, "223"},
				{224, "224"},
				{225, "225"},
				{226, "226"},
				{227, "227"},
				{228, "228"},
				{229, "229"},
				{230, "230"},
				{231, "231"},
				{232, "232"},
				{233, "233"},
				{234, "234"},
				{235, "235"},
				{236, "236"},
				{237, "237"},
				{238, "238"},
				{239, "239"},
				{240, "240"},
				{241, "241"},
				{242, "242"},
				{243, "243"},
				{244, "244"},
				{245, "245"},
				{246, "246"},
				{247, "247"},
				{248, "248"},
				{249, "249"},
				{250, "250"},
				{251, "251"},
				{252, "252"},
				{253, "253"},
				{254, "254"},
				{255, "255"}
			}
		},
		new _spec() {name = null, nbit = DefineConstants.NONSET, sbit = DefineConstants.NONSET, state = table_mode.TABLE_NONSET, map = {}}
	};

    // --------------------------------------------------------------------------------
    public static int update_places_propagation_delay_counter(_propagation_delay_counter propagation_delay_counter, IntWrapper[] places)
    {
        places[0] = propagation_delay_counter.p_msb;
        places[1] = propagation_delay_counter.p_lsb;
        return 1;
    }

    // --------------------------------------------------------------------------------
    public static int set_propagation_delay_counter(_propagation_delay_counter propagation_delay_counter, string index, string[] value, int pos)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;

        if (IsNumeric(value[pos]))
        {
            value_tmp = int.Parse(value[pos]);
        }
        else
        {
            goto error;
        }

        if (update_places_propagation_delay_counter(propagation_delay_counter, places) < 0)
        {
            goto error;
        }

        if (set_param(propagation_delay_counter_spec, places, ref index, value_tmp) < 0)
        {
            goto error;
        }

        return 1;

    error:
        return -1;
    }


    // --------------------------------------------------------------------------------
    public static int get_propagation_delay_counter(_propagation_delay_counter propagation_delay_counter, string index, string[] value)
    {
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);
        int value_tmp = -1;
        if (update_places_propagation_delay_counter(propagation_delay_counter, places) < 0) goto error;
        if (get_param(propagation_delay_counter_spec, places, ref index, ref value_tmp) < 0) goto error;

        value = new string[DefineConstants.MAX_DIGITS_GET + 1];
        for (int i = 0; i < value.Length; i++)
        {
            value[i] = Convert.ToString(DefineConstants.INIT_INT_TO_CHAR);
        }

        string tempValue = value_tmp.ToString();
        value = new string[1] { tempValue };

        return 1;

    error:
        return -1;
    }




    // --------------------------------------------------------------------------------
    public static int destroy_optional(Object[] places, isupss7_optional[] names)
    {
        int i = 0;
        while (names[i] != isupss7_optional.NONE)
        {
            if (places[i] != null)
            {
                switch (names[i])
                {
                    case isupss7_optional.CALLING_PARTY_NUMBER:
                        var cpn = (_calling_party_number)places[i];
                        destroy_calling_party_number(ref cpn);
                        break;
                    case isupss7_optional.PROPAGATION_DELAY_COUNTER:
                        var pdc = (_propagation_delay_counter)places[i];
                        destroy_propagation_delay_counter(ref pdc);
                        break;
                    case isupss7_optional.GENERIC_DIGITS:
                        var gn = (_generic_digits)places[i];
                        destroy_generic_digits(gn);
                        break;
                    case isupss7_optional.JURISDICTION_INFORMATION:
                        destroy_jurisdiction_information((_jurisdiction_information)places[i]);
                        break;
                    case isupss7_optional.CHARGE_NUMBER:
                        _charge_number cn = (_charge_number)places[i];
                        destroy_charge_number(cn);
                        break;
                    case isupss7_optional.ORIGINATING_LINE_INFORMATION:
                        var oli = (_originating_line_information)places[i];
                        destroy_originating_line_information(ref oli);
                        break;
                    case isupss7_optional.ORIGINAL_CALLED_NUMBER:
                        destroy_original_called_number((_original_called_number)places[i]);
                        break;
                    case isupss7_optional.GENERIC_NAME:
                        destroy_generic_name((_generic_name)places[i]);
                        break;
                    default:
                        break;
                }
            }
            i += 1;
        }
        return 1;
    }


    // --------------------------------------------------------------------------------
    //C++ TO C# CONVERTER TASK: Pointer arithmetic is detected on the parameter 'buffer', so pointers on this parameter are left unchanged:
    //C++ TO C# CONVERTER TASK: Pointer arithmetic is detected on the parameter 'size', so pointers on this parameter are left unchanged:
    public static int encode_optional(ref byte[] buffer, int offset, ref int size, object[] places, isupss7_optional[] names)
    {
        int ret = 1;
        int i = 0;
        int length;

        while ((names[i] != isupss7_optional.NONE) && (ret > 0))
        {
            length = 0;
            if (places[i] != null)
            {
                buffer[offset] = (byte)names[i];
                bool typebIsObject = places[i].GetType() == typeof(object);

                switch (names[i])
                {
                    case isupss7_optional.CALLING_PARTY_NUMBER:
                        _calling_party_number cpn = !typebIsObject ? (_calling_party_number)places[i] : new _calling_party_number();
                        ret = encode_calling_party_number(ref buffer, offset + 1, ref cpn);
                        length = get_length_encode_calling_party_number(ref cpn);
                        break;
                    case isupss7_optional.PROPAGATION_DELAY_COUNTER:
                        _propagation_delay_counter pdc = !typebIsObject ? (_propagation_delay_counter)places[i] : new _propagation_delay_counter();
                        ret = encode_propagation_delay_counter(ref buffer, offset + 1, pdc);
                        length = get_length_encode_propagation_delay_counter(pdc);
                        break;
                    case isupss7_optional.GENERIC_DIGITS:
                        _generic_digits gn = !typebIsObject ? (_generic_digits)places[i] : new _generic_digits();
                        ret = encode_generic_digits(ref buffer, offset + 1, gn);
                        length = get_length_encode_generic_digits(gn);
                        break;
                    case isupss7_optional.JURISDICTION_INFORMATION:
                        _jurisdiction_information jinf = !typebIsObject ? (_jurisdiction_information)places[i] : new _jurisdiction_information();
                        ret = encode_jurisdiction_information(ref buffer, offset + 1, jinf);
                        length = typebIsObject ? 0 : get_length_encode_jurisdiction_information(jinf);
                        break;
                    case isupss7_optional.CHARGE_NUMBER:
                        _charge_number cn = !typebIsObject ? (_charge_number)places[i] : new _charge_number();
                        ret = encode_charge_number(ref buffer, offset + 1, cn);
                        length = get_length_encode_charge_number(cn);
                        break;
                    case isupss7_optional.ORIGINATING_LINE_INFORMATION:
                        _originating_line_information oli = !typebIsObject ? (_originating_line_information)places[i] : new _originating_line_information();
                        ret = encode_originating_line_information(ref buffer, offset + 1, oli);
                        length = get_length_encode_originating_line_information(oli);
                        break;
                    case isupss7_optional.ORIGINAL_CALLED_NUMBER:
                        _original_called_number ocn = !typebIsObject ? (_original_called_number)places[i] : new _original_called_number();
                        ret = encode_original_called_number(ref buffer, offset + 1, ocn);
                        length = get_length_encode_original_called_number(ocn);
                        break;
                    case isupss7_optional.GENERIC_NAME:
                        _generic_name gn2 = !typebIsObject ? (_generic_name)places[i] : new _generic_name();
                        ret = encode_generic_name(ref buffer, offset + 1, gn2);
                        length = get_length_encode_generic_name(gn2);
                        break;
                    default:
                        break;
                }
            }
            size += length;
            offset += length;
            //buffer = buffer.Skip(length).ToArray(); // Suponiendo que esto simule el avance del puntero en 'buffer'
            i += 1;
        }

        buffer[offset] = 0x00;

        return 1;
    }

    public static int encode_calling_party_number(ref byte[] buffer, int offset, ref _calling_party_number calling_party_number)
    {
        buffer[offset] = (byte)(get_length_encode_calling_party_number(ref calling_party_number) - 1 - 1);
        int digits = calling_party_number.address_signals == null ? 0 : calling_party_number.address_signals.Length;

        calling_party_number.p_odd_even_indicator.Value = ((digits % 2) == 0) ? 0 : 1;
        IntWrapper[] places = IntWrapper.CreateArray(DefineConstants.MAX_NPARAMS);

        if (update_places_calling_party_number(ref calling_party_number, ref places) < 0) { goto error; }
        
        // This is 2 Bytes length.
        if (encode_sparam(ref buffer, 1 + offset, calling_party_number_spec, places) < 0) { goto error; }
        if (encode_number(ref buffer, 3 + offset, calling_party_number.address_signals) < 0) { goto error; }
        
        return 1;

        error:
            return -1;

    }




    // --------------------------------------------------------------------------------
    public static int belongs(isupss7_optional param, isupss7_optional[] names)
    {
        int i = 0;
        int ret = -1;
        while (names[i] != null && ret == -1)
        {
            if (names[i] == param)
            {
                ret = i;
            }
            else
            {
                i += 1;
            }
        }
        return ret;
    }


    // --------------------------------------------------------------------------------
    public static int decode_optional(byte[] buffer, int offset, ref dynamic[] places, isupss7_optional[] names)
    {
        int pos = -1;
        int ret = 1;
        isupss7_optional param = isupss7_optional.NONE;
        param = (isupss7_optional)buffer[offset];

        while ((byte)param != 0)
        {
            Console.WriteLine($"param_id: {buffer[offset]} \t param_length: {buffer[offset + 1]}");

            pos = belongs(param, names);

            if (pos >= 0)
            {
                //bool typebIsObject = places[pos].GetType() == typeof(object);
                switch (param)
                { 
                    case isupss7_optional.CALLING_PARTY_NUMBER:
                        _calling_party_number rf1 = (_calling_party_number)places[pos];
                        if (create_calling_party_number(ref rf1) > 0)
                        {
                            decode_calling_party_number(ref rf1, ref buffer, 1 + offset);
                            places[pos] = rf1;
                        }
                        break;
                    case isupss7_optional.PROPAGATION_DELAY_COUNTER:
                        var rf2 = (_propagation_delay_counter)places[pos];
                        if (create_propagation_delay_counter(ref rf2) > 0)
                        {
                            ret = decode_propagation_delay_counter(rf2, ref buffer, 1 + offset);
                            places[pos] = rf2;
                        }
                        break;
                    case isupss7_optional.GENERIC_DIGITS:
                        _generic_digits rf3 = (_generic_digits)places[pos];
                        if (create_generic_digits(ref rf3) > 0)
                        {
                            ret = decode_generic_digits(rf3, ref buffer, 1 + offset);
                            places[pos] = rf3;
                        }
                        break;
                    case isupss7_optional.JURISDICTION_INFORMATION:
                        _jurisdiction_information rf4 = (_jurisdiction_information)places[pos];
                        if (create_jurisdiction_information(ref rf4) > 0)
                        {
                            ret = decode_jurisdiction_information(ref rf4, buffer, 1 + offset);
                            places[pos] = rf4;
                        }
                        break;
                    case isupss7_optional.CHARGE_NUMBER:
                        _charge_number rf5 = (_charge_number)places[pos];
                        if (create_charge_number(ref rf5) > 0)
                        {
                            ret = decode_charge_number(rf5, ref buffer, 1 + offset);
                            places[pos] = rf5;
                        }
                        break;
                    case isupss7_optional.ORIGINATING_LINE_INFORMATION:
                        _originating_line_information rf6 = (_originating_line_information)places[pos];
                        if (create_originating_line_information(ref rf6) > 0)
                        {
                            ret = decode_originating_line_information(rf6, ref buffer, 1 + offset);
                            places[pos] = rf6;
                        }
                        break;
                    case isupss7_optional.ORIGINAL_CALLED_NUMBER:
                        _original_called_number rf7 = (_original_called_number)places[pos];
                        if (create_original_called_number(ref rf7) > 0)
                        {
                            ret = decode_original_called_number(ref rf7, ref buffer, 1 + offset);
                            places[pos] = rf7;
                        }
                        break;
                    case isupss7_optional.GENERIC_NAME:
                        _generic_name rf8 = (_generic_name)places[pos];
                        if (create_generic_name(ref rf8) > 0)
                        {
                            ret = decode_generic_name(rf8, ref buffer, 1 + offset);
                            places[pos] = rf8;
                        }
                        break;
                    default:
                        Console.WriteLine("param not supported!");
                        break;
                }
            }
            offset += buffer[offset + 1] + 2;
            param = (isupss7_optional)buffer[offset];
        }

        return ret;
    }

    public static int idem_buffers(byte[] buffer1, int size1, byte[] buffer2, int size2)
    {
        int i = 0;
        int ret = 1;
        if (size1 != size2)
        {
            ret = 0;
        }
        else
        {
            while (i < size1 && ret > 0)
            {
                if (buffer1[i] != buffer2[i])
                {
                    ret = 0;
                }
                i++;
            }
        }
        return ret;
    }

    // --------------------------------------------------------------------------------
    public static int value_optional(object[] places, isupss7_optional[] names, string[] strings, ref string second_index, ref string third_index, int op, string[] value)
    {
        int ret = 1;
        int pos = 0;
        int found = 0;
        while (names[pos] != isupss7_optional.NONE && found == 0)
        {
            if (strings[pos].Equals(second_index))
            {
                found = 1;
            }
            else
            {
                pos += 1;
            }
        }
        if (found == 0)
        {
            ret = -1;
        }
        else
        {
            if (places[pos] == null && op == 1)
            {
                ret = -1;
            }
            else
            {
                switch (names[pos])
                {
                    case isupss7_optional.CALLING_PARTY_NUMBER:
                        if ((_calling_party_number)places[pos] == null)
                        {
                            var rf1 = (_calling_party_number)places[pos];
                            create_calling_party_number(ref rf1);
                            set_default_calling_party_number(ref rf1);
                        }
                        value_calling_party_number((_calling_party_number)places[pos], ref third_index, op, value, pos);
                        break;
                    case isupss7_optional.PROPAGATION_DELAY_COUNTER:
                        var pdc = (_propagation_delay_counter)places[pos];
                        if (pdc == null)
                        {
                            create_propagation_delay_counter(ref pdc);
                            set_default_propagation_delay_counter(pdc);
                        }
                        value_propagation_delay_counter(pdc, ref third_index, op, value, pos);
                        Console.WriteLine("propagation delay counter is in the house!");
                        break;
                    case isupss7_optional.GENERIC_DIGITS:
                        if ((_generic_digits)places[pos] == null)
                        {
                            _generic_digits gd = (_generic_digits)places[pos];
                            create_generic_digits(ref gd);
                            set_default_generic_digits((_generic_digits)places[pos]);
                        }
                        value_generic_digits((_generic_digits)places[pos], ref third_index, op, value);
                        Console.WriteLine("generic digits is in the house!");
                        break;
                    case isupss7_optional.JURISDICTION_INFORMATION:
                        if ((_jurisdiction_information)places[pos] == null)
                        {
                            var pp3 = (_jurisdiction_information)places[pos];
                            create_jurisdiction_information(ref pp3);
                            set_default_jurisdiction_information(pp3);
                        }
                        value_jurisdiction_information((_jurisdiction_information)places[pos], ref third_index, op, value, pos);
                        Console.WriteLine("jurisdiction information is in the house!");
                        break;
                    case isupss7_optional.CHARGE_NUMBER:
                        var pp = (_charge_number)places[pos];
                        if (pp == null)
                        {
                            create_charge_number(ref pp);
                            set_default_charge_number(pp);
                        }
                        value_charge_number(pp, ref third_index, op, value, pos);
                        Console.WriteLine("charge number is in the house!");
                        break;
                    case isupss7_optional.ORIGINATING_LINE_INFORMATION:
                        var oli = (_originating_line_information)places[pos];
                        if (oli == null)
                        {
                            create_originating_line_information(ref oli);
                            set_default_originating_line_information(oli);
                        }
                        value_originating_line_information(oli, third_index, op, value, pos);
                        Console.WriteLine("originating line information is in the house!");
                        break;
                    case isupss7_optional.ORIGINAL_CALLED_NUMBER:
                        var ocn = (_original_called_number)places[pos];
                        if (ocn == null)
                        {
                            create_original_called_number(ref ocn);
                            set_default_original_called_number(ocn);
                        }
                        value_original_called_number(ocn, third_index, op, value, pos);
                        Console.WriteLine("original called number is in the house!");
                        break;
                    case isupss7_optional.GENERIC_NAME:
                        var gn = (_generic_name)places[pos];
                        if (gn == null)
                        {
                            create_generic_name(ref gn);
                            set_default_generic_name(gn);
                        }
                        value_generic_name(gn, ref third_index, op, value, pos);
                        Console.WriteLine("generic name is in the house!");
                        break;
                    default:
                        Console.WriteLine("param not supported!");
                        break;
                }
            }
        }
        return ret;
    }



    // --------------------------------------------------------------------------------
    


    // --------------------------------------------------------------------------------
    public static int destroy_strings_optional(isupss7_optional[] names, string[] strings)
    {
        int i = 0;
        while (names[i] != isupss7_optional.NONE)
        {
            strings[i] = null;
            i += 1;
        }
        return 1;
    }



    // --------------------------------------------------------------------------------
    public static int update_null_optional(object[] places, isupss7_optional[] names)
    {
        int i = 0;
        while (names[i] != isupss7_optional.NONE)
        {
            places[i] = null;
            i += 1;
        }
        return 1;
    }


    // --------------------------------------------------------------------------------
    public static bool exists_optional(object[] places, isupss7_optional[] names)
    {
        bool ret = false;
        int i = 0;
        while (names[i] != isupss7_optional.NONE && ret == false)
        {
            if (places[i] != null)
            {
                ret = true;
            }
            else
            {
                i += 1;
            }
        }
        return ret;
    }



    public static string print_message_type(isupss7_message_type message_type_in)
    {
        switch (message_type_in)
        {
            case isupss7_message_type.ISUPSS7_IAM:
                return "IAM";
            case isupss7_message_type.ISUPSS7_SAM:
                return "SAM";
            case isupss7_message_type.ISUPSS7_ACM:
                return "ACM";
            default:
                return "message_type NONSET";
        }
    }


    public static string print_variant(isupss7_variant variant_in)
	{
	  switch (variant_in)
	  {
	  case isupss7_variant.ANSI:
		  return "ANSI";
	  case isupss7_variant.ITU:
		  return "ITU";
	  default:
		  return "variant NONSET";
	  };
	}
     

    public static int set_value_tree_isupss7lib(_isupss7_tree tree, string index, string[] value, int pos)
    {
        return value_tree(tree, ref index, 0, value, pos);
    }


    //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
    //int destroy_optional(object[][] places, isupss7_optional[] names);
    //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
    //int dump_optional(string[] dumped, object[][] places, isupss7_optional[] names);
    //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
    //int encode_optional(ref byte buffer, ref int size, object[][] places, isupss7_optional[] names);
    //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
    //int decode_optional(ref byte buffer, object[][] places, isupss7_optional[] names);
    //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
    //int value_optional(object[][] places, isupss7_optional[] names, string[] strings, ref string second_index, ref string third_index, int op, string[] value);

    //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
    //int create_strings_optional(isupss7_optional[] names, string[] strings);
    //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
    //int destroy_strings_optional(isupss7_optional[] names, string[] strings);
    //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
    //int update_null_optional(object[][] places, isupss7_optional[] names);
    //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
    //int exists_optional(object[][] places, isupss7_optional[] names);


}
