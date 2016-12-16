namespace CodeExercises.OpenClosed
{
    public class InvoiceTaxRateCalculator : Validator
    {
        public override bool Validate(Invoice invoice)
        {
            return invoice.TaxRate >= 0;
        }
    }
}