using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniSearch.Helper;

namespace UniSearch.Extensions
{
    public static class ExceptionHelper
    {
        public static int LineNumber(this Exception e)
        {
            int linenum = 0;
            try
            {
                linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(' ')));
            }
            catch
            {
                //Stack trace is not available!
            }
            return linenum;
        }
    }

    public static class CustomLogger
    {
        private const string CorrelationIdHeader = "X-Correlation-ID";

        [Flags]
        private enum LogLevel
        {
            TRACE,
            INFO,
            DEBUG,
            WARNING,
            ERROR,
            FATAL
        }

        public static void CreateFolderIfNotExists(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                directory.Create();
            }
        }

        private static void WriteLine(string text, bool append = true)
        {
            try
            {
                string pathDir = AppDomain.CurrentDomain.BaseDirectory + "Logs";
                CreateFolderIfNotExists(pathDir);
                string filename = String.Format("{0:yyyy-MM-dd}{1}", DateTime.Now, ".log");
                string path = Path.Combine(pathDir, filename);
                using (StreamWriter writer = new StreamWriter(path, append, Encoding.UTF8))
                {
                    if (!string.IsNullOrEmpty(text))
                    {
                        writer.WriteLine(text);
                        writer.WriteLine();

                    }
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                //ignore
            }
        }

        private static void WriteFormattedLog(LogLevel level, string text, string methodName = "")
        {
            string pretext;
            string dateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            string correlationId = GetCurrentCorrelationId();
            switch (level)
            {
                case LogLevel.TRACE:
                    pretext = System.DateTime.Now.ToString(dateTimeFormat) + " [TRACE] " + correlationId + " " + methodName + "()    ";
                    break;
                case LogLevel.INFO:
                    pretext = System.DateTime.Now.ToString(dateTimeFormat) + " [INFO] " + correlationId + " " + methodName + "()    ";
                    break;
                case LogLevel.DEBUG:
                    pretext = System.DateTime.Now.ToString(dateTimeFormat) + " [DEBUG] " + correlationId + " " + methodName + "()    ";
                    break;
                case LogLevel.WARNING:
                    pretext = System.DateTime.Now.ToString(dateTimeFormat) + " [WARNING] " + correlationId + " " + methodName + "()    ";
                    break;
                case LogLevel.ERROR:
                    pretext = System.DateTime.Now.ToString(dateTimeFormat) + " [ERROR] " + correlationId + " " + methodName + "()    ";
                    break;
                case LogLevel.FATAL:
                    pretext = System.DateTime.Now.ToString(dateTimeFormat) + " [FATAL] " + correlationId + " " + methodName + "()    ";
                    break;
                default:
                    pretext = "";
                    break;
            }

            WriteLine(pretext + text);
        }


        //public static string GetCurrentCorrelationId()
        //{
        //    // Try accessing CorrelationId from request properties if available
        //    var request = HttpContext.Current?.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
        //    if (request?.Headers != null)
        //    {
        //        return request.Headers.GetValues(CorrelationIdHeader).FirstOrDefault();
        //    }

        //    return string.Empty;
        //}

        private static string GetCurrentCorrelationId()
        {
            // Access HttpContext from HttpContextHelper
            var context = HttpContextHelper.Current;
            if (context != null && context.Request.Headers.ContainsKey(CorrelationIdHeader))
            {
                return context.Request.Headers[CorrelationIdHeader].FirstOrDefault();
            }
            return string.Empty;
        }

        /// <summary>
        /// Log a DEBUG message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Debug(string text, [CallerMemberName] string methodName = "")
        {
            WriteFormattedLog(LogLevel.DEBUG, text, methodName);
        }

        /// <summary>
        /// Log an ERROR message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Error(string text, [CallerMemberName] string methodName = "")
        {
            WriteFormattedLog(LogLevel.ERROR, text, methodName);
        }

        /// <summary>
        /// Log a FATAL ERROR message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Fatal(string text, [CallerMemberName] string methodName = "")
        {
            WriteFormattedLog(LogLevel.FATAL, text, methodName);
        }

        /// <summary>
        /// Log an INFO message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Info(string text, [CallerMemberName] string methodName = "")
        {
            WriteFormattedLog(LogLevel.INFO, text, methodName);
        }

        /// <summary>
        /// Log a TRACE message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Trace(string text, [CallerMemberName] string methodName = "")
        {
            WriteFormattedLog(LogLevel.TRACE, text, methodName);
        }

        /// <summary>
        /// Log a WARNING message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Warning(string text, [CallerMemberName] string methodName = "")
        {
            WriteFormattedLog(LogLevel.WARNING, text, methodName);
        }


        public static void WriteMessageToFile(string text)
        {
            //string pathDir = AppDomain.CurrentDomain.BaseDirectory + "Logs";
            //CreateFolderIfNotExists(pathDir);
            //string filename = String.Format("{0:yyyy-MM-dd}__{1}", DateTime.Now, "log.txt");
            //string path = Path.Combine(pathDir, filename);
            //using (StreamWriter writer = new StreamWriter(path, true))
            //{
            //    writer.WriteLine(text);
            //    writer.Close();
            //}


            WriteFormattedLog(LogLevel.WARNING, text);

        }


        public static void WriteErrorLogToFile(Exception ex)
        {
            //string pathDir = AppDomain.CurrentDomain.BaseDirectory + "Logs";
            //CreateFolderIfNotExists(pathDir);
            //string filename = String.Format("{0:yyyy-MM-dd}__{1}", DateTime.Now, "log.txt");
            //string path = Path.Combine(pathDir,filename);
            //using (StreamWriter writer = new StreamWriter(path, true))
            //{
            //    string methodName = new StackTrace(ex).GetFrame(0).GetMethod().Name;
            //    writer.WriteLine();
            //    writer.Close();
            //}

            //Get stack trace for the exception with source file information
            //var st = new StackTrace(ex, true);
            //// Get the top stack frame
            //var frame = st.GetFrame(0);
            //// Get the line number from the stack frame
            //var line = frame.GetFileLineNumber();

            //int line = ex.LineNumber();

            string methodName = new StackTrace(ex).GetFrame(0).GetMethod().Name;
            string error = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") +
                            " : [" + ex.Source.ToString().Trim() + "] , [" + methodName + "] ; " + ex.ToString() + " " + ex.Message.ToString().Trim() + " " +
                            "\n\n STACK TRACE [ " + ex.StackTrace.ToString() + " ]";


            WriteFormattedLog(LogLevel.ERROR, error);



        }
    }
}
