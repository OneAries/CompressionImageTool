using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompressionImageTool.Command
{
    public static class FileControlAPI
    {
        /// <summary>
        /// 打开文件保存目录
        /// </summary>
        /// <param name="Description">提示语</param>
        /// <returns>文件目录地址</returns>
        public static string OpenFloderToSelectPath(string Description= "选择所有文件存放目录")
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = Description;
            if (folder.ShowDialog() == DialogResult.OK)
            {

                return folder.SelectedPath;
                
            }
            return "";

        }



        public static void GetImagePathsInDirectory(string SourcePath,ref List<string> Result)
        {
            
            DirectoryInfo theFolder = new DirectoryInfo(SourcePath);
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();//获取所在目录的文件夹
            FileInfo[] file = theFolder.GetFiles();//获取所在目录的文件

            foreach (FileInfo fileItem in file) //遍历文件
            {
                if(fileItem.Extension==".png"|| fileItem.Extension == ".jpeg"|| fileItem.Extension == ".jpg")
                Result.Add(fileItem.FullName);
            }
            //遍历文件夹
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                GetImagePathsInDirectory(NextFolder.FullName,ref Result);
            }
            
        }
    }
}
