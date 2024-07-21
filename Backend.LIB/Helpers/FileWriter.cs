using System.Text;

namespace Backend.LIB.Helpers;

public class FileWriter
{
    
    public void WriteToCsv<T>(List<T> list, string filePath)
    {
        using var writer = new StreamWriter(filePath);
        var sb = new StringBuilder();
        var header = "Wert1,Wert2,Wert3,blabla";
        sb.AppendLine(header);
        foreach (var obj in list)
        {
            var values = typeof(T).GetProperties().Select(f => f.GetValue(obj));
            var line = string.Join(",", values);
            sb.AppendLine(line);
        }
        File.WriteAllText(filePath, sb.ToString());
    }
}