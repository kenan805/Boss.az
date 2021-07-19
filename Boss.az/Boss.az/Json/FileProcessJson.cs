using Boss.az.DatabaseNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Boss.az.Json
{
    static class FileProcessJson
    {
        public static void JsonFileWriteAllTextToDatabase(string path, Database db)
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            var textJson = JsonSerializer.Serialize(db, options);
            File.WriteAllText(path, textJson);
        }
    }
}
