﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.Entities.Models.ViewModels
{
    public class RoleAssignViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasAssign { get; set; }
    }
}
