using Dapper;
using QLSV.Context;
using QLSV.Contracts;
using QLSV.Model;
using QLSV.Response;
using System.Data;

namespace QLSV.Repository
{
    public class LopRepository : ILopRepository
    {
        private readonly DapperContext _context;

        public object MaLop { get; private set; }

        public LopRepository(DapperContext context)
        {
            _context = context;
        }

        //Get list sinh viên 
        public async Task<List<Lop>> GetListLop()
        {
            var query = "SELECT * from Lop";

            using (var connection = _context.CreateConnection())
            {
                var lops = await connection.QueryAsync<Lop>(query);
                return lops.ToList();
            }
        }

        //Get sinh viên theo mã sinh viên
        public async Task<Lop> GetLopByMaLop(string MaLop)
        {
            var query = "SELECT * from Lop Where MaLop =  @MaLop";

            using (var connection = _context.CreateConnection())
            {
                var lops = await connection.QueryFirstOrDefaultAsync<Lop>(query, new { MaLop });
                return lops;
            }
        }

        //Insert thêm lop
        public async Task<AddLopResponse> InsertLop(Lop lop)
        {
            var query = "INSERT INTO Lop (MaLop, TenLop) Values (@MaLop, @TenLop)";

            AddLopResponse addLopResponse = new AddLopResponse();
            var parameters = new DynamicParameters();

            parameters.Add("MaLop", lop.MaLop, DbType.String);
            parameters.Add("TenLop", lop.TenLop, DbType.String);


            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var id = await connection.QuerySingleAsync<int>(query, parameters);

                    var createdLop = new Lop
                    {
                        Id = id,
                        MaLop = lop.MaLop,
                        TenLop = lop.TenLop

                    };
                    var lops = await connection.ExecuteAsync(query, createdLop);
                }
            }
            catch (Exception ex)
            {
                addLopResponse.ErrMess = "Da co loi xay ra insert khong thanh cong " + ex.Message;
                addLopResponse.ErrCode = 400;
                return addLopResponse;
            }
            addLopResponse.ErrMess = "Insert Thanh Cong";
            return addLopResponse;
        }

        //Delete lớp
        public async Task DeleteLop(string Malop)
        {
            var query = "DELETE FROM Lop WHERE MaLop = @MaLop";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { MaLop });
            }
        }

        //Update lớp
        public async Task UpdateLop(string MaLop, Lop lop)
        {
            var query = "UPDATE Lop SET TenLop = @TenLop WHERE MaLop = @MaLop";

            var parameters = new DynamicParameters();
            parameters.Add("MaLop", lop.MaLop, DbType.String);
            parameters.Add("TenLop", lop.TenLop, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
