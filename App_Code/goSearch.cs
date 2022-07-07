
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Search.Grouping;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.SessionState;

/// <summary>
/// Summary description for SearchMarc
/// </summary>
public class goSearch
{
    protected DataTable Results = new DataTable();

    public static DataTable Results_group = new DataTable();

    public int startAt;

    public int fromItem;

    public int toItem;

    public int total;

    public TimeSpan duration;

    private IndexReader indexReader;

    public IndexSearcher searcher;


    HighlighText _HighlighText = new HighlighText();

    public readonly int maxResults = 10;

    List<Getfield> _getf = new List<Getfield>();

    static string IndexPath;

    public int CurrentPage;

    string constringSearch = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;

    const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
    public goSearch()
    {

    }
    public DataTable SearchField(string _keyword, string _start, string IndexPath, string _strDropDown, string _searchKeyword, string field_author = "", string field_publisher = "", string author_count = "", string publisher_count = "", string field_groupname = "", string groupname_count = "", string field_filename = "", string filename_count = "", string searchField = "")
    {
        DateTime start = DateTime.Now;

        var hits_limit = 2000000;

        // create the searcher       
        string indexDirectory = IndexPath;

        // create the result DataTable       
        this.Results.Columns.Add("RecordID", typeof(string));
        this.Results.Columns.Add("Title", typeof(string));
        this.Results.Columns.Add("Author", typeof(string));
        this.Results.Columns.Add("Author_lnk", typeof(string));
        this.Results.Columns.Add("RecordGroupID", typeof(string));
        this.Results.Columns.Add("Filetype", typeof(string));
        this.Results.Columns.Add("sample", typeof(string));
        this.Results.Columns.Add("image", typeof(string));
        this.Results.Columns.Add("path", typeof(string));
        this.Results.Columns.Add("Year", typeof(string));
        this.Results.Columns.Add("Dewey", typeof(string));
        this.Results.Columns.Add("PublishPlace", typeof(string));
        this.Results.Columns.Add("Publisher", typeof(string));
        this.Results.Columns.Add("PublishYear", typeof(string));
        this.Results.Columns.Add("Isbn", typeof(string));
        this.Results.Columns.Add("Issn", typeof(string));
        this.Results.Columns.Add("RecordGroupName", typeof(string));
        //this.Results.Columns.Add("RecordGroupID", typeof(string));

        // tìm kiếm field được chọn
        if (!string.IsNullOrEmpty(searchField))
        {
            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            var dir = FSDirectory.Open(indexDirectory);

            indexReader = DirectoryReader.Open(dir);

            searcher = new IndexSearcher(indexReader);

            string[] SelectsearchField;

            if (searchField.Contains("ti"))// khi chọn chỉ nhan đề
            {
                SelectsearchField = new string[] { "ti", "ti_uncode", "filetype", "grid" };
            }
            else if (searchField.Contains("au"))// khi chọn chỉ tác giả
            {
                SelectsearchField = new string[] { "au", "au_uncode", "filetype", "grid" };
            }
            else if (searchField.Contains("pp"))// khi chọn nơi xb
            {
                SelectsearchField = new string[] { "pp", "pp_uncode", "filetype", "grid" };
            }
            else if (searchField.Contains("ps"))// khi chọn nhà xb
            {
                SelectsearchField = new string[] { "ps", "ps_uncode", "filetype", "grid" };
            }
            else if (searchField.Contains("su"))// khi chọn chue đề
            {
                SelectsearchField = new string[] { "su", "su_uncode", "filetype", "grid" };
            }
            else
            {
                SelectsearchField = new string[] { searchField, "filetype", "grid", "gi", "rgn" };// tất cả field còn lại

            }

            var parser = new MultiFieldQueryParser(AppLuceneVersion, SelectsearchField, analyzer);

            parser.DefaultOperator = Operator.AND;

            if (string.IsNullOrEmpty(_strDropDown))
            {
                //tất cả bộ sưu tập
                _keyword = _searchKeyword + " au:" + "\"" + field_author + "\"" + " ps:" + "\"" + field_publisher + "\"" + " rgn:" + "\"" + field_groupname + "\"" + " filetype:" + "\"" + field_filename + "\"";
            }
            else
            {
                //khi chọn bộ sưu tập
                _keyword = _searchKeyword + _strDropDown + " au:" + "\"" + field_author + "\"" + " ps:" + "\"" + field_publisher + "\"" + " rgn:" + "\"" + field_groupname + "\"" + " filetype:" + "\"" + field_filename + "\"";

            }
            // search
            var query = parseQuery(_keyword, parser);

            var queryHighlight = parseQuery(_searchKeyword, parser);// dành cho HighlightField
                                                                    //BooleanQuery.MaxClauseCount = 20480;

           // Insert_Keyword(_HighlighText.HighlightField(_keyword,query));

            var hits = searcher.Search(query, hits_limit);

            if (string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count) && string.IsNullOrEmpty(groupname_count) && string.IsNullOrEmpty(filename_count))// tất cả số lượng =null
            {
                this.total = hits.TotalHits;
            }
            else
            {
                if (!string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count) && string.IsNullOrEmpty(groupname_count) && string.IsNullOrEmpty(filename_count))
                {
                    this.total = Convert.ToInt32(author_count);
                }
                else
                   if (string.IsNullOrEmpty(author_count) && !string.IsNullOrEmpty(publisher_count) && string.IsNullOrEmpty(groupname_count) && string.IsNullOrEmpty(filename_count))
                    this.total = Convert.ToInt32(publisher_count);
                else if (string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count) && !string.IsNullOrEmpty(groupname_count) && string.IsNullOrEmpty(filename_count))
                {
                    this.total = Convert.ToInt32(groupname_count);
                }
                else if (string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count) && string.IsNullOrEmpty(groupname_count) && !string.IsNullOrEmpty(filename_count))
                    this.total = Convert.ToInt32(filename_count);
            }

            // initialize startAt      
            this.startAt = InitStartAt(_start);
            // how many items we should show - less than defined at the end of the results
            int resultsCount = Math.Min(total, this.maxResults + startAt);
            for (int i = startAt; i < resultsCount; i++)
            {
                // get the document from index
                int docid = hits.ScoreDocs[i].Doc;
                Document doc = searcher.Doc(docid);

                // create a new row with the result data
                DataRow row = this.Results.NewRow();

                row["Title"] = _HighlighText.HighlightField(doc.Get("ti").TrimEnd('/').TrimEnd(':'), queryHighlight);
                row["Author"] = _HighlighText.HighlightField(doc.Get("au").TrimEnd(':').TrimEnd(','), queryHighlight);
                row["Author_lnk"] = doc.Get("au").TrimEnd(':');
                row["path"] = "/chi-tiet?id=" + doc.Get("id");
                row["RecordID"] = doc.Get("id");
                //row["image"] = ImageBook(doc.Get("id"), doc.Get("grid"));
                row["Filetype"] = doc.Get("filetype").ToString().ToUpper();
                row["Year"] = doc.Get("py");
                row["Dewey"] = doc.Get("ddc");
                row["PublishPlace"] = doc.Get("pp");
                row["Publisher"] = doc.Get("ps");
                row["PublishYear"] = doc.Get("py");
                row["Isbn"] = doc.Get("nb");
                row["Issn"] = doc.Get("ns");
                row["RecordGroupID"] = doc.Get("grid");
                row["RecordGroupName"]= doc.Get("rgn");
                if (!row.IsNull(0))// kiểm tra nếu title khác null
                {
                    this.Results.Rows.Add(row);
                }

            }
            CurrentPage = startAt;// cập nhật vị trí trang hiện tại   

        }
        else// tìm kiếm tất cả field
        {
                      
            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            var dir = FSDirectory.Open(indexDirectory);

            indexReader = DirectoryReader.Open(dir);

            searcher = new IndexSearcher(indexReader);

            string[] _multiField = new string[] { "text", "id", "ti", "au", "aus", "grid", "pp", "ps", "py", "nb", "ns", "ddc", "la", "yr", "mj", "link", "gi", "rgn", "filetype", "ti_uncode", "au_uncode", "pp_uncode", "ps_uncode", "su_uncode" };

            var parser = new MultiFieldQueryParser(AppLuceneVersion, _multiField, analyzer);

           
            parser.DefaultOperator = Operator.AND;// mac dinh la toan tu AND

            if (string.IsNullOrEmpty(_strDropDown))
            {
                //tất cả bộ sưu tập
                _keyword = _searchKeyword + " au:" + "\"" + field_author + "\"" + " ps:" + "\"" + field_publisher + "\"" + " rgn:" + "\"" + field_groupname + "\"" + " filetype:" + "\"" + field_filename + "\"";
            }
            else
            {
                //khi chọn bộ sưu tập
                _keyword = _searchKeyword + _strDropDown + " au:" + "\"" + field_author + "\"" + " ps:" + "\"" + field_publisher + "\"" + " rgn:" + "\"" + field_groupname + "\"" + " filetype:" + "\"" + field_filename + "\"";

            }
            // search
            var query = parseQuery(_keyword, parser);          
           

            var queryHighlight = parseQuery(_searchKeyword, parser);// dành cho HighlightField

            var hits = searcher.Search(query, hits_limit);

            if (string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count) && string.IsNullOrEmpty(groupname_count) && string.IsNullOrEmpty(filename_count))// tất cả số lượng =null
            {
                this.total = hits.TotalHits;
            }
            else
            {
                if (!string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count) && string.IsNullOrEmpty(groupname_count) && string.IsNullOrEmpty(filename_count))
                {
                    this.total = Convert.ToInt32(author_count);
                }
                else
                   if (string.IsNullOrEmpty(author_count) && !string.IsNullOrEmpty(publisher_count) && string.IsNullOrEmpty(groupname_count) && string.IsNullOrEmpty(filename_count))
                    this.total = Convert.ToInt32(publisher_count);
                else if (string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count) && !string.IsNullOrEmpty(groupname_count) && string.IsNullOrEmpty(filename_count))
                {
                    this.total = Convert.ToInt32(groupname_count);
                }
                else if (string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count) && string.IsNullOrEmpty(groupname_count) && !string.IsNullOrEmpty(filename_count))
                    this.total = Convert.ToInt32(filename_count);
            }

            // initialize startAt      
            this.startAt = InitStartAt(_start);
            // how many items we should show - less than defined at the end of the results
            int resultsCount = Math.Min(total, this.maxResults + startAt);

            for (int i = startAt; i < resultsCount; i++)
            {
                // get the document from index
                int docid = hits.ScoreDocs[i].Doc;
                Document doc = searcher.Doc(docid);

                // create a new row with the result data
                DataRow row = this.Results.NewRow();

                row["Title"] = _HighlighText.HighlightField(doc.Get("ti").TrimEnd('/').TrimEnd(':'), queryHighlight);
                row["Author"] = doc.Get("au").TrimEnd(':').TrimEnd(',');
                row["Author_lnk"] = doc.Get("au").TrimEnd(':');
                row["path"] = "/chi-tiet?id=" + doc.Get("id");
                row["RecordID"] = doc.Get("id");
                row["Filetype"] = doc.Get("filetype").ToString().ToUpper();
                row["Year"] = doc.Get("py");
                row["Dewey"] = doc.Get("ddc");
                row["PublishPlace"] = doc.Get("pp");
                row["Publisher"] = doc.Get("ps");
                row["PublishYear"] = doc.Get("py");
                row["Isbn"] = doc.Get("nb");
                row["Issn"] = doc.Get("ns");
                row["RecordGroupID"] = doc.Get("grid");
                row["RecordGroupName"] = doc.Get("rgn");
                if (!row.IsNull(0))// kiểm tra nếu title khác null
                {
                    this.Results.Rows.Add(row);
                }

            }
            CurrentPage = startAt;// cập nhật vị trí trang hiện tại

        }

        //this.indexReader.Dispose();
        // result information
        this.duration = DateTime.Now - start;
        double dblPageCount = (double)((decimal)startAt / (decimal)maxResults);
        int pageCount = (int)Math.Ceiling(dblPageCount) + 1;// lấy vị trí trang hiện tại
        this.fromItem = pageCount;
        this.toItem = Math.Min(startAt + maxResults, total);
        Close();
        return Results;
    }

    public DataTable SearchFullText(string _keyword, string _start, string IndexPath, string field_author = "", string field_publisher = "", string author_count = "", string publisher_count = "", string field_groupname = "", string groupname_count = "", string searchField = "")
    {
        DateTime start = DateTime.Now;
        var hits_limit = 200000;

        // create the searcher       
        string indexDirectory = IndexPath;
        this.Results.Clear();
        // create the result DataTable       
        this.Results.Columns.Add("RecordID", typeof(string));
        this.Results.Columns.Add("Title", typeof(string));
        this.Results.Columns.Add("Author", typeof(string));
        this.Results.Columns.Add("Author_lnk", typeof(string));
        this.Results.Columns.Add("RecordGroupID", typeof(string));
        this.Results.Columns.Add("Filetype", typeof(string));
        this.Results.Columns.Add("sample", typeof(string));
        this.Results.Columns.Add("image", typeof(string));
        this.Results.Columns.Add("path", typeof(string));
        this.Results.Columns.Add("Year", typeof(string));
        this.Results.Columns.Add("Dewey", typeof(string));
        this.Results.Columns.Add("PublishPlace", typeof(string));
        this.Results.Columns.Add("Publisher", typeof(string));
        this.Results.Columns.Add("PublishYear", typeof(string));
        this.Results.Columns.Add("Isbn", typeof(string));
        this.Results.Columns.Add("Issn", typeof(string));
        this.Results.Columns.Add("RecordGroupName", typeof(string));

        // tìm kiếm field được chọn
        if (!string.IsNullOrEmpty(searchField))

        {
            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            var dir = FSDirectory.Open(indexDirectory);

            indexReader = DirectoryReader.Open(dir);

            searcher = new IndexSearcher(indexReader);

            var parser = new QueryParser(AppLuceneVersion, searchField, analyzer);

            parser.DefaultOperator = Operator.AND;
            // search
            var query = parseQuery(_keyword, parser);

            //RegexpQuery query = new RegexpQuery(new Term(searchField, _keyword));

            //BooleanQuery.MaxClauseCount = 20480;

            var hits = searcher.Search(query, hits_limit);

            if (string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count))
            {
                this.total = hits.TotalHits;
            }
            else
            {
                if (!string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count))
                {
                    this.total = Convert.ToInt32(author_count);
                }
                else
                   if (string.IsNullOrEmpty(author_count) && !string.IsNullOrEmpty(publisher_count))
                    this.total = Convert.ToInt32(publisher_count);
            }

            // initialize startAt      
            this.startAt = InitStartAt(_start);
            // how many items we should show - less than defined at the end of the results
            int resultsCount = Math.Min(total, this.maxResults + startAt);
            for (int i = startAt; i < resultsCount; i++)
            {
                // get the document from index
                int docid = hits.ScoreDocs[i].Doc;
                Document doc = searcher.Doc(docid);

                // create a new row with the result data
                DataRow row = this.Results.NewRow();

                row["Title"] = _HighlighText.HighlightField(doc.Get("ti").TrimEnd('/').TrimEnd(':'), query);
                row["Author"] = doc.Get("au").TrimEnd(':');
                row["Author_lnk"] = doc.Get("au").TrimEnd(':');
                row["path"] = "/chi-tiet?id=" + doc.Get("id");
                row["RecordID"] = doc.Get("id");
                // row["image"] = ImageBook(doc.Get("id"), doc.Get("grid"));
                row["sample"] = _HighlighText.HighlightContent(doc.Get("text").ToLower(), query);
                row["Filetype"] = doc.Get("filetype").ToString().Replace(".", "").ToUpper();
                row["Year"] = doc.Get("py");
                row["Dewey"] = doc.Get("ddc");
                row["PublishPlace"] = doc.Get("pp");
                row["Publisher"] = doc.Get("ps");
                row["PublishYear"] = doc.Get("py");
                row["Isbn"] = doc.Get("nb");
                row["RecordGroupID"] = doc.Get("grid");
                row["RecordGroupName"] = doc.Get("rgn");
                if (!row.IsNull(0))// kiểm tra nếu title khác null
                {
                    this.Results.Rows.Add(row);
                }

            }
            CurrentPage = startAt;// cập nhật vị trí trang hiện tại              
        }
        else// tìm kiếm tất cả field
        {

            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            var dir = FSDirectory.Open(indexDirectory);

            indexReader = DirectoryReader.Open(dir);

            searcher = new IndexSearcher(indexReader);

            string[] _MultiField = new string[] { "text", "id", "ti", "au", "aus", "grid", "pp", "ps", "py", "nb", "ns", "ddc", "la", "yr", "filetype", "yr", "mj" };

            var parser = new MultiFieldQueryParser(AppLuceneVersion, _MultiField, analyzer);

            parser.DefaultOperator = Operator.AND;
            // search
            var query = parseQuery(_keyword, parser);

            // var query = new TermQuery(new Term("text", _keyword));
            // BooleanQuery.MaxClauseCount = 20480;

            var hits = searcher.Search(query, hits_limit);

            if (string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count))
            {
                this.total = hits.TotalHits;
            }
            else
            {
                if (!string.IsNullOrEmpty(author_count) && string.IsNullOrEmpty(publisher_count))
                {
                    this.total = Convert.ToInt32(author_count);
                }
                else
                   if (string.IsNullOrEmpty(author_count) && !string.IsNullOrEmpty(publisher_count))
                    this.total = Convert.ToInt32(publisher_count);
            }

            // initialize startAt      
            this.startAt = InitStartAt(_start);
            // how many items we should show - less than defined at the end of the results
            int resultsCount = Math.Min(total, this.maxResults + startAt);

            for (int i = startAt; i < resultsCount; i++)
            {
                // get the document from index
                int docid = hits.ScoreDocs[i].Doc;
                Document doc = searcher.Doc(docid);

                // create a new row with the result data
                DataRow row = this.Results.NewRow();

                row["Title"] = _HighlighText.HighlightField(doc.Get("ti").TrimEnd('/').TrimEnd(':'), query);
                row["Author"] = doc.Get("au").TrimEnd(':');
                row["Author_lnk"] = doc.Get("au").TrimEnd(':');
                row["path"] = "/chi-tiet?id=" + doc.Get("id");
                row["RecordID"] = doc.Get("id");
                //row["image"] = ImageBook(doc.Get("id"), doc.Get("grid"));

                row["sample"] = _HighlighText.HighlightContent(doc.Get("text").ToLower(), query);
                row["Filetype"] = doc.Get("filetype").ToString().Replace(".", "").ToUpper();
                row["Year"] = doc.Get("py");
                row["Dewey"] = doc.Get("ddc");
                row["PublishPlace"] = doc.Get("pp");
                row["Publisher"] = doc.Get("ps");
                row["PublishYear"] = doc.Get("py");
                row["Isbn"] = doc.Get("nb");
                row["Filetype"] = doc.Get("filetype").ToString().Replace(".", "").ToUpper();
                row["RecordGroupID"] = doc.Get("grid");
                row["RecordGroupName"] = doc.Get("rgn");
                if (!row.IsNull(0))// kiểm tra nếu title khác null
                {
                    this.Results.Rows.Add(row);
                }

            }
            CurrentPage = startAt;// cập nhật vị trí trang hiện tại            
        }
        // result information
        this.duration = DateTime.Now - start;
        double dblPageCount = (double)((decimal)startAt / (decimal)maxResults);
        int pageCount = (int)Math.Ceiling(dblPageCount) + 1;// lấy vị trí trang hiện tại
        this.fromItem = pageCount;
        this.toItem = Math.Min(startAt + maxResults, total);
        Close();
        return Results;

    }
    public void Close()
    {
        if (indexReader != null)
        {
            // return ;
            indexReader.Dispose();
            indexReader = null;
        }
    }
    private int InitStartAt(string _start)//tính toán số trang
    {
        try
        {
            int sa = Convert.ToInt32(_start);

            // too small starting item, return first page
            if (sa < 0)
                return 0;

            // too big starting item, return last page
            if (sa >= total - 1)
            {
                return LastPageStartsAt;
            }

            return sa;
        }
        catch
        {
            return 0;
        }
    }
    private int LastPageStartsAt
    {
        get
        {
            return PageCount * maxResults;
        }
    }
    private int PageCount
    {
        get
        {
            return (total - 1) / maxResults; // floor
        }
    }
    private static Query parseQuery(string _keyword, QueryParser parser)
    {
        Query query;
        try
        {
            query = parser.Parse(_keyword);
        }
        catch (ParseException)
        {
            query = parser.Parse(QueryParser.Escape(_keyword));
        }
        return query;
    }
    public static IEnumerable<SampleDataGroup> groupFieldSelect(IndexSearcher indexSearcher, String groupField, String keywork)
    {
        // IEnumerable<SampleDataGroup> searchResults = new List<SampleDataGroup>();      
        List<SampleDataGroup> StrLst = new List<SampleDataGroup>();
        SampleDataGroup Sgroup = null;
        StandardAnalyzer analyzer = new StandardAnalyzer(AppLuceneVersion);
        // chọn field để nhóm
        GroupingSearch groupingSearch = new GroupingSearch(groupField);

        groupingSearch.SetFillSortFields(true);

        groupingSearch.SetCachingInMB(4.0, true);

        groupingSearch.SetAllGroups(true);

        QueryParser parser = new QueryParser(AppLuceneVersion, groupField, analyzer);

        Query query = parser.Parse(keywork);

        var topGroups = groupingSearch.Search(indexSearcher, query, 0, 10);

        foreach (GroupDocs<BytesRef> groupDocs in topGroups.Groups)
        {
            //nhóm chính            

            /*StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append(groupDocs.GroupValue.Utf8ToString());
            sb.Append(groupDocs.TotalHits.ToString());*/

            Sgroup.Author_gr = groupDocs.GroupValue.Utf8ToString();
            Sgroup.Author_count = groupDocs.TotalHits;
            StrLst.Add(Sgroup);
            //Console.WriteLine("Tac gia: " + groupDocs.GroupValue.Utf8ToString());
            //Console.WriteLine("So luong" + groupDocs.TotalHits.ToString());
            //tài liệu của nhóm
            foreach (ScoreDoc scoreDoc in groupDocs.ScoreDocs)
            {
                Document doc = indexSearcher.Doc(scoreDoc.Doc);
                // Console.WriteLine(" - " + doc.GetField("ti").GetStringValue());
            }
        }
        return StrLst;

    }
    
    
}