using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Search.Grouping;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Book_MenuSearchContain : System.Web.UI.UserControl
{
    String IndexPath;
    public string keyword { get; set; }
    public string CheckFullText { get; set; }
    public string field { get; set; }
    public string fieldgroup { get; set; }

    string str_author = "";

    string str_publisher = "";

    string str_groupName = "";

    string str_fileName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // if (!IsPostBack)
        
        if (!string.IsNullOrEmpty(keyword))
        {
            if (CheckFullText == "false")// tìm kiếm Marc
            {
                IndexPath = Convert.ToString(ConfigurationManager.AppSettings["IndexPathMarc"]);
                Directory dir = FSDirectory.Open(IndexPath);
                IndexReader reader = DirectoryReader.Open(dir);
                IndexSearcher searcher = new IndexSearcher(reader);
                DataList_Author.DataSource = GroupingSearchFilter.Groupfields_Author(searcher, "au_gr", keyword, keyword, fieldgroup, field);
                DataList_Author.DataBind();

                DataList_Publisher.DataSource = GroupingSearchFilter.Groupfields_Publisher(searcher, "ps_gr", keyword, keyword, fieldgroup, field);
                DataList_Publisher.DataBind();

                DataList_RecordGroupName.DataSource = GroupingSearchFilter.Groupfields_GroupName(searcher, "recordGroupName_gr", keyword, keyword, fieldgroup, field);
                DataList_RecordGroupName.DataBind();

                DataList_file.DataSource = GroupingSearchFilter.Groupfields_FileName(searcher, "filetype_gr", keyword, keyword, fieldgroup, field);
                DataList_file.DataBind();
                reader.Dispose();
            }
            else if (CheckFullText == "true")// tìm kiếm Full Text Search
            {
                /*IndexPath = Convert.ToString(ConfigurationManager.AppSettings["IndexPathFullText"]);
                Directory dir = FSDirectory.Open(IndexPath);
                IndexReader reader = DirectoryReader.Open(dir);
                IndexSearcher searcher = new IndexSearcher(reader);

                DataList_Author.DataSource = GroupingSearchFilter.Groupfields_Author(searcher, "au_gr", keyword, keyword, fieldgroup, field);
                DataList_Author.DataBind();

                DataList_Publisher.DataSource = GroupingSearchFilter.Groupfields_Publisher(searcher, "ps_gr", keyword, keyword, fieldgroup, field);
                DataList_Publisher.DataBind();

                DataList_RecordGroupName.DataSource = GroupingSearchFilter.Groupfields_GroupName(searcher, "recordGroupName_gr", keyword, keyword, fieldgroup, field);
                DataList_RecordGroupName.DataBind();
                reader.Dispose();*/
            }
        }

        
    }
    
    
    protected void DataList_Author_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            HyperLink l_author = (HyperLink)e.Item.FindControl("Link_au");            
            str_author = DataBinder.Eval(e.Item.DataItem,"Author_gr").ToString();
            string str_author_count= DataBinder.Eval(e.Item.DataItem, "Author_count").ToString();
            l_author.NavigateUrl = "~/fts?q=" + keyword + "&Field=" + field + "&start=0" + "&ck=" + CheckFullText + "&file=" + fieldgroup + "&filter_au=" + HttpUtility.HtmlDecode(str_author) + "&au_count="+ str_author_count ;
        }
    }
    protected void DataList_Publisher_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            HyperLink l_Publisher = (HyperLink)e.Item.FindControl("Link_Publisher");
            str_publisher = DataBinder.Eval(e.Item.DataItem, "Publisher_gr").ToString();
            string str_publisher_count= DataBinder.Eval(e.Item.DataItem, "Publisher_count").ToString();
            l_Publisher.NavigateUrl = "~/fts?q=" + keyword + "&Field=" + field + "&start=0" + "&ck=" + CheckFullText + "&file=" + fieldgroup + "&filter_ps=" + HttpUtility.HtmlDecode(str_publisher) + "&ps_count=" + str_publisher_count;
        }
    }
    protected void DataList_RecordGroupName_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            HyperLink l_GroupName = (HyperLink)e.Item.FindControl("Link_GroupName");
            str_groupName = DataBinder.Eval(e.Item.DataItem, "RecordGroupName_gr").ToString();
            string str_groupName_count = DataBinder.Eval(e.Item.DataItem, "RecordGroupName_count").ToString();
            l_GroupName.NavigateUrl = "~/fts?q=" + keyword + "&Field=" + field + "&start=0" + "&ck=" + CheckFullText + "&file=" + fieldgroup + "&filter_gn=" + HttpUtility.HtmlDecode(str_groupName) + "&gn_count=" + str_groupName_count;
        }
    }
    protected void DataList_file_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            HyperLink l_fileName = (HyperLink)e.Item.FindControl("Link_File");
            str_fileName = DataBinder.Eval(e.Item.DataItem, "Filename_gr").ToString();
            string str_fileName_count = DataBinder.Eval(e.Item.DataItem, "Filename_count").ToString();
            l_fileName.NavigateUrl = "~/fts?q=" + keyword + "&Field=" + field + "&start=0" + "&ck=" + CheckFullText + "&file=" + fieldgroup + "&filter_fn=" + HttpUtility.HtmlDecode(str_fileName) + "&fn_count=" + str_fileName_count;
        }
    }
    protected void GetSelectedValue(object sender, EventArgs e)
    {
        
        foreach (DataListItem ri in DataList_Author.Items)
        {
           
            CheckBox chkbox_All = ri.FindControl("CheckBox1") as CheckBox;
            if (chkbox_All != null)
            {
                if (!chkbox_All.Checked)
                {
                    Response.Write("No checked");
                }
                else
                {
                    var IDs = chkbox_All.ClientID;
                    Response.Write("ID here...");
                }
            }
        }
    }



   
}