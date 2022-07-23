using EmployeeAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext dataContext;

        public EmployeeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await dataContext.Employees.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get([FromForm] int id)
        {
            Employee? employee = await dataContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return BadRequest("Employee not found");
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Add(Employee employee)
        {
            dataContext.Employees.Add(employee);
            await dataContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Employee employee)
        {
            Employee? dbEmployee = await dataContext.Employees.FindAsync(employee.Id);

            if (dbEmployee == null)
            {
                return BadRequest("Employee not found");
            }

            dbEmployee = employee;

            await dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromForm] int id)
        {
            Employee? dbEmployee = await dataContext.Employees.FindAsync(id);

            if (dbEmployee == null)
            {
                return BadRequest("Employee not found");
            }

            dataContext.Employees.Remove(dbEmployee);
            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
