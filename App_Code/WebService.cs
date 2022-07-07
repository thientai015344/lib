using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net.Http;
using System.Xml.Linq;
using System.Threading.Tasks;
/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        List<string> txtItems = new List<string>();
        if (IsNumeric(prefixText))// kiểm tra nếu từ nhập vào là số hay chữ
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //String strCn = "Data Source=.;Initial Catalog=ThuVien;User ID=obama;Password=libhutechAa@";
            string constring = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;
            cn.ConnectionString = constring;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            //Compare String From Textbox(prefixText) AND String From 
            //Column in DataBase(CompanyName)
            //If String from DataBase is equal to String from TextBox(prefixText) 
            //then add it to return ItemList
            //-----I defined a parameter instead of passing value directly to 
            //prevent SQL injection--------//
            cmd.CommandText = "SET ROWCOUNT @Count select distinct Dewey from psc_RecordInfos Where Dewey like @myParameter order by Dewey ";
            cmd.Parameters.AddWithValue("@myParameter", prefixText + "%");
            cmd.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int, 8)).Value = 10;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];


            //Then return List of string(txtItems) as result

            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Dewey"].ToString().Trim();
                dbValues = dbValues.ToLower();
                txtItems.Add(dbValues);
            }
            // textBox2.Text = textBox1.Text.Substring(textBox1.Text.IndexOf(' ') + 1);
            return txtItems.ToArray();

            // return default(string[]);
        }
        else
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string constringSearch = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;

            cn.ConnectionString = constringSearch;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;

            //prevent SQL injection--------//
            cmd.CommandText = "SET ROWCOUNT @Count select SuggestSearch from Suggest Where SuggestSearch like @myParameter order by Searchpoint desc";
            cmd.Parameters.AddWithValue("@myParameter", prefixText + "%");
            cmd.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int, 8)).Value = 10;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result

            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["SuggestSearch"].ToString();
                dbValues = dbValues.ToLower();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }

    }
    private async Task<String[]> load(string keyword)
    {
        List<string> txtItems = new List<string>();
        List<GoogleSuggestion> x = new List<GoogleSuggestion>();
        x = await GetSearchSuggestions(keyword);
        foreach (GoogleSuggestion item in x)
        {
            txtItems.Add(item.Phrase);
        }
        return txtItems.ToArray();
    }

    public static bool IsNumeric(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c) && c != '.')
            {
                return false;
            }
        }

        return true;
    }

    private static readonly string[] VietNamChar = new string[]
    {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
    };
    public static string LocDau(string str)
    {
        //Thay thế và lọc dấu từng char      
        for (int i = 1; i < VietNamChar.Length; i++)
        {
            for (int j = 0; j < VietNamChar[i].Length; j++)
                str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
        }
        return str;
    }



    private const string _suggestSearchUrl = "http://www.google.com/complete/search?output=toolbar&q={0}&hl=en";

    public async Task<List<GoogleSuggestion>> GetSearchSuggestions(string query)
    {
        if (String.IsNullOrWhiteSpace(query))
        {
            throw new ArgumentException("Argument cannot be null or empty!", "query");
        }

        string result = String.Empty;

        using (HttpClient client = new HttpClient())
        {
            result = await client.GetStringAsync(String.Format(_suggestSearchUrl, query));
        }

        XDocument doc = XDocument.Parse(result);

        var suggestions = from suggestion in doc.Descendants("CompleteSuggestion")
                          select new GoogleSuggestion
                          {
                              Phrase = suggestion.Element("suggestion").Attribute("data").Value
                          };

        return suggestions.ToList();
    }


}
