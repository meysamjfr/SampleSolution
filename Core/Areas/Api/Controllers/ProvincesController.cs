using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs.Provinces;
using Project.Extentions;
using Project.Models;
using Project.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IProvincesService _provincesService;
        private readonly IMapper _mapper;

        public ProvincesController(IProvincesService provincesService, IMapper mapper)
        {
            _provincesService = provincesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<Response<IReadOnlyList<ProvinceDTO>>> Get()
        {
            var provinces = await _provincesService.GetAllWithCities();

            if (provinces.Count < 1)
            {
                return new Response<IReadOnlyList<ProvinceDTO>>(ResponseStatus.NotFound);
            }

            return new Response<IReadOnlyList<ProvinceDTO>>(ResponseStatus.Succeed, _mapper.Map<IReadOnlyList<ProvinceDTO>>(provinces));
        }


        [HttpGet("{id}")]
        public async Task<Response<ProvinceDTO>> Get(int id)
        {
            var city = await _provincesService.GetWithCities(id);

            if (city == null)
            {
                return new Response<ProvinceDTO>(ResponseStatus.NotFound);
            }

            return new Response<ProvinceDTO>(ResponseStatus.Succeed, _mapper.Map<ProvinceDTO>(city));
        }
    }
}
