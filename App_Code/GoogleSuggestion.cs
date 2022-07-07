using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GoogleSuggestion
/// </summary>
public class GoogleSuggestion
{
    public GoogleSuggestion()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public string Phrase { get; set; }
    public override string ToString()
    {
        return this.Phrase;
    }
}