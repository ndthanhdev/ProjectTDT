using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.DataContext
{
    public class TDTContext : DbContext
    {
        public DbSet<HocKy> HocKy { get; set; }
        public DbSet<MonHoc> MonHoc { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=tdtuniversal.sqlite");
        }
    }
    public class HocKy
    {
        [Key]
        public string HocKyId { get; set; }
        public string TenHocKy { get; set; }
        public DateTime NgayBatDau { get; set; }
    }
    public class MonHoc
    {
        [Key]
        public int HocKy { get; set; }
        [Key]
        public string TenMH { get; set; }
        public string MaMH { get; set; }
        public string Nhom { get; set; }
        public string To { get; set; }
    }
    public class LicHoc
    {
        [Key]
        public int Id { get; set; }
        public int HocKy { get; set; }
        public string TenMH { get; set; }

        public string Tiet { get; set; }
        public int Thu { get; set; }
        public string Phong { get; set; }
        public string Tuan { get; set; }
    }


}
