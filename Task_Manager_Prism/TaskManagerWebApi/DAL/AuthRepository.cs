using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

public class AuthRepository : IDisposable
{
    private AuthContext _context;

    private UserManager<IdentityUser> _userManager;

    public AuthRepository()
    {
        _context = new AuthContext();
        _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_context));
    }

    public async Task<IdentityResult> RegisterUser(UserModel userModel)
    {
        IdentityUser user = new IdentityUser();
        user.UserName = userModel.UserName;

        var result = await _userManager.CreateAsync(user, userModel.Password);

        return result;
    }

    public async Task<IdentityUser> FindUser(string userName, string password)
    {
        IdentityUser user = await _userManager.FindAsync(userName, password);

        return user;
    }

    public void Dispose()
    {
        _context.Dispose();
        _userManager.Dispose();

    }

    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("ConnectionString")
        {

        }
    }
}