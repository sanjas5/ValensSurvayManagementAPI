using System;
namespace ValensSurveyManagementAPI
{

    // this class allowes us to store data for only one client
    //for storing all data we need to create a list
    public class User
    {
        // creating public variables that will store the data from the database
        public int id { get; set; }
        public String fullName { get; set; } = String.Empty;
        public String email { get; set; } = String.Empty;
        public String password { get; set; } = String.Empty;
        public String role { get; set; } = String.Empty;



    }



   
}

