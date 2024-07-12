using PensionAccumulationCalculator.Forms;
using PensionAccumulationCalculator.Repos.Implementations;
using PensionAccumulationCalculator.Services.Implementations;

using System.Xml;

namespace PensionAccumulationCalculator {
    internal static class Program {
        public static int ConnectionWaitingTime { get; } = 5000;

        [STAThread]
        static void Main() {
            AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);
            ApplicationConfiguration.Initialize();

            Application.Run(new Login(new UserService(new UserRepo())));
        }

        private async static void Test() {
            XmlDocument xml = new XmlDocument();
            xml.Load("C:\\Projects\\PensionAccumulationCalculator\\test.xml");

            UserRepo userRepo = new UserRepo();
            await userRepo.TryImportXmlAsync(xml);
        }
    }
}