namespace EDSCore
{
    public record ValidationFalhas(IEnumerable<ValidationFalha> Errors) 
    {
        public ValidationFalhas(ValidationFalha error) : this(new[] { error })
        {

        }

        

        
    }
}
