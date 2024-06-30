using PensionAccumulationCalculator.Forms.Users;
using PensionAccumulationCalculator.Repos.Implementations;
using PensionAccumulationCalculator.Services.Implementations;

namespace PensionAccumulationCalculator {
    internal static class Program {
        [STAThread]
        static void Main() {


            ApplicationConfiguration.Initialize();

            Application.Run(new UsersList(new UserService(new UserRepo())));
        }
    }
}