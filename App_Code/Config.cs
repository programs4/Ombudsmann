using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

public static class Config
{

    public static string _DefaultErrorMessages
    {
        get
        {
            return "Sistemdə xəta yarandı.";
        }
    }

    public static string _DefaultSuccessMessages
    {
        get
        {
            return "Əməliyyat uğurla yerinə yetirildi.";
        }
    }

    //Get WebConfig.config App Key
    public static string _Route(string KeyName, string CatchValue = "")
    {
        try
        {
            Page P = (Page)HttpContext.Current.Handler;
            return P.RouteData.Values[KeyName].ToString().ToLower();
        }
        catch
        {
            return CatchValue;
        }
    }


    //Səhifəni yönləndirək:
    public static void RedirectURL(string GetUrl)
    {
        HttpContext.Current.Response.Redirect(GetUrl, false);
        HttpContext.Current.Response.End();
    }

    public static void RedirectLogin()
    {
        HttpContext.Current.Response.Redirect("~/", false);
        HttpContext.Current.Response.End();
    }

    public static void RedirectError()
    {
        HttpContext.Current.Response.Redirect("~/error", false);
        HttpContext.Current.Response.End();
    }

    public static string GetExtension(this object Path)
    {
        return System.IO.Path.GetExtension(Path._ToString()).Trim('.').ToLower();
    }

    //Get WebConfig.config App Key
    public static string _GetAppSettings(string KeyName)
    {
        return ConfigurationManager.AppSettings[KeyName];
    }

    //Get Querystring
    public static string _GetQueryString(string KeyName)
    {
        return HttpContext.Current.Request.QueryString[KeyName]._ToString();
    }

    //ConvertString.
    public static string _ToString(this object Value)
    {
        return Convert.ToString(Value);
    }

    //ConvertString.
    public static int _ToInt16(this object Value)
    {
        return Convert.ToInt16(Value);
    }

    //ConvertString.
    public static int _ToInt32(this object Value)
    {
        return Convert.ToInt32(Value);
    }

    public static double _ToDouble(this object Value)
    {
        try
        {
            return double.Parse(Value._ToString().Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
        }
        catch { return 0; }
    }

    public static string VideoUrlPath(this string VideoUrl)
    {
        try
        {
            VideoUrl = VideoUrl.Remove(0, VideoUrl.IndexOf("v=") + 2);
            if (VideoUrl.IndexOf('&') > -1)
                VideoUrl = VideoUrl.Remove(VideoUrl.IndexOf('&'));

            return VideoUrl;
        }
        catch
        {
            return "";
        }
    }

    //Email validator
    public static bool IsEmail(this string Mail)
    {
        const string StrRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        @".)+))([a-zA-Z]{2,6}|[0-9]{1,3})(\]?)$";

        return (new System.Text.RegularExpressions.Regex(StrRegex)).IsMatch(Mail);
    }

    public static string DateTimeClear(string Data, string ReplaceChar)
    {
        Data = Data.Replace(" ", ReplaceChar);
        Data = Data.Replace(",", ReplaceChar);
        Data = Data.Replace("\"", ReplaceChar);
        Data = Data.Replace("/", ReplaceChar);
        Data = Data.Replace("+", ReplaceChar);
        Data = Data.Replace("-", ReplaceChar);
        Data = Data.Replace("$", ReplaceChar);
        Data = Data.Replace("#", ReplaceChar);
        Data = Data.Replace("=", ReplaceChar);
        Data = Data.Replace("*", ReplaceChar);
        Data = Data.Replace(":", ReplaceChar);
        Data = Data.Replace(".", ReplaceChar);
        return Data;
    }

    //Tarix təmizləyən
    public static object DateTimeFormat(this string Date)
    {
        //Clear
        Date = Date.Trim();
        Date = Date.Replace(",", ".");
        Date = Date.Replace("+", ".");
        Date = Date.Replace("/", ".");
        Date = Date.Replace("-", ".");
        Date = Date.Replace("*", ".");
        Date = Date.Replace("\\", ".");
        Date = Date.Replace(" ", ".");

        if (!IsNumeric(Date.Replace(".", "")))
            return null;

        string[] DtSplit = Date.Split('.');

        if (DtSplit.Length != 3)
            return null;

        //Əgər 2050 keçərsə 1900 yazaq.
        try
        {
            //İli 2 simvol olsa yanına 20 artıq. 3 minici ilə qədər gedər :)
            if (DtSplit[2].Length == 2)
                DtSplit[2] = "20" + DtSplit[2];

            if (DtSplit[2].Length == 1)
                DtSplit[2] = "200" + DtSplit[2];

            if (DtSplit[2]._ToInt16() > 2050)
                DtSplit[2] = (DtSplit[2]._ToInt16() - 100).ToString();
        }
        catch
        {
        }

        try
        {
            DateTime Dt = new DateTime(
                int.Parse(DtSplit[2]),
                int.Parse(DtSplit[1]),
                int.Parse(DtSplit[0])
                );

            return Dt;
        }
        catch
        {
            return null;
        }
    }

    //Get WebConfig.config App Key
    public static string Split(string Value, char Char, int Index, string CatchValue = "0")
    {
        try
        {
            return Value.Split(Char)[Index];
        }
        catch
        {
            return CatchValue;
        }
    }

    //Boşluğa görə type 1 sol tərəfi 2 sağ tərəfi. Tarix və saat 
    public static string SplitDateTime(string Value, int Type, string CatchValue = "")
    {
        try
        {
            if (Type == 1)
                return Value.Remove(Value.IndexOf(' ')).Trim();
            else
                return Value.Remove(0, Value.IndexOf(' ')).Trim();
        }
        catch
        {
            return CatchValue;
        }
    }

    //Açar yaradaq.
    public static string Key(int say)
    {
        Random Rnd = new Random();
        string Bind = "aqwertyuipasdfghjkzxcvbnmQAZWSXEDCRFVTGBYHNUJMKP23456789";
        string Key = "";
        for (int i = 1; i <= say; i++)
        {
            Key += Bind.Substring(Rnd.Next(Bind.Length - 1), 1);
        }
        return Key.Trim();
    }

    public static string ClearChar13(this string Text)
    {
        Text = Text.Trim();
        Text = Text.Replace("\n", " ");
        Text = Text.Replace(((char)13).ToString(), " ");
        return Text;
    }

    //Cümlə çox uzun olanda üç nöqtə qoyaq.
    public static string SizeLimit(object Text, int Length, string More)
    {
        if (Text._ToString().Length > Length)
            Text = Text._ToString().Substring(0, Length) + More;
        return Text._ToString();
    }

    //Numaric testi.
    public static bool IsNumeric(this object Value)
    {
        if (string.IsNullOrEmpty(Value._ToString()))
            return false;

        for (int i = 0; i < Value._ToString().Length; i++)
        {
            if ("0123456789".IndexOf(Value._ToString().Substring(i, 1)) < 0)
            {
                return false;
            }
        }
        return true;
    }

    //Ajax error message
    public static void MsgBoxAjax(string Text, bool isSuccess = false)
    {
        Page P = (Page)HttpContext.Current.Handler;
        ScriptManager.RegisterStartupScript(P, P.Page.GetType(), "PopupScript", "window.focus(); alertPopup('" + (isSuccess ? "Success" : "Error") + "','" + Text + "');", true);
    }

    //Sha1  - özəl
    public static string Sha1(this string Password)
    {
        byte[] result;
        System.Security.Cryptography.SHA1 ShaEncrp = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        Password = String.Format("{0}{1}{0}", "CSAASADM", Password);
        byte[] buffer = new byte[Password.Length];
        buffer = System.Text.Encoding.UTF8.GetBytes(Password);
        result = ShaEncrp.ComputeHash(buffer);
        return Convert.ToBase64String(result);
    }

    public static string SubString(object Value, int Start, int Length, string CatchValue = "-1")
    {
        try
        {
            return Value._ToString().Substring(Start, Length);
        }
        catch
        {
            return CatchValue;
        }
    }

    public static string IntDateDay(this object Value)
    {
        return SubString(Value, 6, 2, "00");
    }
    public static string IntDateMonth(this object Value)
    {
        return SubString(Value, 4, 2, "00");
    }
    public static string IntDateYear(this object Value)
    {
        return SubString(Value, 0, 4, "1000");
    }

    //Səhifəni yönləndirək:
    public static void Rd(string GetUrl)
    {
        HttpContext.Current.Response.Redirect(GetUrl, true);
        HttpContext.Current.Response.End();
    }


    //Tarixləri dolduraq:
    public static System.Data.DataTable NumericList(int From, int To, string BlankInsert = "0")
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("Name", typeof(string));

        if (BlankInsert.Length > 0)
            dt.Rows.Add(BlankInsert, "--");

        for (int i = From; i <= To; i++)
        {
            dt.Rows.Add((i < 10 ? "0" + i.ToString() : i.ToString()), (i < 10 ? "0" + i.ToString() : i.ToString()));
        }
        return dt;
    }

    public static string _Rows(this System.Data.DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt.Rows == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogsInsert("Rows null or count error.  ColumnsName: " + ColumnsName);
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return Dt.Rows[RowsIndex][ColumnsName]._ToString();
        }
    }

    public static object _RowsObject(this System.Data.DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt.Rows == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogsInsert("Rows null or count error.  ColumnsName: " + ColumnsName);
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return Dt.Rows[RowsIndex][ColumnsName];
        }
    }

    //Bazaya Null insert edek
    //EmtpyChar hərhansı char string gələ bilər.
    public static object NullConvert(this object Value, bool IsReturnDbNull = true)
    {
        try
        {
            if (Value == null || Value._ToString() == "")
            {
                if (IsReturnDbNull)
                {
                    return DBNull.Value;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return Value;
            }
        }
        catch
        {
            if (IsReturnDbNull)
            {
                return DBNull.Value;
            }
            else
            {
                return "";
            }
        }
    }

    public static object NullConvert(this object Value, object ReturnValue)
    {
        try
        {
            if (Value == null || Value._ToString() == "")
            {
                return ReturnValue;
            }
            else
            {
                return Value;
            }
        }
        catch
        {
            return ReturnValue;
        }
    }

    //Mobile number control only 9 simvol
    public static string IsMobileNumberControl(string Number, bool IsMobileOperationType)
    {
        try
        {
            Number = Number.Trim('+').TrimStart('0').Replace(" ", "").Replace("-", "").Replace("/", "").Replace(",", "").Trim().TrimStart('0');

            if (Number.Length > 9 && Number.Substring(0, 3) == "994")
                Number = Number.Substring(3);

            if (!Number.IsNumeric() || Number.Length != 9)
                return "-1";

            string Typ = Number.Substring(0, 2);
            if (IsMobileOperationType)
            {
                if (Config._GetAppSettings("MobileOperationTypes").IndexOf(Typ) < 0)
                {
                    return "-1";
                }
            }

            return Number;
        }
        catch
        {
            return "-1";
        }
    }

    public static bool CheckFileContentLength(this HttpPostedFile File, int ValueMB = 10)
    {
        if ((File.ContentLength / 1024) > ValueMB * 1000)
        {
            return false;
        }
        return true;
    }

    //Set title to url (clear latin and special simvols)
    public static string ClearTitle(this string Text)
    {
        Text = Text.ToLower().Trim().Trim('-').Trim('.').Trim();
        Text = Text.Replace("-", " ");
        Text = Text.Replace("–", " ");
        Text = Text.Replace("   ", " ");
        Text = Text.Replace("  ", " ");
        Text = Text.Replace("   ", " ");
        Text = Text.Replace("  ", " ");
        Text = Text.Replace("   ", " ");
        Text = Text.Replace(" ", "-");
        Text = Text.Replace(",", "");
        Text = Text.Replace("\"", "");
        Text = Text.Replace("/", "");
        Text = Text.Replace("\\", "");
        Text = Text.Replace("“", "");
        Text = Text.Replace("”", "");
        Text = Text.Replace("'", "");
        Text = Text.Replace("+", "");
        Text = Text.Replace(":", "");
        Text = Text.Replace(";", "");
        Text = Text.Replace(".", "");
        Text = Text.Replace(",", "");
        Text = Text.Replace("?", "");
        Text = Text.Replace("!", "");
        Text = Text.Replace(">", "");
        Text = Text.Replace("<", "");
        Text = Text.Replace("%", "");
        Text = Text.Replace("$", "");
        Text = Text.Replace("*", "");
        Text = Text.Replace("(", "");
        Text = Text.Replace(")", "");

        Text = Text.Replace("`", "");
        Text = Text.Replace("«", "");
        Text = Text.Replace("»", "");

        Text = Text.Replace("@", "");
        Text = Text.Replace("---", "-");
        Text = Text.Replace("--", "-");
        Text = Text.Replace("#", "");
        Text = Text.Replace("&", "");

        //No Latin
        Text = Text.Replace("ü", "u");
        Text = Text.Replace("ı", "i");
        Text = Text.Replace("ö", "o");
        Text = Text.Replace("ğ", "g");
        Text = Text.Replace("ə", "e");
        Text = Text.Replace("ç", "ch");
        Text = Text.Replace("ş", "sh");

        Text = Text.Replace("\n", " ");
        Text = Text.Replace(((char)13).ToString(), " ");
        Text = Text.Trim();

        return Text;
    }

    public static string ClearBasic(this string Text)
    {
        Text = Text.Replace(">", "");
        Text = Text.Replace("<", "");
        Text = Text.Replace("“", "");
        Text = Text.Replace("”", "");

        return Text;
    }

    public static int IPToInteger(this string IP)
    {
        return BitConverter.ToInt32(System.Net.IPAddress.Parse(IP).GetAddressBytes(), 0);
    }

    public static string IntegerToIP(this int IntegerIP)
    {
        return new System.Net.IPAddress(BitConverter.GetBytes(IntegerIP)).ToString();
    }
}
