using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dateisystem.Data.Models
{
    public interface IEntity
    {
    }
    
    public abstract class BaseEntity<T> : IEntity
    {
        [Key]
        public T? Id { get; set; }

    }

    public abstract class Entity : BaseEntity<int>
    {

    }
}
