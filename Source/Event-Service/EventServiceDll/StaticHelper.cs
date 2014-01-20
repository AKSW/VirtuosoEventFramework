using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.ServiceModel;

namespace VirtuosoEventService
{
    public class StaticHelper
    {
        public static IDbConnection dbcon { get; private set; }
        //public static string ServiceEndpointAddress = System.Configuration.ConfigurationManager.AppSettings.GetValues("endpointAddress")[0];
        private static bool consoleDbg = bool.Parse(WebConfigurationManager.AppSettings["writeDbgMessages"].ToString());
        private static string connectionString = WebConfigurationManager.AppSettings["connectionString"].ToString();
        public static string Namespace = "http://tempuri.org/";

        public static void InitializeDbConnection()
        {
            if (dbcon == null || dbcon.State != ConnectionState.Open)
            {
                dbcon = new OdbcConnection(connectionString);
                dbcon.Open();
            }
        }

        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            unhandledExceptionLog(ex);

        }

        public static void CloseDbConnection()
        {
            if (dbcon != null && dbcon.State == System.Data.ConnectionState.Open)
            {
                dbcon.Close();
                dbcon.Dispose();
            }
        }

        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public static void unhandledExceptionLog(Exception e, string additionalMsg = "", bool innerExc = false)
        {
            string separator = "\\";
            if (StaticHelper.IsLinux)
                separator = "/";
            string errorTxtDateiPfad = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + separator + "errorLog.txt";

            try
            {
                using (StreamWriter file = new System.IO.StreamWriter(errorTxtDateiPfad, true))
                {
                    if (!innerExc)
                    {
                        file.WriteLine("---------------------------new unhandled Exception -------------------------------");
                        writeToConsole("---------------------------new unhandled Exception -------------------------------");
                    }
                    else
                    {
                        file.WriteLine("-------------------------------inner Exception------------------------------------");
                        writeToConsole("-------------------------------inner Exception------------------------------------");
                    }
                    file.WriteLine(DateTime.UtcNow.ToString());
                    writeToConsole(DateTime.UtcNow.ToString());
                    file.WriteLine(System.Environment.MachineName.ToString());
                    writeToConsole(System.Environment.MachineName.ToString());
                    file.WriteLine(additionalMsg);
                    writeToConsole(additionalMsg);
                    file.WriteLine("--------Exception:-----");
                    writeToConsole("--------Exception:-----");
                    file.WriteLine(e.Message);
                    writeToConsole(e.Message);
                    file.WriteLine(e.StackTrace.ToString());
                    writeToConsole(e.StackTrace.ToString());

                    while (e.InnerException != null)
                    {
                        e = e.InnerException;
                        unhandledExceptionLog(e, "", true);
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

        public static void writeToConsole(string output)
        {
            if (consoleDbg)
                Console.WriteLine(output);
        }

        public static string writeArrayToString(object[] inputArray)
        {
            if (inputArray == null)
                return "";
            string output = "";
            foreach (object inn in inputArray)
            {
                Type valueType = inn.GetType();
                if (valueType.IsArray)
                    if (typeof(object).IsAssignableFrom(valueType.GetElementType()))
                        output += writeArrayToString((inn as object[]));
                    else
                        output += "[the element-type of this array is not knowen]";
                else
                    output += "," + inn.ToString();
            }
            return "[" + output.Substring(1) + "]";
        }
    }
}