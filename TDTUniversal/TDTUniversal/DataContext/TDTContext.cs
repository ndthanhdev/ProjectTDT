using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.DataContext
{
    public class TDTContext : DbContext
    {
        public DbSet<HocKy> HocKy { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=tdtuniversal.db");
        }
    }
    public class HocKy
    {
        public string HocKyId { get; set; }
        public string TenHocKy { get; set; }
        public string MonHoc2 { get; set; }
    }
}
