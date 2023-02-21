using DataAccess;

namespace Business
{
    public class BaseMathRoleAuthorEntities
    {
        private MathSoftAuthPlatFormEntities _context = null;
        public MathSoftAuthPlatFormEntities context { get => _context; set => _context = value; }
        public BaseMathRoleAuthorEntities()
        {
            _context = new MathSoftAuthPlatFormEntities();
        }
    }
}
