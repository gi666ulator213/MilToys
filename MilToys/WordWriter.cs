using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace MilToys {
    class WordWriter {
        public static string WriteToFile(string age, Dictionary<string, int> data, string saveDir = null) {
            if (saveDir == null) {
                saveDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            }

            if (!Directory.Exists(saveDir)) {
                try {
                    Directory.CreateDirectory(saveDir);
                }
                catch (Exception ex) {
                    return null;
                }
            }

            var template = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Templates\\Шаблон_Пошуку_Іграшок54.dot"));

            var wordApp = new Word.Application();

            try {
                var doc = wordApp.Documents.Open(template.FullName);

                doc.Content.Find.Execute(FindText: "<AGE>", ReplaceWith: age.ToString());

                var table = doc.Tables[1];

                foreach (var item in data) {
                    var row = table.Rows.Add();
                    row.Cells[1].Range.Text = item.Key.ToString();
                    row.Cells[2].Range.Text = item.Value.ToString();
                }


                var min = data.Min(s => s.Value);
                var theCheapestToy = data.Where(s => s.Value.Equals(min)).Select(s => s.Key).ToList();

                doc.Content.Find.Execute(FindText: "<NAME>", ReplaceWith: theCheapestToy[0].ToString());
                doc.Content.Find.Execute(FindText: "<PRICE>", ReplaceWith: min.ToString());

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string saveFileName = $"Результати пошуку іграшок_{timestamp}.doc";
                string savePath = Path.Combine(saveDir, saveFileName);
                doc.SaveAs(savePath);

                wordApp.ActiveDocument.Close();
                wordApp.Quit();

                return savePath;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            if (wordApp != null) {
                wordApp.Quit();
            }

            return null;
        }
    }
}
