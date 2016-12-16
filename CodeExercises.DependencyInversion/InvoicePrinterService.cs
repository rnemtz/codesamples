namespace CodeExercises.DependencyInversion
{
    public class InvoicePrinterService
    {
        private readonly IInvoicePrinter _invoicePrinter;

        public InvoicePrinterService(IInvoicePrinter invoicePrinter)
        {
            _invoicePrinter = invoicePrinter;
        }

        public void Print(Invoice invoice)
        {
            _invoicePrinter.Print(invoice);
        }
    }
}