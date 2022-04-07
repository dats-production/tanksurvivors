namespace Utils
{
    public class Nullable<T> where T : struct
    {
        public T Value;
    }
    
    public class Nullable<T, T1> where T : struct
    {
        public T Key;
        public T1 Value;
    }
}