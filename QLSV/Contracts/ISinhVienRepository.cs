using QLSV.Model;
using QLSV.Response;


namespace QLSV.Contracts
{
    public interface ISinhVienRepository
    {
        public Task<List<SinhVien>> GetListSinhVien();
        public Task<SinhVien> GetSinhVienByMASV(string MaSV);
        public Task<AddSinhVienResponse> InsertSinhVien(SinhVien sinhvien);
        public Task DeleteSinhVien(string MaSV);
        public Task UpdateSinhVien(string MaSV, SinhVien sinhvien);
        public Task<List<SinhVien>> GetListSinhVienByLop(string MaLop);



    }
}
