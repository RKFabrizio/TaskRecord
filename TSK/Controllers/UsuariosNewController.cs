using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TSK.Models.Entity;
using System.Text;
using System.Security.Cryptography;


namespace TSK.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsuariosNewController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public UsuariosNewController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var usuarios = _context.Usuarios.Select(i => new {
                i.IdUsr,
                i.IdPos,
                i.Nombre,
                i.Usuario1,
                i.Contrasena,
                i.Habilitado,
                i.Extracolumn1,
                i.Extracolumn2,
                i.Extracolumn3
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdUsr" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(usuarios, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Usuario();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdUsr });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Usuarios.FirstOrDefaultAsync(item => item.IdUsr == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Usuarios.FirstOrDefaultAsync(item => item.IdUsr == key);

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IActionResult> PosicionsLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Posicions
                         orderby i.Cargo
                         select new
                         {
                             Value = i.IdPos,
                             Text = i.Cargo
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Usuario model, IDictionary values) {
            string ID_USR = nameof(Usuario.IdUsr);
            string ID_POS = nameof(Usuario.IdPos);
            string NOMBRE = nameof(Usuario.Nombre);
            string USUARIO1 = nameof(Usuario.Usuario1);
            string CONTRASENA = nameof(Usuario.Contrasena);
            string HABILITADO = nameof(Usuario.Habilitado);
            string EXTRACOLUMN1 = nameof(Usuario.Extracolumn1);
            string EXTRACOLUMN2 = nameof(Usuario.Extracolumn2);
            string EXTRACOLUMN3 = nameof(Usuario.Extracolumn3);

            if(values.Contains(ID_USR)) {
                model.IdUsr = Convert.ToInt32(values[ID_USR]);
            }

            if(values.Contains(ID_POS)) {
                model.IdPos = Convert.ToInt32(values[ID_POS]);
            }

            if(values.Contains(NOMBRE)) {
                model.Nombre = Convert.ToString(values[NOMBRE]);
            }

            if(values.Contains(USUARIO1)) {
                model.Usuario1 = Convert.ToString(values[USUARIO1]);
            }

            if (values.Contains(CONTRASENA))
            {
                model.Contrasena = ConvertirSha256((string)values[CONTRASENA]);
            }


            if (values.Contains(HABILITADO))
            {
                model.Habilitado = values[HABILITADO] != null ? Convert.ToBoolean(values[HABILITADO]) : true;
            }
            else
            {
                model.Habilitado = true;
            }


            if (values.Contains(EXTRACOLUMN1)) {
                model.Extracolumn1 = Convert.ToString(values[EXTRACOLUMN1]);
            }

            if(values.Contains(EXTRACOLUMN2)) {
                model.Extracolumn2 = Convert.ToString(values[EXTRACOLUMN2]);
            }

            if(values.Contains(EXTRACOLUMN3)) {
                model.Extracolumn3 = Convert.ToString(values[EXTRACOLUMN3]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }
}