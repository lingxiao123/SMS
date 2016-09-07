using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace SMS.Utility
{
    public class WriteLog
    {
        private static StreamWriter streamWriter; //写文件  

        public static void WriteError(Exception ex)
        {
            try
            {
                //DateTime dt = new DateTime();
                
                string directPath = AppDomain.CurrentDomain.BaseDirectory + "logs";    //获得文件夹路径
                if (!Directory.Exists(directPath))   //判断文件夹是否存在，如果不存在则创建
                {
                    Directory.CreateDirectory(directPath);
                }
                directPath += string.Format(@"\{0}.txt", DateTime.Now.ToString("yyyy-MM-dd"));
                if (streamWriter == null)
                {
                    streamWriter = !File.Exists(directPath) ? File.CreateText(directPath) : File.AppendText(directPath);    //判断文件是否存在如果不存在则创建，如果存在则添加。
                }
                streamWriter.WriteLine("***********************************************************************");
                streamWriter.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                streamWriter.WriteLine("输出信息：错误信息");
                if (ex != null)
                {
                    streamWriter.WriteLine("当前时间：" + DateTime.Now.ToString());
                    streamWriter.WriteLine("异常信息：" + ex.Message);
                    streamWriter.WriteLine("异常对象：" + ex.Source);
                    streamWriter.WriteLine("调用堆栈：\n" + ex.StackTrace.Trim());
                    streamWriter.WriteLine("触发方法：" + ex.TargetSite);
                    streamWriter.WriteLine();
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Close();
                    streamWriter = null;
                }
            }
        }
    }
}