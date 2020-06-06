using System.Collections.Generic;

namespace cp2020.Models
{
    /// <summary>
    /// Сущность "Раздел документа"
    /// </summary>
    public class DocSection
    {
        /// <summary>
        /// Наименование раздела
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список параграфов выделенных текстом
        /// </summary>
        public List<Paragraph> Items { get; set; }
    }
}
