using PensionAccumulationCalculator.Forms;
using PensionAccumulationCalculator.Repos.Implementations;
using PensionAccumulationCalculator.Services.Implementations;

namespace PensionAccumulationCalculator {
    internal static class Program {
        [STAThread]
        static void Main() {
            ApplicationConfiguration.Initialize();
            Application.Run(new Login(new UserService(new UserRepo())));
        }
    }
}