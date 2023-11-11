using EDSCore;

namespace Hubs.Dominio.Entities
{
    public record IdRoleCustomer : AbsID
    {
        private IdRoleCustomer()
        {

        }
        public IdRoleCustomer(string id) : base(id)
        {
        }
    }

    public class RoleCustomer : Entidade<IdRoleCustomer>
    {
        
        public RoleCustomer(IdRoleCustomer id) : base(id)
        {
        }
        public string Role { get; set; }
        public string Menu { get; set; }
        public bool HaveAdd { get; set; }
        public bool HaveEdit { get; set; }
        public bool HaveMove { get; set; }
    }
}
