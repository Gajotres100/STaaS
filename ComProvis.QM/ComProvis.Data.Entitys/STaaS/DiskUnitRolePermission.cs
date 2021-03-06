﻿using System;

namespace ComProvis.Data.Entitys.STaaS
{
    public class DiskUnitRolePermission
    {
        public int Id { get; set; }
        public int DiskUnitId { get; set; }
        public int RoleId { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public DateTime DateCreated { get; set; }

        public DiskUnits DiskUnit { get; set; }
    }
}
