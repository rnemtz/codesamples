namespace CodeExercises.InterfaceSegregation
{
    internal class ComplexInvoiceModifier
    {
        private readonly ComplexInvoice _invoice;

        public ComplexInvoiceModifier(ComplexInvoice invoice)
        {
            _invoice = invoice;
        }

        public void SetSubtotal(decimal subtotal)
        {
            _invoice.Subtotal = subtotal;
        }

        public void SetTaxRate(decimal taxRate)
        {
            _invoice.TaxRate = taxRate;
        }

        public void SetSecondTaxRate(decimal taxRate)
        {
            _invoice.SecondTaxRate = taxRate;
        }

        public ComplexInvoice GenerateInvoice()
        {
            return _invoice;
        }
    }
}