using Dapper;
using Microsoft.AspNetCore.Mvc;
using QLSV.Context;
using QLSV.Model;
using QLSV.Response;
using System.Data;
using QLSV.Business;


namespace QLSV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class SinhVienAPI : ControllerBase
    {
        DapperContext _context;
        BusinessContext _business;
        public SinhVienAPI(DapperContext context)
        {
            _context = context;
        }

        public SinhVienAPI(BusinessContext business)
        {
            _business = business;
        }

        [HttpGet]
        [Route("GetListSinhVien")]
        public async Task<List<SinhVien>> GetListSinhVien()
        {
            return await _business.GetListSinhVien();
        }

        //Lấy sinh viên theo MASV
        [HttpGet]
        [Route("GetSinhVienByMASV")]
        public async Task<SinhVien> GetSinhVienByMASV(string MaSV)
        {
            return await _business.GetSinhVienByMASV(MaSV);
        }

        //Thêm sinh viên
        [HttpPost]
        [Route("InsertSinhVien")]
        public async Task<AddSinhVienResponse> InsertSinhVien(SinhVien sinhvien)
        {
            return await _business.InsertSinhVien(sinhvien);
        }

        //Xóa sinh viên
        [HttpDelete]
        [Route("DeleteSinhVien")]
        public async Task DeleteSinhVien(string MaSV)
        {
            await _business.DeleteSinhVien(MaSV);
        }

        //Update sinh viên
        [HttpPatch]
        [Route("UpdateSinhVien")]
        public async Task UpdateSinhVien(string MaSV, SinhVien sinhvien)
        {
           await _business.UpdateSinhVien(MaSV, sinhvien);
        }


    }
}