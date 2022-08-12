using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignUpAPI.Data;
using SignUpAPI.Models;

namespace SignUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly SignUpAPIDbContext dbContext;

        private readonly IConfiguration _config;

        private string Encrypt_Password(string password)
        {
            string pswstr = string.Empty;
            byte[] psw_encode = new byte[password.Length];
            psw_encode = System.Text.Encoding.UTF8.GetBytes(password);
            pswstr = Convert.ToBase64String(psw_encode);
            return pswstr;
        }

        public SignUpController(SignUpAPIDbContext dbContext, IConfiguration config)
        {
            this.dbContext = dbContext;
            _config = config;
        }
        [HttpGet]
        public IActionResult GetDetails()
        {
            return Ok(dbContext.Sign.ToList());

        }
        /*[HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var details = await dbContext.Sign.FindAsync(id);
            if (details == null)
            {
                return NotFound();

            }
            return Ok(details);
        }*/



        [HttpPost]
        public async Task<IActionResult> AddDetail(AddDetails adddetail)
        {
            var signup = new Signup()
            {

                Id = new int(),
                FirstName = adddetail.FirstName,
                LastName = adddetail.LastName,
                Email = adddetail.Email,
                Password = adddetail.Password


            };
            signup.Password = Encrypt_Password(signup.Password);
            dbContext.Entry(signup).State = EntityState.Modified;
            await dbContext.Sign.AddAsync(signup);
            await dbContext.SaveChangesAsync();

            return Ok(signup);

        }
        /*[HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateDetails([FromRoute] int id, Update updatedetails)
        {
            var details = await dbContext.Sign.FindAsync(id);

            if (details != null)
            {
                details.FirstName = updatedetails.FirstName;
                details.LastName = updatedetails.LastName;
                details.Email = updatedetails.Email;
                details.Password = updatedetails.Password;

                await dbContext.SaveChangesAsync();
                return Ok(details);
            }
            return NotFound();

        }*/


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateDetails([FromRoute] int id, UpEnroll updatedetails)
        {
            var details = await dbContext.Sign.FindAsync(id);



            if (details != null)
            {
                // details.Email = updatedetails.Email;

                details.EmployeeInvestment = updatedetails.EmployeeInvestment;
                details.EmployerInvestment = updatedetails.EmployerInvestment;
                details.RetirementInvestment = updatedetails.RetirementInvestment;




                await dbContext.SaveChangesAsync();
                return Ok(details);
            }
            return NotFound();
        }






        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(Login user)
        {
            var userAvailable = dbContext.Sign.Where(u => u.Email == user.Email && u.Password == Encrypt_Password(user.Password)).FirstOrDefault();
            if (userAvailable != null)
            {
                //return userAvailable;

                return Ok(new JwtService(_config).GenerateToken(userAvailable.Id.ToString(),
                    
                    userAvailable.FirstName,
                    userAvailable.LastName, 
                    userAvailable.Email,
                    userAvailable.EmployeeInvestment.ToString(),
                    userAvailable.EmployerInvestment.ToString(),
                    userAvailable.RetirementInvestment.ToString()));

                
            }

            return Ok("Failure");
            //return new Signup();
        }


        [HttpPut]
        [Route("{id:int}/*")]
        public async Task<IActionResult> Update([FromRoute] int id, Admin update)
        {
            var details = await dbContext.Sign.FindAsync(id);



            if (details != null)
            {
                // details.Email = updatedetails.Email;



                details.Email = update.Email;
                details.Password = update.Password;




                await dbContext.SaveChangesAsync();
                return Ok(details);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var details = await dbContext.Sign.FirstOrDefaultAsync(x => x.Id == id);
            if (details == null)
            {
                return NotFound();



            }
            return Ok(details);
        }



        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteDetails([FromRoute] int id)
        {
            var details = await dbContext.Sign.FindAsync(id);
            if (details != null)
            {
                dbContext.Remove(details);
                await dbContext.SaveChangesAsync();
                return Ok(details);
            }
            return NotFound();
        }
    }
}
