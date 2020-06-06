using cp2020.Models;
using LiteDB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cp2020.DAL
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class LiteDbContext
    {
        const string _filename = "data.db";
        public LiteDatabase Database { get; }

        public LiteDbContext(IConfiguration configuration)
        {
            var needInitDB = !File.Exists(_filename);

            Database = new LiteDatabase(configuration["ConnectionStrings:LiteDB"]);

            if (needInitDB)
                InitDB();
        }

        /// <summary>
        /// Инициализация базы данных
        /// </summary>
        private void InitDB()
        {
            var docs = Database.GetCollection<Doc>();
            var rows = File.ReadAllLines("Resources/result-light.csv").Skip(1);
            var doc = (Doc)null;
            var docSection = (DocSection)null;
            var paragraph = (Paragraph)null;

            foreach (var row in rows)
            {
                var i = 1;
                var cells = row.Split(';');
                var filename = cells[1];
                var section = cells[2];
                var light = float.Parse(cells[cells.Length - 1].Replace('.', ','));
                var text = cells[3];

                while (3 + i < cells.Length - 1)
                {
                    text += cells[3 + i];
                    i++;
                }
                text = text.Trim('"');


                if (doc?.FileName != filename)
                {
                    var doc_old = doc;

                    doc = new Doc
                    {
                        FileName = filename,
                        Sections = new List<DocSection>(),
                    };

                    if (doc_old != null)
                        docs.Insert(doc_old);
                }

                docSection = doc.Sections.FirstOrDefault(z => z.Name == section);

                if (docSection == null)
                {
                    docSection = new DocSection
                    {
                        Name = section,
                        Items = new List<Paragraph>(),
                    };

                    doc.Sections.Add(docSection);
                }


                docSection.Items.Add(new Paragraph
                {
                    Text = text,
                    Scope = light,
                });
            }

            if (doc != null)
                docs.Insert(doc);
        }
    }
}
