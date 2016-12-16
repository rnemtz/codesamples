namespace CodeExercises.InterfaceSegregation.Validation
{
    public class InvoiceTaxRateCalculator : Validator
    {
        public override bool Validate(Invoice invoice)
        {
            return invoice.TaxRate >= 0;
        }
    }
}