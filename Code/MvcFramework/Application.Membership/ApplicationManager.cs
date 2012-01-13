using System.Linq;

namespace Application.Membership
{
    public static class ApplicationManager
    {

        internal static int GetApplicationID(string ApplicationName, string cnnStr)
        {
            using (var db = SecUtility.Data(cnnStr))
            {
                return db.GetApplicationID(ApplicationName);
            }
        }

        internal static int GetApplicationID(this Entities data, string ApplicationName)
        {
            var myApp = data.Applications.FirstOrDefault(app => app.Name == ApplicationName);
            if (myApp == null)
            {
                myApp = new Application() { Name = ApplicationName };
                data.AddToApplications(myApp);
                data.SaveChanges();
            }

            return myApp.ApplicationID;
        }

        internal static Application GetApplication(this Entities data, int AppID)
        {
            return data.Applications.FirstOrDefault(app => app.ApplicationID == AppID);
        }

    }
}
