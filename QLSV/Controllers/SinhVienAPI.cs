using Dapper;
using Microsoft.AspNetCore.Mvc;
using QLSV.Context;
using QLSV.Model;
using QLSV.Response;
using System.Data;

namespace QLSV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class SinhVienAPI : ControllerBase
    {
        DapperContext _context;
        public SinhVienAPI(DapperContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetListSinhVien")]
        public async Task<List<SinhVien>> GetListSinhVien()
        {
            var query = "SELECT * from SinhVien";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<SinhVien>(query);
                return companies.ToList();
            }
        }

        //Lấy sinh viên theo MASV
        [HttpGet]
        [Route("GetSinhVienByMASV")]
        public async Task<SinhVien> GetSinhVienByMASV(string MaSV)
        {
            var query = "SELECT * from SinhVien Where MaSV =  @MaSV";

            using (var connection = _context.CreateConnection())
            {
                var sinhviens = await connection.QueryFirstOrDefaultAsync<SinhVien>(query, new { MaSV });
                return sinhviens;
            }
        }

        //Thêm sinh viên
        [HttpPost]
        [Route("InsertSinhVien")]
        public async Task<AddSinhVienResponse> InsertSinhVien(SinhVien sinhvien)
        {
            var query = "INSERT INTO SinhVien (MaSV, MaLop, HeDaoTao, HoTen, DiemToan, DiemLy, DiemHoa) Values (@MaSV, @MaLop, @HeDaoTao, @HoTen, @DiemToan, @DiemLy, @DiemHoa)";

            AddSinhVienResponse addSinhVienResponse = new AddSinhVienResponse();
            var parameters = new DynamicParameters();

            parameters.Add("MaSV", sinhvien.MaSV, DbType.String);
            parameters.Add("MaLop", sinhvien.MaLop, DbType.String);
            parameters.Add("HeDaoTao", sinhvien.HeDaoTao, DbType.String);
            parameters.Add("HoTen", sinhvien.HoTen, DbType.String);
            parameters.Add("DiemToan", sinhvien.DiemToan, DbType.String);
            parameters.Add("DiemLy", sinhvien.DiemLy, DbType.String);
            parameters.Add("DiemHoa", sinhvien.DiemHoa, DbType.String);

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    // select where MSV = SV.MSV  neu co ton tai =>  bao loi, sinh vien nay da ton tai
                    await connection.ExecuteAsync(query, parameters);
                }
            }
            catch (Exception ex)
            {
                addSinhVienResponse.ErrMess = "Da co loi xay ra insert khong thanh cong " + ex.Message;
                addSinhVienResponse.ErrCode = 1001;
                return addSinhVienResponse;
            }
            addSinhVienResponse.ErrMess = "Insert Thanh Cong";
            return addSinhVienResponse;
        }

    }
}