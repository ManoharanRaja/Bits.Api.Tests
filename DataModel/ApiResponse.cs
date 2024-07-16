namespace Bits.Api.Tests.DataModel
{
    public class ApiResponse
    {
        public string status { get; set; }
        public Data data {  get; set; }
        public ErrorResponse error {  get; set; }   
    }

    public class Data
    {
        public string userId {  get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int rating { get; set; }
    }

    public class ErrorResponse
    {
        public string errorType { get; set; }
        public string errorMessage { get; set; }
    }
}
