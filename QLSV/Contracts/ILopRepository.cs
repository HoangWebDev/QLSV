using QLSV.Model;
using QLSV.Response;

namespace QLSV.Contracts
{
    public interface ILopRepository
    {
        public Task<List<Lop>> GetListLop();
        public Task<Lop> GetLopByMaLop(string MaLop);
        public Task<AddLopResponse> InsertLop(Lop lop);
        public Task DeleteLop(string MaLop);
        public Task UpdateLop(string MaLop, Lop lop);
    }

}
