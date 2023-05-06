﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napelem.Models
{
    public class Storage
    {
        public int storageID { get; set; }
        public int componentID { get; set; }
        public int row { get; set; }
        public int column { get; set; }
        public int level { get; set; }
        public string? objectType { get; set; }
    }
}