using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dateisystem.Data.Models
{
    public class FileDir : Entity
    {
        public string Path { get; set; }
        public int? ParentId { get; set; }
        public string FullPath { get; set; }
        public FileDir? Parent { get; set; }
    }
}
