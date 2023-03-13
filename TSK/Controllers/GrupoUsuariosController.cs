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


namespace TSK.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GrupoUsuariosController : Controller
    {
        private USAEU2GIGDEVSQLContext _context;

        public GrupoUsuariosController(USAEU2GIGDEVSQLContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> UsuarioLookup(int idPos, DataSourceLoadOptions loadOptions)
        {
            var usuarios = _context.Usuarios.Where(x => x.IdPos == idPos).Select(x => x.Nombre).ToList();

            return Json(usuarios);

        }
        [HttpGet]
        public async Task<IActionResult> Get(int idRep, DataSourceLoadOptions loadOptions)
        {
            var grupousuarios = _context.GrupoUsuarios.Where(x => x.IdRep == idRep).Select(i => new {
                i.IdGrus,
                i.IdRep,
                i.IdUsr,
                i.Grupo,
                i.Lider
            });

            return Json(await DataSourceLoader.LoadAsync(grupousuarios, loadOptions));
        }

        public async Task<IActionResult> GetLider(int idRep, DataSourceLoadOptions loadOptions)
        {
            var grupousuarios = _context.GrupoUsuarios
                .Where(x => x.IdRep == idRep)
                .Select(i => new {
                    i.IdGrus,
                    i.IdRep,
                    i.IdUsr,
                    i.Grupo,
                    i.Lider
                })
                .Join(
                    _context.Usuarios.Where(u => u.IdPos == 1 ),
                    gu => gu.IdUsr,
                    u => u.IdUsr,
                    (gu, u) => new {
                        gu.IdGrus,
                        gu.IdRep,
                        gu.IdUsr,
                        gu.Grupo,
                        gu.Lider
                    }
                );

            return Json(await DataSourceLoader.LoadAsync(grupousuarios, loadOptions));
        }


        [HttpGet]
        public async Task<IActionResult> GetTecnico(int idRep, DataSourceLoadOptions loadOptions)
        {
            var grupousuarios = _context.GrupoUsuarios
                .Where(x => x.IdRep == idRep)
                .Select(i => new {
                    i.IdGrus,
                    i.IdRep,
                    i.IdUsr,
                    i.Grupo,
                    i.Lider
                })
                .Join(
                    _context.Usuarios.Where(u => u.IdPos == 2 ),
                    gu => gu.IdUsr,
                    u => u.IdUsr,
                    (gu, u) => new {
                        gu.IdGrus,
                        gu.IdRep,
                        gu.IdUsr,
                        gu.Grupo,
                        gu.Lider
                    }
                );

            return Json(await DataSourceLoader.LoadAsync(grupousuarios, loadOptions));
        }
        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new GrupoUsuario();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            //   valuesDict["IdRep"] = 2015;

            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            if (model.Grupo < 1 || model.Grupo > 4)
            {
                return BadRequest("El valor de Grupo debe estar entre 1 y 4.");

            }

            var usuarioId = model.IdUsr;
            var esLider = _context.Usuarios.Any(u => (u.IdUsr == usuarioId && u.IdPos == 1));
            model.Lider = esLider ? true : false;


            if (model.Lider && _context.GrupoUsuarios.Any(gu => (gu.Grupo == model.Grupo && gu.Lider && gu.IdRep == model.IdRep)))
            {
                return BadRequest("Ya existe un líder para este grupo.");
            }

            if ((model.Lider == false) && _context.GrupoUsuarios.Any(gu => (gu.Grupo == model.Grupo && (gu.Lider == false) && gu.IdRep == model.IdRep && gu.IdUsr == model.IdUsr)))
            {
                return BadRequest("Ya existe un usuario para este grupo.");
            }


            var result = _context.GrupoUsuarios.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdGrus });
        }


        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {
            var model = await _context.GrupoUsuarios.FirstOrDefaultAsync(item => item.IdGrus == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            if (model.Grupo < 1 || model.Grupo > 4)
            {
                return BadRequest("El valor de Grupo debe estar entre 1 y 4.");

            }

            var usuarioId = model.IdUsr;
            var esLider = _context.Usuarios.Any(u => u.IdUsr == usuarioId && u.IdPos == 1);
            model.Lider = esLider ? true : false;

            if (model.Lider && _context.GrupoUsuarios.Any(gu => gu.Grupo == model.Grupo && gu.Lider && gu.IdRep == model.IdRep))
            {
                return BadRequest("Ya existe un líder para este grupo.");
            }

            if ((model.Lider == false) && _context.GrupoUsuarios.Any(gu => (gu.Grupo == model.Grupo && (gu.Lider == false) && gu.IdRep == model.IdRep && gu.IdUsr == model.IdUsr)))
            {
                return BadRequest("Ya existe un usuario para este grupo.");
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.GrupoUsuarios.FirstOrDefaultAsync(item => item.IdGrus == key);

            _context.GrupoUsuarios.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> UsuariosLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Usuarios
                         orderby i.Nombre
                         select new {
                             Value = i.IdUsr,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> UsuariosLiderLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Usuarios
                         where i.IdPos == 1 && i.Habilitado == true
                         orderby i.Nombre
                         select new
                         {
                             Value = i.IdUsr,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> UsuariosTecnicoLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Usuarios
                         orderby i.Nombre
                         where i.IdPos == 2 && i.Habilitado == true
                         select new
                         {
                             Value = i.IdUsr,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }



        [HttpGet]
        public async Task<IActionResult> ReportesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Reportes
                         orderby i.IdRep
                         select new {
                             Value = i.IdRep,
                             Text = i.IdRep
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(GrupoUsuario model, IDictionary values) {
            string ID_GRUS = nameof(GrupoUsuario.IdGrus);
            string ID_REP = nameof(GrupoUsuario.IdRep);
            string ID_USR = nameof(GrupoUsuario.IdUsr);
            string GRUPO = nameof(GrupoUsuario.Grupo);
            string LIDER = nameof(GrupoUsuario.Lider);

            if(values.Contains(ID_GRUS)) {
                model.IdGrus = Convert.ToInt32(values[ID_GRUS]);
            }

            if(values.Contains(ID_REP)) {
                model.IdRep = Convert.ToInt32(values[ID_REP]);
            }

            if(values.Contains(ID_USR)) {
                model.IdUsr = Convert.ToInt32(values[ID_USR]);
            }

            if(values.Contains(GRUPO)) {
                model.Grupo = Convert.ToInt32(values[GRUPO]);
            }

            if(values.Contains(LIDER)) {
                model.Lider = Convert.ToBoolean(values[LIDER]);
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
    }
}