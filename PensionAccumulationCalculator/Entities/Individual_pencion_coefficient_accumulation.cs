namespace PensionAccumulationCalculator.Entities {
    public class Individual_pencion_coefficient_accumulation {
        public int Accumulation_exp_id { get; set; }
        public int User_id { get; set; }
        public int Record_id { get; set; } 
        public required string Record_name { get; set; }
        public float Individual_pencion_coefficient { get; set; } 
        public int Year { get; set; }
    }
}
