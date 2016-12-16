namespace CodeExercises.DependencyInversion
{
    public interface IInvoicePrinter
    {
        void Print(Invoice invoice);
        void PrintComplex(ComplexInvoice complexInvoice);
    }
}