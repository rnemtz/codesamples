namespace CodeExercises.SingleResponsability
{
    public class Invoice
    {
        public decimal Subtotal { get; set; }
        public decimal TaxRate { get; set; }

        public decimal CalculateTax()
        {
            return Subtotal*TaxRate/100;
        }

        public decimal CalculateTotal()
        {
            return Subtotal + CalculateTax();
        }

        
        //Adding Print method will cause a violation within Single Responsability principle
        //Since is not related to the calculation it will break the principle
        //We need to move or refactor this method to another class that also apply this method.
        //public void Print(Invoice invoice)
        //{
        //    //Logic Here
        //}
        
    }
}
