using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.DTOs.Cities;
using Project.Models;
using Project.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _citiesService;
        private readonly IMapper _mapper;

        public CitiesController(ICitiesService citiesService, IMapper mapper)
        {
            _citiesService = citiesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<Response<IReadOnlyList<CityDTO>>> Get()
        {
            var cities = await _citiesService.GetAllWithProvince();

            if (cities.Count < 1)
            {
                return new Response<IReadOnlyList<CityDTO>>(ResponseStatus.NotFound);
            }

            return new Response<IReadOnlyList<CityDTO>>(ResponseStatus.Succeed, _mapper.Map<IReadOnlyList<CityDTO>>(cities));

        }

        [HttpGet("{id}")]
        public async Task<Response<CityDTO>> Get(int id)
        {
            var city = await _citiesService.GetWithProvince(id);

            if (city == null)
            {
                return new Response<CityDTO>(ResponseStatus.NotFound);
            }

            return new Response<CityDTO>(ResponseStatus.Succeed, _mapper.Map<CityDTO>(city));
        }
    }
}
