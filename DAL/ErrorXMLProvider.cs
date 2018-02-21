using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.PL;
using System.Xml;
using System.Windows;
using System.IO;

namespace CSharpGrenadesGASource.DAL
{
    public class ErrorXMLProvider : ErrorProviderBase
    {


        private string dataStore = "C:\\DataStores\\CSharpGrenades\\";
        private XmlDocument xDoc; // Represents the XML document
        private XmlNode root; //Represents the root node
        CustomDialogOK ohk;
    


        public ErrorXMLProvider()
        {
            //ErrorHandling.ErrorLogWriter error = new ErrorHandling.ErrorLogWriter();
            //dataStore += error.LogFileName;
        }

        public override int Insert(Error error)
        {
            //
            int rc = 0; // will be returned, thus can not be declared in try block

            try
            {
                int rcFindErrorNode = 0;
                int rcLoadDoc = 0;

                XmlNode eNode = null;
                XmlElement eElement;

                //
                // A XmlElement is needed for each field of a Subject
                //
                XmlElement eDate;
                XmlElement eType;
                XmlElement eDescription;

                //
                // A XmlText is needed for each field of a Subject
                //
                XmlText eDateText;
                XmlText eTypeText;
                XmlText eDescriptionText;

                rcLoadDoc = loadDataStore();
                if (rcLoadDoc == 0)
                {
                    rcFindErrorNode = findErrorNode(error.Date, ref eNode);

                    if (rcFindErrorNode == -1)
                    {
                        //
                        // For each Subject XmlElement, create the field name
                        //
                        eElement = xDoc.CreateElement("Error");
                        eDate = xDoc.CreateElement("Date");
                        eType = xDoc.CreateElement("Type");
                        eDescription = xDoc.CreateElement("Description");

                        //
                        // For each Subject XmlText, create the field value
                        //
                        eDateText = xDoc.CreateTextNode(error.Date);
                        eTypeText = xDoc.CreateTextNode(error.Type);
                        eDescriptionText = xDoc.CreateTextNode(error.Description);

                        //
                        // For each Subject XmlElement, append the XmlText
                        //
                        eDate.AppendChild(eDateText);
                        eType.AppendChild(eTypeText);
                        eDescription.AppendChild(eDescriptionText);

                        //
                        // For each Subject XmlElement, append to sElement
                        //
                        eElement.AppendChild(eDate);
                        eElement.AppendChild(eType);
                        eElement.AppendChild(eDescription);

                        //
                        // Append sElement to _root
                        //
                        root.AppendChild(eElement);
                        xDoc.Save(dataStore);
                    } // end if
                    else
                    {
                        rc = -1;
                    } // end else
                }// end if
                else
                {
                    //
                    // Since this method does not make provision for a specific return code to
                    // indicate dataStore does not exists, a general exception is thrown
                    // which contains the method name and a meaningful message
                    //
                    throw new Exception("Insert(): " + dataStore + " does not exist (not found)");
                } // end else
            } // end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch
            return rc;  // Single return
        }

        public override List<Error> SelectAll(string path)
        {

            List<Error> errorList = new List<Error>();
            try
            {
                int retCode = 0;
                XmlNodeList nodeList;
                Error eObj;

                // errorList = // this ensures that if there are no records,
                // the returned list will not be null, but
                // it will be empty (Count = 0)
                dataStore += path;
                retCode = loadDataStore();
                if (retCode == 0) // Success
                {
                    nodeList = root.SelectNodes("Error");
                    //
                    //
                    // NOTE: other pre-test loops also valid, but foreach is the safest option
                    //
                    //
                    foreach (XmlNode nodeRef in nodeList)
                    {
                        eObj = convertNodeToErrorObj(nodeRef);
                        errorList.Add(eObj);
                    } // end foreach
                } // end if
                else
                {
                    if (retCode == -1)
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("No Logs generated!");
                        ohk.Show();
                    } // end if
                } // end else
            } // end try
            catch (Exception ex)
            {
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
            } // end catch
            return errorList; // Single return
        }


        private int findErrorNode(string date, ref XmlNode errorNode)
        {
            //
            //Method name : int findSubjectNode(string Name, ref XmlNode subjectNode)
            //Purpose     : Try to find the user supplied Subject in XML data store
            //Re-use      : 
            //Input       : - string code
            //                - the code for the Subject that must be searched for in the XML document
            //              - ref XmlNode subjectNode
            //                - if the Subject node is found, it will be sent back via this method parameter
            //Output      : - int
            //                0 : Subject found
            //                -1: Subject not found
            //                -2: XML data store not loaded

            int rc;  // will be returned, thus can not be declared in try block

            try
            {
                if (xDoc != null)
                {
                    errorNode = root.SelectSingleNode("Error[Date='" + date + "']");
                    if (errorNode != null)
                    {
                        rc = 0; // Success
                    } // end if
                    else
                    {
                        rc = -1; // Not found
                    } // end else
                } // end if
                else
                {
                    rc = -2; // XML document not loaded
                } // end else
            } // end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch
            return rc; // Single return
        } // end metho


        private Error convertNodeToErrorObj(XmlNode errorNode)
        {


            Error eObj; // will be returned, thus can not be declared in try block

            try
            {

                eObj = new Error();
                eObj.Date = errorNode.SelectSingleNode("Date").InnerText;
                eObj.Type = errorNode.SelectSingleNode("Type").InnerText;
                eObj.Description = errorNode.SelectSingleNode("Description").InnerText;

            } // end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch
            return eObj; // Single return
        } // end method

        private int loadDataStore()
        {
            //
            //Method name : int loadDataStore()
            //Purpose     : Try to load the XML document, and select root node via XPath
            //Re-use      : 
            //Input       : 
            //Output      : - int
            //                0 : Success
            //                -1: File not found
            //

            int rc = 0; // will be returned, thus can not be declared in try block

            try
            {
                if (File.Exists(dataStore))
                {
                  
                    xDoc = new XmlDocument();
                    xDoc.Load(dataStore);
                    root = xDoc.SelectSingleNode("Errors");
                } // end if
                else
                {
                   
                    rc = -1; // File not found
                } // end else
            } // end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch
            return rc; // Single return
        } // end method

        public override List<Error> SelectAll(string path,int value)
        {
            List<Error> errorList = new List<Error>();
            try
            {
                int retCode = 0;
                XmlNodeList nodeList;
                Error eObj;

                // errorList = // this ensures that if there are no records,
                // the returned list will not be null, but
                // it will be empty (Count = 0)
              
                retCode = loadDataStore(path);
                if (retCode == 0) // Success
                {
                    nodeList = root.SelectNodes("Error");
                    //
                    //
                    // NOTE: other pre-test loops also valid, but foreach is the safest option
                    //
                    //
                    foreach (XmlNode nodeRef in nodeList)
                    {
                        eObj = convertNodeToErrorObj(nodeRef);
                        errorList.Add(eObj);
                    } // end foreach
                } // end if
                else
                {
                    if (retCode == -1)
                    {


                        ohk = new CustomDialogOK();
                        ohk.SetMessage("No Logs generated!");
                        ohk.Show();
                    } // end if
                } // end else
            } // end try
            catch (Exception ex)
            {
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
            } // end catch
            return errorList; // Single return
        }


        private int loadDataStore(string path)
        {
            //
            //Method name : int loadDataStore()
            //Purpose     : Try to load the XML document, and select root node via XPath
            //Re-use      : 
            //Input       : 
            //Output      : - int
            //                0 : Success
            //                -1: File not found
            //

            int rc = 0; // will be returned, thus can not be declared in try block

            try
            {
                if (File.Exists(path))
                {

                    xDoc = new XmlDocument();
                    xDoc.Load(path);
                    root = xDoc.SelectSingleNode("Errors");
                } // end if
                else
                {

                    rc = -1; // File not found
                } // end else
            } // end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch
            return rc; // Single return
        } // end method
    }

}
