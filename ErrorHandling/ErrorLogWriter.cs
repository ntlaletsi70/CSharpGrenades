using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using CSharpGrenadesGASource.BL;
using CSharpGrenadesGASource.BL.BusinessClasses;

namespace CSharpGrenadesGASource.ErrorHandling
{
   public class ErrorLogWriter
    {
        //private static XmlDocument doc = new XmlDocument();

        //private static XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        //private static XmlElement root = doc.DocumentElement;

        private static ErrorLogWriter _Instance;
        private static object _SyncRoot = new Object();
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();


        public ErrorBL Error { get; set; }
        public StreamWriter Writer { get; set; }

        public string LogPath { get; set; }

        public string LogFileName { get; set; }

        public string LogFileExtension { get; set; }

        public string LogFile { get { return LogFileName + LogFileExtension; } }

        public string LogFullPath { get { return Path.Combine(LogPath, LogFile); } }

        public bool LogExists { get { return File.Exists(LogFullPath); } }

        public ErrorLogWriter()
        {
            LogPath = "C:\\DataStores\\CSharpGrenades";
            LogFileName = DateTime.Now.ToString("dd-MM-yyyy");
            LogFileExtension = ".xml";
            
        }

  

        //protected override void Dispose(bool Disposing)
        //{
        //    if (!Disposed)
        //    {
        //        if (Disposing)
        //        {
        //        }
        //        // Close file
        //        Writer.Close();
        //        Writer = null;
        //        // Disposed
        //        Disposed = true;
        //        // Parent disposing
        //        base.Dispose(Disposing);
        //    }
        //}


        // Close the file
    


        // Implement Encoding() method from TextWriter
        public static ErrorLogWriter Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Instance == null)
                            _Instance = new ErrorLogWriter();
                    }
                }
                return _Instance;
            }
        }

        public void WriteToLog(String inLogMessage, Exception exy)
        {
            _readWriteLock.EnterWriteLock();
            try
            {
                LogFileName = "CSharpGrenades" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml";

                if (Directory.Exists(LogPath))
                {


                    string[] directoryItems = Directory.GetFiles(LogPath);
                    bool flag = false;
                    foreach (string item in directoryItems)
                    {
                        if (item.Contains(LogFileName))
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {

                        XmlDocument doc = new XmlDocument();

                        //(1) the xml declaration is recommended, but not mandatory
                        XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        XmlElement root = doc.DocumentElement;
                        doc.InsertBefore(xmlDeclaration, root);

                        //(2) string.Empty makes cleaner code
                        XmlElement element1 = doc.CreateElement(string.Empty, "Errors", string.Empty);
                        doc.AppendChild(element1);

                        XmlElement element2 = doc.CreateElement(string.Empty, "Error", string.Empty);
                        element1.AppendChild(element2);

                        XmlElement element3 = doc.CreateElement(string.Empty, "Date", string.Empty);
                        XmlText text1 = doc.CreateTextNode(DateTime.Now.ToString());
                        element3.AppendChild(text1);
                        element2.AppendChild(element3);

                        XmlElement element4 = doc.CreateElement(string.Empty, "Type", string.Empty);
                        XmlText text2 = doc.CreateTextNode(exy.GetType() + "");
                        element4.AppendChild(text2);
                        element2.AppendChild(element4);
                        XmlElement element5 = doc.CreateElement(string.Empty, "Description", string.Empty);
                        XmlText text3 = doc.CreateTextNode(exy.ToString());
                        element5.AppendChild(text3);
                        element2.AppendChild(element5);
                        string filename = LogPath + "\\" + LogFileName;
                        doc.Save(filename);
                    }
                    else
                    {

                        // MessageBox.Show(LogFileName + " \nPath exists");
    
                        Error = new ErrorBL("ErrorXMLProvider");
                        // string temp = LogPath;
                        Error.SelectAll("\\" + LogFileName);
                        Error currentError = new Error();
                        currentError.Date = DateTime.Now.ToString();
                        currentError.Type = exy.GetType() + "";
                        currentError.Description = exy.ToString();
                        Error.Insert(currentError);
                        //use XMLProvider

                    }
                    //Directory.CreateDirectory(LogPath);
                }



            }
            catch (Exception ex)
            {
                ErrorLogWriter.Write("Error.  WriteToLog", ex);

            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
        }

        
        public static void Write(string logMessage, Exception ex)
        {
            Instance.WriteToLog(logMessage, ex);
        }

    }
}
