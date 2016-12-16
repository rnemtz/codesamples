using CodeExercises.LiskovSubstitution;

namespace CodeExercises.InterfaceSegregation
{
    public interface IInvoicePrinter
    {
        void Print(Invoice invoice);
        void PrintComplex(ComplexInvoice complexInvoice);
    }
}