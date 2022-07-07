using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for SuggestSearch
/// </summary>
public class SuggestSearch
{
    public SuggestSearch()
    {

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
    public async void Suggestions(string key)
    {
        List<GoogleSuggestion> x = new List<GoogleSuggestion>();
        string k = "";
        try
        {
            x = await GetSearchSuggestions(key);            
            foreach (GoogleSuggestion item in x)
            {
                k += item.Phrase + "\n";
            }
            string first = new StringReader(k).ReadLine();
            if (!string.IsNullOrEmpty(k))
                InserthSuggestions(first);

        }
        finally
        {
            

            
        }
    }

    public int InserthSuggestions(string keysearch)
    {
        int chk;
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Suggest", keysearch);
                cmd.CommandText = "[dbo].[Proc_Suggestions_ins]";
                cmd.Connection = con;
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a == 0)
                {
                    chk = 0;
                }
                else
                {
                    chk = 1;
                }
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return chk;
        }
    }
}




