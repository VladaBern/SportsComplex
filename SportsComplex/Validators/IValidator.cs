namespace SportsComplex.Validators
{
    public interface IValidator<T>
    {
        void Validate(T item);
    }
}
