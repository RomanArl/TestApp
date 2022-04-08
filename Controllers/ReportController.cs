using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestApp.DataContext;
using TestApp.Entities;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private ApplicationContext _context;
        public ReportController(ApplicationContext context)
        {
            _context = context;
        }

        [Route("info")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid guidReport)
        {
            ///Summary
            ///1 - check if reportId is exist
            ///2 - calculate percent
            ///3 - check percent progress
            ///4 - map report to response
            ///5 - return response

            // 1
            var report = await _context.Report.FirstOrDefaultAsync(r => r.Id == guidReport);


            if (report == null)
            {
                return BadRequest("Report is not found");
            }

            // 2
            double percent;
            {
                var timeComplete = 60;
                var time_difference = DateTime.UtcNow.Subtract(report.CreateAt);
                var progress_in_sec = time_difference.TotalSeconds;
                percent = Math.Round(progress_in_sec / timeComplete * 100);
            }

            // 3

            dynamic result = null;
            if (percent >= 100)
            {
                percent = 100;
                result = new
                {
                    user_id = report.UserId,
                    count_sign_in = 12 // ? 
                };
            }

            // 4

            var response = new
            {
                query = report.Id,
                percent = percent,
                result = result
            };

            // 5
            return Ok(response);
        }

        [Route("user_statistics")]
        [HttpPost]
        public async Task<IActionResult> Post(RequestVM model)
        {
            ///Summary
            ///1 - check if UserId is not null
            ///2 - map RequestVM to Report
            ///3 - save Report to DB
            ///4 - return response

            // 1    
            {
                if (model.UserId == Guid.Empty)
                {
                    return BadRequest("User Id is not provided");
                }
            }

            // 2-3
            {
                var report = new Report
                {
                    UserId = model.UserId,
                    CreateAt = DateTime.UtcNow,
                    From = model.From,
                    To = model.To
                };

                _context.Report.Add(report);

                await _context.SaveChangesAsync();

                // 4

                var response = new
                {
                    QuiryId = report.Id
                };

                return Ok(response);
            }
        }
    }
}
