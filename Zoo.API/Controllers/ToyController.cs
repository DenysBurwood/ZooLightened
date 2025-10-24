using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Zoo.API.DTOs;
using Zoo.API.Mappers;
using Zoo.BLL.Exceptions;
using Zoo.BLL.Services;
using Zoo.DL.Entities;
using Zoo.DL.Entities.Humans;

namespace Zoo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToyController : ControllerBase
    {
        private readonly ToyService _toyService;

        public ToyController(ToyService toyService)
        {
            _toyService = toyService;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<ToyIndexDto>> DisplayToys([FromQuery] int page = 0, [FromQuery] int nbPage = 10)
        {

            IEnumerable<Toy> toys = _toyService.GetAll(page, nbPage);
            
            IEnumerable<ToyIndexDto> dtos = [.. toys.Select(t => t.ToToyIndexDto())];

            return Ok(dtos);
        }

        [Authorize]
        [HttpPost("Donation")]

        public ActionResult<ToyIndexDto> Donate([FromBody] ToyDonationFormDto form)
        {
            if (form is null || !ModelState.IsValid)
            {
                throw new ToyNotAllowedException("Form not valid. Donation aborted.");
            }

            ToyDonation donation = form.ToToyDonation();

            _toyService.Donate(donation);

            return Ok();

        }

    }
}
