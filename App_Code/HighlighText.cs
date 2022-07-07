using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HighlighText
/// </summary>
public class HighlighText
{
    const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
   
    public HighlighText()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string HighlightField(string textField, Query q)
    {
       
        QueryScorer scorer = new QueryScorer(q);       
        IFormatter formatter = new SimpleHTMLFormatter("<span style='color:#c7511f; font-weight:700;'>", "</span>");
        Highlighter highlighter = new Highlighter(formatter, scorer);
        SimpleFragmenter fragmenter = new SimpleFragmenter(80);
        TokenStream stream = new StandardAnalyzer(AppLuceneVersion).GetTokenStream("", new StringReader(textField));
       
        var fragments = highlighter.GetBestFragments(stream, textField, 2);
      
        if (fragments == null || fragments.Length == 0) return textField.Length > 120 ? textField.Substring(0, 120) + "..." : textField;

        string highlighted = "";

        foreach (var fragment in fragments)
        {            
             highlighted = fragment;            
        }
        return highlighted;
    }
    public string HighlightContent(string textField, Query q)
    {

        QueryScorer scorer = new QueryScorer(q);
        IFormatter formatter = new SimpleHTMLFormatter("<span style='color:maroon; font-weight:bold;'>", "</span>");
        Highlighter highlighter = new Highlighter(formatter, scorer);
        SimpleFragmenter fragmenter = new SimpleFragmenter(80);
        TokenStream stream = new StandardAnalyzer(AppLuceneVersion).GetTokenStream("", new StringReader(textField));

        var fragments = highlighter.GetBestFragments(stream, textField, 2);

        if (fragments == null || fragments.Length == 0) return textField.Length > 120 ? textField.Substring(0, 120) + "..." : textField;

        string highlighted = "";

        foreach (var fragment in fragments)
        {
            if (textField.StartsWith(fragment))
                highlighted += "<p>" + fragment + " ... </p>";
            else if (textField.EndsWith(fragment))
                highlighted += "<p> ... " + fragment + "</p>";
            else
                highlighted += "<p> ... " + fragment + " ... </p>";
        }
        return highlighted;
    }
    
}