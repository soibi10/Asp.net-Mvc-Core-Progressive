﻿using GsSail.Models;
using System;
using System.Collections.Generic;

namespace GaiboSoibifaSail.Models
{
    public partial class MembershipType
    {
        public MembershipType()
        {
            Membership = new HashSet<Membership>();
        }

        public string MembershipTypeName { get; set; }
        public string Description { get; set; }
        public double RatioToFull { get; set; }

        public ICollection<Membership> Membership { get; set; }
    }
}
