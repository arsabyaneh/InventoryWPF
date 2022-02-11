using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.EntityFramework.DataModels
{
    public class BaseDataModel
    {
        [NotMapped]
        public EntityState EntityState { get; set; }
    }
}
