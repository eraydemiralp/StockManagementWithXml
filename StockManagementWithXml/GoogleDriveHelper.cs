using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Apis.Drive.v3.DriveService;

namespace StockManagementWithXml
{
    class GoogleDriveHelper
    {
        public static string PathToServiceAccountKeyFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
       .Parent.FullName, "Credentials", "service-account-credentials.json");
        public static string ServiceAccountEmail = "stockmanagement@stockmanagement-458314.iam.gserviceaccount.com";
        public static string GoogleFolderID = "1xeUqdDFRltvfKIVaGzbto6ld9bZX20mn";
        public static DriveService service;
        public static GoogleCredential credential;
        public static void BackupAllFilesToDrive()
        {
            using (var stream = new FileStream(PathToServiceAccountKeyFile, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream);
            }
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    ScopeConstants.DriveFile,
                    ScopeConstants.DriveReadonly
                });
            }
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "StockManagement"
            });
            string activitiesXmlPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "XmlFiles", "Activities.xml");
            string partTypeXmlPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "XmlFiles", "PartType.xml");
            string shelveXmlPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "XmlFiles", "Shelve.xml");
            string stockXmlPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "XmlFiles", "Stock.xml");
            string userXmlPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "XmlFiles", "User.xml");
            uploadFile(activitiesXmlPath, "Activities-" + DateTime.Now.ToString("dd.MM.yyyy"));
            uploadFile(partTypeXmlPath, "PartType-" + DateTime.Now.ToString("dd.MM.yyyy"));
            uploadFile(shelveXmlPath, "Shelve-" + DateTime.Now.ToString("dd.MM.yyyy"));
            uploadFile(stockXmlPath, "Stock-" + DateTime.Now.ToString("dd.MM.yyyy"));
            uploadFile(userXmlPath, "User-" + DateTime.Now.ToString("dd.MM.yyyy"));
        }
        private static Google.Apis.Drive.v3.Data.File getFileMetadata(string fileName)
        {
            return new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string>() { GoogleFolderID }
            };
        }
        private static void uploadFile(string xmlPath, string fileName)
        {
            FilesResource.CreateMediaUpload request;
            var fileMetaData = getFileMetadata(fileName);
            using (var stream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                request = service.Files.Create(fileMetaData, stream, "");
                request.Fields = "id";
                var response = request.Upload();
                if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
                {
                    MessageBox.Show("Yedekleme yapılırken hata oluştu !!!");
                }

            }
        }
    }
}
