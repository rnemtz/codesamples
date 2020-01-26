namespace CodeExercises
{
    public interface ITree<T>
    {
        void Add(T data);
        void Get(int i);
        void Delete(int i);
        void PrintInOrder();
        void PringPreOrder();
        void PrintPostOrder();
    }
}
