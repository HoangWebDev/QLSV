using System;
namespace QLSV.Model
{
    public class Lop
    {
        private int Id { get ; set; }
        private string MaLop { get; set; }
        private string TenLop { get; set;  }

        private List<SinhVien> SinhViens { get; set; } = new List<SinhVien>();  

    }
}
