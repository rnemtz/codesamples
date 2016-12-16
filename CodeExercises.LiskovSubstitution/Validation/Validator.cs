namespace CodeExercises.LiskovSubstitution.Validation
{
    public abstract class Validator
    {
        public abstract bool Validate(Invoice invoice);
    }
}