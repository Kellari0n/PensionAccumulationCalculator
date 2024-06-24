namespace PensionAccumulationCalculator.Entities {
    internal class User {
        public int User_id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
