namespace CodeExercises.DependencyInversion.Validation
{
    public class InvoiceTaxValidator : Validator
    {
        public override bool Validate(Invoice invoice)
        {
            return invoice.CalculateTax() >= 0;
        }
    }
}