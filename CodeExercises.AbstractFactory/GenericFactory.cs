namespace CodeExercises.AbstractFactory
{
    public class GenericFactory<T> where T : new()
    {
        public T CreateObject()
        {
            return new T();
        }
    }
}