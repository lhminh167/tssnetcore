namespace Tss.Core.Extensions
{
    public static class DataTypeConverterExtensions
    {
        public static int ToInt(this string text)
        {
            int integer;

            int.TryParse(text, out integer);

            return integer;
        }

    }

}