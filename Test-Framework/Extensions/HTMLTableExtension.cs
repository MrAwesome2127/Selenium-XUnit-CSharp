
namespace Test_Framework.Extensions;

public static class HTMLTableExtension
{
    //Read the ENTIRE table
    private static List<TableDataCollection> ReadTable(IWebElement table)
    {
        //Initialize the Table
        var tableDataCollection = new List<TableDataCollection>();

        //Get all Columns & Rows from the table
        var columns = table.FindElements(By.TagName("th"));
        var rows = table.FindElements(By.TagName("tr"));

        //Create Row Index
        int rowIndex = 0;
        foreach (var row in rows)
        {
            int colIndex = 0;
            var colDatas = row.FindElements(By.TagName("td"));

            //Store data if the row has data in it
            if (colDatas.Count != 0)
            {
                foreach (var colValue in colDatas)
                {
                    tableDataCollection.Add(new TableDataCollection
                    {
                        RowNumber = rowIndex,
                        ColumnName = columns[colIndex].Text != "" ?
                                     columns[colIndex].Text : colIndex.ToString(),
                        ColumnValue = colValue.Text,
                        ColumnSpecialValues = GetControl(colValue)

                    });
                    //Move to next Column iteration
                    colIndex++;
                }
                //Move to next Row iteration
                rowIndex++;
            }
        }
        return tableDataCollection;
    }

    private static ColumnSpecialValue GetControl(IWebElement columnValue)
    {
        ColumnSpecialValue? columnSpecialValue = null;

        //Checks if controls have Input & Hyperlink
        if (columnValue.FindElements(By.TagName("a")).Count > 0)
        {
            columnSpecialValue = new ColumnSpecialValue
            {
                ElementCollection = columnValue.FindElements(By.TagName("a")),
                ControlType = ControlType.hyperlink
            };
        }
        if (columnValue.FindElements(By.TagName("input")).Count > 0)
        {
            columnSpecialValue = new ColumnSpecialValue
            {
                ElementCollection = columnValue.FindElements(By.TagName("input")),
                ControlType = ControlType.input
            };
        }
        return columnSpecialValue;
    }

    public static void PerformActionOnCell(this IWebElement element, string targetColumnIndex, string refColumnName,
        string refColumnValue, string controlToOperate = null)
    {
        //First read the table
        var table = ReadTable(element);

        //Go through each Iteration of the table to find target.
        foreach (int rowNumber in GetDynamicRowNumber(table, refColumnName, refColumnValue))
        {
            var cell = (from e in table
                        where e.ColumnName == targetColumnIndex && e.RowNumber == rowNumber
                        select e.ColumnSpecialValues).SingleOrDefault();

            //Operates the controls
            if (controlToOperate != null && cell != null)
            {
                IWebElement? elementToClick = null;
                if (cell.ControlType == ControlType.hyperlink)
                    elementToClick = (from c in cell.ElementCollection
                                      where c.Text == controlToOperate
                                      select c).SingleOrDefault();
                if (cell.ControlType == ControlType.input)
                    elementToClick = (from c in cell.ElementCollection
                                      where c.GetAttribute("value") == controlToOperate
                                      select c).SingleOrDefault();

                elementToClick?.Click();
            }
            else
            {
                cell.ElementCollection?.First().Click();
            }
        }
    }

    private static IEnumerable GetDynamicRowNumber(List<TableDataCollection> tableCollection,
                                                    string columnName, string columnValue)
    {
        foreach (var table in tableCollection)
            if (table.ColumnName == columnName && table.ColumnValue == columnValue)
                yield return table.RowNumber;
    }

    public class TableDataCollection
    {
        public int RowNumber { get; set; }
        public string? ColumnName { get; set; }
        public string? ColumnValue { get; set; }
        public ColumnSpecialValue? ColumnSpecialValues { get; set; }
    }

    public class ColumnSpecialValue
    {
        public IEnumerable<IWebElement>? ElementCollection { get; set; }
        public ControlType? ControlType { get; set; }
    }

    public enum ControlType
    {
        hyperlink,
        input,
        option,
        select
    }
}