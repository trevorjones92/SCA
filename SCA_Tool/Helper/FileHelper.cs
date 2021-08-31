using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class FileHelper
{
    public List<ExternalSoftware> GetSoftwareUsedInApplication(string filePath)
    {
        string[] FileTypes = { "*.js", "*.dll", "*.nupkg" };

        List<ExternalSoftware> softwareList = new List<ExternalSoftware>();

        DirectoryInfo d = new DirectoryInfo(filePath); //The parent directory given

        foreach (var fileType in FileTypes)
        {
            FileInfo[] Files = d.GetFiles(fileType, SearchOption.AllDirectories); //Getting files of given file extension

            foreach (FileInfo file in Files)
            {
                ExternalSoftware externalSoftware = new ExternalSoftware();

                FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(@file.FullName);

                if (true)
                {

                }
                externalSoftware.SoftwareName = file.Name;

                if (fileVersion.FileVersion == "" || fileVersion.FileVersion is null)
                {
                    externalSoftware.VersionNumber = "";
                }
                else
                {
                    externalSoftware.VersionNumber = fileVersion.FileVersion;
                }
                externalSoftware.FileType = file.Extension;
                softwareList.Add(externalSoftware);
            }
        }

        return softwareList;
    }

    //public string ParseVersionNumber(FileInfo file)
    //{
    //    add regex pattern get numbers from file name
    //    string pattern = "\d+";
    //    Regex rgx = new Regex(pattern);
    //    return string;
    //}

    //public bool DoesSoftwareNameContainVersion(FileInfo file)
    //{
    //    If this is true, call the above method
    //    return bool;
    //}
}