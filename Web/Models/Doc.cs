using System;
using System.Collections.Generic;

namespace cp2020.Models
{
    /// <summary>
    /// Сущность "Документ"
    /// </summary>
    public class Doc
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }

        public List<DocSection> Sections { get; set; }
    }
}
