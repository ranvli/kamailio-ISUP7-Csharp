namespace TestDecodingEncoding
{
    public class _test
    {
        public string name { get; set; }
        public isupss7_message_type message_type_in { get; set; }
        public isupss7_variant variant_in { get; set; }
        public int size_in { get; set; }
        public List<byte> mybuf_in { get; set; } = new List<byte>(DefineConstants.MAX_BUFFER_LENGTH);
    }
}