﻿using CosmosDbDemo1.Entities;
using CosmosDbDemo1.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CosmosDbDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsV1Controller : ControllerBase
    {

        private readonly ILogger<ProfessorsController> _logger;
        private readonly CollegeDbContext _collegeDbContext;

        public ProfessorsV1Controller(ILogger<ProfessorsController> logger, CollegeDbContext collegeDbContext)
        {
            _logger = logger;
            _collegeDbContext = collegeDbContext;
        }


        [HttpGet]
        public ActionResult<IEnumerable<BookListNoSql>> Get()
        {
            var professors = _collegeDbContext
                                    .Professors
                                    .AsNoTracking()
                                    .OrderByDescending(x => x.ProfessorId)
                                    .ToList();

            return Ok(professors);
        }

    }
}