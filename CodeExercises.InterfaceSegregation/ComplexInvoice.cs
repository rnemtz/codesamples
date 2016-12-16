namespace CodeExercises.InterfaceSegregation
{
    public class ComplexInvoice
    {
        public decimal Subtotal { get; set; }
        public decimal TaxRate { get; set; }
        public decimal SecondTaxRate { get; set; }

        public decimal CalculateTax()
        {
            return (Subtotal*TaxRate/100) + (Subtotal*SecondTaxRate/100);
        }

        public decimal CalculateTotal()
        {
            return Subtotal + CalculateTax();
        }
    }
}