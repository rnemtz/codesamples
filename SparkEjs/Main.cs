using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SparkEjs
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            InitializeRulesEngine();
        }

        public RulesEngine RulesEngine { get; set; }
        public string FileName { get; set; }

        private void InitializeRulesEngine()
        {
            RulesEngine = new RulesEngine();
        }

        //public File SparkDocument { get; set; }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            folderSparkFiles.ShowDialog();
            lblFolderPath.Text = folderSparkFiles.SelectedPath;

            var sparkFilesCount = Directory.GetFiles(folderSparkFiles.SelectedPath).Count(x => x.EndsWith(".spark"));
            var subFolders = Directory.GetDirectories(folderSparkFiles.SelectedPath);
            if (subFolders.Any())
            {
                sparkFilesCount += subFolders.Sum(d => Directory.GetFiles(d).Count(x => x.EndsWith(".spark")));
            }

            if (sparkFilesCount == 0)
            {
                MessageBox.Show(
                    string.Format("There are no spark files in the selected path: '{0}', please select another folder",
                        folderSparkFiles.SelectedPath),
                    @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblFileCount.Text = @"No Spark files found";

            }
            else
            {
                lblFileCount.Text = string.Format("Spark files found: {0}",  sparkFilesCount);
            }
        }

        private void TransformFiles(string path)
        {
            var sparkFiles =
                    Directory.GetFiles(path).Select(x => x.EndsWith(".spark")).ToList();

            foreach (var r in from r in RulesEngine.BaseRules from f in sparkFiles select r)
            {
                var file = new StringBuilder();
                using (var reader = File.OpenText(openSparkFile.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (r == null) continue;
                        var rule = r.Split('|')[0].Trim();
                        if (line == null || !line.Contains(rule))
                        {
                            file.Append(line);
                            continue;
                        }
                        var tag = line.Replace(rule, r.Split('|')[1].Trim());
                        file.Append(tag);
                    }
                }
                File.WriteAllText(openSparkFile.FileName, file.ToString());
            }
        }
    }
}