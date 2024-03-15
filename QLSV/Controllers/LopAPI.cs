using Dapper;
using Microsoft.AspNetCore.Mvc;
using QLSV.Context;
using QLSV.Contracts;
using QLSV.Model;
using QLSV.Repository;
using QLSV.Response;
using System.Data;

namespace QLSV.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LopAPI : ControllerBase
    {
        private readonly ILopRepository _lopRepository;

        public LopAPI (ILopRepository lopRepository) 
            {
                _lopRepository = lopRepository;
            }


        [HttpGet]
        [Route("GetListLop")]
        public async Task<List<Lop>> GetListLop()
        {
            return await _lopRepository.GetListLop();
        }

        //Lấy lớp theo MaLop
        [HttpGet]
        [Route("GetLopByMaLop")]
        public async Task<Lop> GetLopByMaLop(string MaLop)
        {
            return await _lopRepository.GetLopByMaLop(MaLop);
        }

        //Thêm lớp
        [HttpPost]
        [Route("InsertLop")]
        public async Task<AddLopResponse> InsertLop(Lop lop)
        {
            return await _lopRepository.InsertLop(lop);
        }

        //Xóa lớp
        [HttpDelete]
        [Route("DeleteLop")]
        public async Task DeleteLop(string MaLop)
        {
            await _lopRepository.DeleteLop(MaLop);
        }

        //Update lớp
        [HttpPatch]
        [Route("UpdateLop")]
        public async Task UpdateLop(string MaLop, Lop lop)
        {
            await _lopRepository.UpdateLop(MaLop, lop);
        }
    }
}
