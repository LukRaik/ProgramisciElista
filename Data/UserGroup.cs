﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserGroup
    {
        public int Id { get; set; }

        public virtual List<User> Users { get; set; }

        public string GroupName { get; set; }
    }
}
