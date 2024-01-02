using Aspose.Cells;
using System.Xml;

var dir = GetDictionary("ViewerMessages.xlsx");

if (dir.Count > 0)
{
    CreateXmlDOC("ViewerMessages.xml", dir);
}

Dictionary<string, string> GetDictionary(string path)
{
    Dictionary<string, string> pairs = new Dictionary<string, string>();
    try
    {
        using (Workbook wb = new Workbook(path))
        {
            WorksheetCollection worksheets = wb.Worksheets;

            foreach (var sheet in worksheets)
            {
                Worksheet worksheet = sheet;
                int rows = worksheet.Cells.MaxDataRow;
                for (int i = 0; i <= rows; i++)
                {
                    string? s1 = worksheet.Cells[i, 0].Value.ToString();
                    string? s2 = worksheet.Cells[i, 1].Value.ToString();

                    if (s1 != null && s2 != null)
                    {
                        pairs.Add(s1, s2);
                    }

                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при выполнение  {ex.Message}");
    }

    return pairs;
}

void CreateXmlDOC(string path, Dictionary<string, string> dir)
{
    XmlDocument xmlDocument = new XmlDocument();
    try
    {
        xmlDocument.Load(path);
        XmlElement? xxml = xmlDocument.DocumentElement;
        if (xxml != null)
        {
            foreach (XmlElement xnode in xxml)
            {
                string key = "";
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    var language = childnode.Attributes?.GetNamedItem("language")?.Value;

                    if (language == "en_US")
                    {
                        if (dir.ContainsKey(childnode.InnerText))
                        {
                            key = childnode.InnerText;
                        }
                        else { break; }
                    }
                    if (language == "ru_RU")
                    {
                        childnode.InnerText = dir[key];
                    }
                }
            }
        }
        xmlDocument.Save("ViewerMessagesTest.xml");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при выполнение  {ex.Message}");       
    }
   
}