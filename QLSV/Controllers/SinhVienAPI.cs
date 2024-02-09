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
    
    public class SinhVienAPI : ControllerBase
    {
        private readonly ISinhVienRepository _businessRepository;

        public SinhVienAPI(ISinhVienRepository businessRepository)
        {
           _businessRepository = businessRepository;
        }


        [HttpGet]
        [Route("GetListSinhVien")]
        public async Task<List<SinhVien>> GetListSinhVien()
        {
            return await _businessRepository.GetListSinhVien();
        }

        //Lấy sinh viên theo MASV
        [HttpGet]
        [Route("GetSinhVienByMASV")]
        public async Task<SinhVien> GetSinhVienByMASV(string MaSV)
        {
            return await _businessRepository.GetSinhVienByMASV(MaSV);
        }

        //Thêm sinh viên
        [HttpPost]
        [Route("InsertSinhVien")]
        public async Task<AddSinhVienResponse> InsertSinhVien(SinhVien sinhvien)
        {
            return await _businessRepository.InsertSinhVien(sinhvien);
        }

        //Xóa sinh viên
        [HttpDelete]
        [Route("DeleteSinhVien")]
        public async Task DeleteSinhVien(string MaSV)
        {
            await _businessRepository.DeleteSinhVien(MaSV);
        }

        //Update sinh viên
        [HttpPatch]
        [Route("UpdateSinhVien")]
        public async Task UpdateSinhVien(string MaSV, SinhVien sinhvien)
        {
           await _businessRepository.UpdateSinhVien(MaSV, sinhvien);
        }

        //Get sinh viên theo lớp
        [HttpGet]
        [Route("GetListSinhVienByLop")]
        public async Task<List<SinhVien>> GetListSinhVienByLop(string MaLop)
        {
            return await _businessRepository.GetListSinhVienByLop(MaLop);
        }
    }
}