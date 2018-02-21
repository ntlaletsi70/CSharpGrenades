using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.ErrorHandling;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using static System.Console;

namespace CSharpGrenadesGASource.DAL
{
    public class MenuSystemSQLProvider : MenuProviderBase
    {
       
        ErrorLogWriter logWriter = new ErrorLogWriter();
        private string conString = "Data Source = c:\\DataStores\\CSharpGrenades\\CSharpGrenadesGA.s3db;Version=3;";
        private SQLiteConnection _sqlConn;


        public MenuSystemSQLProvider()
        {
          
        }

        public override int InsertBeverages(BeveragesClass Beverages)
        {
           

            int rc = 1;

            try
            {
                _sqlConn = new SQLiteConnection(conString);

                _sqlConn.Open();

                string insertQuery = "INSERT INTO Beverage([Name],[Price],[Image]) VALUES(@Name,@Price,@Image)";
                SQLiteCommand sqlComm = new SQLiteCommand(insertQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {


                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Double),
                   new SQLiteParameter("@Image", DbType.Binary),

                };


                sqlParams[0].Value = Beverages.Name;
                sqlParams[1].Value = Beverages.Price;
                sqlParams[2].Value = Beverages.Image;

                sqlComm.Parameters.AddRange(sqlParams);

                int rowsAffected = sqlComm.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                   
                    rc = 0;
                    //Error.WriteLine("Insert Beverage" + "Success");
                 
                }

            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == SQLiteErrorCode.Constraint)
                {
                    rc = -1;
                   // Error.WriteLine(ex);
                    logWriter.WriteToLog(ex.Message, ex);
                
                }
            }
            catch (Exception ex)
            {
                //Error.WriteLine(ex);
               // Error.WriteLine(ex);
              logWriter.WriteToLog(ex.Message, ex);

            }
            finally
            {
                _sqlConn.Close();
            }
          
            return rc;

        }

        public override int InsertBreakafast(BreakfastClass breakfast)
        {

            int rc = 1;
           
          

            try
            {
                _sqlConn = new SQLiteConnection(conString);

                _sqlConn.Open();

                string insertQuery = "INSERT INTO Breakfast([Name],[Price],[Image]) VALUES(@Name,@Price,@Image)";
                SQLiteCommand sqlComm = new SQLiteCommand(insertQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {


                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Double),
                   new SQLiteParameter("@Image", DbType.Binary),

                };


                sqlParams[0].Value = breakfast.Name;
                sqlParams[1].Value = breakfast.Price;
                sqlParams[2].Value = breakfast.Image;

                sqlComm.Parameters.AddRange(sqlParams);

                int rowsAffected = sqlComm.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rc = 0;
                    //Error.WriteLine("Insert Breakfast" + "Success",_loggerPath);

                   
                }


            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == SQLiteErrorCode.Constraint)
                {
                    rc = -1;
                    logWriter.WriteToLog(ex.Message, ex);


                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }
            finally
            {
                _sqlConn.Close();
            }

       
            return rc;
        }

        public override int InsertDinner(DinnerClass dinner)
        {
            int rc = 1;
           

            try
            {
                _sqlConn = new SQLiteConnection(conString);

                _sqlConn.Open();

                string insertQuery = "INSERT INTO Dinner([Name],[Price],[Image]) VALUES(@Name,@Price,@Image)";
                SQLiteCommand sqlComm = new SQLiteCommand(insertQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {


                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Double),
                   new SQLiteParameter("@Image", DbType.Binary),

                };


                sqlParams[0].Value = dinner.Name;
                sqlParams[1].Value = dinner.Price;
                sqlParams[2].Value = dinner.Image;

                sqlComm.Parameters.AddRange(sqlParams);

                int rowsAffected = sqlComm.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rc = 0;            
                }
            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == SQLiteErrorCode.Constraint)
                {
                    logWriter.WriteToLog(ex.Message, ex);
                    rc = -1;
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rc;
        }

        public override int InsertLunch(LunchClass lunch)
        {
            int rc = 1;
          
            try
            {
                _sqlConn = new SQLiteConnection(conString);

                _sqlConn.Open();

                string insertQuery = "INSERT INTO Lunch([Name],[Price],[Image]) VALUES(@Name,@Price,@Image)";
                SQLiteCommand sqlComm = new SQLiteCommand(insertQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {

                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Double),
                   new SQLiteParameter("@Image", DbType.Binary),

                };

                sqlParams[0].Value = lunch.Name;
                sqlParams[1].Value = lunch.Price;
                sqlParams[2].Value = lunch.Image;

                sqlComm.Parameters.AddRange(sqlParams);

                int rowsAffected = sqlComm.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rc = 0;
                   
                }
            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == SQLiteErrorCode.Constraint)
                {

                    logWriter.WriteToLog(ex.Message, ex);
                    rc = -1;
                  
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rc;
        }

        public override int DeleteBeverages(BeveragesClass Beverages)
        {
            int rc = 0;
          
          

            try
            {
                int rowsAffected = 0;
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();

                string deletQuery = string.Format("DELETE FROM Beverage WHERE [ID] = '{0}'", Beverages.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(deletQuery, _sqlConn);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rc = -1;
               
                }
                else
                {
                    rc = 0;
                 
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }

            //Error.Close();
            return rc;
        }

        public override int DeleteDinner(DinnerClass dinner)
        {
            //SetError(Error);
            int rc = 0;
            try
            {
                int rowsAffected = 0;
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();

                string deletQuery = string.Format("DELETE FROM Dinner WHERE [ID] = '{0}'", dinner.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(deletQuery, _sqlConn);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rc = -1;        
                }
                else
                {
                    rc = 0;      
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }

            return rc;
        }

        public override int DeleteBreakafast(BreakfastClass breakfast)
        {
            int rc = 0;
            try
            {
                int rowsAffected = 0;
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();

                string deletQuery = string.Format("DELETE FROM Breakfast WHERE [ID] = '{0}'", breakfast.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(deletQuery, _sqlConn);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rc = -1;
                }
                else
                {
                    rc = 0;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }

            return rc;
        }

        public override int DeleteLunch(LunchClass lunch)
        {
            int rc = 0;
            try
            {
                int rowsAffected = 0;
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();

                string deletQuery = string.Format("DELETE FROM Lunch WHERE [ID] = '{0}'", lunch.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(deletQuery, _sqlConn);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rc = -1;
                }
                else
                {
                    rc = 0;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }

            return rc;
        }

        public override int UpdateBeverages(BeveragesClass Beverages)
        {
            int rowsAffected = 1;
            try
            {

                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string updateQuery = string.Format("UPDATE Beverage SET [Name] = @Name,[Price] = @Price WHERE [ID] = '{0}'", Beverages.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(updateQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {


                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Double)

                };

                sqlParams[0].Value = Beverages.Name;
                sqlParams[1].Value = Beverages.Price;
                sqlCommand.Parameters.AddRange(sqlParams);

                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rowsAffected = 0;
                }
                else
                {
                    rowsAffected = -1;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rowsAffected;
        }

        public override int UpdateDinner(DinnerClass dinner)
        {
            int rowsAffected = 1;
            try
            {

                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string updateQuery = string.Format("UPDATE Dinner SET [Name] = @Name,[Price] = @Price WHERE [ID] = '{0}'", dinner.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(updateQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {


                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Int32)

                };

                sqlParams[0].Value = dinner.Name;
                sqlParams[1].Value = dinner.Price;
                sqlCommand.Parameters.AddRange(sqlParams);

                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rowsAffected = 0;
                }
                else
                {
                    rowsAffected = -1;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rowsAffected;
        }

        public override int UpdateLunch(LunchClass lunch)
        {
            int rowsAffected = 1;
            try
            {

                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string updateQuery = string.Format("UPDATE Lunch SET [Name] = @Name,[Price] = @Price,[Image]=@Image WHERE [ID] = '{0}'", lunch.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(updateQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {

                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Int32),
                   new SQLiteParameter("@Image",DbType.Binary)

                };

                sqlParams[0].Value = lunch.Name;
                sqlParams[1].Value = lunch.Price;
                sqlCommand.Parameters.AddRange(sqlParams);

                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rowsAffected = 0;
                }
                else
                {
                    rowsAffected = -1;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }
            finally
            {
                _sqlConn.Close();
            }
            return rowsAffected;
        }

        public override int UpdateBreakafast(BreakfastClass breakfast)
        {
            int rowsAffected = 1;
            try
            {

                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string updateQuery = string.Format("UPDATE Breakfast SET [Name] = @Name,[Price] = @Price WHERE [ID] = '{0}'", breakfast.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(updateQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {

                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Int32)

                };

                sqlParams[0].Value = breakfast.Name;
                sqlParams[1].Value = breakfast.Price;
                sqlCommand.Parameters.AddRange(sqlParams);

                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rowsAffected = 0;
                }
                else
                {
                    rowsAffected = -1;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }
            finally
            {
                _sqlConn.Close();
            }
            return rowsAffected;
        }

        public override List<BeveragesClass> SelectAllBev()
        {
            List<BeveragesClass> bevs = null;
            ErrorLogWriter logWriter = new ErrorLogWriter();
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                bool readerSuccess = false;
                bevs = new List<BeveragesClass>();

                _sqlConn.Open();
                string selectQuery = "SELECT * FROM Beverage";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectQuery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                readerSuccess = reader.Read();
                while (readerSuccess == true)
                {
                    BeveragesClass bev = new BeveragesClass();
                    bev.Id = Convert.ToInt32(reader["ID"]);
                    bev.Name = Convert.ToString(reader["Name"]);
                    bev.Price = Convert.ToDouble(reader["Price"]);
                    bev.Image = (byte[])reader["Image"];
                    bevs.Add(bev);

                    readerSuccess = reader.Read();
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return bevs;
        }

        public override List<BreakfastClass> SelectAllBreakfast()
        {
            List<BreakfastClass> BreakFast = null;
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                bool readerSuccess = false;
                BreakFast = new List<BreakfastClass>();

                _sqlConn.Open();
                string selectQuery = "SELECT * FROM Breakfast";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectQuery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                readerSuccess = reader.Read();
                while (readerSuccess == true)
                {
                    BreakfastClass breakF = new BreakfastClass();
                    breakF.Id = Convert.ToInt32(reader["ID"]);
                    breakF.Name = Convert.ToString(reader["Name"]);
                    breakF.Price = Convert.ToDouble(reader["Price"]);
                    breakF.Image = (byte[])reader["Image"];
                    BreakFast.Add(breakF);

                    readerSuccess = reader.Read();
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }
            finally
            {
                _sqlConn.Close();
            }
            return BreakFast;
        }

        public override List<DinnerClass> SelectAllDinner()
        {
            List<DinnerClass> Dinner = null;
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                bool readerSuccess = false;
                Dinner = new List<DinnerClass>();

                _sqlConn.Open();
                string selectQuery = "SELECT * FROM Dinner";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectQuery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                readerSuccess = reader.Read();
                while (readerSuccess == true)
                {
                    DinnerClass din = new DinnerClass();
                    din.Id = Convert.ToInt32(reader["ID"]);
                    din.Name = Convert.ToString(reader["Name"]);
                    din.Price = Convert.ToDouble(reader["Price"]);
                    din.Image = (byte[])reader["Image"];
                    Dinner.Add(din);

                    readerSuccess = reader.Read();
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }
            finally
            {
                _sqlConn.Close();
            }
            return Dinner;
        }

        public override List<LunchClass> SelectAllLunch()
        {

            List<LunchClass> lunch = null;
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                bool readerSuccess = false;
                lunch = new List<LunchClass>();

                _sqlConn.Open();
                string selectQuery = "SELECT * FROM Lunch";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectQuery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                readerSuccess = reader.Read();
                while (readerSuccess == true)
                {
                    LunchClass lun = new LunchClass();
                    lun.Id = Convert.ToInt32(reader["ID"]);
                    lun.Name = Convert.ToString(reader["Name"]);
                    lun.Price = Convert.ToDouble(reader["Price"]);
                    lun.Image = (byte[])reader["Image"];
                    lunch.Add(lun);

                    readerSuccess = reader.Read();
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }
            finally
            {
                _sqlConn.Close();
            }
            return lunch;
        }

        public override int DeleteOrders(OrderClass Orders)
        {
            int rc = 0;

            try
            {
                int rowsAffected = 0;
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();

                string deletQuery = string.Format("DELETE FROM Orders WHERE [ID] = '{0}'", Orders.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(deletQuery, _sqlConn);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rc = -1;

                }
                else
                {
                    rc = 0;

                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }
            finally
            {
                _sqlConn.Close();
            }

            return rc;
        }

        public override int DeleteAllOrders()
        {
            int rc = 0;
            try
            {
                int rowsAffected = 0;
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();

                string deletQuery = string.Format("DELETE FROM Orders");
                SQLiteCommand sqlCommand = new SQLiteCommand(deletQuery, _sqlConn);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rc = -1;
                }
                else
                {
                    rc = 0;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }
            finally
            {
                _sqlConn.Close();
            }

            return rc;
        }


        public override int InsertOrders(OrderClass Orders)
        {
            int rc = 1;
          

            try
            {
                _sqlConn = new SQLiteConnection(conString);

                _sqlConn.Open();

                string insertQuery = "INSERT INTO Orders([Name],[Price],[Image],[NOItems]) VALUES(@Name,@Price,@Image,@NOItems)";
                SQLiteCommand sqlComm = new SQLiteCommand(insertQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {


                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Double),
                   new SQLiteParameter("@Image", DbType.Binary),
                   new SQLiteParameter("@NOItems", DbType.Int32),

                };


                sqlParams[0].Value = Orders.Name;
                sqlParams[1].Value = Orders.Price;
                sqlParams[2].Value = Orders.Image;
                sqlParams[3].Value = Orders.NumberOfItems;

                sqlComm.Parameters.AddRange(sqlParams);

                int rowsAffected = sqlComm.ExecuteNonQuery();

                if (rowsAffected == 1)
                {

                    rc = 0;
                   
                }


            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == SQLiteErrorCode.Constraint)
                {

                    logWriter.WriteToLog(ex.Message, ex);
                    rc = -1;

                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rc;
        }

        public override int UpdateOrders(OrderClass Orders)
        {
            int rowsAffected = 1;
            try
            {

                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string updateQuery = string.Format("UPDATE Orders SET [Name] = @Name,[Price] = @Price WHERE [ID] = '{0}'", Orders.Id);
                SQLiteCommand sqlCommand = new SQLiteCommand(updateQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {

                   new SQLiteParameter("@Name", DbType.String),
                   new SQLiteParameter("@Price", DbType.Int32)

                };

                sqlParams[0].Value = Orders.Name;
                sqlParams[1].Value = Orders.Price;
                sqlCommand.Parameters.AddRange(sqlParams);

                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rowsAffected = 0;
                }
                else
                {
                    rowsAffected = -1;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rowsAffected;
        }

        public override List<OrderClass> SelectAllOrders()
        {
            List<OrderClass> orderList = null;
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                bool readerSuccess = false;
                orderList = new List<OrderClass>();

                _sqlConn.Open();
                string selectQuery = "SELECT * FROM Orders";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectQuery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                readerSuccess = reader.Read();
                while (readerSuccess == true)
                {
                    OrderClass orderObj = new OrderClass();
                    orderObj.Id = Convert.ToInt32(reader["ID"]);
                    orderObj.Name = Convert.ToString(reader["Name"]);
                    orderObj.Price = Convert.ToDouble(reader["Price"]);
                    orderObj.NumberOfItems = Convert.ToInt32(reader["NOItems"]);
                    orderList.Add(orderObj);

                    readerSuccess = reader.Read();
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return orderList;
        }



    }

}
