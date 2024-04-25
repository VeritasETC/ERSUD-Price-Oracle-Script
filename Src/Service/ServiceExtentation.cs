using System;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Service
{
    public static class ServiceExtentation
    {
        public static void AddFireBase(this IServiceCollection services, IConfiguration configuration)
        {
            var privateKeyPath = configuration.GetSection("Fire_Base").GetSection("Private_Key_Json_File").Value;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", privateKeyPath);


            //services.AddScoped<IFireBaseWrapper, FireBaseWrapper>();

            FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.GetApplicationDefault() });
        }
    }
}
