namespace EDSCore
{

    public record class AbsID
    {
        public AbsID()
        {

        }

        public AbsID(string id)
        {
            MongoGuid = id;
        }

        public string MongoGuid { get; private set; }
    }
    public abstract class Entidade<ID>
    {

        protected ID _id;

        public Entidade(ID id)
        {
            _id = id;
        }

        public ID Id { get => _id; private set => _id = value; }

  
    }
}