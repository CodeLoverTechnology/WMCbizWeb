using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WMCbizWeb.App_Code
{
    public class CommonFunction
    {
        public static bool IsFolderExist(string FolderPathFromResource)
        {
            try
            {
                if (!Directory.Exists(Path.GetFullPath(FolderPathFromResource)))
                {
                    Directory.CreateDirectory(Path.GetFullPath(FolderPathFromResource));
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool IsFileExist(string NewFileName)
        {
            try
            {
                if (!File.Exists(Path.GetFullPath(NewFileName)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CheckUserAuthentication()
        {
            if (HttpContext.Current.Session["userName"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}