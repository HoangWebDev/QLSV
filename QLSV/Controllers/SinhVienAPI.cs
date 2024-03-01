﻿using Dapper;
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
        private readonly ISinhVienRepository _sinhvienRepository;
        private readonly ILopRepository _lopRepository;

        public SinhVienAPI(ISinhVienRepository sinhvienRepository, ILopRepository lopRepository)
        {
            _sinhvienRepository = sinhvienRepository;
            _lopRepository = lopRepository;
        }


        [HttpGet]
        [Route("GetListSinhVien")]
        public async Task<List<SinhVien>> GetListSinhVien()
        {
            return await _sinhvienRepository.GetListSinhVien();
        }

        //Lấy sinh viên theo MASV
        [HttpGet]
        [Route("GetSinhVienByMASV")]
        public async Task<SinhVien> GetSinhVienByMASV(string MaSV)
        {
            return await _sinhvienRepository.GetSinhVienByMASV(MaSV);
        }

        //Thêm sinh viên
        [HttpPost]
        [Route("InsertSinhVien")]
        public async Task<AddSinhVienResponse> InsertSinhVien(SinhVien sinhvien)
        {
            return await _sinhvienRepository.InsertSinhVien(sinhvien);
        }

        //Xóa sinh viên
        [HttpDelete]
        [Route("DeleteSinhVien")]
        public async Task DeleteSinhVien(string MaSV)
        {
            await _sinhvienRepository.DeleteSinhVien(MaSV);
        }

        //Update sinh viên
        [HttpPatch]
        [Route("UpdateSinhVien")]
        public async Task UpdateSinhVien(string MaSV, SinhVien sinhvien)
        {
           await _sinhvienRepository.UpdateSinhVien(MaSV, sinhvien);
        }

        //Get sinh viên theo lớp
        [HttpGet]
        [Route("GetListSinhVienByLop")]
        public async Task<List<SinhVien>> GetListSinhVienByLop(string MaLop)
        {
            return await _sinhvienRepository.GetListSinhVienByLop(MaLop);
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