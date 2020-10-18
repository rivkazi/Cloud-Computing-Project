using BE;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace DAL
{
    public class GoogleDriveAPITool
    {
        public static string[] Scopes = { DriveService.Scope.Drive };

        //create Drive API service.
        public static DriveService GetService()
        {
            //get Credentials from client_secret.json file 
            UserCredential credential;
            var CSPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/");

            using (var stream = new FileStream(Path.Combine(CSPath, "client_secret.json"), FileMode.Open, FileAccess.Read))
            {
                String FolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/"); ;
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;
            }

            //create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveAPI",
            });
            return service;
        }

        //get all files from Google Drive.
        public static List<GoogleDriveFile> GetDriveFiles()
        {
            DriveService service = GetService();

            // Define parameters of request.
            FilesResource.ListRequest FileListRequest = service.Files.List();
            // for getting folders only.
            // FileListRequest.Q = "mimeType='application/vnd.google-apps.folder'";
            FileListRequest.Fields = "nextPageToken, files(*)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFile> FileList = new List<GoogleDriveFile>();


            // For getting only folders
            // files = files.Where(x => x.MimeType == "application/vnd.google-apps.folder").ToList();
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    GoogleDriveFile File = new GoogleDriveFile
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Size = file.Size,
                        Version = file.Version,
                        CreatedTime = file.CreatedTime,
                        Parents = file.Parents,
                        MimeType = file.MimeType
                    };
                    FileList.Add(File);
                }
            }
            return FileList;
        }

        //file Upload to the Google Drive root folder.
        public static void FileUpload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                DriveService service = GetService();

                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                file.SaveAs(path);

                var FileMetaData = new Google.Apis.Drive.v3.Data.File();
                FileMetaData.Name = Path.GetFileName(file.FileName);
                FileMetaData.MimeType = MimeMapping.GetMimeMapping(path);

                FilesResource.CreateMediaUpload request;

                using (var stream = new FileStream(path, FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
            }
        }

        //Download file from Google Drive by fileId.
        public static string DownloadGoogleFile(string fileId)
        {
            DriveService service = GetService();

            string FolderPath = HttpContext.Current.Server.MapPath("/GoogleDriveFiles/");
            FilesResource.GetRequest request = service.Files.Get(fileId);

            string FileName = request.Execute().Name;
            string FilePath = Path.Combine(FolderPath, FileName);

            MemoryStream stream1 = new MemoryStream();

            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case DownloadStatus.Downloading:
                        {
                            break;
                        }
                    case DownloadStatus.Completed:
                        {
                            SaveStream(stream1, FilePath);
                            break;
                        }
                    case DownloadStatus.Failed:
                        {
                            break;
                        }
                }
            };
            request.Download(stream1);
            return FilePath;
        }

        public string DownloadGoogleFileByName(string fileName)
        {
            string fileId = (from file in GetDriveFiles()
                             where file.Name == (fileName + Path.GetExtension(file.Name))
                             select file.Id).FirstOrDefault();
            if (fileId != null)
                return DownloadGoogleFile(fileId);
            else
                return "icorrect file name.";
        }

        // file save to server path
        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        // Delete file from the Google drive
        public static void DeleteFile(GoogleDriveFile files)
        {
            DriveService service = GetService();
            try
            {
                // Initial validation.
                if (service == null)
                    throw new ArgumentNullException("service");

                if (files == null)
                    throw new ArgumentNullException(files.Id);

                // Make the request.
                service.Files.Delete(files.Id).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
        }

        // Create Folder in root
        public static void CreateFolder(string FolderName)
        {
            DriveService service = GetService();

            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = FolderName;
            FileMetaData.MimeType = "application/vnd.google-apps.folder";

            FilesResource.CreateRequest request;

            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();
        }

        // Create Folder in existing folder
        public static void CreateFolderInFolder(string folderId, string FolderName)
        {

            DriveService service = GetService();

            var FileMetaData = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(FolderName),
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string>
                    {
                        folderId
                    }
            };


            FilesResource.CreateRequest request;

            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();
            Console.WriteLine("Folder ID: " + file.Id);

            var file1 = request;

        }

        // File upload in existing folder
        public static void FileUploadInFolder(string folderId, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                DriveService service = GetService();

                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                file.SaveAs(path);

                var FileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(file.FileName),
                    MimeType = MimeMapping.GetMimeMapping(path),
                    Parents = new List<string>
                    {
                        folderId
                    }
                };

                FilesResource.CreateMediaUpload request;
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
                var file1 = request.ResponseBody;
            }
        }


        // check Folder name exist or note in root
        public static bool CheckFolder(string FolderName)
        {
            bool IsExist = false;

            DriveService service = GetService();

            // Define parameters of request.
            FilesResource.ListRequest FileListRequest = service.Files.List();
            FileListRequest.Fields = "nextPageToken, files(*)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFile> FileList = new List<GoogleDriveFile>();


            //For getting only folders
            files = files.Where(x => x.MimeType == "application/vnd.google-apps.folder" && x.Name == FolderName).ToList();

            if (files.Count > 0)
            {
                IsExist = false;
            }
            return IsExist;
        }


        public static List<GoogleDriveFile> GetDriveFolders()
        {
            DriveService service = GetService();
            List<GoogleDriveFile> FolderList = new List<GoogleDriveFile>();

            FilesResource.ListRequest request = service.Files.List();
            request.Q = "mimeType='application/vnd.google-apps.folder'";
            request.Fields = "files(id, name)";

            Google.Apis.Drive.v3.Data.FileList result = request.Execute();
            foreach (var file in result.Files)
            {
                GoogleDriveFile File = new GoogleDriveFile
                {
                    Id = file.Id,
                    Name = file.Name,
                    Size = file.Size,
                    Version = file.Version,
                    CreatedTime = file.CreatedTime
                };
                FolderList.Add(File);
            }
            return FolderList;
        }

        public static string MoveFiles(String fileId, String folderId)
        {
            DriveService service = GetService();

            // Retrieve the existing parents to remove
            FilesResource.GetRequest getRequest = service.Files.Get(fileId);
            getRequest.Fields = "parents";
            Google.Apis.Drive.v3.Data.File file = getRequest.Execute();
            string previousParents = String.Join(",", file.Parents);

            // Move the file to the new folder
            FilesResource.UpdateRequest updateRequest = service.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = folderId;
            updateRequest.RemoveParents = previousParents;

            file = updateRequest.Execute();
            if (file != null)
            {
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }

        public static string CopyFiles(String fileId, String folderId)
        {
            DriveService service = GetService();

            // Retrieve the existing parents to remove
            FilesResource.GetRequest getRequest = service.Files.Get(fileId);
            getRequest.Fields = "parents";
            Google.Apis.Drive.v3.Data.File file = getRequest.Execute();

            // Copy the file to the new folder
            FilesResource.UpdateRequest updateRequest = service.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = folderId;
            //updateRequest.RemoveParents = previousParents;
            file = updateRequest.Execute();
            if (file != null)
            {
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }

        public static void DeleteGoogleFileByName(string filesName)
        {
            var fileIds = (from file in GetDriveFiles()
                           where file.Name == (filesName)
                           select file.Id).ToList();
            DriveService service = GetService();
            foreach (var fileId in fileIds)
            {
                FilesResource.DeleteRequest DeleteRequest = service.Files.Delete(fileId);
                DeleteRequest.Execute();
            }
        }
    }
}