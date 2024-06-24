namespace PensionAccumulationCalculator.Entities {
    internal class Client {
        public int User_id { get; set; }
        public required string Second_name { get; set; }
        public required string First_name { get; set; }
        public string Last_name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public string Phone_number { get; set; } = string.Empty;
    }
}
