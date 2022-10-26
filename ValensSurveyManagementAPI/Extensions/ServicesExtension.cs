//using System;
//using System.Data;
//using System.Data.SqlClient;
//namespace ValensSurveyManagementAPI.Extension
//{
//    public static class ServicesExtension
//    {
//        public static IServiceCollection AddServices (this IServiceCollection services, WebApplicationBuilder builder)
//        {
//            string dbConnectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

//            services.AddTransient<IDbConnection>((connection) => new SqlConnection(dbConnectionString));

//            return services;
//        }
//    }
//}

