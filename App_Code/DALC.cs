using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

public class DALC
{
    public static SqlConnection SqlConn
    {
        get { return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString); }
    }

    public class NewsCache
    {
        public DataTable DataTable;
        public DateTime DateTime;
    }

    public class AdministratorClass
    {
        public int ID;
        public string Fullname;
        public int UsersStatusID;
    }

    public static AdministratorClass _GetAdministratorsLogin
    {
        get
        {
            if (HttpContext.Current.Session["AdminLogin"] != null)
            {
                AdministratorClass AdministratorClass = new AdministratorClass();
                AdministratorClass = (AdministratorClass)HttpContext.Current.Session["AdminLogin"];
                return AdministratorClass;
            }
            else
            {
                return null;
            }
        }
    }

    public class DataTableResult
    {
        public int Count;
        public DataTable Dt = new DataTable();
    }

    public class Transaction
    {
        public SqlTransaction SqlTransaction = null;
        public SqlCommand Com = new SqlCommand();
    }

    #region Rahatlaşdıran metodlarımız

    #region Single
    //Bazadan tək dəyər alaq:
    public static string GetSingleValues(string Columns, string Table, string WhereAndOrderBy = "", string CatchValue = "-1")
    {
        using (SqlCommand com = new SqlCommand(String.Format("Select {0} From {1} {2}", Columns, Table, WhereAndOrderBy), SqlConn))
        {
            try
            {
                com.Connection.Open();
                return com.ExecuteScalar()._ToString();
            }
            catch (Exception er)
            {
                DALC.ErrorLogsInsert("DALC.GetDbSingleValues catch error: " + er.Message);
                return CatchValue;
            }
            finally
            {
                com.Connection.Close();
                com.Connection.Dispose();
            }
        }
    }

    //Bazadan tək dəyər alaq:
    public static string GetSingleValues(string Columns, string Table, string ParamsColumns, object ParamsValue, string OrderBy = "", string CatchValue = "-1")
    {
        using (SqlCommand com = new SqlCommand(String.Format("Select {0} From {1} Where {2}=@ParamsValue {3}", Columns, Table, ParamsColumns, OrderBy), SqlConn))
        {
            try
            {
                com.Connection.Open();
                com.Parameters.AddWithValue("@ParamsValue", ParamsValue);
                return com.ExecuteScalar()._ToString();
            }
            catch (Exception er)
            {
                DALC.ErrorLogsInsert("DALC.GetSingleValues Params catch error: " + er.Message);
                return CatchValue;
            }
            finally
            {
                com.Connection.Close();
                com.Connection.Dispose();
            }
        }
    }

    //Bazadan tək dəyər alaq - istənilən saytda parametr ilə:
    public static string GetSingleValues(string Columns, string Table, string ParamsKeys, object[] ParamsValues, string OrderBy = "", string CatchValue = "-1")
    {
        //SqlMulti params command
        SqlCommand com = new SqlCommand();

        try
        {
            string[] ParamsKeysArray = ParamsKeys.Split(',');

            //Mütləq value və parametr adları eyni olmalıdır.
            if (ParamsKeysArray.Length != ParamsValues.Length)
            {
                throw new Exception("ParamsKeys and ParamsValues not same size.");
            }

            StringBuilder WhereList = new StringBuilder("1=1");
            for (int i = 0; i < ParamsKeysArray.Length; i++)
            {
                if (ParamsKeysArray[i].Length < 1)
                    continue;

                WhereList.Append(" and " + ParamsKeysArray[i] + "=@" + ParamsKeysArray[i]);
                com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
            }

            com.CommandText = String.Format("Select {0} From {1} Where {2} {3}", Columns, Table, WhereList, OrderBy);
            com.Connection = SqlConn;


            com.Connection.Open();
            return com.ExecuteScalar()._ToString();
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("DALC.GetSingleValues MultiParams catch error: " + er.Message);
            return CatchValue;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
        }
    }
    #endregion

    #region Table

    //Bazadan table alaq
    public static DataTable GetDataTable(string Columns, string Table, string WhereAndOrderBy = "")
    {
        DataTable Dt = new DataTable();
        try
        {
            using (SqlDataAdapter Da = new SqlDataAdapter(String.Format("Select {0} From {1} {2}", Columns, Table, WhereAndOrderBy), SqlConn))
            {
                Da.Fill(Dt);
                return Dt;
            }
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("DALC.GetDataTable catch error: " + er.Message);
            return null;
        }
    }

    //Konkret 1 ədəd paramteri olanda.
    public static DataTable GetDataTable(string Columns, string Table, string ParamsColumns, object ParamsValue, string OrderBy = "")
    {
        DataTable Dt = new DataTable();
        try
        {
            using (SqlDataAdapter Da = new SqlDataAdapter(String.Format("Select {0} From {1} Where {2}=@ParamsValue {3}", Columns, Table, ParamsColumns, OrderBy), SqlConn))
            {
                Da.SelectCommand.Parameters.AddWithValue("@ParamsValue", ParamsValue);
                Da.Fill(Dt);
                return Dt;
            }
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("DALC.GetDataTable Params catch error: " + er.Message);
            return null;
        }
    }

    //Konkret 1 ədəd paramteri olanda.
    public static DataTable GetDataTable(string Columns, string Table, string ParamsKeys, object[] ParamsValues, string OrderBy = "")
    {
        try
        {
            string[] ParamsKeysArray = ParamsKeys.Split(',');

            //Mütləq value və parametr adları eyni olmalıdır.
            if (ParamsKeysArray.Length != ParamsValues.Length)
            {
                throw new Exception("ParamsKeys and ParamsValues not same size.");
            }
            //SqlMulti params command
            using (SqlCommand com = new SqlCommand())
            {
                StringBuilder WhereList = new StringBuilder("1=1");
                const string format = " and {0}=@{0}";

                for (int i = 0; i < ParamsKeysArray.Length; i++)
                {
                    if (ParamsKeysArray[i].Length < 1)
                        continue;

                    WhereList.Append(string.Format(format, ParamsKeysArray[i]));
                    com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
                }

                com.CommandText = String.Format("Select {0} From {1} Where {2} {3}", Columns, Table, WhereList, OrderBy);
                com.Connection = SqlConn;

                DataTable Dt = new DataTable();
                SqlDataAdapter Da = new SqlDataAdapter(com);
                Da.Fill(Dt);

                return Dt;
            }
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("DALC.GetDataTable MultiParams [" + Table + "] catch error: " + er.Message);
            return null;
        }
    }

    //Ümumi sql commanda görə datatable qaytarır.   
    public static DataTable GetDataTableBySqlCommand(string SqlCommand, string ParamsKeys = "", object[] ParamsValues = null)
    {
        try
        {
            string[] ParamsKeysArray = ParamsKeys.Split(',');

            DataTable Dt = new DataTable();
            using (SqlCommand com = new SqlCommand(SqlCommand, DALC.SqlConn))
            {
                if (ParamsValues != null)
                {
                    if (ParamsKeysArray.Length != ParamsValues.Length)
                    {
                        throw new Exception("ParamsKeys and ParamsValues not same size.");
                    }

                    for (int i = 0; i < ParamsKeysArray.Length; i++)
                    {
                        com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
                    }
                }
                com.Connection.Open();
                using (SqlDataReader Reader = com.ExecuteReader())
                {
                    Dt.Load(Reader);
                }
                com.Connection.Close();
                return Dt;
            }
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("DALC.GetDataTableBySqlCommand catch error: " + er.Message);
            return null;
        }
    }

    #endregion

    #region Insert

    public static int InsertDatabase(string TableName, Dictionary<string, object> Dictionary, bool IsErrorLog = true)
    {
        StringBuilder Columns = new StringBuilder();
        StringBuilder Params = new StringBuilder();
        SqlCommand com = new SqlCommand();

        foreach (var Item in Dictionary)
        {
            Columns.Append(Item.Key + ",");
            Params.Append("@" + Item.Key + ",");
            com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
        }

        try
        {
            com.Connection = SqlConn;
            com.CommandText = String.Format("Insert Into {0}({1}) Values({2}); Select SCOPE_IDENTITY();", TableName, Columns.ToString().Trim(','), Params.ToString().Trim(','));
            com.Connection.Open();
            return com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            //Əgər log eliyərkən xəta veribsə sadəcə email göndərək LOOP eləməsin.
            //Çünki log da xəta veribsə yəqin ki, SQL dayanıb.
            if (IsErrorLog)
            {
                ErrorLogsInsert(TableName + " DALC.InsertDatabase də xəta baş verdi: " + er.Message);
            }
            else
            {
                SendAdminMail(TableName, "ErrorLogsInsert-de xəta baş verdi: " + er.Message);
            }
            return -1;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    public static int InsertDatabase(string TableName, string[] Key, object[] Values)
    {
        StringBuilder Columns = new StringBuilder();
        StringBuilder Params = new StringBuilder();
        SqlCommand com = new SqlCommand();

        for (int i = 0; i < Key.Length; i++)
        {
            Columns.Append(Key[i] + ", ");
            Params.Append("@" + Key[i] + ", ");
            com.Parameters.AddWithValue("@" + Key[i], Values[i]);
        }

        try
        {
            com.Connection = SqlConn;
            com.CommandText = String.Format("Insert Into {0}({1}) Values({2}); Select SCOPE_IDENTITY();", TableName, Columns.ToString().Trim().Trim(','), Params.ToString().Trim().Trim(','));
            com.Connection.Open();
            return com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            ErrorLogsInsert(TableName + " DALC.InsertDatabase catch error: " + er.Message, true);
            return 0;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    public static int InsertDatabase(string TableName, Dictionary<string, object> Dictionary, Transaction Transaction, bool IsCommit = false)
    {
        int result = -1;
        StringBuilder Columns = new StringBuilder();
        StringBuilder Params = new StringBuilder();

        // Parametri təmizləyəkki ikinci dəfə eyni parametrlə insertə gəldikdə xəta verməsin
        Transaction.Com.Parameters.Clear();
        foreach (var Item in Dictionary)
        {
            Columns.Append(Item.Key + ",");
            Params.Append("@" + Item.Key + ",");
            Transaction.Com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
        }

        try
        {
            if (Transaction.SqlTransaction == null)
            {
                Transaction.Com.Connection = DALC.SqlConn;
                Transaction.Com.Connection.Open();
                Transaction.SqlTransaction = Transaction.Com.Connection.BeginTransaction();
            }

            Transaction.Com.CommandText = String.Format("Insert Into {0}({1}) Values({2}); Select SCOPE_IDENTITY();", TableName, Columns.ToString().Trim(','), Params.ToString().Trim(','));
            Transaction.Com.Transaction = Transaction.SqlTransaction;
            result = Convert.ToInt32(Transaction.Com.ExecuteScalar());

            if (IsCommit)
            {
                Transaction.SqlTransaction.Commit();
                Transaction.SqlTransaction.Dispose();
            }

            return result;
        }
        catch (Exception er)
        {
            Transaction.SqlTransaction.Rollback();
            IsCommit = true;
            ErrorLogsInsert(TableName + " DALC.InsertDatabase də xəta baş verdi: " + er.Message);
            return -1;
        }

        finally
        {
            if (IsCommit)
            {
                Transaction.Com.Connection.Close();
                Transaction.Com.Connection.Dispose();
                Transaction.Com.Dispose();
            }
        }
    }

    //Toplu insert
    public static int InsertBulk(string TableName, DataTable Dt)
    {
        StringBuilder Columns = new StringBuilder();
        StringBuilder Params = new StringBuilder();
        SqlCommand com = new SqlCommand();
        try
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Params.Append("(");
                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    if (i == 0)
                        Columns.Append(Dt.Columns[j].ColumnName + ",");

                    Params.Append("@P" + i.ToString() + j.ToString() + (j < Dt.Columns.Count - 1 ? "," : ""));
                    com.Parameters.AddWithValue("@P" + i.ToString() + j.ToString(), Dt.Rows[i][j]);
                }

                Params.Append("),");
            }
            com.Connection = SqlConn;
            com.CommandText = String.Format("Insert Into {0}({1}) Values{2}", TableName, Columns.ToString().Trim(','), Params.ToString().Trim(','));

            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert(TableName + " DALC.InsertBulk methodunda xeta bas verdi." + er.Message);
            return -1;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    #endregion

    #region Update
    /// <summary>
    /// Update Numunə: Dictionary.Add("FullName", "Tural Xasiyev");
    /// Where Numunə:  Dictionary.Add("WhereID", "1");
    /// </summary>
    public static int UpdateDatabase(string TableName, Dictionary<string, object> Dictionary)
    {
        SqlCommand com = new SqlCommand();
        StringBuilder Columns = new StringBuilder();
        StringBuilder Where = new StringBuilder();
        string WhereColumnsName = "";
        try
        {
            foreach (var Item in Dictionary)
            {
                if (!Item.Key.ToUpper().Contains("WHERE"))
                {
                    Columns.Append(Item.Key + "=@" + Item.Key + " ,");
                    com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
                }
                else
                {
                    WhereColumnsName = Item.Key.Substring(5);

                    if (Where.Length < 1)
                        Where.Append("1=1");

                    Where.Append(" and " + WhereColumnsName + "=@w" + WhereColumnsName);
                    com.Parameters.AddWithValue("@w" + WhereColumnsName, Item.Value);
                }
            }

            com.Connection = SqlConn;
            com.CommandText = String.Format("Update {0} SET {1} Where {2}", TableName, Columns.ToString().Trim(','), Where);
            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            ErrorLogsInsert(TableName + " DALC.UpdateDatabase də xəta baş verdi: " + er.Message);
            return -1;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    /// <summary>
    /// Nümunə: UpdateDatabase("Persons", new string[] { "Soyad", "Ad", "WhereID" }, new object[] { "Novruzov", "Emin", 1 })
    /// </summary>
    public static int UpdateDatabase(string TableName, string[] Key, object[] Values)
    {
        SqlCommand com = new SqlCommand();
        StringBuilder Columns = new StringBuilder();
        StringBuilder Where = new StringBuilder();
        string WhereColumnsName = "";
        try
        {
            for (int i = 0; i < Key.Length; i++)
            {
                if (!Key[i].ToUpper().Contains("WHERE"))
                {
                    Columns.Append(Key[i] + "=@" + Key[i] + " ,");
                    com.Parameters.AddWithValue("@" + Key[i], Values[i]);
                }
                else
                {
                    WhereColumnsName = Key[i].Substring(5);
                    if (Where.Length < 1)
                        Where.Append("1=1");

                    Where.Append(" and " + WhereColumnsName + "=@w" + WhereColumnsName);
                    com.Parameters.AddWithValue("@w" + WhereColumnsName, Values[i]);
                }
            }

            com.Connection = SqlConn;
            com.CommandText = String.Format("Update {0} SET {1} Where {2}", TableName, Columns.ToString().Trim(','), Where);

            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            ErrorLogsInsert(TableName + " DALC.UpdateDatabase də xəta baş verdi: " + er.Message);
            return -1;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    public static int UpdateDatabase(string TableName, Dictionary<string, object> Dictionary, Transaction Transaction, bool IsCommit = false)
    {
        int result = -1;
        StringBuilder Columns = new StringBuilder();
        StringBuilder Where = new StringBuilder();
        string WhereColumnsName = "";

        // Parametri təmizləyəkki ikinci dəfə eyni parametrlə insertə gəldikdə xəta verməsin
        Transaction.Com.Parameters.Clear();
        foreach (var Item in Dictionary)
        {
            if (!Item.Key.ToUpper().Contains("WHERE"))
            {
                Columns.Append(Item.Key + "=@" + Item.Key + " ,");
                Transaction.Com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
            }
            else
            {
                WhereColumnsName = Item.Key.Substring(5);
                if (Where.Length < 1)
                    Where.Append("1=1");

                Where.Append(" and " + WhereColumnsName + "=@w" + WhereColumnsName);
                Transaction.Com.Parameters.AddWithValue("@w" + WhereColumnsName, Item.Value);
            }
        }

        try
        {
            if (Transaction.SqlTransaction == null)
            {
                Transaction.Com.Connection = DALC.SqlConn;
                Transaction.Com.Connection.Open();
                Transaction.SqlTransaction = Transaction.Com.Connection.BeginTransaction();
            }

            Transaction.Com.CommandText = String.Format("Update {0} SET {1} Where {2}", TableName, Columns.ToString().Trim(','), Where);
            Transaction.Com.Transaction = Transaction.SqlTransaction;
            result = Transaction.Com.ExecuteNonQuery();
            if (IsCommit)
                Transaction.SqlTransaction.Commit();
            return result;
        }
        catch (Exception er)
        {
            Transaction.SqlTransaction.Rollback();
            IsCommit = true;
            ErrorLogsInsert(TableName + " DALC.UpdateDatabase də xəta baş verdi: " + er.Message);
            return -1;
        }
        finally
        {
            if (IsCommit)
            {
                Transaction.Com.Connection.Close();
                Transaction.Com.Connection.Dispose();
                Transaction.Com.Dispose();
            }
        }
    }
    #endregion

    #endregion

    public static DataTable GetRegionalCenters()
    {
        return GetDataTable("*", "RegionalCenters");
    }

    public static DataTableResult GetOrganizationsList(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20)
    {
        DataTableResult OrganizationsList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";

        // Lang-i deqiqileshdirmek lazimdir tesdiq gozleyen xeberlerde dilinden asili olmayraq butun xeberler gelecekmi
        StringBuilder AddWhere = new StringBuilder("Where ParentID!=0");

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and (" + Key.Replace("(BETWEEN)", "") + " Between @PDt1" + i.ToString() + " and @PDt2" + i.ToString() + ")");
                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and " + Key.Replace("(LIKE)", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} in(Select item from SplitString(@wIn{0},','))", Item.Key.Replace("(IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(IN)", ""), Item.Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() over(Order By ID desc) as RowIndex,*
                                From(Select * From Organizations) as O {1} ) as O {2}";

        com.Connection = SqlConn;

        string CacheName = "OrganizationsList";

        if (HttpContext.Current.Cache[CacheName] == null)
        {
            com.CommandText = string.Format(QueryCommand, "COUNT(O.ID)", AddWhere, "");
            try
            {
                com.Connection.Open();
                OrganizationsList.Count = com.ExecuteScalar()._ToInt32();
                HttpContext.Current.Cache[CacheName] = OrganizationsList.Count;
            }
            catch (Exception er)
            {
                ErrorLogsInsert(string.Format("DALC.Organizations count xəta: {0}", er.Message));
                OrganizationsList.Count = -1;
                OrganizationsList.Dt = null;
                return OrganizationsList;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            OrganizationsList.Count = HttpContext.Current.Cache[CacheName]._ToInt32();
        }

        string RowIndexWhere = " Where O.RowIndex BETWEEN @R1 AND @R2";
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "O.*", AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(OrganizationsList.Dt);
            return OrganizationsList;
        }
        catch (Exception er)
        {
            ErrorLogsInsert(string.Format("DALC.Organizations xəta: {0}", er.Message));
            OrganizationsList.Count = -1;
            OrganizationsList.Dt = null;
            return OrganizationsList;
        }
    }

    public static DataTable GetOrganizationTop()
    {
        return GetDataTable("*", "Organizations", "Where ParentID=0");
    }

    public static DataTable GetOrganizationSub(string ParentID)
    {
        return GetDataTableBySqlCommand(@"Select * From Organizations 
                                        Where (ParentID=@ParentID or @ParentID=-1) and ParentID!=0",
                                        "ParentID", new object[] { int.Parse(ParentID) });
    }

    public static DataTable GetOrganizationById(string id)
    {
        return GetDataTable("*", "Organizations", "ID", id);
    }

    public static int InsertOrganization(string Name, string ParentId, bool deleted)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("Name", Name);
        parameters.Add("IsDeleted", deleted);
        if (ParentId != "-1")
        {
            parameters.Add("ParentID", ParentId);
        }

        return InsertDatabase("Organizations", parameters);
    }

    public static int UpdateOrganization(string OrganizationId, string Name, string ParentId, bool deleted)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("WhereId", OrganizationId);
        parameters.Add("Name", Name);
        parameters.Add("IsDeleted", deleted);
        if (ParentId != "-1")
        {
            parameters.Add("ParentID", ParentId);
        }
        return UpdateDatabase("Organizations", parameters);
    }

    public static int DeactivateOrganization(string OrganizationsId)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("WhereId", OrganizationsId);
        parameters.Add("IsDeleted", true);
        return UpdateDatabase("Organizations", parameters);
    }

    public static DataTable GetCallCenterTypes()
    {
        return GetDataTable("*", "CallCenterType");
    }

    public static DataTable GetVisitTypes()
    {
        return GetDataTable("*", "VisitType");
    }

    public static DataTable GetUsers()
    {
        return GetDataTable("*", "Users");
    }

    public static DataTable GetUsersStatus()
    {
        return GetDataTable("*", "UsersStatus");
    }

    public static DataTableResult GetAuditsOrganizations(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20, object[] OtherParams = null, string OrderByType = "desc")
    {
        DataTableResult ApplicationsList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";

        // Lang-i deqiqileshdirmek lazimdir tesdiq gozleyen xeberlerde dilinden asili olmayraq butun xeberler gelecekmi
        StringBuilder AddWhere = new StringBuilder("Where 1=1");

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and (" + Key.Replace("(BETWEEN)", "") + " Between @PDt1" + i.ToString() + " and @PDt2" + i.ToString() + ")");
                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and " + Key.Replace("(LIKE)", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} in(Select item from SplitString(@wIn{0},','))", Item.Key.Replace("(IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(IN)", ""), Item.Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() over(Order By Visit_Dt {1}) as RowIndex,*
                                From(Select a.ID, (Select r.Name from RegionalCenters r where r.ID = a.RegionalCentersID) as RegionalCenter,a.RegionalCentersID,
                                (select o.ParentID from Organizations o where o.ID=a.OrganizationsID) as ParentOrganizationsID,
                                (select o.Name from Organizations o where o.ID=a.OrganizationsID) as Organization,a.OrganizationsID,
                                (select v.Name from VisitType v where v.ID = a.VisitTypeID) as VisitType,
                                (select count(*) from AuditsOrganizationsMeetingPersons mp where mp.AuditsOrganizationsID=a.ID) as MeetingPersonCount,
                                a.VisitTypeID, a.Visit_Dt,Descriptions from AuditsOrganizations a) as AF {2} ) as AF {3}";

        com.Connection = SqlConn;

        string CacheName = "AuditsOrganizations";

        if (HttpContext.Current.Cache[CacheName] == null)
        {
            com.CommandText = string.Format(QueryCommand, "COUNT(AF.ID)", OrderByType, AddWhere, "");
            try
            {
                com.Connection.Open();
                ApplicationsList.Count = com.ExecuteScalar()._ToInt32();
                HttpContext.Current.Cache[CacheName] = ApplicationsList.Count;
            }
            catch (Exception er)
            {
                ErrorLogsInsert(string.Format("DALC.GetAuditsOrganizations count xəta: {0}", er.Message));
                ApplicationsList.Count = -1;
                ApplicationsList.Dt = null;
                return ApplicationsList;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            ApplicationsList.Count = HttpContext.Current.Cache[CacheName]._ToInt32();
        }

        string RowIndexWhere = " Where (AF.RowIndex BETWEEN @R1 AND @R2) and (AF.RowIndex=@RowIndex or @RowIndex=-1)";
        com.Parameters.Add("@RowIndex", SqlDbType.Int).Value = OtherParams[0];
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "AF.*", OrderByType, AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(ApplicationsList.Dt);
            return ApplicationsList;
        }
        catch (Exception er)
        {
            ErrorLogsInsert(string.Format("DALC.GetAuditsOrganizations xəta: {0}", er.Message));
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
    }

    public static DataTable GetAuditsOrganizationsById(string id)
    {
        string query = @"Select a.ID, (Select r.Name from RegionalCenters r where r.ID = a.RegionalCentersID) as RegionalCenter,
                         (select o.ParentID from Organizations o where o.ID=a.OrganizationsID) as TopOrganizationID,
                         (select o.Name from Organizations o where o.ID=a.OrganizationsID) as Organization,
                         (select v.Name from VisitType v where v.ID = a.VisitTypeID) as VisitType,a.CreatedUsersID,a.RegionalCentersID,
                         a.OrganizationsID,a.VisitTypeID,a.Problems,a.Suggestions,a.Descriptions , a.Visit_Dt from AuditsOrganizations a where a.ID=@ID";
        return GetDataTableBySqlCommand(query, "ID", new object[] { id });
    }

    public static DataTable GetAuditsOrganizationsMeetingPersonsByID(int PersonsID)
    {
        return GetDataTable("*", "AuditsOrganizationsMeetingPersons", "ID", PersonsID);
    }

    public static DataTable GetGenderType()
    {
        return GetDataTable("*", "GenderType", "Order By ID asc");
    }

    public static DataTable GetSocialStaus()
    {
        return GetDataTable("*", "SocialStatus", "Order By ID asc");
    }

    public static DataTable GetComplaintType()
    {
        return GetDataTable("*", "ComplaintType", "Order By ID asc");
    }

    public static DataTable GetComplaintResultType()
    {
        return GetDataTable("*", "ComplaintResultType", "Order By ID asc");
    }

    public static int InsertAuditsOrganizations(int UsersID, string RegionalCentersID, string OrganizationsID,
                                                string VisitTypeID, string Problems, string Suggestions, string Descriptions, DateTime VisitDt)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("CreatedUsersID", UsersID);
        parameters.Add("RegionalCentersID", RegionalCentersID);
        parameters.Add("OrganizationsID", OrganizationsID);
        parameters.Add("VisitTypeID", VisitTypeID);
        parameters.Add("Problems", Problems);
        parameters.Add("Suggestions", Suggestions);
        parameters.Add("Descriptions", Suggestions);
        parameters.Add("Visit_Dt", VisitDt);
        parameters.Add("Add_Dt", DateTime.Now);
        parameters.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());
        return InsertDatabase("AuditsOrganizations", parameters);
    }

    public static int UpdateAuditOrganizations(string AuditsOrganizationsID, string RegionalCentersID, string OrganizationsID,
                                                string VisitTypeID, string Problems, string Suggestions, string Descriptions, DateTime VisitDt)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("WhereId", AuditsOrganizationsID);
        parameters.Add("RegionalCentersID", RegionalCentersID);
        parameters.Add("OrganizationsID", OrganizationsID);
        parameters.Add("VisitTypeID", VisitTypeID);
        parameters.Add("Problems", Problems);
        parameters.Add("Suggestions", Suggestions);
        parameters.Add("Descriptions", Descriptions);
        parameters.Add("Visit_Dt", VisitDt);
        return UpdateDatabase("AuditsOrganizations", parameters);
    }

    public static bool DeleteAuditsOrganizationsUsers(int AuditsOrganizationsID)
    {
        //Bu metodu update zamanida islede bilmek ucun insert etmemiseden qabag bu AuditsOrganizationsID-de olan butun melumatlari silek
        SqlCommand cmd = new SqlCommand("delete from AuditsOrganizationsUsers where AuditsOrganizationsID=@AuditsOrganizationsID", SqlConn);
        try
        {
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("AuditsOrganizationsID", AuditsOrganizationsID);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static int InsertAuditsOrganizationsUsers(int AuditsOrganizationsID, int UsersID)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("AuditsOrganizationsID", AuditsOrganizationsID);
        parameters.Add("UsersID", UsersID);
        parameters.Add("Add_Dt", DateTime.Now);
        parameters.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());
        return InsertDatabase("AuditsOrganizationsUsers", parameters);
    }

    public static DataTable GetUsersForAuditOrganizations(int AuditsOrganizationsID)
    {
        return GetDataTable("UsersID", "AuditsOrganizationsUsers", "AuditsOrganizationsID", AuditsOrganizationsID);
    }

    public static DataTableResult GetIndividualComplaints(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20, object[] OtherParams = null, string OrderByType = "desc")
    {
        DataTableResult ApplicationsList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";

        // Lang-i deqiqileshdirmek lazimdir tesdiq gozleyen xeberlerde dilinden asili olmayraq butun xeberler gelecekmi
        StringBuilder AddWhere = new StringBuilder("Where 1=1");

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and (" + Key.Replace("(BETWEEN)", "") + " Between @PDt1" + i.ToString() + " and @PDt2" + i.ToString() + ")");
                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and " + Key.Replace("(LIKE)", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} in(Select item from SplitString(@wIn{0},','))", Item.Key.Replace("(IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(IN)", ""), Item.Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() over(Order By EnterOrganizations_Dt {1}) as RowIndex,
                                 *
                                 From(Select IC.*,
                                     CT.Name as  ComplaintTypeName,
                                     GT.Name as ApplicantsGenderTypeName,
                                     SS.Name as ApplicantsSocialStatusName,
                                     (Select U.Fullname from Users U where U.ID = IC.ExecutiveUsersID) as ExecutiveUsersName,
                                     CRT.Name as ComplaintResultTypeName
                                     From IndividualComplaints IC Left Join 
                                     ComplaintType as CT on IC.ComplaintTypeID=CT.ID Left Join
                                     GenderType as GT on IC.GenderTypeID=GT.ID Left Join
                                     SocialStatus as SS on IC.SocialStatusID=SS.ID Left Join 
									 ComplaintResultType as CRT on IC.ComplaintResultTypeID=CRT.ID) as AF {2} ) as AF {3}";

        com.Connection = SqlConn;

        string CacheName = "IndividualComplaints";

        if (HttpContext.Current.Cache[CacheName] == null)
        {
            com.CommandText = string.Format(QueryCommand, "COUNT(AF.ID)", OrderByType, AddWhere, "");
            try
            {
                com.Connection.Open();
                ApplicationsList.Count = com.ExecuteScalar()._ToInt32();
                HttpContext.Current.Cache[CacheName] = ApplicationsList.Count;
            }
            catch (Exception er)
            {
                ErrorLogsInsert(string.Format("DALC.GetIndividualComplaints count xəta: {0}", er.Message));
                ApplicationsList.Count = -1;
                ApplicationsList.Dt = null;
                return ApplicationsList;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            ApplicationsList.Count = HttpContext.Current.Cache[CacheName]._ToInt32();
        }

        string RowIndexWhere = " Where (AF.RowIndex BETWEEN @R1 AND @R2) and (AF.RowIndex=@RowIndex or @RowIndex=-1)";
        com.Parameters.Add("@RowIndex", SqlDbType.Int).Value = OtherParams[0];
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "AF.*", OrderByType, AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(ApplicationsList.Dt);
            return ApplicationsList;
        }
        catch (Exception er)
        {
            ErrorLogsInsert(string.Format("DALC.GetApplicationsFamily xəta: {0}", er.Message));
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
    }

    public static DataTable GetIndividualComplaintsById(string id)
    {
        return GetDataTableBySqlCommand(@"Select IC.*,
                                             CT.Name as ComplaintTypeName,
                                             GT.Name as ApplicantsGenderTypeName,
                                             SS.Name as ApplicantsSocialStatusName,
                                             (Select U.Fullname from Users U where U.ID = IC.ExecutiveUsersID) as ExecutiveUsersName
                                             From IndividualComplaints IC Left Join
                                             ComplaintType as CT on IC.ComplaintTypeID = CT.ID Left Join
                                             GenderType as GT on IC.GenderTypeID = GT.ID Left Join
                                             SocialStatus as SS on IC.SocialStatusID = SS.ID where IC.ID = @ID", "ID", new object[] { id });
    }

    public static DataTable GetCallCenterType()
    {
        return GetDataTable("*", "CallCenterType");
    }

    public static DataTableResult GetCallCenter(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20, object[] OtherParams = null, string OrderByType = "desc")
    {
        DataTableResult ApplicationsList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";

        // Lang-i deqiqileshdirmek lazimdir tesdiq gozleyen xeberlerde dilinden asili olmayraq butun xeberler gelecekmi
        StringBuilder AddWhere = new StringBuilder("Where 1=1");

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and (" + Key.Replace("(BETWEEN)", "") + " Between @PDt1" + i.ToString() + " and @PDt2" + i.ToString() + ")");
                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and " + Key.Replace("(LIKE)", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} in(Select item from SplitString(@wIn{0},','))", Item.Key.Replace("(IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(IN)", ""), Item.Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() over(Order By Call_Dt {1}) as RowIndex,
                                *
                                From(
                                Select c.ID,
                                c.UsersID,
                                c.CallCenterTypeID,
                                (Select ct.Name From CallCenterType ct where ct.ID = c.CallCenterTypeID) as CallCenterTypeName,
                                c.ComplaintTypeID,
                                CT.Name as ComplaintTypeName,
                                c.ApplicantFullname,  
                                c.GenderTypeID,
                                GT.Name as ApplicantGenderTypeName,
                                c.SocialStatusID,
                                SS.Name as ApplicantSocialStatusName,
                                C.PhoneNumber, 
                                C.VictimsFullname, 
                                C.ComplaintInstitution, 
                                C.StartPunishmentPeriod,
                                C.EndPunishmentPeriod, 
                                C.ComplaintResultTypeID,
                                CRT.Name as ComplaintResultTypeName,
                                c.Results, 
                                c.Descriptions, 
                                c.Call_Dt 
                                From CallCenter C Left Join 
                                ComplaintType as CT on C.ComplaintTypeID=CT.ID Left Join
                                GenderType as GT on C.GenderTypeID=GT.ID Left Join 
                                SocialStatus as SS on C.SocialStatusID=SS.ID Left Join
                                ComplaintResultType as CRT on C.ComplaintResultTypeID=CRT.ID
                                ) as AF {2} ) as AF {3}";

        com.Connection = SqlConn;

        string CacheName = "CallCenter";

        if (HttpContext.Current.Cache[CacheName] == null)
        {
            com.CommandText = string.Format(QueryCommand, "COUNT(AF.ID)", OrderByType, AddWhere, "");
            try
            {
                com.Connection.Open();
                ApplicationsList.Count = com.ExecuteScalar()._ToInt32();
                HttpContext.Current.Cache[CacheName] = ApplicationsList.Count;
            }
            catch (Exception er)
            {
                ErrorLogsInsert(string.Format("DALC.GetCallCenter count xəta: {0}", er.Message));
                ApplicationsList.Count = -1;
                ApplicationsList.Dt = null;
                return ApplicationsList;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            ApplicationsList.Count = HttpContext.Current.Cache[CacheName]._ToInt32();
        }
        string RowIndexWhere = " Where (AF.RowIndex BETWEEN @R1 AND @R2) and (AF.RowIndex=@RowIndex or @RowIndex=-1)";
        com.Parameters.Add("@RowIndex", SqlDbType.Int).Value = OtherParams[0];
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "AF.*", OrderByType, AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(ApplicationsList.Dt);
            return ApplicationsList;
        }
        catch (Exception er)
        {
            ErrorLogsInsert(string.Format("DALC.GetCallCenter xəta: {0}", er.Message));
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
    }

    public static DataTable GetCallCenterById(string id)
    {
        string query = @"Select c.ID,c.UsersID ,c.CallCenterTypeID, (Select ct.Name From CallCenterType ct where ct.ID = c.CallCenterTypeID) as CallCenterTypeName,
                         c.ComplaintTypeID,CT.Name as ComplaintTypeName,c.ApplicantFullname,c.GenderTypeID,GT.Name as ApplicantGenderTypeName,c.SocialStatusID,SS.Name as ApplicantSocialStatusName,c.PhoneNumber, c.VictimsFullname, c.ComplaintInstitution, c.StartPunishmentPeriod,
                         C.ComplaintResultTypeID,
                         CRT.Name as ComplaintResultTypeName,
                         c.EndPunishmentPeriod, c.Results, c.Descriptions, c.Call_Dt 
                         from CallCenter c Left Join 
                         ComplaintType as CT on C.ComplaintTypeID=CT.ID Left Join
                         GenderType as GT on C.GenderTypeID=GT.ID Left join 
                         SocialStatus as SS on C.SocialStatusID=SS.ID Left Join 
						 ComplaintResultType as CRT on C.ComplaintResultTypeID=CRT.ID
                         where c.ID = @ID";
        return GetDataTableBySqlCommand(query, "ID", new object[] { id });
    }

    public static DataTable GetOmbudsmanUsers(string Fullname)
    {

        string query = @"Select u.ID, (Select rc.Name from RegionalCenters rc where rc.ID = u.RegionalCentersID) as RegionalCenterName,u.RegionalCentersID, 
                         u.Username , u.Fullname, u.Contacts, u.Positions, (Select s.Name from UsersStatus s where s.ID = u.UsersStatusID) as UserStatusName,
                         u.UsersStatusID from Users u where u.IsActive=1 and Fullname Like '%' + @Fullname + '%' Order by u.ID desc";
        return GetDataTableBySqlCommand(query, "Fullname", new object[] { Fullname });
    }

    public static DataTable GetOmbudsmanUsersById(string id)
    {
        string query = @"Select u.ID, (Select rc.Name from RegionalCenters rc where rc.ID = u.RegionalCentersID) as RegionalCenterName,u.RegionalCentersID, 
                         u.Username , u.Fullname, u.Contacts, u.Positions,(Select s.Name from UsersStatus s where s.ID = u.UsersStatusID) as UserStatusName,
                         u.UsersStatusID,u.Descriptions from Users u where u.IsActive=1 and u.ID=@ID";
        return GetDataTableBySqlCommand(query, "ID", new object[] { id });
    }

    public static int InsertUsers(string RegionalCentersId, string Username, string Password, string Fullname, string Contacts,
                                  string Positions, string Descriptions, string UsersStatusID)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("RegionalCentersId", RegionalCentersId);
        if (UsersStatusID != "10")
        {
            parameters.Add("Username", Username);
            parameters.Add("Password", Password.Sha1());
        }
        parameters.Add("Fullname", Fullname);
        parameters.Add("Contacts", Contacts);
        parameters.Add("Positions", Positions);
        parameters.Add("Descriptions", Descriptions);
        parameters.Add("UsersStatusID", UsersStatusID);
        parameters.Add("IsActive", 1);
        parameters.Add("Add_Dt", DateTime.Now);
        parameters.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());
        return InsertDatabase("Users", parameters);
    }

    public static int UpdateUsers(string UsersId, string RegionalCentersId, string Password, string Fullname, string Contacts,
                                  string Positions, string Descriptions, string UsersStatusID)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("WhereId", UsersId);
        parameters.Add("RegionalCentersId", RegionalCentersId);
        if (UsersStatusID != "10")
        {
            if (!string.IsNullOrEmpty(Password.Trim()))
            {
                parameters.Add("Password", Password.Sha1());
            }
        }
        parameters.Add("Fullname", Fullname);
        parameters.Add("Contacts", Contacts);
        parameters.Add("Positions", Positions);
        parameters.Add("Descriptions", Descriptions);
        parameters.Add("UsersStatusID", UsersStatusID);
        return UpdateDatabase("Users", parameters);
    }

    public static int DeactivateUsers(string UsersId)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("WhereId", UsersId);
        parameters.Add("IsActive", false);
        return UpdateDatabase("Users", parameters);
    }

    public static int UpdateUserDescription(int UsersId, string Descriptions)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("WhereId", UsersId);
        parameters.Add("Descriptions", Descriptions);
        return UpdateDatabase("Users", parameters);
    }

    public static string GetUsersDescription(int UsersId)
    {
        return GetSingleValues("Descriptions", "Users", "ID", UsersId);
    }

    public static DataTableResult GetMeetingPersons(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20, object[] OtherParams = null, string OrderByType = "desc")
    {
        DataTableResult ApplicationsList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";

        // Lang-i deqiqileshdirmek lazimdir tesdiq gozleyen xeberlerde dilinden asili olmayraq butun xeberler gelecekmi
        StringBuilder AddWhere = new StringBuilder("Where 1=1");

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and (" + Key.Replace("(BETWEEN)", "") + " Between @PDt1" + i.ToString() + " and @PDt2" + i.ToString() + ")");
                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and " + Key.Replace("(LIKE)", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} in(Select item from SplitString(@wIn{0},','))", Item.Key.Replace("(IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(IN)", ""), Item.Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() Over(Order By Meeting_Dt {1}) as RowIndex,
                                 *
                                 From( 
                                        Select MP.*, 
                                        GT.Name as GenderName,
                                        SS.Name as SocialStatusName,
                                        CRT.Name as ComplaintResultTypeName,
                                        (Select o.ParentID from Organizations o where o.ID=a.OrganizationsID) as ParentOrganizationsID,
                                        A.OrganizationsID,O.Name as OrganizationsName 
                                        From 
                                        AuditsOrganizationsMeetingPersons MP  inner join                                
                                        AuditsOrganizations A  on MP.AuditsOrganizationsID=A.ID inner join 
                                        Organizations O on o.ID= A.OrganizationsID Left Join
                                        GenderType as GT on MP.GenderTypeID=GT.ID Left Join
                                        SocialStatus as SS on MP.SocialStatusID=SS.ID Left Join
                                        ComplaintResultType as CRT on MP.ComplaintResultTypeID=CRT.ID
                                        Where MP.IsDeleted=0) as AF {2} ) as AF {3}";

        com.Connection = SqlConn;

        com.CommandText = string.Format(QueryCommand, "COUNT(AF.ID)", OrderByType, AddWhere, "");
        try
        {
            com.Connection.Open();
            ApplicationsList.Count = com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            ErrorLogsInsert(string.Format("DALC.GetAuditsOrganizationsMeetingPersons count xəta: {0}", er.Message));
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
        finally
        {
            com.Connection.Close();
        }


        string RowIndexWhere = " Where (AF.RowIndex BETWEEN @R1 AND @R2) and (AF.RowIndex=@RowIndex or @RowIndex=-1)";
        com.Parameters.Add("@RowIndex", SqlDbType.Int).Value = OtherParams[0];
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "AF.*", OrderByType, AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(ApplicationsList.Dt);
            return ApplicationsList;
        }
        catch (Exception er)
        {
            ErrorLogsInsert(string.Format("DALC.GetAuditsOrganizationsMeetingPersons xəta: {0}", er.Message));
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
    }

    //Error log insert
    public static void ErrorLogsInsert(string LogText, bool IsSendMail = false)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("LogText", Config.SizeLimit(LogText, 990, "..."));
        Dictionary.Add("URL", Config.SizeLimit(HttpContext.Current.Request.Url.ToString(), 90, "..."));
        Dictionary.Add("Add_Dt", DateTime.Now);
        Dictionary.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        InsertDatabase("ErrorLogs", Dictionary, false);

        //Adminə həmdə mail göndərmək lazım olduqda getsin.
        if (IsSendMail)
        {
            DALC.SendAdminMail("OMBUDSMAN", LogText);
        }
    }

    //Send admin mail sender
    public static void SendAdminMail(string Title, string Messages)
    {
        DALC.SendMail(Config._GetAppSettings("ErrorMailList"), Title, Messages + " Ip: " + HttpContext.Current.Request.UserHostAddress + " DateTime: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " Url: " + HttpContext.Current.Request.Url.ToString());
    }

    //Mail sender.
    public static bool SendMail(string MailList, string Title, string Messages)
    {
        try
        {
            return true;
            //Mail gonder
            MailMessage MailServer = new MailMessage(Config._GetAppSettings("MailLogin"), MailList, Title, Messages);

            SmtpClient SmtpMail = new SmtpClient(Config._GetAppSettings("MailServer"));
            SmtpMail.Credentials = new NetworkCredential(Config._GetAppSettings("MailLogin"), Config._GetAppSettings("MailPassword").Decrypt());
            MailServer.BodyEncoding = System.Text.Encoding.UTF8;
            MailServer.Priority = System.Net.Mail.MailPriority.Normal;
            MailServer.IsBodyHtml = true;

            SmtpMail.Send(MailServer);
            return true;
        }
        catch (Exception er)
        {
            //Əgər log da error veribsə mail göndərirsə təkrar log metoduna qaytarmıyaq.
            if (Messages.IndexOf("[ErrorLogsInsert]") < 0)
                DALC.ErrorLogsInsert("DALC.SendMail catch error: " + er.Message);

            return false;
        }
    }
}
