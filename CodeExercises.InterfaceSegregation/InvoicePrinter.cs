namespace CodeExercises.InterfaceSegregation
{
    public class InvoicePrinter : IInvoicePrinter
    {
        public void Print(Invoice invoice)
        {
            //Logic Here
        }

        public void PrintComplex(ComplexInvoice complexInvoice)
        {
            //Logic Here
        }
    }
}