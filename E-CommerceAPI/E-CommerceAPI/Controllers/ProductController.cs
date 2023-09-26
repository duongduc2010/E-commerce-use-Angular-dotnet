using E_CommerceAPI.Data;
using E_CommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        public ProductController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select Products.*,Brand.Name as Brand, category.Name as Category 
                            from dbo.Products 
                            inner join  dbo.Brand
                            on dbo.Products.BrandId = dbo.Brand.Id
                            inner join dbo.Category
                            on dbo.Products.CategoryId = dbo.Category.Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("{id}")]
        public JsonResult GetDetail(int id)
        {
            string query = @"SELECT Products.*,Brand.Name as Brand, category.Name as Category 
                            FROM dbo.Products 
                            inner join  dbo.Brand
                            on dbo.Products.BrandId = dbo.Brand.Id
                            inner join dbo.Category
                            on dbo.Products.CategoryId = dbo.Category.Id
                            WHERE Products.id = @idProduct";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@idProduct", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("Banner")]
        public JsonResult GetBanner()
        {
            string query = @"select top 3 Products.*,Brand.Name as Brand, category.Name as Category 
                            from dbo.Products 
                            inner join  dbo.Brand
                            on dbo.Products.BrandId = dbo.Brand.Id
                            inner join dbo.Category
                            on dbo.Products.CategoryId = dbo.Category.Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("Trendy")]
        public JsonResult GetTrendy()
        {
            string query = @"select top 8 Products.*,Brand.Name as Brand, category.Name as Category 
                            from dbo.Products 
                            inner join  dbo.Brand
                            on dbo.Products.BrandId = dbo.Brand.Id
                            inner join dbo.Category
                            on dbo.Products.CategoryId = dbo.Category.Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult CreateProduct(Product product) 
        {
            string query = @"INSERT INTO dbo.Products(Name, Description, PriceOld, PriceNew, CategoryId, BrandId, Image)
                            VALUES (@Name, @Description, @PriceOld, @PriceNew, @CategoryId, @BrandId, @Image)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", product.Name);
                    myCommand.Parameters.AddWithValue("@Description", product.Description);
                    myCommand.Parameters.AddWithValue("@PriceOld", product.PriceOld);
                    myCommand.Parameters.AddWithValue("@PriceNew", product.PriceNew);
                    myCommand.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    myCommand.Parameters.AddWithValue("@BrandId", product.BrandId);
                    myCommand.Parameters.AddWithValue("@Image", product.Image);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Created new product successfully");
        }
        [HttpPut]
        public JsonResult UpdateProduct(Product product) 
        {
            string query = @"UPDATE dbo.Products 
                             SET Name = @Name, Description = @Description, PriceOld = @PriceOld, PriceNew = @PriceNew,  CategoryId = @CategoryId, BrandId = @BrandId, Image = @Image
                             WHERE Id = @Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using ( SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand( query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", product.Id);
                    myCommand.Parameters.AddWithValue("@Name", product.Name);
                    myCommand.Parameters.AddWithValue("@Description", product.Description);
                    myCommand.Parameters.AddWithValue("@PriceOld", product.PriceOld);
                    myCommand.Parameters.AddWithValue("@PriceNew", product.PriceNew);
                    myCommand.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    myCommand.Parameters.AddWithValue("@BrandId", product.BrandId);
                    myCommand.Parameters.AddWithValue("@Image", product.Image);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteProduct(int id)
        {
            string query = @"DELETE FROM dbo.Products WHERE Id = @Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using( SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query,myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted successfully");
        }

    }
}
