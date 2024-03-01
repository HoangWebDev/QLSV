using System;
namespace QLSV.Model
{
    public class Lop
    {
        public int Id { get ; set; }
        public string MaLop { get; set; }
        public string TenLop { get; set;  }
        public List<SinhVien> SinhViens { get; set; } = new List<SinhVien>();

    }
}
