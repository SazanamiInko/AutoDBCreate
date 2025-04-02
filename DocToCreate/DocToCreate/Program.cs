using DocToCreate;
using DocToCreate.template;
using DocToCreate.template.PodgreSQL;
using DocumentFormat.OpenXml.Bibliography;
using System.Text;

StringBuilder sb = new StringBuilder();

string outputInheritedPath = @"C:\Users\Public\Documents\data\DocToCreate\output\initsql.txt";

//ターゲットのディレクトリからファイルit一覧を取得する
DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Public\Documents\data\DocToCreate");

var define_files = dir.GetFiles("*.xlsx");
mapfile.Initialize();

foreach (var define_file in define_files)
{
    Console.WriteLine(define_file.Name);
    string path = define_file.FullName;
    DefineAnalyzer analyzer = new DefineAnalyzer();
    analyzer.Path = path;

    

    var datamodel = analyzer.CreateCreateSQL();
    CreateEngine engine = new CreateEngine(datamodel);
  
    string text = engine.Create();

    sb.AppendLine(text);
    sb.AppendLine("");
    
}

File.WriteAllText(outputInheritedPath, sb.ToString());