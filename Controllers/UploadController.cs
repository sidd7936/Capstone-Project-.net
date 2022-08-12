using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignUpAPI.Data;
using SignUpAPI.Models;
using System.Data;
using System.Net.Http.Headers;

namespace SignUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IConfiguration _config;
        public readonly SignUpAPIDbContext _Uploadcontext;
        public UploadController(IConfiguration config, SignUpAPIDbContext upload_context)
        {
            this._Uploadcontext = upload_context;
            _config = config;



        }

        


        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadBulk()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "EmployeeData");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var converter = new CsvTo.CsvConverter(fullPath, true);

                    DataTable dt = converter.ToDataTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Upload upload = new Upload();
                        try
                        {
                            upload.FirstName = Convert.ToString(dr["FirstName"]);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        try
                        {
                            upload.LastName = Convert.ToString(dr["LastName"]);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        try
                        {
                            upload.Age = Convert.ToInt32(dr["Age"]);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        try
                        {
                            upload.Gender = Convert.ToString(dr["Gender"]);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        try
                        {
                            upload.Department = Convert.ToString(dr["Department"]);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        try
                        {
                            upload.Contact = Convert.ToInt32(dr["Contact"]);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        try
                        {
                            upload.Grade = Convert.ToString(dr["Grade"]);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        try
                        {
                            upload.Salary = Convert.ToInt32(dr["Salary"]);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        _Uploadcontext.Uploads.Add(upload);
                        _Uploadcontext.SaveChanges();
                    }
                    System.IO.File.Delete(fullPath);

                    //IEnumerable<string[]> c = converter.ToCollection();

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        public async Task <IEnumerable <Upload>> GetDetails()
        {
            return await _Uploadcontext.Uploads.ToListAsync();

        }
    }
}
