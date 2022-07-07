using Lucene.Net.Search;
using Lucene.Net.Util;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search.Grouping;
using System.Collections.Generic;
using Lucene.Net.Documents;
using System.Data;
using System.Linq;
using Lucene.Net.Index;
using System;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;
using System.Configuration;
using Lucene.Net.Analysis;
using java.util;
using System.Text;

/// <summary>
/// Summary description for GroupingSearchFilter
/// </summary>
public class GroupingSearchFilter
{
    public Document GetDocument { get; set; }
   
    public DataTable dt2 = new DataTable();   
    private IndexSearcher _Searcher { get; set; }
    private QueryParser _Parser { get; set; }
    private string _Keyword { get; set; }    
    public String _SearchFields { get; set; }
    public String _CurrentPage { get; set; }
    public IndexReader _IndexReader { get; set; }

    public static List<Document> documents = new List<Document>();

    const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

    public GroupingSearchFilter()
    {
          
    }
   
    public static IEnumerable<SampleDataGroup> Groupfields_Author(IndexSearcher indexSearcher, String groupField, String keyword, string searchKeyword, string strDropDown, string searchField = "")
    {
        //List<SampleDataGroup> _field = new List<SampleDataGroup>();

        SampleDataGroup field;
        
        // field cần nhóm
        GroupingSearch groupingSearch = new GroupingSearch(groupField);

        groupingSearch.SetFillSortFields(true);

        groupingSearch.SetCachingInMB(4.0, true);

        groupingSearch.SetAllGroups(true);

        groupingSearch.SetGroupDocsLimit(10);

        StandardAnalyzer analyzer = new StandardAnalyzer(AppLuceneVersion);
        // field tìm kiếm để nhóm
        String[] fields = new String[] {  "id", "ti", "au", "aus", "grid", "pp", "ps", "py", "nb", "ns", "ddc", "la", "yr", "mj", "link", "gi", "filetype", "ti_uncode", "au_uncode", "pp_uncode", "ps_uncode", "su_uncode" };

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
            SelectsearchField = fields;// tất cả field được chọn
        }
        MultiFieldQueryParser multiQParser = new MultiFieldQueryParser(AppLuceneVersion, SelectsearchField, analyzer);

        multiQParser.DefaultOperator = Operator.AND;

        if (string.IsNullOrEmpty(strDropDown))
        {
            //tất cả bộ sưu tập
            keyword = searchKeyword;
        }
        else
            //khi chọn bộ sưu tập
            keyword = searchKeyword + " " + strDropDown;

       // Query query = multiQParser.Parse(keyword);
        Query query = parseQuery(keyword, multiQParser);

        var result = groupingSearch.Search(indexSearcher, query, 0, 10);

        int n = 1000;

        TopDocs hits = indexSearcher.Search(query, null, n);

        foreach (GroupDocs<BytesRef> groupDocs in result.Groups)
        {
            field = new SampleDataGroup();

            if (!string.IsNullOrEmpty(groupDocs.GroupValue.Utf8ToString()))
            {
                //nhóm tác giả
                field.Author_gr = groupDocs.GroupValue.Utf8ToString();
                // đêm nhóm tác giả
                field.Author_count = int.Parse(groupDocs.TotalHits.ToString());
                
                //tài liệu của nhóm               
                foreach (ScoreDoc scoreDoc in groupDocs.ScoreDocs)
                {   
                    Document doc= indexSearcher.Doc(scoreDoc.Doc);
                    // _obj.Doc = doc.GetField("ti").GetStringValue();
                    //  field.Doc= indexSearcher.Doc(scoreDoc.Doc);
                   // field.Title += doc.GetField("ti").GetStringValue();
                }
                // _field.Add(_obj);
                yield return field;
            }
        }
        //return _field;
    }
    public static IEnumerable<SampleDataGroup> Groupfields_Publisher(IndexSearcher indexSearcher, String groupField, String keyword, string searchKeyword, string strDropDown, string searchField = "")
    {
        //List<SampleDataGroup> _field = new List<SampleDataGroup>();

        SampleDataGroup field;

        // field cần nhóm
        GroupingSearch groupingSearch = new GroupingSearch(groupField);

        groupingSearch.SetFillSortFields(true);

        groupingSearch.SetCachingInMB(4.0, true);

        groupingSearch.SetAllGroups(true);

        groupingSearch.SetGroupDocsLimit(10);

        StandardAnalyzer analyzer = new StandardAnalyzer(AppLuceneVersion);
        // field tìm kiếm để nhóm
        String[] fields = new String[] {  "id", "ti", "au", "aus", "grid", "pp", "ps", "py", "nb", "ns", "ddc", "la", "yr", "mj", "link", "gi", "filetype", "ti_uncode", "au_uncode", "pp_uncode", "ps_uncode", "su_uncode" };

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
            SelectsearchField = fields;// tất cả field được chọn
        }
        MultiFieldQueryParser multiQParser = new MultiFieldQueryParser(AppLuceneVersion, SelectsearchField, analyzer);

        multiQParser.DefaultOperator = Operator.AND;

        if (string.IsNullOrEmpty(strDropDown))
        {
            //tất cả bộ sưu tập
            keyword = searchKeyword;
        }
        else
            //khi chọn bộ sưu tập
            keyword = searchKeyword + " " + strDropDown;

        // Query query = multiQParser.Parse(keyword);
        Query query = parseQuery(keyword, multiQParser);

        var result = groupingSearch.Search(indexSearcher, query, 0, 10);

        int n = 1000;

        TopDocs hits = indexSearcher.Search(query, null, n);

        foreach (GroupDocs<BytesRef> groupDocs in result.Groups)
        {
            field = new SampleDataGroup();

            if (!string.IsNullOrEmpty(groupDocs.GroupValue.Utf8ToString()))
            {
                //nhóm nhà xb
                field.Publisher_gr = groupDocs.GroupValue.Utf8ToString();
                // đêm nhóm nhà xb
                field.Publisher_count = int.Parse(groupDocs.TotalHits.ToString());

                //tài liệu của nhóm               
                foreach (ScoreDoc scoreDoc in groupDocs.ScoreDocs)
                {
                    Document doc = indexSearcher.Doc(scoreDoc.Doc);
                    // _obj.Doc = doc.GetField("ti").GetStringValue();
                    //  field.Doc= indexSearcher.Doc(scoreDoc.Doc);
                    // field.Title += doc.GetField("ti").GetStringValue();
                }
                // _field.Add(_obj);
                yield return field;
            }
        }
        //return _field;
    }
    public static IEnumerable<SampleDataGroup> Groupfields_GroupName(IndexSearcher indexSearcher, String groupField, String keyword, string searchKeyword, string strDropDown, string searchField = "")
    {
        //List<SampleDataGroup> _field = new List<SampleDataGroup>();

        SampleDataGroup field;

        // field cần nhóm
        GroupingSearch groupingSearch = new GroupingSearch(groupField);

        groupingSearch.SetFillSortFields(true);

        groupingSearch.SetCachingInMB(4.0, true);

        groupingSearch.SetAllGroups(true);

        groupingSearch.SetGroupDocsLimit(10);

        StandardAnalyzer analyzer = new StandardAnalyzer(AppLuceneVersion);
        // field tìm kiếm để nhóm
        String[] fields = new String[] { "id", "ti", "au", "aus", "grid", "pp", "ps", "py", "nb", "ns", "ddc", "la", "yr", "mj", "link", "gi", "filetype", "ti_uncode", "au_uncode", "pp_uncode", "ps_uncode", "su_uncode" };

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
            SelectsearchField = fields;// tất cả field được chọn
        }
        MultiFieldQueryParser multiQParser = new MultiFieldQueryParser(AppLuceneVersion, SelectsearchField, analyzer);

        multiQParser.DefaultOperator = Operator.AND;

        if (string.IsNullOrEmpty(strDropDown))
        {
            //tất cả bộ sưu tập
            keyword = searchKeyword;
        }
        else
            //khi chọn bộ sưu tập
            keyword = searchKeyword + " " + strDropDown;

        // Query query = multiQParser.Parse(keyword);
        Query query = parseQuery(keyword, multiQParser);

        var result = groupingSearch.Search(indexSearcher, query, 0, 10);

        int n = 1000;

        TopDocs hits = indexSearcher.Search(query, null, n);

        foreach (GroupDocs<BytesRef> groupDocs in result.Groups)
        {
            field = new SampleDataGroup();

            if (!string.IsNullOrEmpty(groupDocs.GroupValue.Utf8ToString()))
            {
                //bộ sưu tập
                field.RecordGroupName_gr = groupDocs.GroupValue.Utf8ToString();
                // đêm nhóm bộ sưu tập
                field.RecordGroupName_count = int.Parse(groupDocs.TotalHits.ToString());

                //tài liệu của nhóm               
                foreach (ScoreDoc scoreDoc in groupDocs.ScoreDocs)
                {
                    Document doc = indexSearcher.Doc(scoreDoc.Doc);
                    // _obj.Doc = doc.GetField("ti").GetStringValue();
                    //  field.Doc= indexSearcher.Doc(scoreDoc.Doc);
                    // field.Title += doc.GetField("ti").GetStringValue();
                }
                // _field.Add(_obj);
                yield return field;
            }
        }
        //return _field;
    }
    public static IEnumerable<SampleDataGroup> Groupfields_FileName(IndexSearcher indexSearcher, String groupField, String keyword, string searchKeyword, string strDropDown, string searchField = "")
    {
        //List<SampleDataGroup> _field = new List<SampleDataGroup>();

        SampleDataGroup field;

        // field cần nhóm
        GroupingSearch groupingSearch = new GroupingSearch(groupField);

        groupingSearch.SetFillSortFields(true);

        groupingSearch.SetCachingInMB(4.0, true);

        groupingSearch.SetAllGroups(true);

        groupingSearch.SetGroupDocsLimit(10);

        StandardAnalyzer analyzer = new StandardAnalyzer(AppLuceneVersion);
        // field tìm kiếm để nhóm
        String[] fields = new String[] { "id", "ti", "au", "aus", "grid", "pp", "ps", "py", "nb", "ns", "ddc", "la", "yr", "mj", "link", "gi", "filetype", "ti_uncode", "au_uncode", "pp_uncode", "ps_uncode", "su_uncode" };

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
            SelectsearchField = fields;// tất cả field được chọn
        }
        MultiFieldQueryParser multiQParser = new MultiFieldQueryParser(AppLuceneVersion, SelectsearchField, analyzer);

        multiQParser.DefaultOperator = Operator.AND;

        if (string.IsNullOrEmpty(strDropDown))
        {
            //tất cả bộ sưu tập
            keyword = searchKeyword;
        }
        else
            //khi chọn bộ sưu tập
            keyword = searchKeyword + " " + strDropDown;

        // Query query = multiQParser.Parse(keyword);
        Query query = parseQuery(keyword, multiQParser);

        var result = groupingSearch.Search(indexSearcher, query, 0, 10);

        int n = 1000;

        TopDocs hits = indexSearcher.Search(query, null, n);

        foreach (GroupDocs<BytesRef> groupDocs in result.Groups)
        {
            field = new SampleDataGroup();

            if (!string.IsNullOrEmpty(groupDocs.GroupValue.Utf8ToString()))
            {
                //bộ sưu tập
                field.Filename_gr = groupDocs.GroupValue.Utf8ToString();
                // đêm nhóm bộ sưu tập
                field.Filename_count = int.Parse(groupDocs.TotalHits.ToString());

                //tài liệu của nhóm               
                foreach (ScoreDoc scoreDoc in groupDocs.ScoreDocs)
                {
                    Document doc = indexSearcher.Doc(scoreDoc.Doc);
                    // _obj.Doc = doc.GetField("ti").GetStringValue();
                    //  field.Doc= indexSearcher.Doc(scoreDoc.Doc);
                    // field.Title += doc.GetField("ti").GetStringValue();
                }
                // _field.Add(_obj);
                yield return field;
            }
        }
        //return _field;
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
   
    private IEnumerable<SampleDataGroup> _mapLuceneToData(IEnumerable<Document> doc)
    {
        return doc.Select(_mapLuceneDocumentToData).ToList();
    }
    private static SampleDataGroup _mapLuceneDocumentToData(Document doc)
    {
        return new SampleDataGroup
        {
            RecordId =doc.Get("id").ToString(),
            Title = doc.Get("ti").ToString().Trim(),
            Author = doc.Get("au").ToString(),
            RecordGroupID = doc.Get("grid").ToString(),
            PublishPlace = doc.Get("pp").ToString(),
            Publisher = doc.Get("ps").ToString(),
            PublishYear = doc.Get("py").ToString(),
            Isbn = doc.Get("nb").ToString(),
            Dewey = doc.Get("ddc").ToString(),
            Subject = doc.Get("su").ToString(),

            //dung de nhom khong danh cho search
            Author_gr = doc.Get("au_gr").ToString(),
            Publishplace_gr = doc.Get("pp_gr").ToString(),
            Publisher_gr = doc.Get("ps_gr").ToString(),
            Year_gr = doc.Get("py_gr").ToString(),
            Subject_gr = doc.Get("su_gr").ToString(),
            RecordGroupName_gr = doc.Get("recordGroupName_gr").ToString()
        };
    }  
    
}