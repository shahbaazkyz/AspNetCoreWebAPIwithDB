using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        private IConfiguration _config;
        public TestController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost("CrudTest")]
        public string CrudTest([FromBody] TestModel obj)
        {
            var response = Crud_Test(obj, HttpContext);
            string json = JsonConvert.SerializeObject(response, Formatting.Indented);
            return json;
        }
        private dynamic Crud_Test(TestModel obj, HttpContext context)
        {
            try
            {

                List<SqlParameter> parm = new List<SqlParameter>();
                parm.Add(new SqlParameter() { ParameterName = "@Action", SqlDbType = SqlDbType.Int, Value = obj.Action });
                parm.Add(new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = (obj.Id == 0 ? null : obj.Id) });
                parm.Add(new SqlParameter() { ParameterName = "@Orderid", SqlDbType = SqlDbType.Int, Value = (obj.Orderid == 0 ? null : obj.Orderid) });
                parm.Add(new SqlParameter() { ParameterName = "@description", SqlDbType = SqlDbType.VarChar, Value = obj.Description });

                var spName = "sp_tbl_test";
                DataSet ds = new DapperManager(_config.GetConnectionString("MyConnection")).GetDataSet(spName, parm.ToArray());

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    return ds.Tables[0];
                //}
                //else
                //{
                //    return null;
                //}
                return ds;


            }
            catch (Exception ex)
            {
                return  ex.Message;
            }
        }

    }
}
