using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Collections.Generic;
using System.Linq;
using Clockwork.API.Services;

namespace Clockwork.API.Controllers
{
    public class CurrentTimeController : Controller
    {
        [Route("api/[controller]/latest")]
        // GET api/currenttime
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var utcTime = DateTime.UtcNow;
                var serverTime = TimeLordService.ConvertTime(DateTime.UtcNow, TimeLordService.CurrentTimeZone);
                var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

                var returnVal = new CurrentTimeQuery
                {
                    UTCTime = utcTime,
                    ClientIp = ip,
                    Time = serverTime
                };

                using (var db = new ClockworkContext())
                {
                    db.CurrentTimeQueries.Add(returnVal);
                    var count = db.SaveChanges();
                    Console.WriteLine("{0} records saved to database", count);

                    Console.WriteLine();
                    foreach (var CurrentTimeQuery in db.CurrentTimeQueries)
                    {
                        Console.WriteLine(" - {0}", CurrentTimeQuery.UTCTime);
                    }
                }

                return Ok(returnVal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/currenttime
        [Route("api/[controller]/all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var returnVal = new List<CurrentTimeQuery>();

                using (var db = new ClockworkContext())
                {
                    returnVal.AddRange(db.CurrentTimeQueries.ToList());
                }

                return Ok(returnVal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/update/{timeZone}")]
        [HttpGet]
        public IActionResult UpdateTimeZone(string timeZone)
        {
            try
            {
                TimeLordService.CurrentTimeZone = timeZone;

                using (var db = new ClockworkContext())
                {
                    foreach (var entry in db.CurrentTimeQueries)
                        entry.Time = TimeLordService.ConvertTime(entry.UTCTime, TimeLordService.CurrentTimeZone);

                    db.SaveChanges();
                }

                return Ok(timeZone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/timeZones")]
        [HttpGet]
        public IActionResult GetAllTimeZones()
        {
            try
            {
                return Ok(TimeLordService.GetAllTimeZones());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
