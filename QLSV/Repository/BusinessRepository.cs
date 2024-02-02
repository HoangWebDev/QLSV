using Dapper;
using QLSV.Context;
using QLSV.Contracts;
using QLSV.Model;
using QLSV.Response;
using System.Data;

namespace QLSV.Repository
{
    public class BusinessRepository: IBusinessRepository
    {
        private readonly DapperContext _context;

        public BusinessRepository(DapperContext context)
        {
            _context = context;
        }

        //Get list sinh viên 
        public async Task<List<SinhVien>> GetListSinhVien()
        {
            var query = "SELECT * from SinhVien";

            using (var connection = _context.CreateConnection())
            {
                var sinhviens = await connection.QueryAsync<SinhVien>(query);
                return sinhviens.ToList();
            }
        }

        //Get sinh viên theo mã sinh viên
        public async Task<SinhVien> GetSinhVienByMASV(string MaSV)
        {
            var query = "SELECT * from SinhVien Where MaSV =  @MaSV";

            using (var connection = _context.CreateConnection())
            {
                var sinhviens = await connection.QueryFirstOrDefaultAsync<SinhVien>(query, new { MaSV });
                return sinhviens;
            }
        }

        //Insert thêm sinh viên
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
                    var id = await connection.QuerySingleAsync<int>(query, parameters);

                    var createdSinhVien = new SinhVien
                    {
                        Id = id,
                        MaSV = sinhvien.MaSV,
                        MaLop = sinhvien.MaLop,
                        HeDaoTao = sinhvien.HeDaoTao,
                        HoTen = sinhvien.HoTen,
                        DiemToan = sinhvien.DiemToan,
                        DiemLy = sinhvien.DiemLy,
                        DiemHoa = sinhvien.DiemHoa
                    };
                    var sinhviens = await connection.ExecuteAsync(query, createdSinhVien);
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

        //Delete sinh viên
        public async Task DeleteSinhVien(string MaSV)
        {
            var query = "DELETE FROM SinhVien WHERE MaSV = @MaSV";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { MaSV });
            }
        }

        //Update sinh viên
        public async Task UpdateSinhVien(string MaSV, SinhVien sinhvien)
        {
            var query = "UPDATE SinhVien SET MaLop = @MaLop, HeDaoTao = @HeDaoTao, HoTen = @HoTen, DiemToan = @DiemToan, DiemLy = @DiemLy, DiemHoa = @DiemHoa WHERE MaSV = @MaSV";

            var parameters = new DynamicParameters();
            parameters.Add("MaSV", sinhvien.MaSV, DbType.String);
            parameters.Add("MaLop", sinhvien.MaLop, DbType.String);
            parameters.Add("HeDaoTao", sinhvien.HeDaoTao, DbType.String);
            parameters.Add("HoTen", sinhvien.HoTen, DbType.String);
            parameters.Add("DiemToan", sinhvien.DiemToan, DbType.String);
            parameters.Add("DiemLy", sinhvien.DiemLy, DbType.String);
            parameters.Add("DiemHoa", sinhvien.DiemHoa, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        //Get sinh viên theo lớp
        public async Task<List<SinhVien>> GetListSinhVienByLop(string maLop)
        {
            var query = "SELECT * FROM SinhVien WHERE MaLop = @MaLop";

            using (var connection = _context.CreateConnection())
            {
                var sinhviens = await connection.QueryAsync<SinhVien>(query, new { MaLop = maLop });
                return sinhviens.ToList();
            }
        }
    }
}
